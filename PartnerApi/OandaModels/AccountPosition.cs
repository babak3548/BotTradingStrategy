using PreprocessDataStocks.PartnerApi.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.OandaModels.AccountPosition
{
    public class longPosition
    {
        public string units { get; set; }
        public string pl { get; set; }
        public string resettablePL { get; set; }
        public string financing { get; set; }
        public string dividendAdjustment { get; set; }
        public string guaranteedExecutionFees { get; set; }
        public List<string> tradeIDs { get; set; }
        public string unrealizedPL { get; set; }
    }

    public class shortPosition
    {
        public string units { get; set; }
        public string averagePrice { get; set; }
        public string pl { get; set; }
        public string resettablePL { get; set; }
        public string financing { get; set; }
        public string dividendAdjustment { get; set; }
        public string guaranteedExecutionFees { get; set; }
        public List<string> tradeIDs { get; set; }
        public string unrealizedPL { get; set; }
    }

    public class position
    {
        public string instrument { get; set; }
        public longPosition @long { get; set; }
        public shortPosition @short { get; set; }
        public string pl { get; set; }
        public string resettablePL { get; set; }
        public string financing { get; set; }
        public string commission { get; set; }
        public string dividendAdjustment { get; set; }
        public string guaranteedExecutionFees { get; set; }
        public string unrealizedPL { get; set; }
        public string marginUsed { get; set; }
    }

    public class rootObject
    {
        public position position { get; set; }
        public string lastTransactionID { get; set; }

        public OpenPositions FillDataPosition()
        {
            OpenPositions openPositions = new OpenPositions
            {
                LongUnits = decimal.Parse(this.position.@long.units),
                LongTradeIDs = this.position.@long.tradeIDs,
                ShortTradeIDs = this.position.@short.tradeIDs,
                ShortUnits = decimal.Parse(this.position.@short.units)
            };
            return openPositions;
    }
    }





}
