using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.Models
{
 
   // [Table("Tick")]
    public partial class Tick
    {
        public int Id { get; set; }
        public DateTime TickDatetime { get; set; }
        public DateTime CandleDatetime { get; set; }

        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
        public int AskLiquiditySum { get; set; }
        public int BidLiquiditySum { get; set; }
        
        public decimal Period { get; set; }
        public int SymbolId { get; set; }

        public bool Tradeable { get; set; } = true;
        [NotMapped]
        public decimal HomeConversion { get; set; }
    }
}
