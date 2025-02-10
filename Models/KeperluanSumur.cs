using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class KeperluanSumur
    {
        [Key]
        public int IdKeperluans { get; set; }

        [Required(ErrorMessage = "Sumur harus dipilih.")]
        public int IdSumur { get; set; }

        [ForeignKey("IdSumur")]
        public Sumur? Sumur { get; set; }

        [Required(ErrorMessage = "Kode Material harus diisi.")]
        [StringLength(50, ErrorMessage = "Kode Material tidak boleh lebih dari 50 karakter.")]
        public string KodeMaterial { get; set; } = string.Empty;

        [Required(ErrorMessage = "Deskripsi Material harus diisi.")]
        [StringLength(100, ErrorMessage = "Deskripsi Material tidak boleh lebih dari 100 karakter.")]
        public string DeskripsiMaterial { get; set; } = string.Empty;

        [Required(ErrorMessage = "Satuan harus diisi.")]
        [StringLength(20, ErrorMessage = "Satuan tidak boleh lebih dari 20 karakter.")]
        public string BaseUnit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Jumlah harus diisi.")]
        [Range(1, int.MaxValue, ErrorMessage = "Jumlah harus lebih dari 0.")]
        public int Jumlah { get; set; }
    }
}
