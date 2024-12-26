using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.PartnerApi.OandaModels.OandaSymbolSetting
{

    public class Instrument
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string DisplayName { get; set; }
        public int PipLocation { get; set; }
        public int DisplayPrecision { get; set; }
        public int TradeUnitsPrecision { get; set; }
        public string MinimumTradeSize { get; set; }
        public string MaximumTrailingStopDistance { get; set; }
        public string MinimumTrailingStopDistance { get; set; }
        public string MaximumPositionSize { get; set; }
        public string MaximumOrderUnits { get; set; }
        public string MarginRate { get; set; }
        public string GuaranteedStopLossOrderMode { get; set; }
        public List<Tag> Tags { get; set; }
        public Financing Financing { get; set; }
    }

    public class Tag
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Financing
    {
        public string LongRate { get; set; }
        public string ShortRate { get; set; }
        public List<FinancingDayOfWeek> FinancingDaysOfWeek { get; set; }
    }

    public class FinancingDayOfWeek
    {
        public string DayOfWeek { get; set; }
        public int DaysCharged { get; set; }
    }

    public class InstrumentsResponse
    {
        public List<Instrument> Instruments { get; set; }
        public string LastTransactionID { get; set; }
    }

}
