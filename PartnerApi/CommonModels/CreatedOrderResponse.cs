using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.CommonModels
{
    public class CreatedOrderResponse
    {
        public LiveOperationStatus MarketStatus { get;  set; }

        public string TradeIds { get; set; }

        public decimal CloseoutAsk { get;  set; }
        public decimal CloseoutBid { get;  set; }
        public decimal GainQuoteHomeConversionFactor { get;  set; }
        public decimal LossQuoteHomeConversionFactor { get;  set; }
        
        public decimal PriceTran { get;  set; }

       
        public decimal HalfSpreadCost { get;  set; }
        public decimal Financing { get;  set; }
        public string RelatedTransactionIDs { get;  set; }
        public decimal Commission { get;  set; }
        public string Reason { get;  set; }
        public decimal AccountBalance { get;  set; }
        public decimal PL { get;  set; }

        public decimal Units { get;  set; }
        public DateTime Time { get;  set; }

        public decimal InitialMarginRequired { get; set; }
        public string FullResponseBody { get; set; }



        public void AddExchangeResponseToOrder(Order openOrder)
        {
            // Assign values from this to myOrderObj
            openOrder.ExCreateRespMarketStatus = this.MarketStatus;
            openOrder.ExCreateRespTradeIds = this.TradeIds;
            openOrder.ExCreateRespCloseoutAsk = this.CloseoutAsk;
            openOrder.ExCreateRespGainQuoteHomeConversionFactor = this.GainQuoteHomeConversionFactor;
            openOrder.ExCreateRespLossQuoteHomeConversionFactor = this.LossQuoteHomeConversionFactor;
            openOrder.ExCreateRespFullResponseBody = this.FullResponseBody;
            openOrder.ExCreateRespCloseoutBid = this.CloseoutBid;
            openOrder.ExCreateRespPriceTran = this.PriceTran;
            openOrder.ExCreateRespHalfSpreadCost = this.HalfSpreadCost;
            openOrder.ExCreateRespFinancing = this.Financing;
            openOrder.ExCreateRespRelatedTransactionIDs = this.RelatedTransactionIDs;
            openOrder.ExCreateRespCommission = this.Commission;
            openOrder.ExCreateRespReason = this.Reason;
            openOrder.ExCreateRespAccountBalance = this.AccountBalance;
            openOrder.ExCreateRespPL = this.PL;
            openOrder.ExCreateRespUnits = this.Units;
            openOrder.ExCreateRespTime = this.Time;
            openOrder.ExCreateRespInitialMarginRequired = this.InitialMarginRequired;
            
        }

    }
}
