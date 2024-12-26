using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreprocessDataStocks.CandleQueueManagerModule;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi;
using PreprocessDataStocks.TickQueueManagerModule;

namespace PreprocessDataStocks.CandleProducerModule.TickProducerModule
{
    public class TickProducer
    {
        //const int CountEveryAveMax = 30;
        //static int maxQueueCount = 0;
        //static int symbolTaskCounter = 0;
        static long totalExecutionTime = 0;
        int i = 0;
        Tick tick;
        Symbol symbol;
        // bool theradWorked = true;

        private readonly TickQueueManager _queueManager;
        ICandleDataReader _dataReader;
        DataReaderFactory _dataReaderFactory;
        PreprocessDataStockContext _preprocessDataStockContex;
        public TickProducer(TickQueueManager queueManager, PreprocessDataStockContext preprocessDataStockContext)
        {
            _queueManager = queueManager;
            _preprocessDataStockContex = preprocessDataStockContext;
            _dataReaderFactory = new DataReaderFactory();
        }

        public void Produce()
        {
            // Symbol symbol;
            while (true)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                symbol = ConfigBot.GetNextSymbolForTick();
                _dataReader = _dataReaderFactory.GetDataReader(symbol.Exchange);
                tick = _dataReader.ReadLastTick(symbol.Id, _preprocessDataStockContex);
                if (tick != null)
                {
                    _queueManager.Enqueue(tick);
                }
                else
                {
                    i++;
                }

                stopwatch.Stop();
                totalExecutionTime += stopwatch.ElapsedMilliseconds;

                if (i >= 5)
                {
                    Thread.Sleep(500);
                    i = 0;
                }
                LogManager.ConsoleWriteLine($"  times:{totalExecutionTime}, max Queue Count:{_queueManager.Count}", $"Produce Tick symbolId:{symbol.Id}", 60);
            }
        }
    }

}


