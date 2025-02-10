using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InventoryManagement.Models
{
    public class Sumur
    {
        [Key]
        public int IdSumur { get; set; }

        [Required]
        public string DaerahSumur { get; set; }

        [Required]
        public string NamaSumur { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        // Relasi ke KeperluanSumur
        public List<KeperluanSumur>? KeperluanSumurs { get; set; }
    }
}
