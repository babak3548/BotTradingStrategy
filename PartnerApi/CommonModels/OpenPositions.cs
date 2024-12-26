using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.CommonModels
{
    public class OpenPositions
    {
        public decimal LongUnits { get; set; }
        public decimal ShortUnits { get; set; }
        public List<string> LongTradeIDs { get; set; }
        public List<string> ShortTradeIDs { get; set; }
    }
}
