using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Controllers
{
    public class HomeSumurController : Controller
    {
        private readonly InventoryContext _context;

        public HomeSumurController(InventoryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                // **Ambil semua sumur dengan area dan keperluan materialnya**
                var sumurs = _context.Sumurs
                    .Include(s => s.Area) // Pastikan area tersedia
                    .Include(s => s.KeperluanSumurs) // Ambil kebutuhan material sumur
                    .ThenInclude(ks => ks.StockBarang) // Ambil informasi stok barang
                    .OrderBy(s => s.Area.NamaArea) // Urutkan berdasarkan area
                    .ThenBy(s => s.StartDate) // Lalu urutkan berdasarkan tanggal mulai
                    .ToList();

                // **Dictionary untuk stok barang yang tersedia**
                var stockBarangs = _context.StockBarangs.ToDictionary(sb => sb.KodeMaterial, sb => sb.Jumlah);

                // **List untuk menyimpan data sumur yang akan dikirim ke view**
                var sumurList = new List<dynamic>();

                foreach (var sumur in sumurs)
                {
                    var keperluanForSumur = sumur.KeperluanSumurs.ToList();
                    int totalMaterial = keperluanForSumur.Count;
                    int materialTersedia = 0;
                    var materialDetails = new List<dynamic>();

                    foreach (var keperluan in keperluanForSumur)
                    {
                        var kodeMaterial = keperluan.KodeMaterial;
                        var stokAwal = stockBarangs.ContainsKey(kodeMaterial) ? stockBarangs[kodeMaterial] : 0;
                        int kebutuhan = keperluan.Jumlah;
                        int stokSisa = Math.Max(stokAwal - kebutuhan, 0);
                        int kekurangan = kebutuhan > stokAwal ? kebutuhan - stokAwal : 0;

                        // **Kurangi stok untuk sumur berikutnya**
                        stockBarangs[kodeMaterial] = stokSisa;

                        // **Jika stok cukup, hitung sebagai material yang tersedia**
                        if (kekurangan == 0) materialTersedia++;

                        // **Simpan informasi material**
                        materialDetails.Add(new
                        {
                            NamaMaterial = keperluan.DeskripsiMaterial,
                            Kebutuhan = kebutuhan,
                            StokAwal = stokAwal,
                            StokSisa = stokSisa,
                            Kekurangan = kekurangan
                        });
                    }

                    int overallStatus = totalMaterial > 0 ? (materialTersedia * 100) / totalMaterial : 0;

                    sumurList.Add(new
                    {
                        NamaArea = sumur.Area?.NamaArea ?? "Tidak Diketahui",
                        NamaSumur = sumur.NamaSumur,
                        StartDate = sumur.StartDate.ToString("dd/MM/yyyy"),
                        OverallStatus = overallStatus,
                        MaterialDetails = materialDetails
                    });
                }

                ViewBag.SumurData = sumurList;
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Kesalahan: {ex.Message}";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
