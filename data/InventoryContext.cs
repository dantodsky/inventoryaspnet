using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

        public DbSet<Sumur> Sumurs { get; set; }
        public DbSet<KeperluanSumur> KeperluanSumurs { get; set; }
        public DbSet<StockBarang> StockBarangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relasi Sumur -> KeperluanSumur (One-to-Many)
            modelBuilder.Entity<KeperluanSumur>()
                .HasOne(ks => ks.Sumur)
                .WithMany(s => s.KeperluanSumurs)
                .HasForeignKey(ks => ks.IdSumur)
                .OnDelete(DeleteBehavior.Cascade);

            // Relasi KeperluanSumur -> StockBarang (Berdasarkan KodeMaterial)
            modelBuilder.Entity<KeperluanSumur>()
    .HasOne<StockBarang>()
    .WithMany()
    .HasForeignKey(ks => ks.KodeMaterial)
    .HasPrincipalKey(sb => sb.KodeMaterial)
    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
