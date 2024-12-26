using PreprocessDataStocks.PartnerApi.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.OandaModels.ShortTransaction
{



    public class shortPositionCloseout
    {
        public string instrument { get; set; }
        public string units { get; set; }
    }

    public class shortOrderCreateTransaction
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
        public shortPositionCloseout shortPositionCloseout { get; set; }
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

    public class FullPrice
    {
        public string closeoutBid { get; set; }
        public string closeoutAsk { get; set; }
        public DateTime timestamp { get; set; }
        public List<bids> bids { get; set; }
        public List<asks> asks { get; set; }
    }


    public class homeConversionFactors
    {
        public gainQuoteHome gainQuoteHome { get; set; }
        public lossQuoteHome lossQuoteHome { get; set; }
        public gainBaseHome gainBaseHome { get; set; }
        public ShortTransaction lossBaseHome { get; set; }
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

    public class ShortTransaction
    {
        public string factor { get; set; }
    }


    public class shortOrderFillTransaction
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
        public FullPrice fullPrice { get; set; }
        public homeConversionFactors homeConversionFactors { get; set; }
    }

    public class root
    {
        public shortOrderCreateTransaction shortOrderCreateTransaction { get; set; }
        public shortOrderFillTransaction shortOrderFillTransaction { get; set; }
        public List<string> relatedTransactionIDs { get; set; }
        public string lastTransactionID { get; set; }

        internal ClosePositionsResponse GetClosePositionResponse(string responseBody)
        {
            ClosePositionsResponse result = new ClosePositionsResponse
            {
                TradeIds = string.Join(",", this.shortOrderFillTransaction.tradesClosed.Select(t => t.tradeID)),
                Time = this.shortOrderFillTransaction.time,
                Units = decimal.Parse(this.shortOrderFillTransaction.units),
                PL = decimal.Parse(this.shortOrderFillTransaction.pl),
                AccountBalance = decimal.Parse(this.shortOrderFillTransaction.accountBalance),
                Reason = this.shortOrderFillTransaction.reason,
                Commission = decimal.Parse(this.shortOrderFillTransaction.commission),
                HalfSpreadCost = decimal.Parse(this.shortOrderFillTransaction.halfSpreadCost),
                Financing = decimal.Parse(this.shortOrderFillTransaction.financing),
                RelatedTransactionIDs = string.Join(",", this.relatedTransactionIDs),
                RequestedUnits = decimal.Parse(this.shortOrderFillTransaction.requestedUnits),
                Symbol = this.shortOrderCreateTransaction.instrument,
                PriceTran = decimal.Parse(this.shortOrderFillTransaction.price),
                FullVWAP = decimal.Parse(this.shortOrderFillTransaction.fullVWAP),
                CloseoutBid = decimal.Parse(this.shortOrderFillTransaction.fullPrice.closeoutBid),
                CloseoutAsk = decimal.Parse(this.shortOrderFillTransaction.fullPrice.closeoutAsk),
                GainQuoteHomeConversionFactor = decimal.Parse(this.shortOrderFillTransaction.gainQuoteHomeConversionFactor),
                LossQuoteHomeConversionFactor = decimal.Parse(this.shortOrderFillTransaction.lossQuoteHomeConversionFactor),
                FullResponseBody = responseBody,
            };

            return result;
        }
    }

}
