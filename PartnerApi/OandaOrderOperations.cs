using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreateOrder = PreprocessDataStocks.PartnerApi.OandaModels.CreateOrderModels;
using LongTran = PreprocessDataStocks.PartnerApi.OandaModels.LongTransaction;
using ShorTran = PreprocessDataStocks.PartnerApi.OandaModels.ShortTransaction;
using AccPos = PreprocessDataStocks.PartnerApi.OandaModels.AccountPosition;
using PreprocessDataStocks.PartnerApi.CommonModels;
using System.Text.Json;
using PreprocessDataStocks.PartnerApi.OandaModels.TiksPrice;

using OrderRegRes = PreprocessDataStocks.PartnerApi.OandaModels.CreateOrderResponse;

using Newtonsoft.Json;
using PreprocessDataStocks.PartnerApi.OandaModels.GetTradeInfo;
using System.Net.Http.Headers;
using PreprocessDataStocks.PartnerApi.OandaModels.OandaAccountDetails;
using System.Net.Http;
using JsonSerializer = System.Text.Json.JsonSerializer;
using PreprocessDataStocks.PartnerApi.OandaModels.TradeCloseTransaction;
using System.Text.Json.Nodes;
using PreprocessDataStocks.CandleConsumerModule.TickConsumerModule;

namespace PreprocessDataStocks.PartnerApi
{
    public class OandaOrderOperations : IOrderOperations
    {
        private static readonly OandaOrderOperations instance = new OandaOrderOperations();
        private string _endpointUrl;
        private string _endpoint { get { return _endpointUrl; } }
        private DateTime _stopRegOrderUntil = DateTime.MinValue;

        // Define the authorization token
        private string _authToken;

        public static OandaOrderOperations Instance => instance;
        private OandaOrderOperations()
        {
            _endpointUrl = ConfigBot.OandaEndpoint + $"accounts/{ConfigBot.OandaAccountId}/";
            _authToken = ConfigBot.OandaAuthToken;
        }

        public async Task<ClosePositionsResponse> ClosePositions(string symbol, Position position)
        {
            string jsonBody;
            string url;
            if (position == Position.Long)
            {
                jsonBody = @"{
            ""longUnits"": ""ALL""
              }";
            }
            else
            {
                jsonBody = @"{
            ""shortUnits"": ""ALL""
              }";
            }

            // Define the API endpoint
            url = _endpoint + $"positions/{symbol}/close";

