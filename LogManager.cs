using System;
using System.IO;
using Newtonsoft.Json; // You need to install the Newtonsoft.Json package
using PreprocessDataStocks.Models;

namespace PreprocessDataStocks
{


    public static class LogManager
    {
        // Define a class to represent the log entry
        public class LogEntry
        {
            public string Message { get; set; }
            public byte Level { get; set; }
            public string Type { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private static readonly Dictionary<string, DateTime> lastLogTime = new Dictionary<string, DateTime>();

        public static void ConsoleWriteLine(string text,string key, double seconds)
        {
            if (ConfigBot.IsTestingMode) seconds = seconds /10;
            DateTime currentTime = DateTime.Now;
            if (lastLogTime.ContainsKey(key))
            {
                DateTime lastTime = lastLogTime[key];
                if ((currentTime - lastTime).TotalSeconds >= seconds)
                {
                    Console.WriteLine(key+" => "+text +" S:"+ seconds);
                    lastLogTime[key] = currentTime;
                }
            }
            else
            {
                Console.WriteLine(key+" => "+text + " S:" + seconds);
                lastLogTime[key] = currentTime;
            }
        }

        public static string WriteErrorLog(string TextError, byte level, string type)
        {
            // Create a new log entry
            LogEntry logEntry = new LogEntry
            {
                Message = TextError,
                Level = level,
                Type = type,
                Timestamp = DateTime.Now
            };

            // Serialize the log entry to JSON
            string jsonLogEntry = JsonConvert.SerializeObject(logEntry);

            // Define the log file path
            string logFilePath = $"errorLog{DateTime.UtcNow.Date.ToString("yyyy_MM_dd")}.txt";

            try
            {
                Console.WriteLine("Error=> "+jsonLogEntry);
                // Write the JSON string to the text file
                File.AppendAllText(logFilePath, jsonLogEntry + Environment.NewLine);

                return "Log written successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to write log: {ex.Message}";
            }
        }
        public static string WriteWarnigLog(string TextWarning, byte level, string type)
        {
            // Create a new log entry
            LogEntry logEntry = new LogEntry
            {
                Message = TextWarning,
                Level = level,
                Type = type,
                Timestamp = DateTime.Now
            };

            // Serialize the log entry to JSON
            string jsonLogEntry = JsonConvert.SerializeObject(logEntry);

            // Define the log file path
            string logFilePath = $"WarnigLog{DateTime.UtcNow.Date.ToString("yyyy_MM_dd")}.txt";

            try
            {

                Console.WriteLine("Warning=> "+jsonLogEntry);
                // Write the JSON string to the text file
                File.AppendAllText(logFilePath, jsonLogEntry + Environment.NewLine);
                return "Log written successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to write log: {ex.Message}";
            }
        }

        public static void WriteTicksToCsv(string filePath, List<Tick> ticks)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id,TickDatetime,CandleDatetime,Ask,Bid,AskLiquiditySum,BidLiquiditySum,Period,SymbolId,Tradeable,HomeConversion");
                foreach (var tick in ticks)
                {
                    writer.WriteLine($"{tick.Id},{tick.TickDatetime:yyyy-MM-dd HH:mm:ss},{tick.CandleDatetime:yyyy-MM-dd HH:mm:ss},{tick.Ask},{tick.Bid},{tick.AskLiquiditySum},{tick.BidLiquiditySum},{tick.Period},{tick.SymbolId},{tick.Tradeable},{tick.HomeConversion}");
                }
            }
        }

        public static string WriteTickToCsv(string filePath, Tick tick)
        {
            bool fileExists = File.Exists(filePath);

            using (var writer = new StreamWriter(filePath, append: true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Id,TickDatetime,CandleDatetime,Ask,Bid,AskLiquiditySum,BidLiquiditySum,Period,SymbolId,Tradeable,HomeConversion");
                }

                writer.WriteLine($"{tick.Id},{tick.TickDatetime:yyyy-MM-dd HH:mm:ss},{tick.CandleDatetime:yyyy-MM-dd HH:mm:ss},{tick.Ask},{tick.Bid},{tick.AskLiquiditySum},{tick.BidLiquiditySum},{tick.Period},{tick.SymbolId},{tick.Tradeable},{tick.HomeConversion}");
            }

            return Path.GetFullPath(filePath);
        }
    }
}
