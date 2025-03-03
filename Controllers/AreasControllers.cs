using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class AreasController : Controller
    {
        private readonly InventoryContext _context;

        public AreasController(InventoryContext context)
        {
            _context = context;
        }

        // **1️⃣ READ: Tampilkan daftar area**
        public async Task<IActionResult> Index()
        {
            var areas = await _context.Areas.ToListAsync();
            return View(areas);
        }

        // **2️⃣ CREATE: Form tambah area**
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NamaArea")] Areas area)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Validasi gagal: " + string.Join("; ", ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage));
                return View(area);
            }

            try
            {
                _context.Add(area);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Area berhasil ditambahkan!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Gagal menambahkan area: " + ex.Message;
                return View(area);
            }
        }

        // **3️⃣ UPDATE: Form edit area**
        public async Task<IActionResult> Edit(int id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                TempData["ErrorMessage"] = "Area tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAreas, NamaArea")] Areas area)
        {
            if (id != area.IdAreas)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Validasi gagal: " + string.Join("; ", ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage));
                return View(area);
            }

            try
            {
                _context.Update(area);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Area berhasil diperbarui!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Gagal memperbarui area: " + ex.Message;
                return View(area);
            }
        }

        // **4️⃣ DELETE: Konfirmasi hapus area**
        public async Task<IActionResult> Delete(int id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                TempData["ErrorMessage"] = "Area tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area != null)
            {
                _context.Areas.Remove(area);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Area berhasil dihapus!";
            }
            else
            {
                TempData["ErrorMessage"] = "Area tidak ditemukan.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
