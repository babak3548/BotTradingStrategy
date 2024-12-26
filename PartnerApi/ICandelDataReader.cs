using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi
{
    public interface ICandleDataReader
    {
        List<Candle> ReadLastCandles(int symbolId);
        Tick ReadLastTick(int symbolId, PreprocessDataStockContext _preprocessDataStockContex);

    }
}
