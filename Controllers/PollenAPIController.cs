using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gruppe3.Models; // Her ligger IndexInfo og ColorInfo
using Gruppe3.Data;   // Endre hvis AppDbContext ligger i et annet namespace

namespace Gruppe3.Controllers
{
    public class PollenAPIController : Controller
    {
        private readonly AppDbContext _context;

        public PollenAPIController(AppDbContext context)
        {
            _context = context;
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
    }
}
