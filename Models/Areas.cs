using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Areas
    {
        [Key]
        public int IdAreas { get; set; }

        [Required(ErrorMessage = "Nama Area wajib diisi")]
        [StringLength(255, ErrorMessage = "Nama Area maksimal 255 karakter")]
        public string? NamaArea { get; set; } // ✅ Jadikan Nullable

        // **One-to-Many ke Sumurs (Tidak Wajib)**
        public ICollection<Sumur>? Sumurs { get; set; } // ✅ Tidak wajib memiliki Sumur
    }
}
