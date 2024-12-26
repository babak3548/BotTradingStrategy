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
    [Migration("20240426184048_chageTypeColVolume_v1")]
    partial class chageTypeColVolume_v1
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

                    b.Property<DateTime>("CloseTickDatetime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CloseTickPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CounterCandelDiff")
                        .HasColumnType("int");

                    b.Property<string>("History")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LastBarsRepetion")
                        .HasColumnType("int");

                    b.Property<decimal>("LastTotalBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LotSize")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OpenTickDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OpenTickPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OrderDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.Property<decimal>("PeriodTickMin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReasonCloseOrders")
                        .HasColumnType("int");

                    b.Property<decimal>("Revenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("StopLoss")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SymbolId")
                        .HasColumnType("int");

                    b.Property<decimal>("TakeProfit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VPlow_high")
                        .HasColumnType("decimal(18,2)");

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

                    b.Property<decimal>("ConfirmTrendNum")
                        .HasColumnType("decimal(6,6)");

                    b.Property<byte>("EfectedVolumeOnNextCandles")
                        .HasColumnType("tinyint");

                    b.Property<string>("Exchange")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<DateTime>("LastCandleUpdated")
                        .HasColumnType("datetime2");

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

                    b.Property<decimal>("StopLossMultiple")
                        .HasColumnType("decimal(2,1)");

                    b.Property<decimal>("TakeProfitMultiple")
                        .HasColumnType("decimal(2,1)");

                    b.Property<decimal>("VpLowHigh")
                        .HasColumnType("decimal(6,6)");

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

                    b.Property<DateTime>("CandleDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CandleId")
                        .HasColumnType("int");

                    b.Property<decimal>("Period")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SymbolId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TickDatetime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CandleId");

                    b.ToTable("Tick");
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
                        .HasColumnType("decimal(8,6)");

                    b.Property<int?>("LastBarRepetationVolume")
                        .HasColumnType("int");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(8,6)");

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
