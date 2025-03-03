using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class SumurController : Controller
    {
        private readonly InventoryContext _context;

        public SumurController(InventoryContext context)
        {
            _context = context;
        }

        // **1️⃣ READ: Tampilkan daftar sumur**
        public async Task<IActionResult> Index()
        {
            var sumurs = await _context.Sumurs
                .Include(s => s.Area) // ✅ Include Area agar NamaArea bisa diakses
                .ToListAsync();
            return View(sumurs);
        }

        // **2️⃣ CREATE: Form tambah sumur**
        public IActionResult Create()
        {
            ViewData["Areas"] = _context.Areas.ToList(); // ✅ Kirim daftar Area ke dropdown
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NamaSumur, StartDate, IdAreas")] Sumur sumur)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Validasi gagal: " + string.Join("; ", errors);
                ViewData["Areas"] = _context.Areas.ToList();
                return View(sumur);
            }

            try
            {
                // **Cek apakah Area valid**
                var area = await _context.Areas.FindAsync(sumur.IdAreas);
                if (area == null)
                {
                    TempData["ErrorMessage"] = "Error: Area tidak ditemukan di database.";
                    ViewData["Areas"] = _context.Areas.ToList();
                    return View(sumur);
                }

                _context.Sumurs.Add(sumur);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Data sumur berhasil ditambahkan!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Gagal menambahkan sumur: " + ex.Message;
                ViewData["Areas"] = _context.Areas.ToList();
                return View(sumur);
            }
        }

        // **3️⃣ UPDATE: Form edit sumur**
        public async Task<IActionResult> Edit(int id)
        {
            var sumur = await _context.Sumurs.FindAsync(id);
            if (sumur == null)
            {
                TempData["ErrorMessage"] = "Data sumur tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Areas"] = _context.Areas.ToList(); // ✅ Kirim daftar Area ke dropdown
            return View(sumur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSumur, NamaSumur, StartDate, IdAreas")] Sumur sumur)
        {
            if (id != sumur.IdSumur)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Validasi gagal: " + string.Join("; ", errors);
                ViewData["Areas"] = _context.Areas.ToList();
                return View(sumur);
            }

            try
            {
                // **Cek apakah Area valid**
                var area = await _context.Areas.FindAsync(sumur.IdAreas);
                if (area == null)
                {
                    TempData["ErrorMessage"] = "Error: Area tidak ditemukan di database.";
                    ViewData["Areas"] = _context.Areas.ToList();
                    return View(sumur);
                }

                _context.Update(sumur);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Data sumur berhasil diperbarui!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Gagal memperbarui sumur: " + ex.Message;
                ViewData["Areas"] = _context.Areas.ToList();
                return View(sumur);
            }
        }

        // **4️⃣ DELETE: Konfirmasi penghapusan sumur**
        public async Task<IActionResult> Delete(int id)
        {
            var sumur = await _context.Sumurs
                .Include(s => s.Area)
                .FirstOrDefaultAsync(s => s.IdSumur == id);
            if (sumur == null)
            {
                TempData["ErrorMessage"] = "Data sumur tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }
            return View(sumur);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sumur = await _context.Sumurs.FindAsync(id);
            if (sumur != null)
            {
                _context.Sumurs.Remove(sumur);
                await _context.SaveChangesAsync();
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
