using Microsoft.EntityFrameworkCore;
using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.CandleConsumerModule.TickConsumerModule
{
    public class ForexAccountManager : BaseAccountManager, IAccountManager
    {
        private static readonly ForexAccountManager instant = new ForexAccountManager();

        public static ForexAccountManager Instant => instant;
        // Constructor
        private ForexAccountManager()
        {
            Balance = ConfigBot.InitialInvestment;
        }


    }

    public abstract class BaseAccountManager
    {

        private decimal _balance;
        private readonly object _balanceLock = new object();

        protected decimal Balance
        {
            get
            {
                lock (_balanceLock)
                {
                    return _balance;
                }
            }
            set
            {
                lock (_balanceLock)
                {
                    _balance = value;
                }
            }
        }

        private decimal _margin;
        private readonly object _marginLock = new object();

        private decimal Margin
        {
            get
            {
                lock (_marginLock)
                {
                    return _margin;
                }
            }
            set
            {
                lock (_marginLock)
                {
                    _margin = value;
                }
            }
        }
        private decimal FreeMargin => Equity - Margin;
        private decimal MarginLevel => Equity / Margin * 100;
        private decimal Equity => Balance;

        protected decimal leverage = 100; // Default leverage
        protected int concurrentPostion = 2;
        //private decimal LotSizeMultiple = 0;
        protected readonly decimal lotUnit = 100000;

        // Method to update account values based on tick price
        public virtual void UpdateAcountAfterRegOrder(decimal marginOrder)
        {
            Margin += marginOrder;
        }

        public virtual void UpdateBalanceAfterCloseOrder(decimal revenue, decimal marginOrder)
        {
            Margin -= marginOrder;
            Balance += revenue;

        }

        public BalanceResult BalanceAndMargin(decimal openPriceOrder, decimal closePriceOrder, decimal lotSizeOrder, OrderTypes orderTypes, decimal marginOrder)
        {
            decimal revenue = orderTypes == OrderTypes.buy ? closePriceOrder - openPriceOrder : openPriceOrder - closePriceOrder;
            revenue = revenue * lotSizeOrder * lotUnit;

            return new BalanceResult { lastTotalInvestment = Balance + revenue, revenue = revenue, AccountMargin = Margin - marginOrder };
        }
        // Method to calculate margin required for open positions
        public virtual decimal LotSize(Tick tick, int symbolId, OrderTypes orderType)
        {
            decimal price;
            Symbol sym = ConfigBot.Symbols.First(s => s.Id == symbolId);

            decimal ratioStopLossToSpread = sym.VpLowHigh * sym.StopLossMultiple / (tick.Ask - tick.Bid);
            if (ratioStopLossToSpread < ConfigBot.RatioStopLossToSpreadConst)
            {
                Console.WriteLine($"log Extra Spread, {sym.Name} ratioStopLossToSpread:{ratioStopLossToSpread}  ratioStopLossToSpreadConst:{ConfigBot.RatioStopLossToSpreadConst}");
                return 0;
            }

            price = (orderType == OrderTypes.buy ? tick.Ask : tick.Bid) * tick.HomeConversion;

            if (FreeMargin < price || price == 0)
                return 0;

            decimal lot = Balance * leverage / (price * lotUnit * concurrentPostion);
            return Math.Round(lot, 4);
        }

        public virtual decimal CalculateTakeProfit(decimal price, OrderTypes orderTypes, decimal takeProfitMultiple, decimal VPlow_high, int symbolId)
        {
            //todo  *10 When fast test 
            decimal takeProfit = (orderTypes == OrderTypes.buy ? takeProfitMultiple : -takeProfitMultiple) * VPlow_high + price;//todo *10 just for fast test
            return Math.Round(takeProfit, 5);
        }

        public virtual decimal CalculateStopLoss(decimal price, OrderTypes orderTypes, decimal stoplossMultiple, decimal VPlow_high, int symbolId)
        {//todo  *10 When fast test
            decimal stopLoss = (orderTypes == OrderTypes.buy ? -stoplossMultiple : stoplossMultiple) * VPlow_high + price;//todo *10 just for fast test
            return Math.Round(stopLoss, 5);
        }

        public decimal CalculateMarginOrder(decimal lotSize, decimal tickPrice) => lotSize * tickPrice * lotUnit / leverage;
    }

}
