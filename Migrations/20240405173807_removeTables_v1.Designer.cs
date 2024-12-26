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
    [Migration("20240405173807_removeTables_v1")]
    partial class removeTables_v1
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

                    b.ToTable("PreProcessData");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.VolumeProfiler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<int>("PreProcessDatumId")
                        .HasColumnType("int");

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

                    b.HasIndex("PreProcessDatumId");

                    b.ToTable("VolumeProfiler", (string)null);
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.VolumeProfiler", b =>
                {
                    b.HasOne("PreprocessDataStocks.Models.PreProcessDatum", "PreProcessDatum")
                        .WithMany("VolumeProfilers")
                        .HasForeignKey("PreProcessDatumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PreProcessDatum");
                });

            modelBuilder.Entity("PreprocessDataStocks.Models.PreProcessDatum", b =>
                {
                    b.Navigation("VolumeProfilers");
                });
#pragma warning restore 612, 618
        }
    }
}