            // Create a HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Create a HttpRequestMessage
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);

                // Add headers
                request.Headers.Add("Authorization", _authToken);
                // request.Headers.Add("Content-Type", "application/json");

                // Add JSON body
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Send the request
                HttpResponseMessage response = await client.SendAsync(request);

                // Check if the request was successful

                if (response.IsSuccessStatusCode)
                {
                    // Read and display the response
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON string to RootObject
                    if (position == Position.Long)
                    {
                        if (responseBody.Contains("MARKET_HALTED")) new ClosePositionsResponse { marketStatus = LiveOperationStatus.MarketClose };
                        LongTran.root rootObjectLo = JsonSerializer.Deserialize<LongTran.root>(responseBody);
                        ClosePositionsResponse result = rootObjectLo.GetClosePositionResponse(responseBody);
                        return result;
                    }
                    else
                    {
                        if (responseBody.Contains("MARKET_HALTED")) new ClosePositionsResponse { marketStatus = LiveOperationStatus.MarketClose };
                        ShorTran.root rootObjectSh = JsonSerializer.Deserialize<ShorTran.root>(responseBody);
                        ClosePositionsResponse result = rootObjectSh.GetClosePositionResponse(responseBody);
                        return result;
                    }
                }
                else
                {
                    // Display the error message
                    Console.WriteLine("Errorr: " + response.StatusCode + "response: " + response);
                    return new ClosePositionsResponse { marketStatus = LiveOperationStatus.DoNotSuccess };
                }
            }
        }

        public async Task<OpenPositions> GetOpenPositions(string symbol)
        {
            // Create HttpClient
            HttpClient httpClient = new HttpClient();

            // Set headers
            httpClient.DefaultRequestHeaders.Add("Authorization", _authToken);

            // Make GET request
            string url = _endpoint + $"positions/{symbol}";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // Check if request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read response content
                string responseBody = await response.Content.ReadAsStringAsync();
                // Deserialize JSON string to RootObject
                AccPos.rootObject rootObject = JsonSerializer.Deserialize<AccPos.rootObject>(responseBody);
                OpenPositions openPositions = rootObject.FillDataPosition();
                return openPositions;
            }
            else
            {
                Console.WriteLine($"Failed to make request. Status code: {response.StatusCode}");
                return null;
            }


        }

        public async Task<CreatedOrderResponse> CreateOrder(string symbol, decimal lotSize, OrderTypes orderTypes, decimal takeProfit, decimal stopLoss)
        {
            if (_stopRegOrderUntil > DateTime.UtcNow) return null;

            int unit = (orderTypes == OrderTypes.buy ? 1 : -1) * (int)(lotSize * 100000);
            string jsonBody = new CreateOrder.rootObject(unit, symbol, takeProfit, stopLoss).CreateOrderJson();

            // Define the API endpoint

            string url = _endpoint + "orders";

            // Create a HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Create a HttpRequestMessage
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

                // Add headers
                request.Headers.Add("Authorization", _authToken);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Send the request
                HttpResponseMessage response = await client.SendAsync(request);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and display the response
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (responseBody.Contains("MARKET_HALTED"))
                    {
                        _stopRegOrderUntil = DateTime.UtcNow.AddMinutes(2);
                        return new CreatedOrderResponse { MarketStatus = LiveOperationStatus.MarketClose };
                    }
                    // Deserialize JSON string to RootObject
                    OrderRegRes.root rootObject = JsonSerializer.Deserialize<OrderRegRes.root>(responseBody);
                    CreatedOrderResponse createdOrderResponse = rootObject.FillRegOrderResp(responseBody);
                    return createdOrderResponse;

                }
                else
                {
                    // Display the error message
                    Console.WriteLine("bad Error Reg Order: " + JsonSerializer.Serialize(response));
                    return null;
                }

            }
        }

        public async Task<ClosePositionsResponse> GetClosedTradeInfo(string tradeId)
        {
            ClosePositionsResponse closePositionsResponse;

            var tradeInfo = getTradeInfo(tradeId).Result;

            //if (tradeInfo.Trade.ClosingTransactionIDs.Count > 1)
            //{
            //    Console.WriteLine($"error not implemented: trade:{tradeId} include many Closing Transaction IDs ");
            //    closePositionsResponse = new ClosePositionsResponse();
            //    closePositionsResponse.marketStatus = LiveOperationStatus.DoNotSuccess;
            //}
            if (tradeInfo.Trade.State == "CLOSED")
            {
                var transactionInfo = getTransactionInfoClosedTrade(tradeInfo.Trade.ClosingTransactionIDs.First()).Result;
                closePositionsResponse = transactionInfo.OandaTransactionResponse.FillClosePositionsResponse(transactionInfo.JsonBody);
            }
            else
            {
                closePositionsResponse = new ClosePositionsResponse();
                closePositionsResponse.marketStatus = LiveOperationStatus.DoNotSuccess;
                LogManager.WriteErrorLog("Trade is not closed", 1, "CloseOrder");
            }
            return closePositionsResponse;
        }

        private async Task<OandaTradeResponse> getTradeInfo(string tradeId)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set headers
                httpClient.DefaultRequestHeaders.Add("Authorization", _authToken);

                // Make GET request
                string url = _endpoint + $"trades/{tradeId}";
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Deserialize JSON string to RootObject
                    //AccPos.rootObject rootObject = JsonSerializer.Deserialize<AccPos.rootObject>(responseBody);
                    OandaTradeResponse tradeResponse = JsonConvert.DeserializeObject<OandaTradeResponse>(responseBody);

                    return tradeResponse;
                }
                else
                {
                    Console.WriteLine($"Failed to make request. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }

        private async Task<OandaTransactionResponseCloseTrade> getTransactionInfoClosedTrade(string closedTradeTransactionId)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Set headers
                httpClient.DefaultRequestHeaders.Add("Authorization", _authToken);

                // Make GET request
                string url = _endpoint + $"transactions/{closedTradeTransactionId}";
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Deserialize JSON string to RootObject
                    OandaTransactionResponse transactionResponse = JsonConvert.DeserializeObject<OandaTransactionResponse>(responseBody);
                    return new OandaTransactionResponseCloseTrade
                    {
                        OandaTransactionResponse = transactionResponse,
                        JsonBody = responseBody
                    };
                    
                }
                else
                {
                    Console.WriteLine($"Failed to make request in getTransactionInfoClosedTrade. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
    public class OandaTransactionResponseCloseTrade
    {
        public OandaTransactionResponse OandaTransactionResponse { get; set; }
        public string JsonBody { get; set; }

    }





}
