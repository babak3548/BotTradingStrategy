using Microsoft.EntityFrameworkCore;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi;
using PreprocessDataStocks.PartnerApi.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PreprocessDataStocks.TickConsumerModule
{
    public class TraderAgent
    {
        private int _effectedVolumeOnNextCandles;
        private int _minRepetitionVolumeNeededTrade;
        private decimal _confirmTrendNum;
        private decimal _vpLowHigh;
        private decimal _percentageInvestTrade;
        private decimal _takeProfitMultiple;
        private decimal _stoplossMultiple;

        private Symbol _currentSymbol;
        private readonly ForexAccountManager _forexAccountManager;
        private readonly OrderOperationFactory _orderOperationFactory;
        private IOrderOperations _orderOperationsAPI;
        private readonly PreprocessDataStockContext _dbContext;

        public TraderAgent(PreprocessDataStockContext dbContext)
        {
            _dbContext = dbContext;
            _forexAccountManager = ForexAccountManager.ForexAccountManagerInstant;
            _orderOperationFactory = new OrderOperationFactory();
        }

        private void UpdateConfigParameterSymbol(Tick tick)
        {
            _currentSymbol = ConfigBot.Symbols.First(c => c.Id == tick.SymbolId);
            _effectedVolumeOnNextCandles = _currentSymbol.EfectedVolumeOnNextCandles;
            _minRepetitionVolumeNeededTrade = _currentSymbol.MinRepetationVolumeNeededTrade;
            _confirmTrendNum = _currentSymbol.ConfirmTrendNum;
            _vpLowHigh = _currentSymbol.VpLowHigh;
            _percentageInvestTrade = _currentSymbol.PercentageInvestTrade;
            _takeProfitMultiple = _currentSymbol.TakeProfitMultiple;
            _stoplossMultiple = _currentSymbol.StopLossMultiple;

            InitializeRecentlyCandlesQueue(tick);
            _orderOperationsAPI = _orderOperationFactory.GetOrderOperation(_currentSymbol.Exchange);
        }

        private void InitializeRecentlyCandlesQueue(Tick tick)
        {
            if (_currentSymbol.RecentlyCandlesQueue == null)
            {
                _currentSymbol.RecentlyCandlesQueue = new Queue<Candle>();
                var lastCandles = _dbContext.Candle
                    .Where(c => c.Datetime <= tick.CandleDatetime)
                    .OrderByDescending(c => c.Datetime)
                    .Take(_effectedVolumeOnNextCandles)
                    .ToList();
                foreach (var candle in lastCandles)
                {
                    _currentSymbol.RecentlyCandlesQueue.Enqueue(candle);
                }
            }
        }

        public void CheckAndDoTrade(Tick tick, decimal openPriceCurrCandle)
        {
            UpdateConfigParameterSymbol(tick);
            var currCandle = GetCurrentCandle(tick);
            var recentCandlesIds = GetRecentCandlesIds(currCandle);
            var vps = GetRelevantVolumeProfiles(tick, recentCandlesIds);

            CheckTakeProfitAndStopLoss(tick.Price, currCandle.Datetime.Value, tick.SymbolId);

            openPriceCurrCandle = openPriceCurrCandle == 0 ? tick.Price : openPriceCurrCandle;
            var lowCurrCandle = Math.Min(decimal.MaxValue, tick.Price);
            var highCurrCandle = Math.Max(0, tick.Price);

            EvaluateTradingOpportunities(tick, lowCurrCandle, highCurrCandle, openPriceCurrCandle, tick.Price, currCandle, vps);
            _dbContext.SaveChanges();
        }

        private void EvaluateTradingOpportunities(Tick tick, decimal lowCurrCandle, decimal highCurrCandle, decimal openPriceCurrCandle, decimal tickPrice, Candle currCandle, List<VolumeProfiler> vps)
        {
            foreach (var vp in vps)
            {
                var candleDiff = currCandle.Id - vp.Candle.Id;

                if (vp.Low - _confirmTrendNum > tickPrice)
                {
                    if (openPriceCurrCandle - tickPrice > _vpLowHigh / 3)
                    {
                        Sell(currCandle.Datetime.Value, tickPrice, candleDiff, vp, tick.Period, tick.SymbolId);
                    }
                }
                else if (vp.High + _confirmTrendNum < tickPrice)
                {
                    if (tickPrice - openPriceCurrCandle > _vpLowHigh / 3)
                    {
                        Buy(currCandle.Datetime.Value, tickPrice, candleDiff, vp, tick.Period, tick.SymbolId);
                    }
                }
            }
        }

        private List<VolumeProfiler> GetRelevantVolumeProfiles(Tick tick, List<int> recentCandlesIds)
        {
            return _dbContext.VolumeProfiler
                .Where(vp => recentCandlesIds.Contains(vp.Candle.Id) && vp.LastBarRepetationVolume >= _minRepetitionVolumeNeededTrade && vp.SymbolId == tick.SymbolId)
                .OrderByDescending(vp => vp.LastBarRepetationVolume)
                .ToList();
        }

        private List<int> GetRecentCandlesIds(Candle currCandle)
        {
            var isNewCandle = !_currentSymbol.RecentlyCandlesQueue.Any(c => c.Id == currCandle.Id);
            if (isNewCandle && _currentSymbol.RecentlyCandlesQueue.Count >= _effectedVolumeOnNextCandles)
            {
                _currentSymbol.RecentlyCandlesQueue.Dequeue();
            }
            if (isNewCandle)
            {
                _currentSymbol.RecentlyCandlesQueue.Enqueue(currCandle);
            }
            return _currentSymbol.RecentlyCandlesQueue.Select(c => c.Id).ToList();
        }

        private Candle GetCurrentCandle(Tick tick)
        {
            return _dbContext.Candle.FirstOrDefault(c => c.Datetime == tick.CandleDatetime && c.SymbolId == tick.SymbolId);
        }

        private void CheckTakeProfitAndStopLoss(decimal tickPrice, DateTime closeDateTime, int symbolId)
        {
            var openOrder = GetOpenOrder(tickPrice, closeDateTime, symbolId);
            if (openOrder == null) return;

            if ((openOrder.OrderType == OrderTypes.buy && (tickPrice <= openOrder.StopLoss || tickPrice >= openOrder.TakeProfit)) ||
                (openOrder.OrderType == OrderTypes.sell && (tickPrice >= openOrder.StopLoss || tickPrice <= openOrder.TakeProfit)))
            {
                var reason = tickPrice <= openOrder.StopLoss ? ReasonCloseOrders.CallStopLoss : ReasonCloseOrders.CallTakeProfit;
                closeOrder(openOrder, tickPrice, reason.ToString(), reason, closeDateTime);
            }
        }

        private int Buy(DateTime tickDateTime, decimal tickPrice, int candleDiff, VolumeProfiler vp, decimal periodTick, int symbolId)
        {
            return HandleOrder(tickDateTime, tickPrice, candleDiff, vp, periodTick, symbolId, OrderTypes.buy);
        }

        private int Sell(DateTime tickDateTime, decimal tickPrice, int candleDiff, VolumeProfiler vp, decimal periodTick, int symbolId)
        {
            return HandleOrder(tickDateTime, tickPrice, candleDiff, vp, periodTick, symbolId, OrderTypes.sell);
        }

        private int HandleOrder(DateTime tickDateTime, decimal tickPrice, int candleDiff, VolumeProfiler vp, decimal periodTick, int symbolId, OrderTypes orderType)
        {
            var openOrder = GetOpenOrder(tickPrice, tickDateTime, symbolId);

            if (openOrder != null)
            {
                if (openOrder.OrderType == orderType)
                {
                    openOrder.History += orderType == OrderTypes.buy ? "Buy again," : "Sell again,";
                    _dbContext.SaveChanges();
                    return 0;
                }
                else
                {
                    var closeReason = orderType == OrderTypes.buy ? ReasonCloseOrders.FindedBuyPostion : ReasonCloseOrders.FindedSellPostion;
                    closeOrder(openOrder, tickPrice, $"{orderType} Called,", closeReason, tickDateTime);
                    return 2;
                }
            }

            if ((_currentSymbol.PermisionTradeType == PermisionTradeType.buy && orderType == OrderTypes.buy) ||
                (_currentSymbol.PermisionTradeType == PermisionTradeType.sell && orderType == OrderTypes.sell) ||
                _currentSymbol.PermisionTradeType == PermisionTradeType.buyAndSell)
            {
                return CreateOrder(tickDateTime, tickPrice, candleDiff, vp, periodTick, symbolId, orderType) > 0 ? 1 : -1;
            }

            return -1;
        }

        private int CreateOrder(DateTime tickDateTime, decimal tickPrice, int candleDiff, VolumeProfiler vp, decimal periodTick, int symbolId, OrderTypes orderType)
        {
            var lotSize = _forexAccountManager.LotSize(tickPrice);
            if (lotSize == 0) return -1;

            var order = new Order
            {
                VolumeProfilerId = vp.Id,
                OpenTickPrice = tickPrice,
                OpenTickDateTime = tickDateTime,
                OrderDatetime = DateTime.Now,
                CounterCandelDiff = candleDiff,
                LastBarsRepetion = vp.LastBarRepetationVolume,
                CloseTickPrice = 0,
                LotSize = lotSize,
                Status = OrderStatus.open,
                OrderType = orderType,
                History = orderType == OrderTypes.buy ? "register buy order," : "register sell order,",
                VPlow_high = _vpLowHigh,
                PeriodTickMin = periodTick,
                TakeProfit = _forexAccountManager.CalculateTakeProfit(tickPrice, orderType, _takeProfitMultiple, _vpLowHigh),
                StopLoss = _forexAccountManager.CalculateStopLoss(tickPrice, orderType, _stoplossMultiple, _vpLowHigh),
                LastTotalBalance = 0,
                AccountMargin = 0,
                OrderMargin = _forexAccountManager.CalculateMarginOrder(lotSize, tickPrice),
                SymbolId = symbolId
            };

            if (_orderOperationsAPI != null)
            {
                var createdOrderResponse = _orderOperationsAPI.CreateOrder(_currentSymbol.Name, lotSize, orderType, order.TakeProfit, order.StopLoss).Result;
                if (createdOrderResponse.MarketStatus == MarketStatus.Close) return -1;
                createdOrderResponse.AddExchangeResponseToOrder(order);
            }

            _dbContext.Order.Add(order);
            var result = _dbContext.SaveChanges();
            if (result > 0) _forexAccountManager.UpdateAcountAfterRegOrder(order.OrderMargin);
            return result;
        }

        private void closeOrder(Order openOrder, decimal tickPrice, string history, ReasonCloseOrders reasonCloseOrders, DateTime closeDatetime)
        {
            var closePrice = ConfigBot.IsTestingMode
                ? (reasonCloseOrders == ReasonCloseOrders.CallTakeProfit ? openOrder.TakeProfit : openOrder.StopLoss)
                : tickPrice;

            if (_orderOperationsAPI != null)
            {
                var closePositionsResponse = _orderOperationsAPI.ClosePositions(_currentSymbol.Name, openOrder.OrderType.GetPositionType()).Result;
                if (closePositionsResponse.marketStatus == MarketStatus.Close) return;
                closePositionsResponse.CloseResponseToOrder(openOrder);
            }

            openOrder.History += history;
            openOrder.ReasonCloseOrders = reasonCloseOrders;
            openOrder.Status = OrderStatus.close;
            openOrder.CloseTickPrice = closePrice;
            openOrder.CloseTickDatetime = closeDatetime;

            var res = _forexAccountManager.GetBalanceAndMargin(openOrder.OpenTickPrice, closePrice, openOrder.LotSize, openOrder.OrderType, openOrder.OrderMargin);
            openOrder.LastTotalBalance = res.LastTotalInvestment;
            openOrder.Revenue = res.Revenue;
            openOrder.AccountMargin = res.AccountMargin;

            if (_dbContext.SaveChanges() > 0)
            {
                _forexAccountManager.UpdateBalanceAfterCloseOrder(res.Revenue, openOrder.OrderMargin);
            }
        }

        private Order GetOpenOrder(decimal tickPrice, DateTime currentTickDatetime, int symbolId)
        {
            var openOrders = _dbContext.Order.Where(o => o.Status == OrderStatus.open && o.SymbolId == symbolId).ToList();

            if (openOrders.Count > 1)
            {
                foreach (var order in openOrders)
                {
                    closeOrder(order, tickPrice, "Too Many Open Orders Detected", ReasonCloseOrders.TooManyOpenOrder, currentTickDatetime);
                }

                throw new InvalidOperationException("Multiple open orders detected.");
            }

            return openOrders.SingleOrDefault();
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
            TooManyOpenOrder = 5
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
}
