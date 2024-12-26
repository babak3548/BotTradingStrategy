using Microsoft.EntityFrameworkCore.Metadata;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi.OandaModels.CandlesInfo;
using PreprocessDataStocks.PartnerApi.OandaModels.TiksPrice;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using CandlesInfo = PreprocessDataStocks.PartnerApi.OandaModels.CandlesInfo;
using TiksPrice = PreprocessDataStocks.PartnerApi.OandaModels.TiksPrice;

namespace PreprocessDataStocks.PartnerApi
{
    //Attention Oanda API work with UTC datetime
    public class OandaDataApiReader : BaseDataReader, ICandleDataReader
    {
        private static readonly OandaDataApiReader instance = new OandaDataApiReader();
        private string _endpointUrl;
        private string _endpoint { get { return _endpointUrl; } }
        private string _authToken;
        private string _accountId;

        private readonly int outputsize = 5000;
        private readonly HttpClient _httpClient;
        private DateTime _stopRegOrderUntil = DateTime.MinValue;
        private DateTime LastCallAPIForTick { get; set; } = DateTime.MinValue;
        private DateTime LastCallAPIForCandle { get; set; } = DateTime.MinValue;
        private readonly double PeriodTick = 1;
        int periodCallApiInSeconds = 40;
        public static OandaDataApiReader Instance => instance;
        private OandaDataApiReader() : base()
        {
            _httpClient = new HttpClient();
            _endpointUrl = ConfigBot.OandaEndpoint;
            _authToken = ConfigBot.OandaAuthToken;
            _accountId = ConfigBot.OandaAccountId;
        }

        public List<Candle> ReadLastCandles(int symbolId)
        {
            if (_stopRegOrderUntil > DateTime.Now) return new List<Candle>();

           

            var symbol = ConfigBot.GetSymbolConfig(symbolId);

            double periodHourse = symbol.Period.GetPeriodInHours();
            

          //  DateTime startDate = symbol.LastCandleReaded.AddHours(periodHourse); //  "2024-04-01"; // DateTime.UtcNow.AddDays(-(outputsize - 1)).ToString("yyyy-MM-dd");
            if (!CommonTools.SecondsIsPassed("OandaCndCall"+symbolId, periodCallApiInSeconds)) return new List<Candle>();

            // Define the URL with parameters
            string url = _endpoint + $"instruments/{symbol.Name}/candles?count={outputsize}&price=M&from={symbol.LastCandleReaded.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ")}&granularity={symbol.Period.GranularityOanda()}";

            // Create HttpClient
            HttpClient httpClient = new HttpClient();

            // Set headers
            httpClient.DefaultRequestHeaders.Add("Authorization", _authToken);
            // httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            // Make GET request
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            LastCallAPIForCandle = DateTime.UtcNow;

            // Check if request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read response content
                string responseBody = response.Content.ReadAsStringAsync().Result;
                CandlesInfo.rootObject root = JsonSerializer.Deserialize<CandlesInfo.rootObject>(responseBody);
                var cdls = root.candles.Select(v => ConvertToCandle(v, symbolId)).OrderBy(c => c.Datetime).ToList();//.Where(c => c.complete)
                if (cdls.Any())
                {
                    _candles.AddRange(cdls);
                    symbol.LastCandleReaded = cdls.Last().Datetime.Value;
                }
                else { _stopRegOrderUntil = DateTime.Now.AddMinutes(1); }

