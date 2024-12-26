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
    [Migration("20240401210354_AddAutoIncrementToId")]
    partial class AddAutoIncrementToId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PreprocessDataStocks.Models.PreProcessDatum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Close")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("close");

                    b.Property<DateTime?>("Datetime")
                        .HasColumnType("datetime")
                        .HasColumnName("datetime");

                    b.Property<bool?>("DecreaseNOpip")
                        .HasColumnType("bit")
                        .HasColumnName("decrease_N_opip");

                    b.Property<bool?>("DontChange")
                        .HasColumnType("bit")
                        .HasColumnName("dontChange");

                    b.Property<decimal?>("High")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("high");

                    b.Property<bool?>("IncreaceNOpip")
                        .HasColumnType("bit")
                        .HasColumnName("increace_N_opip");

                    b.Property<decimal?>("Low")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("low");

                    b.Property<bool?>("NoMOPipWillTouch")
                        .HasColumnType("bit")
                        .HasColumnName("No_M_oPip_willTouch");

                    b.Property<decimal?>("Open")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("open");

                    b.Property<bool?>("Tl13mOPipWillTouch")
                        .HasColumnType("bit")
                        .HasColumnName("TL_1_3M_oPip_willTouch");

                    b.Property<bool?>("TpMOPipWillTouch")
                        .HasColumnType("bit")
                        .HasColumnName("TP_M_oPip_willTouch");

                    b.Property<int?>("_48Or72lastBar32x24bar")
                        .HasColumnType("int")
                        .HasColumnName("48_or_72LastBar_32x24bar");

                    b.Property<int?>("_48Or72lastBar50x24bar")
                        .HasColumnType("int")
                        .HasColumnName("48_or_72LastBar_50x24bar");

                    b.HasKey("Id")
                        .HasName("PK__PreProce__3214EC07CE0CEDF9");

                    b.ToTable("PreProcessData");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Table__3214EC07A4460751");

                    b.ToTable("Table", (string)null);
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.Table1", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Table1__3214EC07CCC77CF8");

                    b.ToTable("Table1", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
