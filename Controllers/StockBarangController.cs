using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System.Linq;

namespace InventoryManagement.Controllers
{
    public class StockBarangController : Controller
    {
        private readonly InventoryContext _context;

        public StockBarangController(InventoryContext context)
        {
            _context = context;
        }

        // READ: Tampilkan daftar stock barang
        public IActionResult Index()
        {
            var stockBarangs = _context.StockBarangs.ToList();
            return View(stockBarangs);
        }

        // CREATE: Form tambah stock barang
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StockBarang stockBarang)
        {
            if (ModelState.IsValid)
            {
                // Tambahkan ke database
                _context.StockBarangs.Add(stockBarang);
                _context.SaveChanges();

                // Berikan pesan sukses
                TempData["SuccessMessage"] = "Stock barang berhasil ditambahkan!";
                return RedirectToAction(nameof(Index));
            }

            // Jika validasi gagal
            TempData["ErrorMessage"] = "Terjadi kesalahan saat menambahkan stock barang.";
            return View(stockBarang);
        }

        // UPDATE: Form edit stock barang
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
        public IActionResult Edit(StockBarang stockBarang)
        {
            if (ModelState.IsValid)
            {
                // Update database
                _context.StockBarangs.Update(stockBarang);
                _context.SaveChanges();

                // Berikan pesan sukses
                TempData["SuccessMessage"] = "Stock barang berhasil diperbarui!";
                return RedirectToAction(nameof(Index));
            }

            // Jika validasi gagal
            TempData["ErrorMessage"] = "Terjadi kesalahan saat memperbarui stock barang.";
            return View(stockBarang);
        }

        // DELETE: Konfirmasi penghapusan stock barang
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
        public IActionResult DeleteConfirmed(int id)
        {
            var stockBarang = _context.StockBarangs.FirstOrDefault(sb => sb.IdBarang == id);
            if (stockBarang != null)
            {
                // Hapus dari database
                _context.StockBarangs.Remove(stockBarang);
                _context.SaveChanges();

                // Berikan pesan sukses
                TempData["SuccessMessage"] = "Stock barang berhasil dihapus!";
            }
            else
            {
                TempData["ErrorMessage"] = "Stock barang tidak ditemukan.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
