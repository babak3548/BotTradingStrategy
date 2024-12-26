using Microsoft.EntityFrameworkCore;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi;
using PreprocessDataStocks.PartnerApi.CommonModels;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace PreprocessDataStocks.CandleConsumerModule.TickConsumerModule
{
    public class TraderAgent
    {
        private static TraderAgent instance;

        int recentCandlesForVolumeSelector = 0;
        int minRepetationVolumeNeededTrade = 0;
        decimal confirmTrandNum = 0;
        decimal VPlow_high = 0;
        decimal percentageInvestTrade = 0;
        decimal takeProfitMultiple = 0;
        decimal stoplossMultiple = 0;
        Symbol CurrSymbol;
        AccountManagerFactory accountManagerFactory;
        IAccountManager accountManager;
        OrderOperationFactory orderOperationFactory;
        IOrderOperations orderOperationsAPI;
        private static readonly ConcurrentDictionary<int, object> lockObjects = new ConcurrentDictionary<int, object>();

        private PreprocessDataStockContext _dbContext;

        public static TraderAgent Instance(PreprocessDataStockContext dbContext)
        {
            if (instance != null) { return instance; }
            else
            {
                instance = new TraderAgent(dbContext);
                return instance;
            }
        }
        private TraderAgent(PreprocessDataStockContext dbContext)
        {
            _dbContext = dbContext;
            accountManagerFactory = new AccountManagerFactory();
            orderOperationFactory = new OrderOperationFactory();
        }

        private void updateConfigParameterSymbol(Tick tick)
        {
            CurrSymbol = ConfigBot.Symbols.First(c => c.Id == tick.SymbolId);

            recentCandlesForVolumeSelector = CurrSymbol.RecentCandlesForVolumeSelector;
            minRepetationVolumeNeededTrade = CurrSymbol.MinRepetationVolumeNeededTrade;
            confirmTrandNum = CurrSymbol.ConfirmTrendNum;
            VPlow_high = CurrSymbol.VpLowHigh;
            percentageInvestTrade = CurrSymbol.PercentageInvestTrade;
            takeProfitMultiple = CurrSymbol.TakeProfitMultiple;
            stoplossMultiple = CurrSymbol.StopLossMultiple;

            //if (CurrSymbol.RecentlyCandlesList == null)
            //{
            //    CurrSymbol.RecentlyCandlesList = new List<Candle>();
            //    CurrSymbol.RecentlyCandlesList = _dbContext.Candle.Where(c => c.Datetime <= tick.CandleDatetime).ToList().TakeLast(recentCandlesForVolumeSelector).ToList();
            //}

            orderOperationsAPI = orderOperationFactory.GetOrderOperation(CurrSymbol.Exchange);
            accountManager = accountManagerFactory.GetAccountManager(CurrSymbol.Exchange);
        }

        public void CheckAndDoTrade(Tick tick)
        {
            //decimal lowCurrCandel, highCurrCandel;
            //lowCurrCandel = decimal.MaxValue;
            //  highCurrCandel = 0;

            updateConfigParameterSymbol(tick);
            Candle currCandle = GetCurrentCandle(tick);
            decimal openPriceCurrCandel = currCandle.Open;

            List<int> recentCandlesIds = GetRecentCandlesIds(tick);
            List<VolumeProfiler> vps = GetRelevantVolumeProfiles(tick, recentCandlesIds);

            LogManager.ConsoleWriteLine($" Last candle Ids:{string.Join(",",
                recentCandlesIds)},  Last candle volume Ids:{string.Join(",", vps.Select(v => v.Id))},  Minimum repetion volume:{minRepetationVolumeNeededTrade}", $"Volume SymbolId:{tick.SymbolId}", 25);
            CheckTakeProfitAndStopLoss(tick.Ask, tick.Bid, tick.TickDatetime, tick.SymbolId);

            EvaluateTradingOpportunities(tick, openPriceCurrCandel, currCandle, vps);

            _dbContext.SaveChanges();
        }

        private void EvaluateTradingOpportunities(Tick tick, decimal openPriceCurrCandel, Candle currCandle, List<VolumeProfiler> vps)
        {
            int counterCandelDiff = 0;
            bool buyFirstCondition = false;
            bool sellFirstCondition = false;
            foreach (var v in vps)
            {
                counterCandelDiff = (int)((currCandle.Datetime.Value - v.Candle.Datetime.Value).TotalHours / CurrSymbol.Period.GetPeriodInHours());//Assumption increase on Ids one by one
                buyFirstCondition = v.High + confirmTrandNum < tick.Ask;
                sellFirstCondition = v.Low - confirmTrandNum > tick.Bid;
                LogManager.ConsoleWriteLine($"Starting with Symbol ID:{tick.SymbolId}, Tick Datetime:{tick.TickDatetime}, Buy First Condition:{buyFirstCondition}, Sell First Condition:{sellFirstCondition}, Candle Datetime:{tick.CandleDatetime}", "Evaluate Opportunities Order", 25);
                if (buyFirstCondition) // v.High + (confirmTrandNum + 3*VPlow_high ) < tick.Ask
                {
                    //3 && CurrSymbol.RecentlyCandlesList.ElementAt(2).Low < CurrSymbol.RecentlyCandlesList.ElementAt(1).Low
                    bool trand3candel = CurrSymbol.RecentlyCandlesList.Count >= 2
                        && CurrSymbol.RecentlyCandlesList.ElementAt(1).Low > CurrSymbol.RecentlyCandlesList.ElementAt(0).Low;
                    bool currPriceBigOpen = tick.Ask - openPriceCurrCandel > VPlow_high / 3;
                    // if (counterCandelDiff <= 4 || currPriceBigOpen)
                    if (currPriceBigOpen)
                    {
                        Buy(tick, counterCandelDiff, v);
                    }
                }
                else if (sellFirstCondition) //&&v.Low - (confirmTrandNum + 3*VPlow_high) < tick.Bid
                {
                    //3 && CurrSymbol.RecentlyCandlesList.ElementAt(2).High > CurrSymbol.RecentlyCandlesList.ElementAt(1).High
                    bool trand3candel = CurrSymbol.RecentlyCandlesList.Count >= 2
                        && CurrSymbol.RecentlyCandlesList.ElementAt(1).High > CurrSymbol.RecentlyCandlesList.ElementAt(0).High;
                    bool currPriceBigOpen = openPriceCurrCandel - tick.Bid > VPlow_high / 3;
                    //if (counterCandelDiff <= 4 || currPriceBigOpen)
                    if (currPriceBigOpen)
                    {
                        Sell(tick, counterCandelDiff, v);
                    }
                }

            }
        }

        private List<VolumeProfiler> GetRelevantVolumeProfiles(Tick tick, List<int> recentCandlesIds)
        {
            return _dbContext.VolumeProfiler
                .Where(vp => recentCandlesIds.Contains(vp.Candle.Id) && vp.LastBarRepetationVolume >= minRepetationVolumeNeededTrade && vp.SymbolId == tick.SymbolId)
                .OrderByDescending(o => o.LastBarRepetationVolume).ToList();//TODO Take(1) active in fast test  
        }
        /// <summary>
        /// just candles returned to used volume profile them 
        /// </summary>
        /// <param name="currCandle"></param>
        /// <returns></returns>
        private List<int> GetRecentCandlesIds(Tick tick)
        {
            CurrSymbol.RecentlyCandlesList = _dbContext.Candle.Where(c => c.Datetime <= tick.CandleDatetime && c.SymbolId == tick.SymbolId).ToList().TakeLast(recentCandlesForVolumeSelector).ToList();

            var beforcandles = CurrSymbol.RecentlyCandlesList.Select(c => c.Id).ToList(); // Materialize candle IDs
            return beforcandles;
        }

        public Candle GetCurrentCandle(Tick tick)
        {
            return _dbContext.Candle.FirstOrDefault(c => c.Datetime == tick.CandleDatetime && c.SymbolId == tick.SymbolId);
        }


        private void CheckTakeProfitAndStopLoss(decimal Ask, decimal Bid, DateTime closeDateTime, int symbolId)
        {
            decimal askBid = Math.Round((Ask + Bid) / 2, 5);
            Order? openOrder = GetOpenOrder(askBid, closeDateTime, symbolId);

            if (openOrder == null) return;

            if (openOrder.OrderType == OrderTypes.buy)
            {
                if (Bid <= openOrder.StopLoss)
                    closeOrder(openOrder, Bid, "Stop loss called,", ReasonCloseOrders.CallStopLoss, closeDateTime);
                else if (Bid >= openOrder.TakeProfit)
                    closeOrder(openOrder, Bid, "Call take profit,", ReasonCloseOrders.CallTakeProfit, closeDateTime);
            }
            else if (openOrder.OrderType == OrderTypes.sell)
            {
                if (Ask >= openOrder.StopLoss)
                    closeOrder(openOrder, Ask, "Stop loss called,", ReasonCloseOrders.CallStopLoss, closeDateTime);
                else if (Ask <= openOrder.TakeProfit)
                    closeOrder(openOrder, Ask, "Call take profit,", ReasonCloseOrders.CallTakeProfit, closeDateTime);
            }
        }

        private int Buy(Tick tick, int counterCandelDiff, VolumeProfiler v)
        {
            Order? openOrder = GetOpenOrder(tick.Ask, tick.TickDatetime, tick.SymbolId);

            if (openOrder != null && openOrder.OrderType == OrderTypes.buy)
            {
                //  closeOrder(openOrder, tick.Ask, "Close just for test TODO,", ReasonCloseOrders.FindedBuyPostion, TickDateTime);//TODO fast real test  Remove
                CurrSymbol.BuyCallAginCounter += 1;
                if (CurrSymbol.BuyCallAginCounter > ConfigBot.BuySellCallAginCount)
                {
                    openOrder.History = openOrder.History + $"Buy again count:{CurrSymbol.BuyCallAginCounter},";
                    _dbContext.SaveChanges();
                    CurrSymbol.BuyCallAginCounter = 0;
                }
                return 0;
            }
            else if (openOrder != null && openOrder.OrderType == OrderTypes.sell)
            {
                // closeOrder(openOrder, tick.Ask, "Buy Called,", ReasonCloseOrders.FindedBuyPostion, tick.TickDatetime);todo true
                return 2;
            }
            int result = 0;
            if (CurrSymbol.PermisionTradeType == PermisionTradeType.buy || CurrSymbol.PermisionTradeType == PermisionTradeType.buyAndSell)
                result = CreateOrder(tick, counterCandelDiff, v, OrderTypes.buy);
            return result > 0 ? 1 : -1;
        }

        private int Sell(Tick tick, int counterCandelDiff, VolumeProfiler v)
        {
            Order? openOrder = GetOpenOrder(tick.Ask, tick.TickDatetime, tick.SymbolId);

            if (openOrder != null && openOrder.OrderType == OrderTypes.sell)
            {
                // closeOrder(openOrder, tick.Bid, "Close just for test TODO", ReasonCloseOrders.FindedSellPostion, TickDateTime);//TODO fast real test  Remove
                CurrSymbol.SellCallAginCounter += 1;
                if (CurrSymbol.SellCallAginCounter > ConfigBot.BuySellCallAginCount)
                {
                    openOrder.History = openOrder.History + $"Sell again count:{CurrSymbol.SellCallAginCounter},";
                    _dbContext.SaveChanges();
                    CurrSymbol.SellCallAginCounter = 0;
                }

                return 0;
            }
            else if (openOrder != null && openOrder.OrderType == OrderTypes.buy)
            {
                //closeOrder(openOrder, tick.Ask, "Sell Called,", ReasonCloseOrders.FindedSellPostion, tick.TickDatetime);todo true
                return 2;
            }
            int result = 0;
            if (CurrSymbol.PermisionTradeType == PermisionTradeType.sell || CurrSymbol.PermisionTradeType == PermisionTradeType.buyAndSell)
                result = CreateOrder(tick, counterCandelDiff, v, OrderTypes.sell);
            return result > 0 ? 1 : -1;
        }


        private int CreateOrder(Tick tick, int counterCandelDiff, VolumeProfiler v, OrderTypes orderType)
        {
            LogManager.ConsoleWriteLine($"Starting with Symbol Id:{tick.SymbolId}, Order Types:{orderType}, VP High:{v.High}, VP Low:{v.Low}, VP Repetation{v.LastBarRepetationVolume}, Tick Datetime:{tick.TickDatetime}," +
                $" Candle Datetime:{tick.CandleDatetime}, period Tick:{tick.Period}", "Create Order", 25);
            // Get or add a lock object for the specific symbolId
            var lockObject = lockObjects.GetOrAdd(tick.SymbolId, new object());
            lock (lockObject)
            {
                try
                {
                    var lotsize = accountManager.LotSize(tick, tick.SymbolId, orderType);
                    if (lotsize == 0) return -1;

                    Order order = new Order
                    {
                        VolumeProfilerId = v.Id,
                        OpenTickPrice = tick.Ask,
                        OpenTickDateTime = tick.TickDatetime,
                        OrderDatetime = DateTime.UtcNow,
                        CounterCandelDiff = counterCandelDiff,
                        LastBarsRepetion = v.LastBarRepetationVolume,
                        CloseTickPrice = 0,
                        LotSize = lotsize,
                        Status = OrderStatus.open,
                        OrderType = orderType,
                        History = orderType == OrderTypes.buy ? "register buy order," : "register sell order,",
                        VPlow_high = VPlow_high,
                        PeriodTickMin = tick.Period,
                        TakeProfit = accountManager.CalculateTakeProfit(orderType == OrderTypes.buy ? tick.Bid : tick.Ask, orderType, takeProfitMultiple, VPlow_high, tick.SymbolId),
                        StopLoss = accountManager.CalculateStopLoss(orderType == OrderTypes.buy ? tick.Bid : tick.Ask, orderType, stoplossMultiple, VPlow_high, tick.SymbolId),
                        LastTotalBalance = 0,
                        AccountMargin = 0,
                        OrderMargin = accountManager.CalculateMarginOrder(lotsize, tick.Ask),
                        SymbolId = tick.SymbolId
                    };

                    if (orderOperationsAPI != null)
                    {
                        CreatedOrderResponse createdOrderResponse = orderOperationsAPI.CreateOrder(CurrSymbol.Name, lotsize, orderType, order.TakeProfit, order.StopLoss).Result;

                        if (createdOrderResponse == null || createdOrderResponse.MarketStatus != LiveOperationStatus.Success) return -1;

                        createdOrderResponse.AddExchangeResponseToOrder(order);
                    }

                    _dbContext.Order.Add(order);
                    int result = _dbContext.SaveChanges();
                    if (result > 0) accountManager.UpdateAcountAfterRegOrder(order.OrderMargin);
                    CheckOpenPostionWithLive();
                    return result;
                }
                catch (Exception ex)
                {
                    LogManager.WriteErrorLog($"on symbol:{tick.SymbolId}, TickDatetime:{tick.TickDatetime}, CandleDatetime:{tick.CandleDatetime}, ex.Message:{ex.Message}", 1, "Create order");
                    return -1;
                }
            }

        }

        private void closeOrder(Order openOrder, decimal tickPrice, string history, ReasonCloseOrders reasonCloseOrders, DateTime closeDatetime)
        {
            decimal closePrice;
            //if (reasonCloseOrders == ReasonCloseOrders.CallTakeProfit) closePrice = openOrder.TakeProfit; //ConfigBot.IsTestingMode && todo
            //else if (reasonCloseOrders == ReasonCloseOrders.CallStopLoss) closePrice = openOrder.StopLoss;//ConfigBot.IsTestingMode &&
            //else closePrice = tickPrice;
            closePrice = tickPrice;
            if (orderOperationsAPI != null)
            {
                ClosePositionsResponse closePositionsResponse = orderOperationsAPI.ClosePositions(CurrSymbol.Name, openOrder.OrderType.GetPositionType()).Result;

                if (closePositionsResponse.marketStatus == LiveOperationStatus.DoNotSuccess)
                {
                    closePositionsResponse = orderOperationsAPI.GetClosedTradeInfo(openOrder.ExCreateRespTradeIds).Result;
                    // SynchronousOpenPostionWithLive(closePrice);
                }
                else if (closePositionsResponse.marketStatus != LiveOperationStatus.Success) { return; }

                closePositionsResponse.CloseResponseToOrder(openOrder);
            }

            BalanceResult res = FillInfoCloseOrderLocal(openOrder, history, reasonCloseOrders, closeDatetime, closePrice);

            int sRes = _dbContext.SaveChanges();
            if (sRes > 0) accountManager.UpdateBalanceAfterCloseOrder(res.revenue, openOrder.OrderMargin);
        }

        private BalanceResult FillInfoCloseOrderLocal(Order openOrder, string history, ReasonCloseOrders reasonCloseOrders, DateTime closeDatetime, decimal closePrice)
        {
            openOrder.History = openOrder.History + history;
            openOrder.ReasonCloseOrders = reasonCloseOrders;
            openOrder.Status = OrderStatus.close;
            openOrder.CloseTickPrice = closePrice;
            openOrder.CloseTickDatetime = closeDatetime;
            var res = accountManager.BalanceAndMargin(openOrder.OpenTickPrice, closePrice, openOrder.LotSize, openOrder.OrderType, openOrder.OrderMargin);
            openOrder.LastTotalBalance = res.lastTotalInvestment;
            openOrder.Revenue = res.revenue;
            openOrder.AccountMargin = res.AccountMargin;
            return res;
        }

        private void SynchronousOpenPostionWithLive(decimal closePrice)
        {
            var openListOrders = _dbContext.Order.Where(o => o.Status == OrderStatus.open && o.SymbolId == CurrSymbol.Id).ToList();
            decimal totalPosition = openListOrders.Sum(c => c.ExCreateRespUnits);
            //Check exchange API 
            if (orderOperationsAPI != null)
            {
                OpenPositions openPositions = orderOperationsAPI.GetOpenPositions(CurrSymbol.Name).Result;
                if (openPositions == null && openListOrders.Count == 0) return;

                if (openPositions.LongUnits + openPositions.ShortUnits != totalPosition)
                {
                    LogManager.WriteErrorLog("Big logical Error Open position in local server and live server do not equeal" + string.Join(",", openListOrders.Select(o => o.Id)), 1, "closeOrder");
                    foreach (var ord in openListOrders)
                    {
                        BalanceResult res = FillInfoCloseOrderLocal(ord, "Big_logical_Error_Asynchronous_With_Live", ReasonCloseOrders.AsynchronousWithLive, DateTime.Now, closePrice);

                        int sRes = _dbContext.SaveChanges();
                        if (sRes > 0) accountManager.UpdateBalanceAfterCloseOrder(res.revenue, ord.OrderMargin);
                    }
                    ClosePositionsResponse closePositionsResponse = null;
                    if (openPositions.LongUnits > 0)
                    {
                        closePositionsResponse = orderOperationsAPI.ClosePositions(CurrSymbol.Name, Position.Long).Result;
                        if (closePositionsResponse != null && closePositionsResponse.marketStatus == LiveOperationStatus.Success) accountManager.UpdateBalanceAfterCloseOrder(0, 0);
                    }
                    else if (openPositions.ShortUnits > 0)
                    {
                        closePositionsResponse = orderOperationsAPI.ClosePositions(CurrSymbol.Name, Position.Short).Result;
                        if (closePositionsResponse != null && closePositionsResponse.marketStatus == LiveOperationStatus.Success) accountManager.UpdateBalanceAfterCloseOrder(0, 0);
                    }
                }
                _dbContext.SaveChanges();
            }
        }

        private void CheckOpenPostionWithLive()
        {
            var openListOrders = _dbContext.Order.Where(o => o.Status == OrderStatus.open && o.SymbolId == CurrSymbol.Id).ToList();
            decimal totalPositionLocal = openListOrders.Sum(c => c.ExCreateRespUnits);
            //Check exchange API 
            if (orderOperationsAPI != null)
            {
                OpenPositions openPositions = orderOperationsAPI.GetOpenPositions(CurrSymbol.Name).Result;
                if (openPositions == null && openListOrders.Count == 0) return;
                decimal totalPostionServer = openPositions.LongUnits + openPositions.ShortUnits;
                if (totalPostionServer != totalPositionLocal)
                {
                    Console.WriteLine($"Big logical Error Open position in loca server and live server do not equeal PostionServer:{totalPostionServer}, PositionLocal:{totalPositionLocal}"
                        + string.Join(",", openListOrders.Select(o => o.Id)));
                }
            }
        }

        private Order? GetOpenOrder(decimal tickprice, DateTime currentTickDatatime, int symbolID)
        {
            var openList = _dbContext.Order.Where(o => o.Status == OrderStatus.open && o.SymbolId == symbolID).ToList();

            if (openList.Count > 1)
            {
                foreach (var order in openList)
                {
                    closeOrder(order, tickprice, "Too Many Open Orders Detected", ReasonCloseOrders.TooManyOpenOrder, currentTickDatatime);
                }

                throw new Exception("Multiple open orders detected.");
            }
            else if (openList.Count == 1)
            {
                return openList.First();
            }
            else
            {
                return null;
            }
        }
    }

    public class BalanceResult
    {
        public decimal lastTotalInvestment { get; set; }
        public decimal revenue { get; set; }
        public decimal AccountMargin { get; set; }
    }
    public enum OrderTypes
    {
        buy = 1,
        sell = 2
    }

    public enum ReasonCloseOrders
    {
        CallTakeProfit = 1,
        CallStopLoss = 2,
        FindedBuyPostion = 3,
        FindedSellPostion = 4,
        TooManyOpenOrder = 5,
        AsynchronousWithLive = 6
    }
    public enum Position
    {
        Short = 1,
        Long = 2
    }
    public enum OrderStatus
    {
        open = 1,
        close = 2
    }
    public enum PermisionTradeType
    {
        buy = 1,
        sell = 2,
        buyAndSell = 3
    }
}
