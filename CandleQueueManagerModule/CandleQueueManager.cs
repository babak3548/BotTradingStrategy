using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.CandleQueueManagerModule
{
    public class CandleQueueManager
    {
        private readonly Queue<Candle> _queue = new Queue<Candle>();
        private readonly object _lockObject = new object();

        public int Count => _queue.Count;

        public void Enqueue(Candle candle)
        {
            lock (_lockObject)
            {
                  _queue.Enqueue(candle);
            }
        }

        public Candle Dequeue()
        {
            lock (_lockObject)
            {
                return _queue.Dequeue();
            }
        }
    }
}
