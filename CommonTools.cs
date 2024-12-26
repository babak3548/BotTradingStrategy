using PreprocessDataStocks.CandleConsumerModule.TickConsumerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks
{
    public static class CommonTools
    {
        private static readonly Dictionary<string, DateTime> lastLogTime = new Dictionary<string, DateTime>();

        private static readonly Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < n; i++)
            {
                int j = random.Next(i, n);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        public static bool SecondsIsPassed(string key,int seconds)
        {
            DateTime currentTime = DateTime.Now;
            if (lastLogTime.ContainsKey(key))
            {
                DateTime lastTime = lastLogTime[key];
                if ((currentTime - lastTime).TotalSeconds >= seconds)
                {
                    lastLogTime[key] = currentTime;
                    return true;
                }
                else { return false; }
            }
            else
            {
                lastLogTime[key] = currentTime;
                return true;
            }
        }
        public static DateTime GetLastCandelDateTime(this DateTime tickTime, double periodInHours, DateTime startACandle)
        {
            //tickTime = tickTime.AddHours(-1 * periodInHours);
            if (startACandle > tickTime)
            {
                throw new Exception("startACandle can not be greater than tickTime");
            }

            double totalHoursDifference = (tickTime - startACandle).TotalHours;
            double numberOfPeriods = Math.Floor(totalHoursDifference / periodInHours);
            DateTime lastCandleDateTime = startACandle.AddHours(numberOfPeriods * periodInHours);

            return lastCandleDateTime;
        }
        public static double GetPeriodInHours(this string period)
        {
            switch (period)
            {
                case "1h":
                    return 1;
                case "4h":
                    return 4;
                case "1d":
                    return 24;
                default:
                    throw new Exception("Period is not defined");
            }
        }

        public static string GranularityOanda(this string period)
        {
            switch (period)
            {
                case "1h":
                    return "H1";
                case "4h":
                    return "H4";
                case "1d":
                    return "D";
                default:
                    throw new Exception("Period is not defined");
            }
        }

        public static Position GetPositionType(this OrderTypes orderType)
        {
            switch (orderType)
            {
                case OrderTypes.buy:
                    return Position.Long;
                case OrderTypes.sell:
                    return Position.Short;
                default:
                    throw new Exception("Period is not defined");
            }
        }

        /// <summary>
        /// Calculates the end date after adding a specified number of periods to the given DateTime.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="periodLengthInMinutes">The length of each period in minutes.</param>
        /// <param name="numberOfPeriods">The number of periods to add.</param>
        /// <returns>The calculated end date.</returns>
        public static DateTime AddPeriods(this DateTime startDate, int periodLengthInMinutes, int numberOfPeriods)
        {
            DateTime newTime= startDate.AddMinutes(periodLengthInMinutes * numberOfPeriods);
           
            return newTime;
        }



    }
}
