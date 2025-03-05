using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class KeperluanHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdKeperluans { get; set; }

        [ForeignKey("IdKeperluans")]
        public KeperluanSumur? KeperluanSumur { get; set; }

        [Required]
        public int IdSumur { get; set; }

        [ForeignKey("IdSumur")]
        public Sumur? Sumur { get; set; }

        [Required]
        [StringLength(50)]
        public string KodeMaterial { get; set; } = string.Empty;

        [ForeignKey("KodeMaterial")]
        public StockBarang? StockBarang { get; set; }

        [Required]
        public DateTime Tanggal { get; set; }

        [Required]
        public int JumlahKeluar { get; set; }
    }
}
