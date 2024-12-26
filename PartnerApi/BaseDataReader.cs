using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi.OandaModels.TiksPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi
{
    public class BaseDataReader
    {
        private const int piecesCount = 24;
        internal List<Candle> _candles = new List<Candle>();
        internal Dictionary<int, Queue<Tick>> dTickQueue = new Dictionary<int, Queue<Tick>>();

        internal virtual (List<decimal>, decimal) SplitLowHighCandles(decimal low, decimal high, decimal open, decimal close,int piecesCount)
        {
            var step = (high - low) / piecesCount;
            var list1 = new List<decimal>();
            var list2 = new List<decimal>();
            
            for (var tickPrice = open; tickPrice >= low; tickPrice -= step)
                {
                    list1.Add(tickPrice);
                }
            list1.Shuffle();//Changable
            for (var tickPrice = open; tickPrice <= high; tickPrice += step)
                {
                    list2.Add(tickPrice);
                }
            list2.Shuffle();//Changable 
           if (open<close)
            {
                list1.AddRange(list2);
                return (list1 , step);
            }
           else
            {
                list2.AddRange(list1);
                return (list2, step);
            }

        }
        public virtual Tick ReadLastTick(int symbolId, PreprocessDataStockContext _preprocessDataStockContex)
        {
            var symbol = ConfigBot.GetSymbolConfig(symbolId);
            if (!symbol.TickReaded && _candles.Any(c => c.SymbolId == symbolId))
            {
                dTickQueue.Add(symbolId, new Queue<Tick>());
                var candles = _candles.OrderBy(c => c.Datetime).Where(c => c.SymbolId == symbolId).ToList();
                decimal halfSpread = Math.Round(symbol.AverageSpread / 2, 6);
                double periodHourse = symbol.Period.GetPeriodInHours();
                foreach (var currCandle in candles)
                {
                    var (ticks, step) = SplitLowHighCandles(currCandle.Low, currCandle.High, currCandle.Open, currCandle.Close, piecesCount);

                    foreach (var tickPrice in ticks)
                    {
                        var tick = CreateTick(symbol, halfSpread, periodHourse, currCandle, tickPrice, step);
                        dTickQueue[symbol.Id].Enqueue(tick);
                    }
                }
                symbol.TickReaded = true;
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

        protected Tick CreateTick( Symbol symbol, decimal halfSpread, double periodHourse, Candle? currCandle,decimal tickPrice, decimal step)
        {
           return new Tick
            {
                Ask = Math.Round(tickPrice + halfSpread, 5),
                Bid = Math.Round(tickPrice - halfSpread, 5),
                AskLiquiditySum = 100000000,
                BidLiquiditySum = 100000000,
                TickDatetime = currCandle.Datetime.Value,
                CandleDatetime = currCandle.Datetime.Value.GetLastCandelDateTime(periodHourse, symbol.StartCandle),
                Period = step,
                SymbolId = symbol.Id,
                HomeConversion = symbol.ApproximateHomeConversion,
                Tradeable= true,
            };
        }
        protected Tick CreateTick(TickDB tickDB, Symbol symbol)
        {
            return new Tick
            {
                Ask =tickDB.Ask,
                Bid = tickDB.Bid,
                AskLiquiditySum = 100000000,
                BidLiquiditySum = 100000000,
                TickDatetime = tickDB.TickDBDatetime,
                CandleDatetime = tickDB.CandleDatetime,
                Period = tickDB.Period,
                SymbolId = tickDB.SymbolId,
                Tradeable = tickDB.Tradeable,
                HomeConversion = symbol.ApproximateHomeConversion
            };
        }
        protected TickDB CreateTickDB(Tick tick)
        {
            return new TickDB
            {
                Ask = tick.Ask,
                Bid = tick.Bid,
                TickDBDatetime = tick.TickDatetime,
                CandleDatetime = tick.CandleDatetime,
                Period = tick.Period,
                SymbolId = tick.SymbolId,
                Tradeable = tick.Tradeable,
            };
        }
    }



}
