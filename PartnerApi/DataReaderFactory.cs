using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi
{
    internal class DataReaderFactory
    {
        public ICandleDataReader GetDataReader(string exchange)
        {
            switch (exchange)
            {
                case "tweldaC":
                    return CandleTickDataReaderCsv.Instance;
                case "tweldaL":
                    return TwelveDataApiReader.Instance;
                case "tweldaLT":
                    return TwelveDataApiReaderHistoryTick.Instance;
                case "oandaL": 
                    return OandaDataApiReader.Instance;
                default:
                    throw new ArgumentException("Invalid product type");
            }
        }
    }
}
