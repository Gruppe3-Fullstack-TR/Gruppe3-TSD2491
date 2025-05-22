using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gruppe3.Models; // Her ligger IndexInfo og ColorInfo
using Gruppe3.Data;   // Endre hvis AppDbContext ligger i et annet namespace
using Gruppe3.Service;

namespace Gruppe3.Controllers
{
    public class PollenAPIController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPollenAPIService _pollenService;

        public PollenAPIController(AppDbContext context, IPollenAPIService pollenService)
        {
            _context = context;
            _pollenService = pollenService;
        }

        public async Task<IActionResult> Index()
        {
            // Hent alle IndexInfo med ColorInfo
            var indexList = await _context.IndexInfos
                .Include(i => i.ColorInfo)
                .OrderByDescending(i => i.Date)
                .ToListAsync();

            // Gruppér på dato og ta f.eks. den med høyest verdi per dag
            var perDay = indexList
                .GroupBy(i => i.Date.Date)
                .Select(g => g.OrderByDescending(x => x.Value).First())
                .OrderByDescending(x => x.Date)
                .Take(5)
                .ToList();

            return View(perDay);
        }

        /// <summary>
        /// Henter RGB-fargekoden for et varsel (IndexInfo) basert på tilknyttet ColorInfo.
        /// </summary>
        /// <param name="indexInfo">IndexInfo-objektet</param>
        /// <returns>RGB-fargekode som string, f.eks. "rgb(255, 0, 0)"</returns>
        public string GetRgbColor(IndexInfo indexInfo)
        {
            if (indexInfo?.ColorInfo == null)
                return "rgb(255,255,255)"; // fallback til hvit

            // Konverter til int for CSS
            int r = (int)indexInfo.ColorInfo.Red;
            int g = (int)indexInfo.ColorInfo.Green;
            int b = (int)indexInfo.ColorInfo.Blue;
            return $"rgb({r}, {g}, {b})";
        }

        public async Task<IActionResult> Import()
        {
            await _pollenService.ImportPollenDataAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult TestData()
        {
            var count = _context.IndexInfos.Count();
            return Content($"Antall IndexInfo: {count}");
        }
    }
}
