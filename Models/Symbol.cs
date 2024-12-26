using Microsoft.VisualBasic;
using PreprocessDataStocks.CandleConsumerModule.TickConsumerModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PreprocessDataStocks.Models
{

    [Table("Symbol")]
    public partial class Symbol
    {
        public int Id { get; set; }
        [StringLength(8)]
        public string Name { get; set; }
        [StringLength(8)]
        public string Exchange { get; set; }
        [StringLength(8)]
        public string Period { get; set; }
        public DateTime LastCandleProccessed { get; set; }

        // BackRangeCandles property
        public byte RecentCandlesForVolumeSelector { get; set; }
        // MinLastBarVolProfile property
        public byte MinRepetationVolumeNeededTrade { get; set; }
        public byte RangeCandlesFoCalcVolume { get; set; }

        // ConfimTrandNum property EfectedVolumeOnNextCandles
        [Column(TypeName = "decimal(12,6)")]
        public decimal ConfirmTrendNum { get; set; }
        // VPlowHigh property
        [Column(TypeName = "decimal(12,6)")]
        public decimal VpLowHigh { get; set; }
        // PercentageInvestTrade property
        public byte PercentageInvestTrade { get; set; }
        // TakeProfitMultiple property
        [Column(TypeName = "decimal(3,1)")]
        public decimal TakeProfitMultiple { get; set; }
        // StoplossMultiple property
        [Column(TypeName = "decimal(3,1)")]
        public decimal StopLossMultiple { get; set; }
        public PermisionTradeType PermisionTradeType { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "decimal(12,6)")]
        public decimal ApproximateHomeConversion { get; set; }
        public float ConversionHistoryRate { get; set; }
        public float ConversionRealRate { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal LimitsExceededOrder { get; set; }
        [Column(TypeName = "decimal(14,6)")]
        public decimal AverageSpread { get; set; }

        public DateTime StartCandle { get;  set; }
        [NotMapped] // 
        public List<Candle> RecentlyCandlesList { get;
            set; }
        [NotMapped] // 
        public bool CandlesAddedToDB { get; set; } = false;
        [NotMapped]
        public bool TickReaded { get; set; } = false;
        [NotMapped]
        public DateTime LastCandleReaded
        {
            get;
            set;
        }
        [NotMapped]
        public DateTime LastTickReaded { get; set; }
        [NotMapped]
        public int displayPrecision { get; set; }
        [NotMapped]
        public int Leverage { get; set; }

        [NotMapped]
        public int BuyCallAginCounter { get; set; } = 0;
        [NotMapped]
        public int SellCallAginCounter { get; set; } = 0;
   
        // public ICollection<VolumeProfiler> VolumeProfilers { get; set; }
        // public ICollection<Order> Orders { get; set; }
        // public ICollection<Candle> Candles { get; set; }
    }

}
