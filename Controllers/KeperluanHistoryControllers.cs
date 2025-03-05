using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

namespace InventoryManagement.Controllers
{
    public class KeperluanHistoryController : Controller
    {
        private readonly InventoryContext _context;

        public KeperluanHistoryController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Menampilkan semua data KeperluanHistory
        public IActionResult Index()
        {
            var keperluanHistories = _context.KeperluanHistories
                .Include(kh => kh.Sumur)  // ✅ Pastikan Sumur dimuat
                .Include(kh => kh.StockBarang)  // ✅ Pastikan StockBarang dimuat
                .OrderBy(kh => kh.Tanggal)
                .ToList();

            return View("~/Views/KeperluanHistory/Index.cshtml", keperluanHistories);
        }
    }
}
