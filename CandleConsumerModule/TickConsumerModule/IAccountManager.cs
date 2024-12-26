using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.CandleConsumerModule.TickConsumerModule
{


    public interface IAccountManager
    {


        void UpdateAcountAfterRegOrder(decimal marginOrder);
        void UpdateBalanceAfterCloseOrder(decimal revenue, decimal marginOrder);
        BalanceResult BalanceAndMargin(decimal openPriceOrder, decimal closePriceOrder, decimal lotSizeOrder, OrderTypes orderTypes, decimal marginOrder);
        decimal LotSize(Tick tick, int symbolId, OrderTypes orderType);
        decimal CalculateTakeProfit(decimal price, OrderTypes orderTypes, decimal takeProfitMultiple, decimal VPlow_high, int symbolId);
        decimal CalculateStopLoss(decimal price, OrderTypes orderTypes, decimal stoplossMultiple, decimal VPlow_high, int symbolId);
        decimal CalculateMarginOrder(decimal lotSize, decimal tickPrice);

    }
}
