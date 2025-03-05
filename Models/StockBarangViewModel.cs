using System;

namespace InventoryManagement.Models
{
    public class StockBarangViewModel
    {
        public int IdBarang { get; set; }
        public string? KodeMaterial { get; set; } // ✅ Jadikan Nullable
        public string? DeskripsiMaterial { get; set; } // ✅ Jadikan Nullable
        public string? BaseUnit { get; set; } // ✅ Jadikan Nullable
        public int HargaPerUnit { get; set; }

        public int BarangMasuk { get; set; }
        public int BarangKeluar { get; set; }
        public int StokTersedia { get; set; }
        
        public DateTime? TanggalTerakhirMasuk { get; set; } // ✅ Jadikan Nullable
    }
}
