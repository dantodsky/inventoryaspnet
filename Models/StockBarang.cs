using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class StockBarang
    {
        [Key]
        public int IdBarang { get; set; }

        [Required]
        public string KodeGudang { get; set; }

        [Required]
        public string KodeMaterial { get; set; }

        [Required]
        public string DeskripsiMaterial { get; set; }

        [Required]
        public string BaseUnit { get; set; }

        [Required]
        public int Jumlah { get; set; }

        [Required]
        public int HargaPerUnit { get; set; }

        public int TotalHargaBarang => Jumlah * HargaPerUnit; // Properti kalkulasi
    }
}
