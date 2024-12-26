using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.TickQueueManagerModule
{
    public class TickQueueManager
    {
        private readonly Queue<Tick> _queue = new Queue<Tick>();
        private readonly object _lockObject = new object();

        public int Count => _queue.Count;

        public void Enqueue(Tick item)
        {
            lock (_lockObject)
            {
                _queue.Enqueue(item);
            }
        }

        public Tick Dequeue()
        {
            lock (_lockObject)
            {
                return _queue.Dequeue();
            }
        }
    }
}
