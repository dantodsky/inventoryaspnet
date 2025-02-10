using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System.Linq;

namespace InventoryManagement.Controllers
{
    public class SumurController : Controller
    {
        private readonly InventoryContext _context;

        public SumurController(InventoryContext context)
        {
            _context = context;
        }

        // READ: Tampilkan daftar sumur
        public IActionResult Index()
        {
            var sumurs = _context.Sumurs.ToList();
            return View(sumurs);
        }

        // CREATE: Form tambah sumur
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sumur sumur)
        {
            if (ModelState.IsValid)
            {
                _context.Sumurs.Add(sumur);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Data sumur berhasil ditambahkan!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Terjadi kesalahan saat menambahkan data sumur.";
            return View(sumur);
        }

        // UPDATE: Form edit sumur
        public IActionResult Edit(int id)
        {
            var sumur = _context.Sumurs.FirstOrDefault(s => s.IdSumur == id);
            if (sumur == null)
            {
                TempData["ErrorMessage"] = "Data sumur tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }
            return View(sumur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Sumur sumur)
        {
            if (ModelState.IsValid)
            {
                _context.Sumurs.Update(sumur);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Data sumur berhasil diperbarui!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Terjadi kesalahan saat memperbarui data sumur.";
            return View(sumur);
        }

        // DELETE: Konfirmasi penghapusan sumur
        public IActionResult Delete(int id)
        {
            var sumur = _context.Sumurs.FirstOrDefault(s => s.IdSumur == id);
            if (sumur == null)
            {
                TempData["ErrorMessage"] = "Data sumur tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }
            return View(sumur);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sumur = _context.Sumurs.FirstOrDefault(s => s.IdSumur == id);
            if (sumur != null)
            {
                _context.Sumurs.Remove(sumur);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Data sumur berhasil dihapus!";
            }
            else
            {
                TempData["ErrorMessage"] = "Data sumur tidak ditemukan.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
