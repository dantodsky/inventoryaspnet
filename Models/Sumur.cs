using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Sumur
    {
        [Key]
        public int IdSumur { get; set; }

        [Required(ErrorMessage = "Nama Sumur wajib diisi")]
        [StringLength(255)]
        public string NamaSumur { get; set; }

        [Required(ErrorMessage = "Tanggal Mulai wajib diisi")]
        public DateTime StartDate { get; set; }

        // **Foreign Key ke Areas**
        [Required(ErrorMessage = "Pilih Daerah Sumur")]
        public int IdAreas { get; set; }

        [ForeignKey("IdAreas")]
        public Areas? Area { get; set; } // ✅ Jadikan nullable agar tidak divalidasi

        public ICollection<KeperluanSumur>? KeperluanSumurs { get; set; }
    }
}
