using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.TickQueueManagerModule;

namespace PreprocessDataStocks.CandleConsumerModule.TickConsumerModule
{
    public class TickConsumer
    {
        int CountEveryAveMax = 300;
        static int maxQueueCount = 0;
        static long totalExecutionTime = 0;
        Tick currentTick = null;

        private TraderAgent _traderAgent;
        private readonly TickQueueManager _queueManager;

        public TickConsumer(TickQueueManager queueManager, PreprocessDataStockContext preprocessDataStockContext)
        {
            _queueManager = queueManager;
            _traderAgent = TraderAgent.Instance(preprocessDataStockContext);
        }

        public void Consume()
        {
            int i = 0;
            int symbolId = 0;
            //string tickTime="";
            while (true)
            {
                if (_queueManager.Count > 0)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    // if (currentTick == null)//  uncomment when Test history data
                    // {
                    currentTick = _queueManager.Dequeue();
                    symbolId = currentTick.SymbolId;
                    //LogManager.WriteTickToCsv("TicksInConsumer1.csv", currentTick);
                    // }
                    Candle currCandle = _traderAgent.GetCurrentCandle(currentTick);
                    //wait until Candle Processed
                    if (currentTick != null && currentTick.Tradeable && currCandle != null)
                    {
                        _traderAgent.CheckAndDoTrade(currentTick);
                        if (ConfigBot.LogCSVTicks) LogManager.WriteTickToCsv($"TicksInConsumer{DateTime.Now.ToString("yyyy-MM-dd")}.csv", currentTick);
                        //  tickTime = currentTick?.TickDatetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        // currentTick = null;
                    }
                    else
                    {
                        LogManager.WriteWarnigLog($"warnig leve 1: pass process tick {currentTick?.TickDatetime}, symbolId:{symbolId} becuase currentTick.Tradeable:{currentTick.Tradeable} or currCandle:" +
                            (currCandle == null ? "Null" : "Not null"), 1, "Process Tick");
                        Thread.Sleep(500);
                    }

                    stopwatch.Stop();
                    totalExecutionTime = stopwatch.ElapsedMilliseconds;
                    LogManager.ConsoleWriteLine($"Tick Time:{currentTick?.TickDatetime.ToString("yyyy-MM-dd HH:mm:ss.fff")}, Candle time:{currCandle?.Datetime?.ToString("yyyy-MM-dd HH:mm:ss.fff")}" +
                        $" ExecutionTime:{totalExecutionTime},  Queue Count:{_queueManager.Count}", $"Tick Consumer Working,  SymbolId: {symbolId}", 60);
                }
                else
                {
                    // Thread.Sleep(500);
                    LogManager.ConsoleWriteLine("Queue no any tick  ", "Tick Consumer", 30);
                }

                // maxQueueCount = Math.Max(maxQueueCount, _queueManager.Count);
                // CountEveryAveMax = new Random().Next(1000000, 3000000);
                // i++;
                //if (i > CountEveryAveMax)
                // {
                //     Console.WriteLine($"Tick Consumer,  SymbolId: {symbolId}, Time:{DateTime.Now}, ExecutionTime Ave {CountEveryAveMax} times:{(long)totalExecutionTime / CountEveryAveMax}, max Queue Count:{(long)maxQueueCount}");
                //     totalExecutionTime = 0;
                //     maxQueueCount = 0;
                //     i = 0;
                // }
            }
        }
    }
}
