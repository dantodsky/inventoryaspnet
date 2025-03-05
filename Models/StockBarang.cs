using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace InventoryManagement.Models
{
    public class StockBarang
    {
        [Key]
        public int IdBarang { get; set; }

        [Required]
        [StringLength(50)]
        public required string KodeGudang { get; set; }

        [Required]
        [StringLength(50)]
        public required string KodeMaterial { get; set; }

        [Required]
        [StringLength(100)]
        public required string DeskripsiMaterial { get; set; }

        [Required]
        [StringLength(20)]
        public required string BaseUnit { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Harga harus lebih dari 0")]
        public int HargaPerUnit { get; set; }

        // ✅ **Relasi ke StockHistory**
        public virtual ICollection<StockHistory> StockHistories { get; set; } = new List<StockHistory>();

        // ✅ **Relasi ke KeperluanSumur**
        public virtual ICollection<KeperluanSumur> KeperluanSumurs { get; set; } = new List<KeperluanSumur>();

        // ✅ **Relasi ke KeperluanHistories**
        public virtual ICollection<KeperluanHistory> KeperluanHistories { get; set; } = new List<KeperluanHistory>();

        // ✅ **Properti Perhitungan Jumlah Stok**
        [NotMapped]
        public int Jumlah => (StockHistories?.Sum(sh => sh.BarangMasuk) ?? 0) - 
                             (KeperluanHistories?.Sum(kh => kh.JumlahKeluar) ?? 0);

        // ✅ **Properti Total Harga Barang**
        [NotMapped]
        public int TotalHargaBarang => Jumlah * HargaPerUnit;
    }
}
