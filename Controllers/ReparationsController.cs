using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpressVoitures.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReparationsController : Controller
    {
        private readonly ExpressVoituresContext _context;

        public ReparationsController(ExpressVoituresContext context)
        {
            _context = context;
        }

        // GET: Reparations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reparation.OrderBy(x => x.LibelleReparation).ToListAsync());
        }

        // GET: Reparations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparation = await _context.Reparation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reparation == null)
            {
                return NotFound();
            }

            return View(reparation);
        }

        // GET: Reparations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reparations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibelleReparation")] Reparation reparation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reparation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reparation);
        }

        // GET: Reparations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparation = await _context.Reparation.FindAsync(id);
            if (reparation == null)
            {
                return NotFound();
            }
            return View(reparation);
        }

        // POST: Reparations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibelleReparation")] Reparation reparation)
        {
            if (id != reparation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reparation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReparationExists(reparation.Id))
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
            return View(reparation);
        }

        // GET: Reparations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparation = await _context.Reparation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reparation == null)
            {
                return NotFound();
            }

            return View(reparation);
        }

        // POST: Reparations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reparation = await _context.Reparation.FindAsync(id);
            if (reparation != null)
            {
                _context.Reparation.Remove(reparation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReparationExists(int id)
        {
            return _context.Reparation.Any(e => e.Id == id);
        }
    }
}
