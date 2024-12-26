using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using PreprocessDataStocks.CandleQueueManagerModule;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi;


namespace PreprocessDataStocks.CandleProducerModule
{
    public class CandleProducer
    {

        private long _totalExecutionTime = 0;
        
        private readonly CandleQueueManager _queueManager;
        private ICandleDataReader _dataReader;
        DataReaderFactory _dataReaderFactory;
        Symbol symbol;
        public CandleProducer(CandleQueueManager queueManager)
        {
            _queueManager = queueManager ?? throw new ArgumentNullException(nameof(queueManager));
            _dataReaderFactory = new DataReaderFactory();
            //_dataReader = dataReader ?? throw new ArgumentNullException(nameof(dataReader));
        }

        public void Produce()
        {
            string RecentCandleDateTime ="";
            int idelCounter = 0;
            while (true)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                symbol = ConfigBot.GetNextSymbolForCandle();
                _dataReader = _dataReaderFactory.GetDataReader(symbol.Exchange);
                List<Candle> lastCandles = _dataReader.ReadLastCandles(symbol.Id);
                if (lastCandles.Count > 0)
                {
                    foreach (var candle in lastCandles)
                    {
                        _queueManager.Enqueue(candle);
                        RecentCandleDateTime = candle.Datetime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    stopwatch.Stop();
                    _totalExecutionTime += stopwatch.ElapsedMilliseconds;
                }
                else
                {
                    idelCounter++;
                }
                if (idelCounter > 5)
                {
                    Thread.Sleep(1000);
                    idelCounter= 0;
                }

                LogManager.ConsoleWriteLine($"Last Candle Time:{RecentCandleDateTime}, Total ExecutionTime: {_totalExecutionTime}, maximum queue count: {_queueManager.Count}",
                    $"Candle produced, symbol.id={symbol.Id}",60);
            }
        }
    }
}
