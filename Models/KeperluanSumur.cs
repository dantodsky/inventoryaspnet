using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class KeperluanSumur
    {
        [Key]
        public int IdKeperluans { get; set; }

        [Required]
        public int IdSumur { get; set; }

        [ForeignKey("IdSumur")]
        public Sumur? Sumur { get; set; }

        [Required]
        [StringLength(50)]
        public string KodeMaterial { get; set; } = string.Empty;

        [ForeignKey("KodeMaterial")]
        public StockBarang? StockBarang { get; set; } // **✅ Pastikan relasi ini benar!**

        [Required]
        [StringLength(100)]
        public string DeskripsiMaterial { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string BaseUnit { get; set; } = string.Empty;

        [Required]
        public int Jumlah { get; set; }
    }
}
