using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreprocessDataStocks.Models
{
 
   [Table("TickDB")]
    public partial class TickDB
    {
        public int Id { get; set; }
        public DateTime TickDBDatetime { get; set; }
        public DateTime CandleDatetime { get; set; }

        [Column(TypeName = "decimal(12,6)")]
        public decimal Ask { get; set; }
        [Column(TypeName = "decimal(12,6)")]
        public decimal Bid { get; set; }

        /// <summary>
        /// period in real call APIs according to minutes but on generate from candels acording to step
        /// </summary>
         [Column(TypeName = "decimal(12,6)")]
        public decimal Period { get; set; }
        public int SymbolId { get; set; }

        public bool Tradeable { get; set; } = true;

    }
}
