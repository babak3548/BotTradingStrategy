﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PreprocessDataStocks.Models;

#nullable disable

namespace PreprocessDataStocks.Migrations
{
    [DbContext(typeof(PreprocessDataStockContext))]
    [Migration("20240627044306_changeCollName_table_v1")]
    partial class changeCollName_table_v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PreprocessDataStocks.Models.Candle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Close")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("close");

                    b.Property<DateTime?>("Datetime")
                        .HasColumnType("datetime")
                        .HasColumnName("datetime");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("high");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("low");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("open");

                    b.Property<int>("SymbolId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__PreProce__3214EC07CE0CEDF9");

                    b.ToTable("Candle");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AccountMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CloseTickDatetime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CloseTickPrice")
                        .HasColumnType("decimal(14,6)");

                    b.Property<int>("CounterCandelDiff")
                        .HasColumnType("int");

                    b.Property<decimal>("ExClosePosAccountBalance")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExClosePosCloseoutAsk")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExClosePosCloseoutBid")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExClosePosCommission")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExClosePosFinancing")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("ExClosePosFullResponseBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExClosePosFullVWAP")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExClosePosGainQuoteHomeConversionFactor")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExClosePosHalfSpreadCost")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExClosePosLossQuoteHomeConversionFactor")
                        .HasColumnType("decimal(18,5)");

                    b.Property<int>("ExClosePosMarketStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("ExClosePosPL")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExClosePosPriceTran")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("ExClosePosReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExClosePosRelatedTransactionIDs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExClosePosRequestedUnits")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("ExClosePosSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExClosePosTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExClosePosTradeIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExClosePosUnits")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespAccountBalance")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespCloseoutAsk")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespCloseoutBid")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespCommission")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespFinancing")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("ExCreateRespFullResponseBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExCreateRespGainQuoteHomeConversionFactor")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespHalfSpreadCost")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespInitialMarginRequired")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespLossQuoteHomeConversionFactor")
                        .HasColumnType("decimal(18,5)");

                    b.Property<int>("ExCreateRespMarketStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("ExCreateRespPL")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal>("ExCreateRespPriceTran")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("ExCreateRespReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExCreateRespRelatedTransactionIDs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExCreateRespTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExCreateRespTradeIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExCreateRespUnits")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("History")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LastBarsRepetion")
                        .HasColumnType("int");

                    b.Property<decimal>("LastTotalBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LotSize")
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("OpenTickDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OpenTickPrice")
                        .HasColumnType("decimal(14,6)");

                    b.Property<DateTime>("OrderDatetime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.Property<decimal>("PeriodTickMin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReasonCloseOrders")
                        .HasColumnType("int");

                    b.Property<decimal>("Revenue")
                        .HasColumnType("decimal(18,3)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("StopLoss")
                        .HasColumnType("decimal(14,6)");

                    b.Property<int>("SymbolId")
                        .HasColumnType("int");

                    b.Property<decimal>("TakeProfit")
                        .HasColumnType("decimal(14,6)");

                    b.Property<decimal>("VPlow_high")
                        .HasColumnType("decimal(14,6)");

                    b.Property<int>("VolumeProfilerId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Order__3214EC07A4460751");

                    b.HasIndex("VolumeProfilerId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Symbol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<decimal>("ApproximateHomeConversion")
                        .HasColumnType("decimal(12,6)");

                    b.Property<decimal>("AverageSpread")
                        .HasColumnType("decimal(14,6)");

                    b.Property<decimal>("ConfirmTrendNum")
                        .HasColumnType("decimal(12,6)");

                    b.Property<float>("ConversionHistoryRate")
                        .HasColumnType("real");

                    b.Property<float>("ConversionRealRate")
                        .HasColumnType("real");

                    b.Property<string>("Exchange")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<DateTime>("LastCandleProccessed")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("LimitsExceededOrder")
                        .HasColumnType("decimal(18,5)");

                    b.Property<byte>("MinRepetationVolumeNeededTrade")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<byte>("PercentageInvestTrade")
                        .HasColumnType("tinyint");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int>("PermisionTradeType")
                        .HasColumnType("int");

                    b.Property<byte>("RangeCandlesFoCalcVolume")
                        .HasColumnType("tinyint");

                    b.Property<byte>("RecentCandlesForVolumeSelector")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("StartCandle")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("StopLossMultiple")
                        .HasColumnType("decimal(3,1)");

                    b.Property<decimal>("TakeProfitMultiple")
                        .HasColumnType("decimal(3,1)");

                    b.Property<decimal>("VpLowHigh")
                        .HasColumnType("decimal(12,6)");

                    b.HasKey("Id")
                        .HasName("PK__Symbol__3214EC07A4460751");

                    b.ToTable("Symbol", (string)null);
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Tick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Ask")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AskLiquiditySum")
                        .HasColumnType("int");

                    b.Property<decimal>("Bid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BidLiquiditySum")
                        .HasColumnType("int");

                    b.Property<DateTime>("CandleDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CandleId")
                        .HasColumnType("int");

                    b.Property<decimal>("Period")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SymbolId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TickDatetime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Tradeable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CandleId");

                    b.ToTable("Tick");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.TickDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Ask")
                        .HasColumnType("decimal(12,6)");

                    b.Property<decimal>("Bid")
                        .HasColumnType("decimal(12,6)");

                    b.Property<DateTime>("CandleDatetime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Period")
                        .HasColumnType("decimal(12,6)");

                    b.Property<int>("SymbolId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TickDBDatetime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Tradeable")
                        .HasColumnType("bit");

                    b.HasKey("Id")
                        .HasName("PK__TickDB__3214EC07A4460751");

                    b.ToTable("TickDB", (string)null);
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.VolumeProfiler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(12,6)");

                    b.Property<int?>("LastBarRepetationVolume")
                        .HasColumnType("int");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(12,6)");

                    b.Property<int>("SymbolId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__VolumeProfiler__3214EC07A4460751");

                    b.HasIndex("CandleId");

                    b.ToTable("VolumeProfiler", (string)null);
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Order", b =>
                {
                    b.HasOne("PreprocessDataStocks.Models.VolumeProfiler", "VolumeProfiler")
                        .WithMany("Orders")
                        .HasForeignKey("VolumeProfilerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VolumeProfiler");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Tick", b =>
                {
                    b.HasOne("PreprocessDataStocks.Models.Candle", null)
                        .WithMany("Ticks")
                        .HasForeignKey("CandleId");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.VolumeProfiler", b =>
                {
                    b.HasOne("PreprocessDataStocks.Models.Candle", "Candle")
                        .WithMany("VolumeProfilers")
                        .HasForeignKey("CandleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candle");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Candle", b =>
                {
                    b.Navigation("Ticks");

                    b.Navigation("VolumeProfilers");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.VolumeProfiler", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
