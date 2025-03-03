using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            // **Ambil semua data**
            var keperluanSumurs = _context.KeperluanSumurs.Include(k => k.Sumur).ToList();
            var stockHistories = _context.StockHistories.ToList();
            var stockBarangs = _context.StockBarangs.ToList();

            // **Dictionary untuk menyimpan data per material**
            var inventoryDetail = new Dictionary<string, dynamic>();

            foreach (var material in stockBarangs)
            {
                var materialCode = material.KodeMaterial;
                var materialName = material.DeskripsiMaterial;

                // **Inisialisasi per bulan**
                var jumlahSumur = new Dictionary<string, int>();
                var kebutuhan = new Dictionary<string, int>();
                var stok = new Dictionary<string, int>();
                var balance = new Dictionary<string, int>();

                foreach (var bulan in new[] { "JAN", "FEB", "MAR", "APR", "MEI", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" })
                {
                    jumlahSumur[bulan] = 0;
                    kebutuhan[bulan] = 0;
                    stok[bulan] = 0;
                    balance[bulan] = 0;
                }

                // **Hitung jumlah sumur dan kebutuhan material per bulan**
                foreach (var keperluan in keperluanSumurs.Where(k => k.KodeMaterial == materialCode))
                {
                    if (keperluan.Sumur != null)
                    {
                        string bulan = keperluan.Sumur.StartDate.ToString("MMM").ToUpper();
                        if (jumlahSumur.ContainsKey(bulan)) jumlahSumur[bulan]++;
                        if (kebutuhan.ContainsKey(bulan)) kebutuhan[bulan] += keperluan.Jumlah;
                    }
                }

                // **Ambil stok awal dari histori terakhir**
                int stokAwal = stockHistories
                    .Where(s => s.KodeMaterial == materialCode)
                    .OrderByDescending(s => s.TanggalTransaksi)
                    .Select(s => s.Balance)
                    .FirstOrDefault();

                if (stokAwal == 0) stokAwal = material.Jumlah; // Jika tidak ada histori, pakai jumlah di StockBarang

                int lastBalance = stokAwal;

                foreach (var bulan in new[] { "JAN", "FEB", "MAR", "APR", "MEI", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" })
                {
                    // **Ambil histori stok pada bulan tersebut**
                    var stockEntry = stockHistories
                        .Where(s => s.KodeMaterial == materialCode && s.TanggalTransaksi.ToString("MMM").ToUpper() == bulan)
                        .OrderBy(s => s.TanggalTransaksi)
                        .ToList();

                    stok[bulan] = stockEntry.Any() ? stockEntry.Last().Balance : lastBalance;

                    // **Kurangi stok berdasarkan kebutuhan sumur bulan itu**
                    lastBalance = stok[bulan] - kebutuhan[bulan];

                    balance[bulan] = lastBalance;
                }

                // **Masukkan data ke dictionary**
                inventoryDetail[materialCode] = new
                {
                    KodeMaterial = materialCode,
                    DeskripsiMaterial = materialName,
                    JumlahSumur = jumlahSumur,
                    Kebutuhan = kebutuhan,
                    Stok = stok,
                    Balance = balance
                };
            }

            // **Kirim data ke View**
            ViewBag.DetailInventory = inventoryDetail.Values.ToList();

            return View();
        }
    }
}
