using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreprocessDataStocks.CandleQueueManagerModule;
using PreprocessDataStocks.Models;

namespace PreprocessDataStocks.CandleConsumerModule
{
    public class CandleConsumer
    {
       //const int CountEveryAveMax = 30;
       // static int maxQueueCount = 0;
        static long totalExecutionTime = 0;
        Candle candle;
        private readonly CandleQueueManager _queueManager;
        private ProcessAndSaveCandleInfo _ProcessAndSaveCandleInfo;

        public CandleConsumer(CandleQueueManager queueManager, PreprocessDataStockContext preprocessDataStockContext)
        {
            _queueManager = queueManager;
            _ProcessAndSaveCandleInfo = new ProcessAndSaveCandleInfo(preprocessDataStockContext);
        }

        public void Consume()
        {
          
            while (true)
            {
                if (_queueManager.Count > 0)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                     candle = _queueManager.Dequeue();
                    _ProcessAndSaveCandleInfo.ProccessDataCandle(candle);
                    ConfigBot.Symbols.First(c => c.Id == candle.SymbolId).LastCandleProccessed = candle.Datetime.Value;
                    stopwatch.Stop();
                    totalExecutionTime += stopwatch.ElapsedMilliseconds;
                }
                else
                {
                   Thread.Sleep(500);
                }
              
                LogManager.ConsoleWriteLine($" totalExecutionTime:{(long)totalExecutionTime }," +
                  $" max Queue Count:{_queueManager.Count}", $"Candle Consumer SymbolId:{candle?.SymbolId}", 60);
            }
        }
    }
}
