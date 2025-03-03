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
        public DbSet<Areas> Areas { get; set; } // ✅ Tambahkan DbSet untuk Areas

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ **Relasi Sumur -> Areas (One-to-Many)**
            modelBuilder.Entity<Sumur>()
                .HasOne(s => s.Area)   // Properti navigasi di Sumur
                .WithMany(a => a.Sumurs) // Relasi ke Areas
                .HasForeignKey(s => s.IdAreas)
                .OnDelete(DeleteBehavior.Restrict); // Jangan hapus otomatis jika area dihapus

            // ✅ **Relasi Sumur -> KeperluanSumur (One-to-Many)**
            modelBuilder.Entity<KeperluanSumur>()
                .HasOne(ks => ks.Sumur)
                .WithMany(s => s.KeperluanSumurs)
                .HasForeignKey(ks => ks.IdSumur)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ **Relasi KeperluanSumur -> StockBarang (Berdasarkan KodeMaterial)**
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
        }
    }
}
