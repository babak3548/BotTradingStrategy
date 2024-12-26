using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.Json;
using PreprocessDataStocks.PartnerApi.OandaModels.CommonModels;

namespace PreprocessDataStocks.PartnerApi.OandaModels.CreateOrderModels
{

    public class order
    {

        public string units { get; set; }


        public string instrument { get; set; }


        public string timeInForce { get; set; }


        public string type { get; set; }


        public string positionFill { get; set; }

        public takeProfitOnFill takeProfitOnFill { get; set; }


        public stopLossOnFill stopLossOnFill { get; set; }


        public clientExtensions clientExtensions { get; set; }
    }

    public class rootObject
    {

        public rootObject(int units, string symbol, decimal takeProfit, decimal stopLoss)
        {
            // Generate RootObject instance with provided JSON object values
            this.order = new order
            {
                units = units.ToString(),
                instrument = symbol,
                timeInForce = "FOK",
                type = "MARKET",
                positionFill = "DEFAULT",
                takeProfitOnFill = new takeProfitOnFill
                {
                    price = takeProfit.ToString(),
                },
                stopLossOnFill = new stopLossOnFill
                {
                    price = stopLoss.ToString(),
                },
                clientExtensions = new clientExtensions
                {
                    id = "my_client_id" + symbol,
                }
            };
        }

        public string CreateOrderJson()
        {
            // Serialize RootObject to JSON
            string serializedJson = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            return serializedJson;
        }
        public order order { get; set; }
    }

}
