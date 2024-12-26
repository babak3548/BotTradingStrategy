using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using PreprocessDataStocks.CandleConsumerModule.TickConsumerModule;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi.OandaModels.OandaSymbolSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OanadaAccount = PreprocessDataStocks.PartnerApi.OandaModels.OandaAccountDetails;

namespace PreprocessDataStocks.PartnerApi
{
    public class OandaAccountManager : BaseAccountManager, IAccountManager
    {
        private static readonly OandaAccountManager instant = new OandaAccountManager();

        private decimal balance { get; set; }
        private decimal marginAvailable { get; set; }
        private decimal marginUsed { get; set; }
        private decimal marginRate { get; set; }
        private decimal pl { get; set; }
        private decimal unrealizedPL { get; set; }
        private decimal financing { get; set; }
        private int openTradeCount { get; set; }
        private string guaranteedStopLossOrderMode { get; set; }
        private  string ExchangeName { get { return "oandaL"; } }

        //private DateTime lastUpdateTime = DateTime.MinValue;

        // private decimal lotSize;balance
        //  private int openPositions; // Number of open positions

        public static OandaAccountManager Instant => instant;
        // Constructor
        private OandaAccountManager()
        {
            updateAccountDataFromAPI();
            UpadateSettingSymbols();
            concurrentPostion = 3;
        }

        public override decimal LotSize(Tick tick, int symbolId, OrderTypes orderTypes)
        {
            Symbol sym = ConfigBot.Symbols.First(s => s.Id == symbolId);

            decimal ratioStopLossToSpread = (sym.VpLowHigh * sym.StopLossMultiple) / (tick.Ask- tick.Bid) ; 
            if(ratioStopLossToSpread < ConfigBot.RatioStopLossToSpreadConst)
            {
                LogManager.ConsoleWriteLine( $"{sym.Name} ratio StopLoss value per Spread:{ratioStopLossToSpread} Accepteble ratio StopLoss value per Spread:{ConfigBot.RatioStopLossToSpreadConst}", "Info Extra Spread", 10);
                return 0;
            }
            decimal tickPrice = (orderTypes==OrderTypes.buy ?  tick.Ask : tick.Bid ) * tick.HomeConversion;
            if (marginAvailable < tickPrice || marginAvailable < 10)
                return 0;

            decimal amountTrade =Math.Min( (balance/concurrentPostion) , marginAvailable );
            decimal lot = Math.Round(amountTrade * sym.Leverage / (tickPrice * lotUnit), 5);

            if(sym.LimitsExceededOrder > 0)
            {
                decimal unit = Math.Min((lot * lotUnit), sym.LimitsExceededOrder);
                lot =Math.Round( unit/lotUnit , 5);
            }
            
            return lot;
        }
       public override decimal CalculateTakeProfit(decimal price, OrderTypes orderTypes, decimal takeProfitMultiple, decimal VPlow_high,  int symbolId)
        {
            int displayPrecision = ConfigBot.Symbols.First(s => s.Id == symbolId).displayPrecision;
            return Math.Round( base.CalculateTakeProfit(price, orderTypes, takeProfitMultiple, VPlow_high,  symbolId), displayPrecision);
        }
        public override decimal CalculateStopLoss(decimal price, OrderTypes orderTypes, decimal stoplossMultiple, decimal VPlow_high, int symbolId)
        {
            int displayPrecision = ConfigBot.Symbols.First(s => s.Id == symbolId).displayPrecision;
            return Math.Round( base.CalculateStopLoss(price, orderTypes, stoplossMultiple, VPlow_high,symbolId), displayPrecision) ;
        }
        public override void UpdateAcountAfterRegOrder(decimal marginOrder)
        {
            base.UpdateAcountAfterRegOrder(marginOrder);
            updateAccountDataFromAPI();
        }
        public override void UpdateBalanceAfterCloseOrder(decimal revenue, decimal marginOrder)
        {
            base.UpdateBalanceAfterCloseOrder(revenue, marginOrder);
            updateAccountDataFromAPI();
        }
        private async Task updateAccountDataFromAPI()
        {

            string url = $"{ConfigBot.OandaEndpoint}accounts/{ConfigBot.OandaAccountId}";
            string _authToken = ConfigBot.OandaAuthToken;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", _authToken);

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    OanadaAccount.root rootObject = JsonSerializer.Deserialize<OanadaAccount.root>(responseBody);
                    fillDataAccountResp(rootObject);
                }
                else
                {
                    LogManager.WriteErrorLog($"Error log Account Failed with status code {response.StatusCode}", 1, "AccountManager");
                }
            }
        }



        private async Task UpadateSettingSymbols()
        {
            List<Symbol> symbols= ConfigBot.Symbols.Where(s => s.Exchange == ExchangeName).ToList();
            string instruments= string.Join(",", symbols.Select(s=>s.Name));
          string apiUrl = $"{ConfigBot.OandaEndpoint}accounts/{ConfigBot.OandaAccountId}/instruments?instruments={instruments}";
          string apiKey = ConfigBot.OandaAuthToken;
            try
            {
                string responseBody;
                using (HttpClient client = new HttpClient())
                {
                    // Set the request headers
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", apiKey);

                    // Send the GET request
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    // Read and output the response body
                     responseBody = await response.Content.ReadAsStringAsync();
                   // Console.WriteLine(responseBody);
                }


                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                InstrumentsResponse instrumentsResponse = JsonSerializer.Deserialize<InstrumentsResponse>(responseBody, options);

                foreach (var instrument in instrumentsResponse.Instruments)
                {
                   var sym= symbols.First(s=>s.Name== instrument.Name);
                    sym.Leverage =  (int)(1 / float.Parse(instrument.MarginRate));
                    sym.displayPrecision = instrument.DisplayPrecision;
                }
            }
            catch (HttpRequestException e)
            {
                LogManager.WriteErrorLog("Request error: " + e.Message,1,"AccountManager");
            }


            }
        private void fillDataAccountResp(OanadaAccount.root rootObj)
        {
            balance = decimal.Parse(rootObj.account.balance);
            marginAvailable = decimal.Parse(rootObj.account.marginAvailable);
            marginUsed = decimal.Parse(rootObj.account.marginUsed);
            marginRate = decimal.Parse(rootObj.account.marginRate);
            pl = decimal.Parse(rootObj.account.pl);
            unrealizedPL = decimal.Parse(rootObj.account.unrealizedPL);
            financing = decimal.Parse(rootObj.account.financing);
            openTradeCount = rootObj.account.openTradeCount;
            guaranteedStopLossOrderMode = rootObj.account.guaranteedStopLossOrderMode;
            leverage = 1.0m / marginRate;
        }

    }
}
