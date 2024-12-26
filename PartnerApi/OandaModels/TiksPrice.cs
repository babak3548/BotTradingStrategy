using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.OandaModels.TiksPrice
{

public class bid
    {
        public string price { get; set; }
        public int liquidity { get; set; }
    }

    public class ask
    {
        public string price { get; set; }
        public int liquidity { get; set; }
    }

    public class quoteHomeConversionFactors
    {
        public string positiveUnits { get; set; }
        public string negativeUnits { get; set; }
    }

    public class price
    {
        public string type { get; set; }
        public DateTime time { get; set; }
        public List<bid> bids { get; set; }
        public List<ask> asks { get; set; }
        public string closeoutBid { get; set; }
        public string closeoutAsk { get; set; }
        public string status { get; set; }
        public bool tradeable { get; set; }
        public quoteHomeConversionFactors quoteHomeConversionFactors { get; set; }
        public string instrument { get; set; }
    }

    public class rootObject
    {
        public DateTime time { get; set; }
        public List<price> prices { get; set; }
    }


}
