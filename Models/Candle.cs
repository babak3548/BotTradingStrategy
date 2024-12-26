using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreprocessDataStocks.Models;

[Table("Candle")]

public partial class Candle
{
    public int Id { get; set; }

    public DateTime? Datetime { get; set; }

    [Column(TypeName = "decimal(14,6)")]
    public decimal Open { get; set; }
    [Column(TypeName = "decimal(14,6)")]
    public decimal High { get; set; }
    [Column(TypeName = "decimal(14,6)")]
    public decimal Low { get; set; }
    [Column(TypeName = "decimal(14,6)")]
    public decimal Close { get; set; }

    //firstRangeCandles
    public int SymbolId { get; set; }


    public ICollection<VolumeProfiler> VolumeProfilers { get; set; }
    public ICollection<Tick> Ticks { get; set; }

    //public int? lastBar32x24bar { get; set; }

    //public int? lastBar50x24bar { get; set; }

    //public int? _48Or72lastBar32x24bar { get; set; }

    //public int? _48Or72lastBar50x24bar { get; set; }

    //public bool? IncreaceNOpip { get; set; }

    //public bool? DecreaseNOpip { get; set; }

    //public bool? DontChange { get; set; }

    //public bool? TpMOPipWillTouch { get; set; }

    //public bool? Tl13mOPipWillTouch { get; set; }

    //public bool? NoMOPipWillTouch { get; set; }
}
