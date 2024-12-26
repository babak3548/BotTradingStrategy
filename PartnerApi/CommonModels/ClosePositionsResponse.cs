using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.CommonModels
{
    public class ClosePositionsResponse
    {
        public LiveOperationStatus marketStatus {  get; set; }
        public string TradeIds {  get; set; }

        public decimal CloseoutAsk { get; internal set; }
        public decimal GainQuoteHomeConversionFactor { get; internal set; }
        public decimal LossQuoteHomeConversionFactor { get; internal set; }
        public string FullResponseBody { get; internal set; }
        public decimal CloseoutBid { get; internal set; }
        public decimal FullVWAP { get; internal set; }
        public decimal PriceTran { get; internal set; }
        public string Symbol { get; internal set; }
        public decimal RequestedUnits { get; internal set; }
        public decimal HalfSpreadCost { get; internal set; }
        public decimal Financing { get; internal set; }
        public string RelatedTransactionIDs { get; internal set; }
        public decimal Commission { get; internal set; }
        public string Reason { get; internal set; }
        public decimal AccountBalance { get; internal set; }
        public decimal PL { get; internal set; }
     
        public decimal Units { get; internal set; }
        public DateTime Time { get; internal set; }

        public void CloseResponseToOrder(Order openOrder)
        {
            // Assign values from this to myOrderObj
            openOrder.ExClosePosMarketStatus = this.marketStatus;
            openOrder.ExClosePosTradeIds = this.TradeIds;
            openOrder.ExClosePosCloseoutAsk = this.CloseoutAsk;
            openOrder.ExClosePosGainQuoteHomeConversionFactor = this.GainQuoteHomeConversionFactor;
            openOrder.ExClosePosLossQuoteHomeConversionFactor = this.LossQuoteHomeConversionFactor;
            openOrder.ExClosePosFullResponseBody = this.FullResponseBody;
            openOrder.ExClosePosCloseoutBid = this.CloseoutBid;
            openOrder.ExClosePosFullVWAP = this.FullVWAP;
            openOrder.ExClosePosPriceTran = this.PriceTran;
            openOrder.ExClosePosSymbol = this.Symbol;
            openOrder.ExClosePosRequestedUnits = this.RequestedUnits;
            openOrder.ExClosePosHalfSpreadCost = this.HalfSpreadCost;
            openOrder.ExClosePosFinancing = this.Financing;
            openOrder.ExClosePosRelatedTransactionIDs = this.RelatedTransactionIDs;
            openOrder.ExClosePosCommission = this.Commission;
            openOrder.ExClosePosReason = this.Reason;
            openOrder.ExClosePosAccountBalance = this.AccountBalance;
            openOrder.ExClosePosPL = this.PL;
            openOrder.ExClosePosUnits = this.Units;
            openOrder.ExClosePosTime = this.Time;
        }
    }

    public enum LiveOperationStatus
    {
        Success = 1,
        MarketClose = 2,
        DoNotSuccess = 3
        
    }
}
