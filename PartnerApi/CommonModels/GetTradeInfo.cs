using PreprocessDataStocks.CandleConsumerModule.TickConsumerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.CommonModels
{
    public class GetTradeInfo
    {
        //        "state": "CLOSED",
        public OrderStatus OrderStatus { get; set; }
        public ReasonCloseOrders ReasonCloseOrders { get; set; }
        public decimal realizedPL  { get; set; }
        public decimal Financing { get; set; }
        public string ClosingTransactionIDs { get; set; }
        public DateTime closeTime { get; set; }

    }
}
