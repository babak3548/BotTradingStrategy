using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PreprocessDataStocks.PartnerApi.OandaModels.GetTradeInfo
{

    public class ClientExtensions
    {
        public string Id { get; set; }
        public string Tag { get; set; }
    }

    public class TakeProfitOrder
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Type { get; set; }
        public string TradeID { get; set; }
        public string Price { get; set; }
        public string TimeInForce { get; set; }
        public string TriggerCondition { get; set; }
        public string State { get; set; }
        public string CancellingTransactionID { get; set; }
        public DateTime CancelledTime { get; set; }
    }

    public class StopLossOrder
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Type { get; set; }
        public string TradeID { get; set; }
        public string Price { get; set; }
        public string TimeInForce { get; set; }
        public string TriggerCondition { get; set; }
        public string TriggerMode { get; set; }
        public string State { get; set; }
        public string FillingTransactionID { get; set; }
        public DateTime FilledTime { get; set; }
        public List<string> TradeClosedIDs { get; set; }
    }

    public class Trade
    {
        public string Id { get; set; }
        public string Instrument { get; set; }
        public string Price { get; set; }
        public DateTime OpenTime { get; set; }
        public string InitialUnits { get; set; }
        public string InitialMarginRequired { get; set; }
        public string State { get; set; }
        public string CurrentUnits { get; set; }
        public string RealizedPL { get; set; }
        public List<string> ClosingTransactionIDs { get; set; }
        public string Financing { get; set; }
        public string DividendAdjustment { get; set; }
        public DateTime CloseTime { get; set; }
        public string AverageClosePrice { get; set; }
        public ClientExtensions ClientExtensions { get; set; }
        public TakeProfitOrder TakeProfitOrder { get; set; }
        public StopLossOrder StopLossOrder { get; set; }
    }

    public class OandaTradeResponse
    {
        public Trade Trade { get; set; }
        public string LastTransactionID { get; set; }
    }

}
