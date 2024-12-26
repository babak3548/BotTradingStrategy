using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using PreprocessDataStocks.PartnerApi.CommonModels;

namespace PreprocessDataStocks.PartnerApi.OandaModels.TradeCloseTransaction
{
        public class TradesClosed
    {
        public string TradeID { get; set; }
        public string ClientTradeID { get; set; }
        public string Units { get; set; }
        public string RealizedPL { get; set; }
        public string Financing { get; set; }
        public string BaseFinancing { get; set; }
        public string Price { get; set; }
        public string GuaranteedExecutionFee { get; set; }
        public string QuoteGuaranteedExecutionFee { get; set; }
        public string HalfSpreadCost { get; set; }
        public string PlHomeConversionCost { get; set; }
        public string BaseFinancingHomeConversionCost { get; set; }
        public string GuaranteedExecutionFeeHomeConversionCost { get; set; }
        public string HomeConversionCost { get; set; }
    }

    public class Bid
    {
        public string Price { get; set; }
        public string Liquidity { get; set; }
    }

    public class Ask
    {
        public string Price { get; set; }
        public string Liquidity { get; set; }
    }

    public class FullPrice
    {
        public string CloseoutBid { get; set; }
        public string CloseoutAsk { get; set; }
        public string Timestamp { get; set; }
        public List<Bid> Bids { get; set; }
        public List<Ask> Asks { get; set; }
    }

    public class GainQuoteHome
    {
        public string Factor { get; set; }
    }

    public class LossQuoteHome
    {
        public string Factor { get; set; }
    }

    public class GainBaseHome
    {
        public string Factor { get; set; }
    }

    public class LossBaseHome
    {
        public string Factor { get; set; }
    }

    public class HomeConversionFactors
    {
        public GainQuoteHome GainQuoteHome { get; set; }
        public LossQuoteHome LossQuoteHome { get; set; }
        public GainBaseHome GainBaseHome { get; set; }
        public LossBaseHome LossBaseHome { get; set; }
    }

    public class Transaction
    {
        public string Id { get; set; }
        public string AccountID { get; set; }
        public int UserID { get; set; }
        public string BatchID { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
        public string OrderID { get; set; }
        public string Instrument { get; set; }
        public string Units { get; set; }
        public string RequestedUnits { get; set; }
        public string Price { get; set; }
        public string Pl { get; set; }
        public string QuotePL { get; set; }
        public string Financing { get; set; }
        public string BaseFinancing { get; set; }
        public string Commission { get; set; }
        public string AccountBalance { get; set; }
        public string GainQuoteHomeConversionFactor { get; set; }
        public string LossQuoteHomeConversionFactor { get; set; }
        public string GuaranteedExecutionFee { get; set; }
        public string QuoteGuaranteedExecutionFee { get; set; }
        public string HalfSpreadCost { get; set; }
        public string FullVWAP { get; set; }
        public string Reason { get; set; }
        public List<TradesClosed> TradesClosed { get; set; }
        public FullPrice FullPrice { get; set; }
        public HomeConversionFactors HomeConversionFactors { get; set; }
        public string PlHomeConversionCost { get; set; }
        public string BaseFinancingHomeConversionCost { get; set; }
        public string GuaranteedExecutionFeeHomeConversionCost { get; set; }
        public string HomeConversionCost { get; set; }
    }

    public class OandaTransactionResponse
    {
        public Transaction Transaction { get; set; }
        public string LastTransactionID { get; set; }

        internal ClosePositionsResponse FillClosePositionsResponse(string jsonString)
        {
            ClosePositionsResponse closePositionsResponse = new ClosePositionsResponse
            {
                TradeIds = string.Join(",", this.Transaction.TradesClosed.Select(tc => tc.TradeID)),
                CloseoutAsk = decimal.Parse(this.Transaction.FullPrice.CloseoutAsk),
                GainQuoteHomeConversionFactor = decimal.Parse(this.Transaction.GainQuoteHomeConversionFactor),
                LossQuoteHomeConversionFactor = decimal.Parse(this.Transaction.LossQuoteHomeConversionFactor),
                FullResponseBody = jsonString,
                CloseoutBid = decimal.Parse(this.Transaction.FullPrice.CloseoutBid),
                FullVWAP = decimal.Parse(this.Transaction.FullVWAP),
                PriceTran = decimal.Parse(this.Transaction.Price),
                Symbol = this.Transaction.Instrument,
                RequestedUnits = decimal.Parse(this.Transaction.RequestedUnits),
                HalfSpreadCost = decimal.Parse(this.Transaction.HalfSpreadCost),
                Financing = decimal.Parse(this.Transaction.Financing),
                RelatedTransactionIDs = string.Join(",", this.Transaction.TradesClosed.Select(tc => tc.TradeID)),
                Commission = decimal.Parse(this.Transaction.Commission),
                Reason = this.Transaction.Reason,
                AccountBalance = decimal.Parse(this.Transaction.AccountBalance),
                PL = decimal.Parse(this.Transaction.Pl),
                Units = decimal.Parse(this.Transaction.Units),
                Time = DateTime.Parse(this.Transaction.Time)
            };

            return closePositionsResponse;
        }
    }

}
