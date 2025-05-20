using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gruppe3.Data;
using Gruppe3.Models;

namespace Gruppe3.Controllers
{
    public class PollenRegisteringController : Controller
    {
        private readonly AppDbContext _context;

        public PollenRegisteringController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PollenRegistering
        public async Task<IActionResult> Index()
        {
            return View(await _context.PollenRegisterings.ToListAsync());
        }

        // GET: PollenRegistering/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollenRegistering = await _context.PollenRegisterings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollenRegistering == null)
            {
                return NotFound();
            }

            return View(pollenRegistering);
        }

        // GET: PollenRegistering/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PollenRegistering/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeOfPollen,Level,Date")] PollenRegistering pollenRegistering)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pollenRegistering);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pollenRegistering);
        }

        // GET: PollenRegistering/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollenRegistering = await _context.PollenRegisterings.FindAsync(id);
            if (pollenRegistering == null)
            {
                return NotFound();
            }
            return View(pollenRegistering);
        }

        // POST: PollenRegistering/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfPollen,Level,Date")] PollenRegistering pollenRegistering)
        {
            if (id != pollenRegistering.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollenRegistering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollenRegisteringExists(pollenRegistering.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pollenRegistering);
        }

        // GET: PollenRegistering/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollenRegistering = await _context.PollenRegisterings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pollenRegistering == null)
            {
                return NotFound();
            }

            return View(pollenRegistering);
        }

        // POST: PollenRegistering/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollenRegistering = await _context.PollenRegisterings.FindAsync(id);
            if (pollenRegistering != null)
            {
                _context.PollenRegisterings.Remove(pollenRegistering);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PollenRegisteringExists(int id)
        {
            return _context.PollenRegisterings.Any(e => e.Id == id);
        }
    }
}
