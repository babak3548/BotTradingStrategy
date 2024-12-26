using PreprocessDataStocks.CandleConsumerModule.TickConsumerModule;
using PreprocessDataStocks.PartnerApi.CommonModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PreprocessDataStocks.Models
{
    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }
        public int VolumeProfilerId { get; set; }
        [Column(TypeName = "decimal(14,6)")]
        public decimal OpenTickPrice { get; set; }
        public DateTime OpenTickDateTime { get; set; }
        public DateTime CloseTickDatetime { get; set; }
        public DateTime OrderDatetime { get; set; }
        public int? LastBarsRepetion { get; set; }
        public int CounterCandelDiff { get; set; }
        public OrderTypes OrderType { get; set; }
        [Column(TypeName = "decimal(14,6)")]
        public decimal TakeProfit { get; set; }
        [Column(TypeName = "decimal(14,6)")]
        public decimal StopLoss { get; set; }
        public VolumeProfiler VolumeProfiler { get; set;}
        [Column(TypeName = "decimal(14,6)")]
        public decimal VPlow_high { get;  set; }

        public decimal PeriodTickMin { get;  set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal LotSize { get;  set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderMargin { get;  set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AccountMargin { get;  set; }
        public ReasonCloseOrders ReasonCloseOrders { get;  set; }
        public string History { get;  set; } = string.Empty;
        public OrderStatus Status { get;  set; }
       public int SymbolId { get; set; }
        //[ForeignKey("SymbolId")]
        //  public Symbol Symbol { get; set; }
        [Column(TypeName = "decimal(14,6)")]
        public decimal CloseTickPrice { get;  set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Revenue { get;  set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastTotalBalance { get;  set; }
        // ---------- colse postion response ----------------------------------------------------------------
        public LiveOperationStatus ExClosePosMarketStatus { get;  set; }
        public string ExClosePosTradeIds { get; set; } =  string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosCloseoutAsk { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosGainQuoteHomeConversionFactor { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosLossQuoteHomeConversionFactor { get; set; }

        public string ExClosePosFullResponseBody { get; set; }= string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosCloseoutBid { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosFullVWAP { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosPriceTran { get; set; }
        public string ExClosePosSymbol { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosRequestedUnits { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosHalfSpreadCost { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosFinancing { get; set; }
        public string ExClosePosRelatedTransactionIDs { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosCommission { get; set; }
        public string ExClosePosReason { get; set; }  = string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosAccountBalance { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosPL { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExClosePosUnits { get; set; }
        public DateTime ExClosePosTime { get; set; }
        

        //------------Create Order Response --------------------------------------------------
        public LiveOperationStatus ExCreateRespMarketStatus { get;  set; }
        public string ExCreateRespTradeIds { get;  set; } = string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespCloseoutAsk { get;  set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespGainQuoteHomeConversionFactor { get;  set; }
        public string ExCreateRespFullResponseBody { get;  set; } = string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespCloseoutBid { get;  set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespPriceTran { get;  set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespLossQuoteHomeConversionFactor { get;  set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespHalfSpreadCost { get;  set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespFinancing { get;  set; }
        public string ExCreateRespRelatedTransactionIDs { get;  set; } = string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespCommission { get;  set; }
        public string ExCreateRespReason { get;  set; } = string.Empty;
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespAccountBalance { get;  set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespPL { get;  set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespUnits { get;  set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal ExCreateRespInitialMarginRequired { get; set; }
        public DateTime ExCreateRespTime { get;  set; }


    }
}