                return cdls;//important attention: candles must be order by date small date to big date
            }
            else
            {
                Console.WriteLine($"write on error logs symbol {symbolId} -- Failed to make request. Status code: {response.StatusCode}"); // Empty list on error or no data
                return new List<Candle>();
            }

        }

        public override Tick ReadLastTick(int symbolId, PreprocessDataStockContext _preprocessDataStockContex)
        {
            //todo check timeing  call API
            var symbol = ConfigBot.GetSymbolConfig(symbolId);

            if (LastCallAPIForTick.AddSeconds(PeriodTick) < DateTime.UtcNow)
            {
                try
                {
                    string responseBody = GetOandaPricing(symbol.Exchange).Result;
                    TiksPrice.rootObject root = JsonSerializer.Deserialize<TiksPrice.rootObject>(responseBody);

                    var currentExchangeSym = ConfigBot.Symbols.Where(s => s.Exchange == symbol.Exchange);
                    foreach (var price in root.prices)
                    {
                        var sym = ConfigBot.Symbols.First(s => s.Exchange == symbol.Exchange && s.Name == price.instrument);
                        double periodHourse = sym.Period.GetPeriodInHours();
                        if (!dTickQueue.ContainsKey(sym.Id)) { dTickQueue.Add(sym.Id, new Queue<Tick>()); }
                        decimal bidAve = price.bids.Count == 0 ? 0 : price.bids.Average(a => decimal.Parse(a.price));
                        int bidLiquiditySum = price.bids.Count == 0 ? 0 : price.bids.Sum(a => a.liquidity);
                        decimal askAve = price.asks.Count == 0 ? 0 : price.asks.Average(a => decimal.Parse(a.price));
                        int askLiquiditySum = price.asks.Count == 0 ? 0 : price.asks.Sum(a => a.liquidity);
                        decimal HomeConversion = (decimal.Parse(price.quoteHomeConversionFactors.positiveUnits) + decimal.Parse(price.quoteHomeConversionFactors.positiveUnits)) / 2;
                        //  decimal priceCurrency = bidAskAve * HomeConversion;
                        var tick = new Tick
                        {
                            Ask = Math.Round(askAve, 5),
                            AskLiquiditySum = askLiquiditySum,
                            HomeConversion = HomeConversion,
                            Bid = Math.Round(bidAve, 5),
                            BidLiquiditySum = bidLiquiditySum,
                            TickDatetime = price.time,
                            CandleDatetime = price.time.GetLastCandelDateTime(periodHourse, sym.StartCandle),
                            //(DateTime.UtcNow - sym.LastCandleReaded).TotalHours < 2 * periodHourse ? sym.LastCandleReaded : DateTime.UtcNow,
                            Period = (decimal)PeriodTick,
                            SymbolId = sym.Id,
                            Tradeable = price.tradeable
                        };

                        dTickQueue[sym.Id].Enqueue(tick);
                    }
                    LastCallAPIForTick = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error leve3 Read Tick:", ex.Message);
                }
            }

            if (dTickQueue.ContainsKey(symbolId) && dTickQueue[symbolId].Any(t => t.SymbolId == symbolId))
            {
                return dTickQueue[symbolId].Dequeue();
            }
            else
            {
                return null;
            }
        }

        private async Task<string> GetOandaPricing(string exchange)
        {
            string instruments = string.Join(",", ConfigBot.Symbols.Where(s => s.Exchange == exchange).Select(i => i.Name));
            var endpoint = $"pricing?instruments={instruments}";
            string url = $"{_endpoint}accounts/{_accountId}/{endpoint}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _authToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    throw new Exception($"write on error logs symbol {instruments} -- Failed to make request. Status code: {response.StatusCode}"); // Empty list on error or no data
                }
            }
        }

        private Candle ConvertToCandle(CandlesInfo.candle candle, int symbolId)
        {
            return new Candle
            {
                Datetime = candle.time,
                Open = Convert.ToDecimal(candle.mid.o, CultureInfo.InvariantCulture),
                High = Convert.ToDecimal(candle.mid.h, CultureInfo.InvariantCulture),
                Low = Convert.ToDecimal(candle.mid.l, CultureInfo.InvariantCulture),
                Close = Convert.ToDecimal(candle.mid.c, CultureInfo.InvariantCulture),
                SymbolId = symbolId,
            };
        }

    }



}
