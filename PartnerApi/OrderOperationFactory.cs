using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi
{
    internal class OrderOperationFactory
    {
        public IOrderOperations GetOrderOperation(string exchange)
        {
            switch (exchange)
            {
                case "tweldaC":
                    return null;
                case "tweldaL":
                    return null;
                case "tweldaLT":
                    return null;
                case "oandaL": 
                    return OandaOrderOperations.Instance;
                default:
                    throw new ArgumentException("Invalid product type");
            }
        }
    }
}
