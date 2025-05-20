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

        public IActionResult Index()
        {
            // Hent de 5 siste pollenregistreringene
            var indexList = _context.IndexInfos
                                    .Include(i => i.ColorInfo)
                                    .OrderByDescending(i => i.Id)
                                    .Take(5)
                                    .ToList();

            return View(indexList);
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
    }
}
