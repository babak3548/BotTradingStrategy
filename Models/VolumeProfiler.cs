using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.Models
{

    public partial class VolumeProfiler
    {
        public int Id { get; set; }

        public DateTime Datetime { get; set; }
        [Column(TypeName = "decimal(12,6)")]
        public decimal High { get; set; }
        [Column(TypeName = "decimal(12,6)")]
        public decimal Low { get; set; }

        public int? LastBarRepetationVolume { get; set; }

        //public int? lastBar50x24bar { get; set; }

        //public int? _48Or72lastBar32x24bar { get; set; }

        //public int? _48Or72lastBar50x24bar { get; set; }

        //public bool? IncreaceNOpip { get; set; }

        //public bool? DecreaseNOpip { get; set; }

        //public bool? DontChange { get; set; }

        //public bool? TpMOPipWillTouch { get; set; }

        //public bool? Tl13mOPipWillTouch { get; set; }

        //public bool? NoMOPipWillTouch { get; set; }

        
        public int SymbolId { get; set; }
        public Candle Candle { get; set; }
        public ICollection<Order> Orders { get; set; }
        
    }
}
