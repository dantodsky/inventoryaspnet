using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class StockBarangController : Controller
    {
        private readonly InventoryContext _context;

        public StockBarangController(InventoryContext context)
        {
            _context = context;
        }

        // ✅ **1️⃣ READ: Menampilkan daftar stock barang dengan perhitungan stok histori**
        public IActionResult Index()
        {
            var stockBarangs = _context.StockBarangs
                .Select(sb => new StockBarangViewModel
                {
                    IdBarang = sb.IdBarang,
                    KodeMaterial = sb.KodeMaterial,
                    DeskripsiMaterial = sb.DeskripsiMaterial,
                    BaseUnit = sb.BaseUnit,
                    HargaPerUnit = sb.HargaPerUnit,

                    BarangMasuk = _context.StockHistories
                        .Where(sh => sh.KodeMaterial == sb.KodeMaterial)
                        .Sum(sh => sh.BarangMasuk),

                    BarangKeluar = _context.KeperluanHistories
                        .Where(kh => kh.KodeMaterial == sb.KodeMaterial)
                        .Sum(kh => kh.JumlahKeluar),

                    StokTersedia = _context.StockHistories
                        .Where(sh => sh.KodeMaterial == sb.KodeMaterial)
                        .Sum(sh => sh.BarangMasuk)
                        - _context.KeperluanHistories
                        .Where(kh => kh.KodeMaterial == sb.KodeMaterial)
                        .Sum(kh => kh.JumlahKeluar),

                    TanggalTerakhirMasuk = _context.StockHistories
                        .Where(sh => sh.KodeMaterial == sb.KodeMaterial)
                        .OrderByDescending(sh => sh.TanggalTransaksi)
                        .Select(sh => sh.TanggalTransaksi)
                        .FirstOrDefault()
                })
                .ToList();

            return View(stockBarangs);
        }

        // ✅ **2️⃣ CREATE: Form tambah stock barang baru**
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockBarang stockBarang)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Pastikan semua data telah diisi dengan benar.";
                return View(stockBarang);
            }

            // **Cek apakah barang dengan KodeMaterial sudah ada**
            var existingStock = _context.StockBarangs.FirstOrDefault(sb => sb.KodeMaterial == stockBarang.KodeMaterial);

            if (existingStock != null)
            {
                TempData["ErrorMessage"] = "Kode Material sudah ada. Silakan gunakan kode lain.";
                return View(stockBarang);
            }

            // **Tambahkan barang ke StockBarang tanpa langsung mempengaruhi stok**
            _context.StockBarangs.Add(stockBarang);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Stock barang berhasil ditambahkan!";
            return RedirectToAction(nameof(Index));
        }

        // ✅ **3️⃣ EDIT: Form edit stock barang (tanpa mempengaruhi stok)**
        public IActionResult Edit(int id)
        {
            var stockBarang = _context.StockBarangs.FirstOrDefault(sb => sb.IdBarang == id);
            if (stockBarang == null)
            {
                TempData["ErrorMessage"] = "Stock barang tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }

            return View(stockBarang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockBarang stockBarang)
        {
            if (ModelState.IsValid)
            {
                _context.Update(stockBarang);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Data stock barang berhasil diperbarui!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Terjadi kesalahan saat memperbarui stock barang.";
            return View(stockBarang);
        }

        // ✅ **4️⃣ DELETE: Menghapus stock barang hanya jika tidak memiliki histori pemakaian**
        public IActionResult Delete(int id)
        {
            var stockBarang = _context.StockBarangs.FirstOrDefault(sb => sb.IdBarang == id);
            if (stockBarang == null)
            {
                TempData["ErrorMessage"] = "Stock barang tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }

            return View(stockBarang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockBarang = _context.StockBarangs.FirstOrDefault(sb => sb.IdBarang == id);

            if (stockBarang != null)
            {
                // **Cek apakah barang masih memiliki histori stok**
                bool adaStockHistory = _context.StockHistories.Any(sh => sh.KodeMaterial == stockBarang.KodeMaterial);
                bool adaKeperluanHistory = _context.KeperluanHistories.Any(kh => kh.KodeMaterial == stockBarang.KodeMaterial);

                if (!adaStockHistory && !adaKeperluanHistory)
                {
                    // **Jika tidak ada histori, boleh dihapus**
                    _context.StockBarangs.Remove(stockBarang);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Stock barang berhasil dihapus!";
                }
                else
                {
                    // **Jika masih ada histori, ubah status menjadi non-aktif**
                    TempData["ErrorMessage"] = "Stock barang tidak dapat dihapus karena memiliki histori pemakaian.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Stock barang tidak ditemukan.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
