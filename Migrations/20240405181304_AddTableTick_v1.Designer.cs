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
    [Migration("20240405181304_AddTableTick_v1")]
    partial class AddTableTick_v1
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

                    b.HasKey("Id")
                        .HasName("PK__PreProce__3214EC07CE0CEDF9");

                    b.ToTable("Candle");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Tick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandleId")
                        .HasColumnType("int");

                    b.Property<decimal>("Close")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id")
                        .HasName("PK__Tick__3214EC07A4460751");

                    b.HasIndex("CandleId");

                    b.ToTable("Tick", (string)null);
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.VolumeProfiler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandlesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DecreaseNOpip")
                        .HasColumnType("bit");

                    b.Property<bool?>("DontChange")
                        .HasColumnType("bit");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("IncreaceNOpip")
                        .HasColumnType("bit");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("NoMOPipWillTouch")
                        .HasColumnType("bit");

                    b.Property<bool?>("Tl13mOPipWillTouch")
                        .HasColumnType("bit");

                    b.Property<bool?>("TpMOPipWillTouch")
                        .HasColumnType("bit");

                    b.Property<int?>("_48Or72lastBar32x24bar")
                        .HasColumnType("int");

                    b.Property<int?>("_48Or72lastBar50x24bar")
                        .HasColumnType("int");

                    b.Property<int?>("lastBar32x24bar")
                        .HasColumnType("int");

                    b.Property<int?>("lastBar50x24bar")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__VolumeProfiler__3214EC07A4460751");

                    b.HasIndex("CandlesId");

                    b.ToTable("VolumeProfiler", (string)null);
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Tick", b =>
                {
                    b.HasOne("PreprocessDataStocks.Models.Candle", "Candle")
                        .WithMany("Ticks")
                        .HasForeignKey("CandleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candle");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.VolumeProfiler", b =>
                {
                    b.HasOne("PreprocessDataStocks.Models.Candle", "Candles")
                        .WithMany("VolumeProfilers")
                        .HasForeignKey("CandlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candles");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Candle", b =>
                {
                    b.Navigation("Ticks");

                    b.Navigation("VolumeProfilers");
                });
#pragma warning restore 612, 618
        }
    }
}
