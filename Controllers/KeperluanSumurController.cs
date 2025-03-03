using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class KeperluanSumurController : Controller
    {
        private readonly InventoryContext _context;

        public KeperluanSumurController(InventoryContext context)
        {
            _context = context;
        }

        // ✅ 1️⃣ Tampilkan daftar keperluan sumur
        public async Task<IActionResult> Index()
        {
            var keperluanSumurs = await _context.KeperluanSumurs
                .Include(k => k.Sumur)
                .Include(k => k.StockBarang)
                .ToListAsync();

            return View(keperluanSumurs);
        }

        // ✅ 2️⃣ CREATE: Form tambah keperluan sumur
        public IActionResult Create()
        {
            ViewData["Sumurs"] = _context.Sumurs.ToList();
            ViewData["StockBarangs"] = _context.StockBarangs.ToList();

            return View(new List<KeperluanSumur> { new KeperluanSumur() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<KeperluanSumur> keperluanSumurs)
        {
            if (keperluanSumurs == null || !keperluanSumurs.Any())
            {
                TempData["ErrorMessage"] = "Harus menambahkan minimal satu material.";
                return RedirectToAction(nameof(Create));
            }

            try
            {
                int idSumur = keperluanSumurs.First().IdSumur;
                var sumur = await _context.Sumurs.FindAsync(idSumur);

                if (sumur == null)
                {
                    TempData["ErrorMessage"] = "Sumur tidak ditemukan.";
                    return RedirectToAction(nameof(Create));
                }

                foreach (var keperluanSumur in keperluanSumurs)
                {
                    if (keperluanSumur.KodeMaterial == null || keperluanSumur.Jumlah <= 0)
                    {
                        TempData["ErrorMessage"] = "Pastikan semua data telah diisi dengan benar.";
                        return RedirectToAction(nameof(Create));
                    }

                    var stock = _context.StockBarangs.FirstOrDefault(sb => sb.KodeMaterial == keperluanSumur.KodeMaterial);
                    if (stock == null)
                    {
                        TempData["ErrorMessage"] = "Material tidak ditemukan.";
                        return RedirectToAction(nameof(Create));
                    }

                    int stokDapatDigunakan = Math.Min(stock.Jumlah, keperluanSumur.Jumlah);
                    int kekurangan = keperluanSumur.Jumlah - stokDapatDigunakan;
                    stock.Jumlah -= stokDapatDigunakan;

                    keperluanSumur.DeskripsiMaterial = stock.DeskripsiMaterial;
                    keperluanSumur.BaseUnit = stock.BaseUnit;
                    keperluanSumur.Kekurangan = kekurangan;

                    _context.KeperluanSumurs.Add(keperluanSumur);
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Keperluan sumur berhasil ditambahkan!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Terjadi kesalahan saat menambahkan data.";
            }

            return RedirectToAction(nameof(Create));
        }

        // ✅ 3️⃣ EDIT: Form edit keperluan sumur
        public async Task<IActionResult> Edit(int id)
        {
            var keperluanSumur = await _context.KeperluanSumurs.FindAsync(id);
            if (keperluanSumur == null)
            {
                TempData["ErrorMessage"] = "Keperluan sumur tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Sumurs"] = _context.Sumurs.ToList();
            ViewData["StockBarangs"] = _context.StockBarangs.ToList();
            return View(keperluanSumur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KeperluanSumur keperluanSumur)
        {
            if (id != keperluanSumur.IdKeperluans)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keperluanSumur);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Data keperluan sumur berhasil diperbarui!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "Terjadi kesalahan saat menyimpan data.";
                }
            }

            ViewData["Sumurs"] = _context.Sumurs.ToList();
            ViewData["StockBarangs"] = _context.StockBarangs.ToList();
            return View(keperluanSumur);
        }

        // ✅ 4️⃣ DELETE: Konfirmasi hapus keperluan sumur
        public async Task<IActionResult> Delete(int id)
        {
            var keperluanSumur = await _context.KeperluanSumurs
                .Include(k => k.Sumur)
                .Include(k => k.StockBarang)
                .FirstOrDefaultAsync(k => k.IdKeperluans == id);

            if (keperluanSumur == null)
            {
                TempData["ErrorMessage"] = "Keperluan sumur tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }

            return View(keperluanSumur);
        }

        [HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var keperluanSumur = await _context.KeperluanSumurs.FindAsync(id);

    if (keperluanSumur != null)
    {
        try
        {
            // **1️⃣ Ambil stok barang terkait**
            var stock = await _context.StockBarangs.FirstOrDefaultAsync(sb => sb.KodeMaterial == keperluanSumur.KodeMaterial);
            
            if (stock != null)
            {
                // **2️⃣ Kembalikan stok yang dikurangi**
                int stokDikembalikan = keperluanSumur.Jumlah - keperluanSumur.Kekurangan;
                stock.Jumlah += stokDikembalikan;
            }

            // **3️⃣ Hapus keperluan sumur**
            _context.KeperluanSumurs.Remove(keperluanSumur);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Keperluan sumur berhasil dihapus dan stok telah dikembalikan!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Kesalahan: {ex.Message}";
        }
    }
    else
    {
        TempData["ErrorMessage"] = "Keperluan sumur tidak ditemukan.";
    }

    return RedirectToAction(nameof(Index));
}
    }
}
