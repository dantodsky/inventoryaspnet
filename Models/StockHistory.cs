using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class StockHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? KodeMaterial { get; set; }  // ✅ Jadikan Nullable

        [ForeignKey("KodeMaterial")]
        public StockBarang? StockBarang { get; set; }  // ✅ Navigasi Nullable

        [Required]
        public DateTime TanggalTransaksi { get; set; }

        [Required]
        public int StokAwal { get; set; }

        [Required]
        public int BarangMasuk { get; set; }

        public int BarangKeluar { get; set; } = 0;  // ✅ Default ke 0

        [Required]
        public int Balance { get; set; }
    }
}
