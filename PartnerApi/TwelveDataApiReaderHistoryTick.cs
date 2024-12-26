using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;
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
    public class TwelveDataApiReaderHistoryTick : BaseDataReader, ICandleDataReader
    {
        private static readonly TwelveDataApiReaderHistoryTick instance = new TwelveDataApiReaderHistoryTick();
        private readonly string _apiKey = "yourapikey";


        private readonly int outputsize = 5000;
        private readonly HttpClient _httpClient;

        //private DateTime LastTickTime = DateTime.MinValue;
        private readonly short PeriodTickMin = 5;
        public static TwelveDataApiReaderHistoryTick Instance => instance;
        private TwelveDataApiReaderHistoryTick()
        {
            _httpClient = new HttpClient();
          
        }

        public List<Candle> ReadLastCandles(int symbolId)
        {
            if (CommonTools.SecondsIsPassed($"ReadLastCandles{symbolId}", 10))
            { return new List<Candle>(); }

            //  LastCallAPIForTick = DateTime.UtcNow;

            var symbol = ConfigBot.GetSymbolConfig(symbolId);
            if (symbol.CandlesAddedToDB) return new List<Candle>();


            var startDate = symbol.LastCandleReaded.ToString("yyyy-MM-dd"); // DateTime.UtcNow.AddDays(-(outputsize - 1)).ToString("yyyy-MM-dd");
            var endDate = DateTime.UtcNow.ToString("yyyy-MM-dd"); // "2024-06-01"; &exchange={"Binance"}&timezone={"Asia/Singapore"}

            var url = $"https://api.twelvedata.com/time_series?symbol={symbol.Name}&interval={symbol.Period}&outputsize={outputsize}&apikey={_apiKey}&source=docs&start_date={startDate}&end_date={endDate}";

            HttpResponseMessage response = _httpClient.GetAsync(url).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonSerializer.Deserialize<TwelveDataApiResponse>(content); // Deserialize to TwelveDataApiResponse class

                if (result.status == "ok")
                {
                    var cdls = result.values.Select(v => ConvertToCandle(v, symbolId)).OrderBy(c => c.Datetime).ToList();
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
                Datetime = value.datetime.Length > 11 ? DateTime.ParseExact(value.datetime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) :
                                                     DateTime.ParseExact(value.datetime, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Open = Convert.ToDecimal(value.open, CultureInfo.InvariantCulture),
                High = Convert.ToDecimal(value.high, CultureInfo.InvariantCulture),
                Low = Convert.ToDecimal(value.low, CultureInfo.InvariantCulture),
                Close = Convert.ToDecimal(value.close, CultureInfo.InvariantCulture),
                SymbolId = symbolId,
            };
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        public override Tick ReadLastTick(int symbolId, PreprocessDataStockContext _preprocessDataStockContex)
        {
           
            var sym = ConfigBot.GetSymbolConfig(symbolId);

            if (CommonTools.SecondsIsPassed($"ReadLastTickHistoryTwelve{symbolId}", 20) && sym.LastTickReaded.AddPeriods(PeriodTickMin, 3) <= DateTime.UtcNow)
            {
                try
                {
                   // TickDB tickDB = _preprocessDataStockContex.TickDBs.Where(t=>t.SymbolId == symbolId).OrderBy(t=>t.TickDBDatetime).LastOrDefault();
                    var endTime = sym.LastTickReaded.AddPeriods(PeriodTickMin, outputsize + 1000);
                    endTime = endTime <= DateTime.UtcNow ? endTime : DateTime.UtcNow;
                    if (!dTickQueue.ContainsKey(sym.Id)) { dTickQueue.Add(sym.Id, new Queue<Tick>()); }
                    // load from chache DB
                    if (_preprocessDataStockContex.TickDBs.Any(t => t.TickDBDatetime > sym.LastTickReaded && t.SymbolId== symbolId))
                    {
                        var TickDbs = _preprocessDataStockContex.TickDBs.Where(t => t.TickDBDatetime > sym.LastTickReaded && t.SymbolId == sym.Id).ToList();
                        foreach (var tickDb in TickDbs)
                        {
                            var tick = CreateTick(tickDb, sym);
                            dTickQueue[sym.Id].Enqueue(tick);
                            sym.LastTickReaded = tick.TickDatetime;
                        }
                    }
                    else
                    {
                        List<Candle> listCandle = FetchCandels(sym.Id, sym.Name, sym.LastTickReaded, endTime);
                        listCandle = listCandle.OrderBy(c => c.Datetime).ToList();
                        decimal halfSpread = Math.Round(sym.AverageSpread / 2, 6);
                        double periodHourse = sym.Period.GetPeriodInHours();
                        sym.LastTickReaded = listCandle.Last().Datetime.Value;
                        foreach (var tCandle in listCandle)
                        {
                            var (ticks, step) = SplitLowHighCandles(tCandle.Low, tCandle.High, tCandle.Open, tCandle.Close, 2);
                            foreach (var tickPrice in ticks)
                            {
                                var tick = CreateTick(sym, halfSpread, periodHourse, tCandle, tickPrice, step);
                                dTickQueue[sym.Id].Enqueue(tick);
                                var newTickDB = CreateTickDB(tick);
                                _preprocessDataStockContex.TickDBs.Add(newTickDB);
                            }
                        }
                        _preprocessDataStockContex.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error leve3 Read Tick:", ex.Message);
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
        internal override (List<decimal>, decimal) SplitLowHighCandles(decimal low, decimal high, decimal open, decimal close, int piecesCount)
        {
            var step = (high - low) / 2;
            var l = new List<decimal>();

            l.Add(close);
            return (l, step);
        }

        private List<Candle> FetchCandels(int symbolId, string symbolName, DateTime startDate, DateTime endDate)
        {
          //  string tickPeriod = "5min";//1min, 15min , 1h 
            var url = $"https://api.twelvedata.com/time_series?symbol={symbolName}&interval={PeriodTickMin}min&outputsize={outputsize}&apikey=" +
                $"{_apiKey}&source=docs&start_date={startDate.ToString("yyyy-MM-dd HH:mm:ss")}&end_date={endDate.ToString("yyyy-MM-dd HH:mm:ss")}";

            HttpResponseMessage response = _httpClient.GetAsync(url).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonSerializer.Deserialize<TwelveDataApiResponse>(content); // Deserialize to TwelveDataApiResponse class

                if (result.status == "ok")
                {
                    var cdls = result.values.Select(v => ConvertToCandle(v, symbolId)).OrderBy(c => c.Datetime).ToList();
                    _candles.AddRange(cdls);
                    //symbol.CandlesAddedToDB = true;
                    return cdls;//important attention candles must be order by date small date to big date
                }
            }
            throw new Exception($"raed candel from symbol {symbolId} faild"); // Empty list on error or no data  dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }



        ////////////////////////////////////////////////////////////////////////////////////////////


    }



}
