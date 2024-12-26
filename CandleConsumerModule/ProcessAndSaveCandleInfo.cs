using System;
//using System.Data.Entity;
//using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using Microsoft.EntityFrameworkCore;
// Removed unused imports to clean up the code.
using PreprocessDataStocks.Models; // Assuming this namespace contains the PreProcessData and VolumeProfiler classes.

namespace PreprocessDataStocks.CandleConsumerModule
{
    internal class ProcessAndSaveCandleInfo
    {
        decimal VpLowHigh = 192;
        int RangeCandlesFoCalcVolume = 32;
        //  int secondRangeCandels = 50;

        PreprocessDataStockContext _preprocessDataStockContext;

        internal ProcessAndSaveCandleInfo(PreprocessDataStockContext preprocessDataStockContext)
        {
            this._preprocessDataStockContext = preprocessDataStockContext;
            //step = this._preprocessDataStockContext.Candle.Average(d => d.High - d.Low) / 2;
        }
        private void saveCandel(Candle candle)
        {
            _preprocessDataStockContext.Candle.Add(candle);
            _preprocessDataStockContext.SaveChanges();
        }
        private void UpdateCandel(Candle newCandle,Candle dbCandle )
        {
            dbCandle.Close=newCandle.Close;
            dbCandle.Open = newCandle.Open;
            dbCandle.Low = newCandle.Low;
            dbCandle.High = newCandle.High;
            
            _preprocessDataStockContext.SaveChanges();
        }
        private void updateAndRemoveCandelAndVolumeProfile(Candle candle)
        {
            var dbcandle = _preprocessDataStockContext.Candle.FirstOrDefault(c => c.Datetime == candle.Datetime && c.SymbolId == candle.SymbolId);

            if (dbcandle != null)
            {
                var removeVPs = _preprocessDataStockContext.VolumeProfiler.Where(v => v.Candle == dbcandle && v.SymbolId == candle.SymbolId);

                if (removeVPs.Any())
                {
                    _preprocessDataStockContext.VolumeProfiler.RemoveRange(removeVPs);
                    _preprocessDataStockContext.SaveChanges();
                }
                UpdateCandel(candle, dbcandle);
            }
            else
            {
                saveCandel(candle);
            }
        }

        private void SpliteCandleToVolume(Candle currCandel)
        {
            for (decimal i = currCandel.Low; i < currCandel.High; i += VpLowHigh) // Using '<' to ensure we don't exceed 'High'.
            {
                VolumeProfiler vp = new VolumeProfiler
                {
                    Low = i,
                    High = Math.Min(i + VpLowHigh, currCandel.High), // Ensure 'High' doesn't exceed the preData.High.
                    Datetime = (DateTime)currCandel.Datetime,// Assuming Datetime is not nullable; removed the cast.
                    Candle = currCandel,
                    SymbolId = currCandel.SymbolId,
                };

                _preprocessDataStockContext.VolumeProfiler.Add(vp);
                _preprocessDataStockContext.SaveChanges();
            }

        }

        private bool updateLastCandleRepetition(Candle currCandle)
        {
            // Order PreProcessData and VolumeProfiler by Id
            var candles = _preprocessDataStockContext.Candle.Where(c => c.SymbolId == currCandle.SymbolId).OrderByDescending(d => d.Id);
            var volumeProfiler = _preprocessDataStockContext.VolumeProfiler.Where(c => c.SymbolId == currCandle.SymbolId).Include(e => e.Candle).ToList();

            // Assuming 'firstRangeCandles' is declared and initialized somewhere in your code
            var rangeCandles = candles.Where(d => d.Id < currCandle.Id).Take(RangeCandlesFoCalcVolume).ToList();

            // Filter VolumeProfiler records based on rangeCandles
            var vps = volumeProfiler.Where(vp => rangeCandles.Any(c => c.Id == vp.Candle.Id)).ToList();

            // Filter VolumeProfiler records for current candle
            var listCurrCandleVP = volumeProfiler.Where(vp => vp.Candle.Id == currCandle.Id).ToList();

            foreach (var vp in listCurrCandleVP)
            {
                // Calculate lastBar32x24bar based on conditions
                vp.LastBarRepetationVolume = vps.Where(v =>
                    vp.Low <= v.Low && v.Low <= vp.High || vp.Low <= v.High && v.High <= vp.High)
                    .Select(v => v.Candle.Id).Distinct().Count();
            }

            // Save changes to database
            int resultSave = _preprocessDataStockContext.SaveChanges();

            // Return true if any changes were made
            return resultSave > 0;
        }




        internal void ProccessDataCandle(Candle candle)
        {
            var symbol = ConfigBot.GetSymbolConfig(candle.SymbolId);
            VpLowHigh = symbol.VpLowHigh;
            RangeCandlesFoCalcVolume = symbol.RangeCandlesFoCalcVolume;

            updateAndRemoveCandelAndVolumeProfile(candle);
           // saveCandel(candle);
            SpliteCandleToVolume(candle);
            updateLastCandleRepetition(candle);


        }

    }
}


