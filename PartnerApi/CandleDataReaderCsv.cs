using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PreprocessDataStocks.PartnerApi
{
    // Singleton pattern implemented
    public class CandleTickDataReaderCsv : BaseDataReader, ICandleDataReader
    {
        private static readonly CandleTickDataReaderCsv instance = new CandleTickDataReaderCsv();

        private List<Candle> _candles = new List<Candle>();
        private Queue<Tick> tickQueue = new Queue<Tick>();
      
        // Private constructor to prevent instantiation
        private CandleTickDataReaderCsv() { }

        public static CandleTickDataReaderCsv Instance => instance;

        public List<Candle> ReadLastCandles(int symbolId)
        {
            var symbol = ConfigBot.GetSymbolConfig(symbolId);
            if (symbol.CandlesAddedToDB) { return new List<Candle>(); }

            var lines = File.ReadAllLines(ConfigBot.CandleCsvFilePath).Skip(1);
            var cdls = lines.Select(line => line.Split(','))
                               .Select(parts => new Candle
                               {
                                   Datetime = DateTime.Parse(parts[0]),
                                   Open = decimal.Parse(parts[1]),
                                   High = decimal.Parse(parts[2]),
                                   Low = decimal.Parse(parts[3]),
                                   Close = decimal.Parse(parts[4]),
                                   SymbolId = symbolId
                               })
                               .OrderBy(c => c.Datetime).ToList();  //important attention candles must be order by date small date to big date

            _candles.AddRange(cdls);
            symbol.CandlesAddedToDB = true;
            return cdls;//important attention candles must be order by date small date to big date
            
        }






    }
}
