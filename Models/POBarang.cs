namespace InventoryManagement.Models
{
    public class POBarang
    {
        public int IdPOBarang { get; set; } // Primary Key
        public int KodeRO { get; set; }
        public string Vendor { get; set; }
        public string KodeGudang { get; set; }
        public string KodeMaterial { get; set; }
        public string DeskripsiMaterial { get; set; }
        public string BaseUnit { get; set; }
        public int Jumlah { get; set; }
        public int HargaPerUnit { get; set; }
        public DateTime EtaDate { get; set; }
    }
}
