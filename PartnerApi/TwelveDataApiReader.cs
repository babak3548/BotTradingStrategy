using Microsoft.EntityFrameworkCore.Metadata;
using PreprocessDataStocks.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PreprocessDataStocks.PartnerApi
{
    public class TwelveDataApiReader : BaseDataReader, ICandleDataReader
    {
        private static readonly TwelveDataApiReader instance = new TwelveDataApiReader();
        private readonly string _apiKey = "yourApiKey";


        private readonly int outputsize = 5000;
        private readonly HttpClient _httpClient;

        private DateTime LastCallAPIForTick;
        private readonly short PeriodTick =10;
        public static TwelveDataApiReader Instance => instance;
        private TwelveDataApiReader()
        {
            _httpClient = new HttpClient();
        }

        public List<Candle> ReadLastCandles(int symbolId)
        {
            if (LastCallAPIForTick.AddSeconds(PeriodTick) > DateTime.UtcNow)
            { return new List<Candle>(); }

            LastCallAPIForTick = DateTime.UtcNow;

                var symbol = ConfigBot.GetSymbolConfig(symbolId);
            if (symbol.CandlesAddedToDB) return new List<Candle>();

           
            var startDate = symbol.LastCandleReaded; // DateTime.UtcNow.AddDays(-(outputsize - 1)).ToString("yyyy-MM-dd");
            var endDate = DateTime.UtcNow.ToString("yyyy-MM-dd"); // "2024-06-01"; &exchange={"Binance"}&timezone={"Asia/Singapore"}

            var url = $"https://api.twelvedata.com/time_series?symbol={symbol.Name}&interval={symbol.Period}&outputsize={outputsize}&apikey={_apiKey}&source=docs&start_date={startDate}&end_date={endDate}";

            HttpResponseMessage response = _httpClient.GetAsync(url).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonSerializer.Deserialize<TwelveDataApiResponse>(content); // Deserialize to TwelveDataApiResponse class

                if (result.status == "ok")
                {
                    var cdls = result.values.Select(v => ConvertToCandle(v, symbolId)).OrderBy(c=>c.Datetime).ToList();
                    _candles.AddRange(cdls);
                    symbol.CandlesAddedToDB = true;
                    return cdls;//important attention candles must be order by date small date to big date
                }
            }
            throw new Exception($"raed candel from symbol {symbolId} faild"); // Empty list on error or no data
        }
        private Candle ConvertToCandle(TwelveDataValue value, int symbolId)
        {
            return new Candle
            {
                Datetime = value.datetime.Length>11? DateTime.ParseExact(value.datetime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) :
                                                     DateTime.ParseExact(value.datetime, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Open = Convert.ToDecimal(value.open, CultureInfo.InvariantCulture),
                High = Convert.ToDecimal(value.high, CultureInfo.InvariantCulture),
                Low = Convert.ToDecimal(value.low, CultureInfo.InvariantCulture),
                Close = Convert.ToDecimal(value.close, CultureInfo.InvariantCulture),
                SymbolId = symbolId,
            };
        }
    }

    public class TwelveDataApiResponse // Define a class to match the JSON response structure
    {
        public Meta meta { get; set; }
        public List<TwelveDataValue> values { get; set; }
        public string status { get; set; }
    }

    public class TwelveDataValue // Define a class to represent a single value object
    {
        public string datetime { get; set; }
        public string open { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string close { get; set; }
    }
    public class Meta
    {
        public string Symbol { get; set; }
        public string Interval { get; set; }
        public string CurrencyBase { get; set; }
        public string CurrencyQuote { get; set; }
        public string Type { get; set; }
    }

}
