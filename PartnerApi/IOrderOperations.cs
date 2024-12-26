using PreprocessDataStocks.CandleConsumerModule.TickConsumerModule;
using PreprocessDataStocks.PartnerApi.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi
{
    public interface IOrderOperations
    {
        public  Task<ClosePositionsResponse> ClosePositions(string symbol, Position position);

        public  Task<OpenPositions> GetOpenPositions(string symbol);

        public  Task<CreatedOrderResponse> CreateOrder(string symbol, decimal lotSize, OrderTypes orderTypes, decimal takeProfit, decimal stopLoss);
        public Task<ClosePositionsResponse> GetClosedTradeInfo(string tradeId);

    }
}
