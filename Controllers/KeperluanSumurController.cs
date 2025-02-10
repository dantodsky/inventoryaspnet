using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Controllers
{
    public class KeperluanSumurController : Controller
    {
        private readonly InventoryContext _context;

        public KeperluanSumurController(InventoryContext context)
        {
            _context = context;
        }

        // **READ: Tampilkan daftar keperluan sumur**
        public IActionResult Index()
        {
            var keperluanSumurs = _context.KeperluanSumurs
                .Include(ks => ks.Sumur) // Relasi ke tabel Sumur
                .OrderBy(ks => ks.IdSumur) // Urutkan berdasarkan IdSumur
                .ToList();

            return View(keperluanSumurs);
        }

        // **CREATE: Form tambah keperluan sumur**
        public IActionResult Create()
        {
            ViewData["Sumurs"] = _context.Sumurs.ToList(); // Dropdown untuk Sumur
            ViewData["StockBarangs"] = _context.StockBarangs.ToList(); // Dropdown untuk Stock Barang

            return View(new List<KeperluanSumur>()); // List kosong untuk multiple material
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<KeperluanSumur> keperluanSumurs)
        {
            if (keperluanSumurs == null || !keperluanSumurs.Any())
            {
                TempData["ErrorMessage"] = "Harus menambahkan minimal satu material.";
                return RedirectToAction(nameof(Create));
            }

            try
            {
                int idSumur = keperluanSumurs.First().IdSumur;

                var sumur = _context.Sumurs.Find(idSumur);
                if (sumur == null)
                {
                    TempData["ErrorMessage"] = "Sumur tidak ditemukan.";
                    return RedirectToAction(nameof(Create));
                }

                foreach (var keperluanSumur in keperluanSumurs)
                {
                    var stock = _context.StockBarangs.FirstOrDefault(sb => sb.KodeMaterial == keperluanSumur.KodeMaterial);
                    if (stock == null)
                    {
                        TempData["ErrorMessage"] = "Material tidak ditemukan.";
                        return RedirectToAction(nameof(Create));
                    }

                    if (stock.Jumlah < keperluanSumur.Jumlah)
                    {
                        TempData["ErrorMessage"] = $"Stok untuk {stock.DeskripsiMaterial} tidak mencukupi.";
                        return RedirectToAction(nameof(Create));
                    }

                    stock.Jumlah -= keperluanSumur.Jumlah;
                    keperluanSumur.DeskripsiMaterial = stock.DeskripsiMaterial;
                    keperluanSumur.BaseUnit = stock.BaseUnit;

                    _context.KeperluanSumurs.Add(keperluanSumur);
                }

                _context.SaveChanges();
                TempData["SuccessMessage"] = "Keperluan sumur berhasil ditambahkan dan stok telah diperbarui!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Kesalahan: {ex.Message}";
            }

            ViewData["Sumurs"] = _context.Sumurs.ToList();
            ViewData["StockBarangs"] = _context.StockBarangs.ToList();
            return View(keperluanSumurs);
        }

        // **EDIT: Form edit keperluan sumur**
        public IActionResult Edit(int id)
        {
            var keperluanSumur = _context.KeperluanSumurs
                .AsNoTracking()
                .FirstOrDefault(ks => ks.IdKeperluans == id);

            if (keperluanSumur == null)
            {
                TempData["ErrorMessage"] = "Keperluan sumur tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }

            return View(keperluanSumur);
        }

        // **EDIT (POST): Update hanya jumlah material**
        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult EditJumlah(KeperluanSumur updatedData)
{
    try
    {
        var keperluanSumur = _context.KeperluanSumurs.FirstOrDefault(ks => ks.IdKeperluans == updatedData.IdKeperluans);
        if (keperluanSumur == null)
        {
            TempData["ErrorMessage"] = "Keperluan sumur tidak ditemukan.";
            return RedirectToAction(nameof(Index));
        }

        var stock = _context.StockBarangs.FirstOrDefault(sb => sb.KodeMaterial == keperluanSumur.KodeMaterial);
        if (stock == null)
        {
            TempData["ErrorMessage"] = "Material tidak ditemukan di stok.";
            return RedirectToAction(nameof(Edit), new { id = updatedData.IdKeperluans });
        }

        // Kembalikan stok lama sebelum update
        stock.Jumlah += keperluanSumur.Jumlah;

        // Validasi jumlah baru tidak melebihi stok yang tersedia
        if (updatedData.Jumlah > stock.Jumlah)
        {
            TempData["ErrorMessage"] = $"Stok untuk {stock.DeskripsiMaterial} tidak mencukupi.";
            return RedirectToAction(nameof(Edit), new { id = updatedData.IdKeperluans });
        }

        // Update jumlah dan kurangi stok
        keperluanSumur.Jumlah = updatedData.Jumlah;
        stock.Jumlah -= updatedData.Jumlah;

        _context.SaveChanges();

        TempData["SuccessMessage"] = "Jumlah material berhasil diperbarui.";
        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = $"Kesalahan: {ex.Message}";
        return RedirectToAction(nameof(Index));
    }
}


        // **DELETE: Konfirmasi hapus keperluan sumur**
        public IActionResult Delete(int id)
        {
            var keperluanSumur = _context.KeperluanSumurs
                .Include(ks => ks.Sumur)
                .FirstOrDefault(ks => ks.IdKeperluans == id);

            if (keperluanSumur == null)
            {
                TempData["ErrorMessage"] = "Keperluan sumur tidak ditemukan.";
                return RedirectToAction(nameof(Index));
            }

            return View(keperluanSumur);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var keperluanSumur = _context.KeperluanSumurs.Find(id);

            if (keperluanSumur != null)
            {
                try
                {
                    var stock = _context.StockBarangs.FirstOrDefault(sb => sb.KodeMaterial == keperluanSumur.KodeMaterial);
                    if (stock != null)
                    {
                        stock.Jumlah += keperluanSumur.Jumlah;
                    }

                    _context.KeperluanSumurs.Remove(keperluanSumur);
                    _context.SaveChanges();

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
