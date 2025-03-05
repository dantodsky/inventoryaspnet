using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        // **DbSet untuk setiap tabel**
        public DbSet<Sumur> Sumurs { get; set; }
        public DbSet<KeperluanSumur> KeperluanSumurs { get; set; }
        public DbSet<StockBarang> StockBarangs { get; set; }
        public DbSet<StockHistory> StockHistories { get; set; }
        public DbSet<KeperluanHistory> KeperluanHistories { get; set; }
        public DbSet<Areas> Areas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ **Relasi Sumur -> Areas (One-to-Many)**
            modelBuilder.Entity<Sumur>()
                .HasOne(s => s.Area)
                .WithMany(a => a.Sumurs)
                .HasForeignKey(s => s.IdAreas)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ **Relasi Sumur -> KeperluanSumur (One-to-Many)**
            modelBuilder.Entity<KeperluanSumur>()
                .HasOne(ks => ks.Sumur)
                .WithMany(s => s.KeperluanSumurs) // **Gunakan KeperluanSumurs, bukan KeperluanHistories**
                .HasForeignKey(ks => ks.IdSumur)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ **Relasi KeperluanSumur -> StockBarang (One-to-Many, Berdasarkan KodeMaterial)**
            modelBuilder.Entity<KeperluanSumur>()
                .HasOne(ks => ks.StockBarang) 
                .WithMany()
                .HasForeignKey(ks => ks.KodeMaterial)
                .HasPrincipalKey(sb => sb.KodeMaterial)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ **Relasi StockHistory -> StockBarang (One-to-Many)**
            modelBuilder.Entity<StockHistory>()
                .HasOne(sh => sh.StockBarang)
                .WithMany()
                .HasForeignKey(sh => sh.KodeMaterial)
                .HasPrincipalKey(sb => sb.KodeMaterial)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ **Relasi KeperluanHistories -> KeperluanSumur, Sumur, dan StockBarang**
            modelBuilder.Entity<KeperluanHistory>()
                .HasOne(kh => kh.KeperluanSumur)
                .WithMany()
                .HasForeignKey(kh => kh.IdKeperluans);

            modelBuilder.Entity<KeperluanHistory>()
                .HasOne(kh => kh.Sumur)
                .WithMany()
                .HasForeignKey(kh => kh.IdSumur);

            modelBuilder.Entity<KeperluanHistory>()
                .HasOne(kh => kh.StockBarang)
                .WithMany()
                .HasForeignKey(kh => kh.KodeMaterial)
                .HasPrincipalKey(sb => sb.KodeMaterial);
        }
    }
}
