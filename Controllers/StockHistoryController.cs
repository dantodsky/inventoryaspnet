using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System;
using System.Linq;

namespace InventoryManagement.Controllers
{
    public class StockHistoryController : Controller
    {
        private readonly InventoryContext _context;

        public StockHistoryController(InventoryContext context)
        {
            _context = context;
        }

        // **READ: Menampilkan daftar histori stok**
        public IActionResult Index()
        {
            var stockHistories = _context.StockHistories
                .Include(sh => sh.StockBarang)  // **Hubungkan dengan StockBarang**
                .OrderByDescending(sh => sh.TanggalTransaksi)
                .ToList();

            return View(stockHistories);
        }

        // **CREATE: Form tambah histori stok**
        public IActionResult Create()
        {
            ViewData["Materials"] = _context.StockBarangs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StockHistory stockHistory)
        {
            if (ModelState.IsValid)
            {
                // Ambil stok awal dari histori terakhir atau dari StockBarangs
                var lastRecord = _context.StockHistories
                    .Where(s => s.KodeMaterial == stockHistory.KodeMaterial)
                    .OrderByDescending(s => s.TanggalTransaksi)
                    .FirstOrDefault();

                int stokAwal = lastRecord != null ? lastRecord.Balance : _context.StockBarangs
                    .Where(sb => sb.KodeMaterial == stockHistory.KodeMaterial)
                    .Select(sb => sb.Jumlah)
                    .FirstOrDefault();

                // **Hanya menambah stok, tidak ada pengurangan di sini**
                stockHistory.StokAwal = stokAwal;
                stockHistory.Balance = stokAwal + stockHistory.BarangMasuk;

                _context.StockHistories.Add(stockHistory);
                _context.SaveChanges();

                // **Tambahkan stok di StockBarangs**
                UpdateStockBarangs(stockHistory.KodeMaterial, stockHistory.Balance);

                return RedirectToAction(nameof(Index));
            }
            return View(stockHistory);
        }

        // **EDIT: Form edit histori stok**
        public IActionResult Edit(int id)
        {
            var stockHistory = _context.StockHistories.Find(id);
            if (stockHistory == null) return NotFound();

            ViewData["Materials"] = _context.StockBarangs.ToList();
            return View(stockHistory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, StockHistory stockHistory)
        {
            if (id != stockHistory.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.StockHistories.Update(stockHistory);
                _context.SaveChanges();

                // **Update jumlah di StockBarangs**
                UpdateStockBarangs(stockHistory.KodeMaterial, stockHistory.Balance);

                return RedirectToAction(nameof(Index));
            }
            return View(stockHistory);
        }

        // **DELETE: Konfirmasi hapus histori stok**
        public IActionResult Delete(int id)
        {
            var stockHistory = _context.StockHistories.Find(id);
            if (stockHistory == null) return NotFound();

            return View(stockHistory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var stockHistory = _context.StockHistories.Find(id);
            if (stockHistory != null)
            {
                _context.StockHistories.Remove(stockHistory);
                _context.SaveChanges();

                // **Update ulang stok di StockBarangs berdasarkan histori terbaru**
                var lastRecord = _context.StockHistories
                    .Where(s => s.KodeMaterial == stockHistory.KodeMaterial)
                    .OrderByDescending(s => s.TanggalTransaksi)
                    .FirstOrDefault();

                int newBalance = lastRecord != null ? lastRecord.Balance : 0;
                UpdateStockBarangs(stockHistory.KodeMaterial, newBalance);
            }
            return RedirectToAction(nameof(Index));
        }

        // **METHOD: Update Stok di StockBarangs setelah transaksi**
        private void UpdateStockBarangs(string kodeMaterial, int balance)
        {
            var stockBarang = _context.StockBarangs.FirstOrDefault(sb => sb.KodeMaterial == kodeMaterial);
            if (stockBarang != null)
            {
                stockBarang.Jumlah = balance;
                _context.StockBarangs.Update(stockBarang);
                _context.SaveChanges();
            }
        }
    }
}
