using Microsoft.Extensions.Configuration;

using PreprocessDataStocks.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks
{
    public class ConfigBot
    {



        public static bool IsTestingMode { get; set; }
        public static bool LogCSVTicks { get; set; }
        public static decimal InitialInvestment { get; set; }
        public static string CandleCsvFilePath { get; set; }
        public static decimal RatioStopLossToSpreadConst { get; set; }
        public static string OandaEndpoint { get; set; }
        public static string OandaAuthToken { get; set; }
        public static string OandaAccountId { get; set; }

        public static byte ApplySpread { get; set; }
        public static byte BuySellCallAginCount { get; set; }

        public static List<Symbol> Symbols { get; set; }
        public static int CurrentIndexCandle { get; set; } = 0;
        public static int CurrentIndexTick { get; set; } = 0;

        #region class Props

        #endregion

        public static string ConnectionString
        {
            get
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                  .Build();
                return configuration["ConfigBot:ConnectionString"];
            }
        }

        internal static Symbol GetSymbolConfig(int symbolId)
        {
            Symbol symbolConfig = Symbols.Find(s => s.Id == symbolId);
            return symbolConfig;
        }


        // Method to get the next item in the list
        public static Symbol GetNextSymbolForCandle()
        {
            // Get the next index using modulo arithmetic
            CurrentIndexCandle = (CurrentIndexCandle + 1) % Symbols.Count;
            return Symbols[CurrentIndexCandle];
        }
        public static Symbol GetNextSymbolForTick()
        {
            // Get the next index using modulo arithmetic
            CurrentIndexTick = (CurrentIndexTick + 1) % Symbols.Count;
            return Symbols[CurrentIndexTick];
        }

        public static void LoadConfiguration(PreprocessDataStockContext dbContextCandleConsumer)
        {
            loadAppSettingFile();
            Symbols = dbContextCandleConsumer.Symbols.Where(s => s.Active).ToList();
            foreach (Symbol symbol in Symbols)
            {
                var last = dbContextCandleConsumer.Candle.OrderBy(c => c.Datetime).LastOrDefault(c => c.SymbolId == symbol.Id);
                if (last != null)
                {
                    symbol.LastCandleReaded = last.Datetime.Value;
                
                }
                else
                {
                    symbol.LastCandleReaded = symbol.StartCandle;
                }

                if (IsTestingMode)
                {
                    symbol.LastTickReaded = symbol.StartCandle;
                }
                else
                {
                    symbol.LastTickReaded = last.Datetime.Value;
                }
            }
            Console.WriteLine("Load Configuration Program done.");
        }
        // internal static long minTimeExecutionTimeTheradMiliSecond=10;

        private static void loadAppSettingFile()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                             .Build();

            // Manually map configuration values to ConfigBot properties
            ConfigBot.IsTestingMode = bool.Parse(configuration["ConfigBot:IsTestingMode"]);
            ConfigBot.LogCSVTicks = bool.Parse(configuration["ConfigBot:LogCSVTicks"]);
            ConfigBot.InitialInvestment = decimal.Parse(configuration["ConfigBot:InitialInvestment"], CultureInfo.InvariantCulture);
            ConfigBot.CandleCsvFilePath = configuration["ConfigBot:CandleCsvFilePath"];
            ConfigBot.RatioStopLossToSpreadConst = decimal.Parse(configuration["ConfigBot:RatioStopLossToSpreadConst"], CultureInfo.InvariantCulture);
            ConfigBot.OandaEndpoint = configuration["ConfigBot:OandaEndpoint"];
            ConfigBot.OandaAuthToken = configuration["ConfigBot:OandaAuthToken"];
            ConfigBot.OandaAccountId = configuration["ConfigBot:OandaAccountId"];
            ConfigBot.ApplySpread = byte.Parse(configuration["ConfigBot:ApplySpread"], CultureInfo.InvariantCulture);
            ConfigBot.BuySellCallAginCount = byte.Parse(configuration["ConfigBot:BuySellCallAginCount"], CultureInfo.InvariantCulture);

        }

    }
}
