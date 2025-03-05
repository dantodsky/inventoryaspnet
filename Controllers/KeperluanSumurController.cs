using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System;
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

        // ✅ 2️⃣ Form tambah keperluan sumur
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
                TempData["ErrorMessage"] = "⚠️ Harus menambahkan minimal satu material.";
                return RedirectToAction(nameof(Create));
            }

            try
            {
                int idSumur = keperluanSumurs.First().IdSumur;
                var sumur = await _context.Sumurs.FirstOrDefaultAsync(s => s.IdSumur == idSumur);

                if (sumur == null)
                {
                    TempData["ErrorMessage"] = "⚠️ Sumur tidak ditemukan.";
                    return RedirectToAction(nameof(Create));
                }

                foreach (var keperluanSumur in keperluanSumurs)
                {
                    if (string.IsNullOrEmpty(keperluanSumur.KodeMaterial) || keperluanSumur.Jumlah <= 0)
                    {
                        TempData["ErrorMessage"] = "⚠️ Pastikan semua data telah diisi dengan benar.";
                        return RedirectToAction(nameof(Create));
                    }

                    var stock = await _context.StockBarangs.FirstOrDefaultAsync(sb => sb.KodeMaterial == keperluanSumur.KodeMaterial);
                    if (stock == null)
                    {
                        TempData["ErrorMessage"] = $"⚠️ Material {keperluanSumur.KodeMaterial} tidak ditemukan!";
                        return RedirectToAction(nameof(Create));
                    }

                    // **Tambahkan KeperluanSumur**
                    _context.KeperluanSumurs.Add(keperluanSumur);
                    await _context.SaveChangesAsync(); // Simpan dulu agar mendapatkan Id

                    // ✅ **Tambahkan KeperluanHistory dengan Id yang sudah tersimpan**
                    var keperluanHistory = new KeperluanHistory
                    {
                        IdKeperluans = keperluanSumur.IdKeperluans, // Pastikan ini sudah ada di DB
                        IdSumur = keperluanSumur.IdSumur,
                        KodeMaterial = keperluanSumur.KodeMaterial,
                        Tanggal = sumur.StartDate,
                        JumlahKeluar = keperluanSumur.Jumlah
                    };

                    _context.KeperluanHistories.Add(keperluanHistory);
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "✅ Keperluan sumur berhasil ditambahkan!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = $"⚠️ Error Database: {ex.InnerException?.Message}";
                Console.WriteLine($"DEBUG ERROR: {ex.InnerException?.Message}");
                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"⚠️ Terjadi kesalahan: {ex.Message}";
                Console.WriteLine($"DEBUG ERROR: {ex.Message}");
                return RedirectToAction(nameof(Create));
            }
        }
    }
}
