using Microsoft.VisualBasic;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.OandaModels.CreateOrderResponse
{
    public class takeProfitOnFill
    {
        public string price { get; set; }
        public string timeInForce { get; set; }
    }

    public class stopLossOnFill
    {
        public string price { get; set; }
        public string timeInForce { get; set; }
        public string triggerMode { get; set; }
    }

    public class orderCreateTransaction
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
        public takeProfitOnFill takeProfitOnFill { get; set; }
        public stopLossOnFill stopLossOnFill { get; set; }
        public string reason { get; set; }
    }

    public class tradeOpened
    {
        public string price { get; set; }
        public string tradeID { get; set; }
        public string units { get; set; }
        public string guaranteedExecutionFee { get; set; }
        public string quoteGuaranteedExecutionFee { get; set; }
        public string halfSpreadCost { get; set; }
        public string initialMarginRequired { get; set; }
    }

    public class Bids
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
        public List<Bids> bids { get; set; }
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

    public class orderFillTransaction
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
        public tradeOpened tradeOpened { get; set; }
        public fullPrice fullPrice { get; set; }
    }

    public class root
    {
        public orderCreateTransaction orderCreateTransaction { get; set; }
        public orderFillTransaction orderFillTransaction { get; set; }
        public List<string> relatedTransactionIDs { get; set; }
        public string lastTransactionID { get; set; }

        public CreatedOrderResponse FillRegOrderResp(string responseBody)
        {
            CreatedOrderResponse res;
            if (this.orderFillTransaction!= null && this.orderFillTransaction.tradeOpened != null)
            {
                 res = new CreatedOrderResponse
                {
                    MarketStatus = LiveOperationStatus.Success,

                    TradeIds = this.orderFillTransaction.tradeOpened.tradeID,
                    Time = this.orderFillTransaction.time,
                    Units = decimal.Parse(this.orderFillTransaction.units),
                    PL = decimal.Parse(this.orderFillTransaction.pl),
                    AccountBalance = decimal.Parse(this.orderFillTransaction.accountBalance),
                    Reason = this.orderFillTransaction.reason,
                    Commission = decimal.Parse(this.orderFillTransaction.commission),
                    HalfSpreadCost = decimal.Parse(this.orderFillTransaction.halfSpreadCost),
                    Financing = decimal.Parse(this.orderFillTransaction.financing),
                    RelatedTransactionIDs = string.Join(",", this.relatedTransactionIDs),
                    PriceTran = decimal.Parse(this.orderFillTransaction.price),
                    CloseoutBid = decimal.Parse(this.orderFillTransaction.fullPrice.closeoutBid),
                    CloseoutAsk = decimal.Parse(this.orderFillTransaction.fullPrice.closeoutAsk),
                    GainQuoteHomeConversionFactor = decimal.Parse(this.orderFillTransaction.gainQuoteHomeConversionFactor),
                    LossQuoteHomeConversionFactor = decimal.Parse(this.orderFillTransaction.lossQuoteHomeConversionFactor),
                    InitialMarginRequired = decimal.Parse(this.orderFillTransaction.tradeOpened.initialMarginRequired),
                    FullResponseBody = responseBody,
                };
            }
            else
            {
                Console.WriteLine($"Error leve1--Fiall craeted Order-- responseBody: {responseBody}");
                res = new CreatedOrderResponse
                {
                    MarketStatus = LiveOperationStatus.DoNotSuccess,
                    FullResponseBody = responseBody,
                };
            }



            return res;
        }
    }
}
