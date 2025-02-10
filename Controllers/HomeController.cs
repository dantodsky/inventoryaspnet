using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace InventoryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly InventoryContext _context;

        public HomeController(InventoryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Dictionary konversi bulan dari angka ke format "JAN", "FEB", dst.
            var months = new Dictionary<int, string>
            {
                { 1, "JAN" }, { 2, "FEB" }, { 3, "MAR" }, { 4, "APR" }, { 5, "MEI" },
                { 6, "JUN" }, { 7, "JUL" }, { 8, "AUG" }, { 9, "SEP" }, { 10, "OCT" },
                { 11, "NOV" }, { 12, "DEC" }
            };

            // Ambil data dari database
            var rawData = _context.KeperluanSumurs
                .Join(_context.Sumurs, ks => ks.IdSumur, s => s.IdSumur, (ks, s) => new
                {
                    ks.KodeMaterial,
                    ks.DeskripsiMaterial,
                    Bulan = months[s.StartDate.Month], // Konversi angka bulan ke format JAN, FEB, dst.
                    TotalKebutuhan = ks.Jumlah
                })
                .ToList();

            var detailInventory = rawData
                .GroupBy(km => new { km.KodeMaterial, km.DeskripsiMaterial })
                .Select(g =>
                {
                    var stokAwal = _context.StockBarangs
                        .Where(sb => sb.KodeMaterial == g.Key.KodeMaterial)
                        .Select(sb => sb.Jumlah)
                        .FirstOrDefault();

                    // Pastikan semua bulan memiliki nilai default 0
                    var jumlahSumur = months.Values.ToDictionary(m => m, m => g.Where(k => k.Bulan == m).Count());
                    var kebutuhan = months.Values.ToDictionary(m => m, m => g.Where(k => k.Bulan == m).Sum(k => k.TotalKebutuhan));
                    var stok = new Dictionary<string, int>();
                    var balance = new Dictionary<string, int>();

                    // Inisialisasi stok dan balance
                    int stokSebelumnya = stokAwal;

                    foreach (var bulan in months.Values)
                    {
                        stok[bulan] = stokSebelumnya;
                        balance[bulan] = stokSebelumnya - kebutuhan[bulan];
                        stokSebelumnya = balance[bulan]; // Update stok untuk bulan berikutnya
                    }

                    return new
                    {
                        KodeMaterial = g.Key.KodeMaterial,
                        DeskripsiMaterial = g.Key.DeskripsiMaterial,
                        JumlahSumur = jumlahSumur,
                        Kebutuhan = kebutuhan,
                        Stok = stok,
                        Balance = balance
                    };
                })
                .ToList();

            ViewBag.DetailInventory = detailInventory;
            return View();
        }
    }
}
