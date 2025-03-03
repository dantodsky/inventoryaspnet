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
    public string KodeMaterial { get; set; }

    [ForeignKey("KodeMaterial")]
    public StockBarang? StockBarang { get; set; }

    [Required]
    public DateTime TanggalTransaksi { get; set; }

    [Required]
    public int StokAwal { get; set; }

    [Required]
    public int BarangMasuk { get; set; }

    public int BarangKeluar { get; set; } = 0;  // **Tambahkan properti ini**

    [Required]
    public int Balance { get; set; }
}

}
