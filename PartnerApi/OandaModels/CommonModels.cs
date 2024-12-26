using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PreprocessDataStocks.PartnerApi.OandaModels.CommonModels
{


    public class takeProfitOnFill
    {
        public string price { get; set; }
        public string timeInForce { get; set; } = "GTC";
    }

    public class stopLossOnFill
    {
        public string price { get; set; }
       public string timeInForce { get; set; } = "GTC";
        public string triggerMode { get; set; } = "TOP_OF_BOOK";
    }

    public class clientExtensions
    {
        public string id { get; set; }
    }




}
