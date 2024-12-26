using PreprocessDataStocks.PartnerApi.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.OandaModels.LongTransaction
{
    public class longPositionCloseout
    {
        public string instrument { get; set; }
        public string units { get; set; }
    }

    public class longOrderCreateTransaction
    {
        public string id { get; set; }
        public string accountID { get; set; }
        public int userID { get; set; }
        public string batchID { get; set; }
        public string requestID { get; set; }
        public DateTime time { get; set; }
        public string type { get; set; }
        public string instrument { get; set; }
        public string units { get; set; }
        public string timeInForce { get; set; }
        public string positionFill { get; set; }
        public string reason { get; set; }
        public longPositionCloseout longPositionCloseout { get; set; }
    }

    public class tradesClosed
    {
        public string tradeID { get; set; }
        public string clientTradeID { get; set; }
        public string units { get; set; }
        public string realizedPL { get; set; }
        public string financing { get; set; }
        public string baseFinancing { get; set; }
        public string price { get; set; }
        public string guaranteedExecutionFee { get; set; }
        public string quoteGuaranteedExecutionFee { get; set; }
        public string halfSpreadCost { get; set; }
    }

    public class bids
    {
        public string price { get; set; }
        public string liquidity { get; set; }
    }

    public class asks
    {
        public string price { get; set; }
        public string liquidity { get; set; }
    }

    public class fullPrice
    {
        public string closeoutBid { get; set; }
        public string closeoutAsk { get; set; }
        public DateTime timestamp { get; set; }
        public List<bids> bids { get; set; }
        public List<asks> asks { get; set; }
    }

    public class gainQuoteHome
    {
        public string factor { get; set; }
    }

    public class lossQuoteHome
    {
        public string factor { get; set; }
    }

    public class gainBaseHome
    {
        public string factor { get; set; }
    }

    public class lossBaseHome
    {
        public string factor { get; set; }
    }

    public class homeConversionFactors
    {
        public gainQuoteHome gainQuoteHome { get; set; }
        public lossQuoteHome lossQuoteHome { get; set; }
        public gainBaseHome gainBaseHome { get; set; }
        public lossBaseHome lossBaseHome { get; set; }
    }

    public class longOrderFillTransaction
    {
        public string id { get; set; }
        public string accountID { get; set; }
        public int userID { get; set; }
        public string batchID { get; set; }
        public string requestID { get; set; }
        public DateTime time { get; set; }
        public string type { get; set; }
        public string orderID { get; set; }
        public string instrument { get; set; }
        public string units { get; set; }
        public string requestedUnits { get; set; }
        public string price { get; set; }
        public string pl { get; set; }
        public string quotePL { get; set; }
        public string financing { get; set; }
        public string baseFinancing { get; set; }
        public string commission { get; set; }
        public string accountBalance { get; set; }
        public string gainQuoteHomeConversionFactor { get; set; }
        public string lossQuoteHomeConversionFactor { get; set; }
        public string guaranteedExecutionFee { get; set; }
        public string quoteGuaranteedExecutionFee { get; set; }
        public string halfSpreadCost { get; set; }
        public string fullVWAP { get; set; }
        public string reason { get; set; }
        public List<tradesClosed> tradesClosed { get; set; }
        public fullPrice fullPrice { get; set; }
    }

    public class root
    {
        public longOrderCreateTransaction longOrderCreateTransaction { get; set; }
        public longOrderFillTransaction longOrderFillTransaction { get; set; }
        public List<string> relatedTransactionIDs { get; set; }
        public string lastTransactionID { get; set; }

        public ClosePositionsResponse GetClosePositionResponse(string responseBody)
        {
            ClosePositionsResponse result = new ClosePositionsResponse
            {
                marketStatus=LiveOperationStatus.Success,
                
                TradeIds = string.Join(",", this.longOrderFillTransaction.tradesClosed.Select(t => t.tradeID)),
                Time = this.longOrderFillTransaction.time,
                Units = decimal.Parse(this.longOrderFillTransaction.units),
                PL = decimal.Parse(this.longOrderFillTransaction.pl),
                AccountBalance = decimal.Parse(this.longOrderFillTransaction.accountBalance),
                Reason = this.longOrderFillTransaction.reason,
                Commission = decimal.Parse(this.longOrderFillTransaction.commission),
                HalfSpreadCost = decimal.Parse(this.longOrderFillTransaction.halfSpreadCost),
                Financing = decimal.Parse(this.longOrderFillTransaction.financing),
                RelatedTransactionIDs = string.Join(",", this.relatedTransactionIDs),
                RequestedUnits = decimal.Parse(this.longOrderFillTransaction.requestedUnits),
                Symbol = this.longOrderCreateTransaction.instrument,
                PriceTran = decimal.Parse(this.longOrderFillTransaction.price),
                FullVWAP = decimal.Parse(this.longOrderFillTransaction.fullVWAP),
                CloseoutBid = decimal.Parse(this.longOrderFillTransaction.fullPrice.closeoutBid),
                CloseoutAsk = decimal.Parse(this.longOrderFillTransaction.fullPrice.closeoutAsk),
                GainQuoteHomeConversionFactor = decimal.Parse(this.longOrderFillTransaction.gainQuoteHomeConversionFactor),
                LossQuoteHomeConversionFactor = decimal.Parse(this.longOrderFillTransaction.lossQuoteHomeConversionFactor),
                FullResponseBody = responseBody,
            };
            return result;
        }
    }

}