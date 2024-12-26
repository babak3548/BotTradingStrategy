using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PreprocessDataStocks.Models;

public partial class PreprocessDataStockContext : DbContext
{
    public PreprocessDataStockContext()
    {
    }

    public PreprocessDataStockContext(DbContextOptions<PreprocessDataStockContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candle> Candle { get; set; }
    public virtual DbSet<VolumeProfiler> VolumeProfiler { get; set; }
    //public virtual DbSet<Tick> Tick { get; set; }
    public virtual DbSet<Order> Order { get; set; }
    public virtual DbSet<Symbol> Symbols { get; set; }
    public virtual DbSet<TickDB> TickDBs { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(ConfigBot.ConnectionString);
            //"Server=(localdb)\\MSSQLLocalDB;Database=PreprocessDataStock;Trusted_Connection=True;TrustServerCertificate=true;"); - 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PreProce__3214EC07CE0CEDF9");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();//.ValueGeneratedNever();
            entity.Property(e => e.Close)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("close");
                entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.Low)
    .HasColumnType("decimal(18, 4)")
    .HasColumnName("low");
            entity.Property(e => e.High)
    .HasColumnType("decimal(18, 4)")
    .HasColumnName("high");

            entity.Property(e => e.Open)
    .HasColumnType("decimal(18, 4)")
    .HasColumnName("open");

        });
        modelBuilder.Entity<VolumeProfiler>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VolumeProfiler__3214EC07A4460751");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("VolumeProfiler");

           
        });
        //modelBuilder.Entity<Tick>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PK__Tick__3214EC07A4460751");
        //    entity.Property(e => e.Id).ValueGeneratedOnAdd();
        //    entity.ToTable("Tick");
        //});
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07A4460751");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.ToTable("Order");
        });
        modelBuilder.Entity<Symbol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Symbol__3214EC07A4460751");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.ToTable("Symbol");
        });
        modelBuilder.Entity<TickDB>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TickDB__3214EC07A4460751");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.ToTable("TickDB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
