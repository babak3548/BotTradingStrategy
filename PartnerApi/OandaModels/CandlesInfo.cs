using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.OandaModels.CandlesInfo
{

    public class mid
    {
        public string o { get; set; }
        public string h { get; set; }
        public string l { get; set; }
        public string c { get; set; }
    }

    public class candle
    {
        public bool complete { get; set; }
        public int volume { get; set; }
        public DateTime time { get; set; }
        public mid mid { get; set; }
    }

    public class rootObject
    {
        public string instrument { get; set; }
        public string granularity { get; set; }
        public List<candle> candles { get; set; }
    }

}
