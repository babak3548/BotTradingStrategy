using PreprocessDataStocks.PartnerApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.CandleConsumerModule.TickConsumerModule
{
    internal class AccountManagerFactory
    {
        public IAccountManager GetAccountManager(string exchange)
        {
            switch (exchange)
            {
                case "tweldaC":
                    return ForexAccountManager.Instant; ;
                case "tweldaL":
                    return ForexAccountManager.Instant; ;
                case "tweldaLT":
                    return ForexAccountManager.Instant; ;
                case "oandaL":
                    return OandaAccountManager.Instant;
                default:
                    throw new ArgumentException("Invalid product type");
            }
        }
    }
}
