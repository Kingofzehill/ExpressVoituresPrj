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
    public class ModelesController : Controller
    {
        private readonly ExpressVoituresContext _context;

        public ModelesController(ExpressVoituresContext context)
        {
            _context = context;
        }

        // GET: Modeles
        public async Task<IActionResult> Index()
        {
            var expressVoituresContext = _context.Modele.Include(m => m.Marque);
            return View(await expressVoituresContext.OrderBy(x => x.LibelleModele).ToListAsync());
        }

        // GET: Modeles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modele = await _context.Modele
                .Include(m => m.Marque)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modele == null)
            {
                return NotFound();
            }

            return View(modele);
        }

        // GET: Modeles/Create
        public IActionResult Create()
        {
            // UPD04 : alphabetical order for vizews dropdownlist
            ViewData["MarqueId"] = new SelectList(_context.Marque.OrderBy(x=>x.LibelleMarque), "Id", "LibelleMarque");
            return View();
        }

        // POST: Modeles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibelleModele,MarqueId")] Modele modele)
        {
            // FIX02 ModelState incorrectly reference navigation property for validation
            //      https://stackoverflow.com/questions/55115319/modelstate-errors-for-all-navigation-properties
            // ignore navigation property
            ModelState.Remove(nameof(Modele.Marque));

            if (ModelState.IsValid)
            {
                _context.Add(modele);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarqueId"] = new SelectList(_context.Marque.OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", modele.MarqueId);
            return View(modele);
        }

        // GET: Modeles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modele = await _context.Modele.FindAsync(id);
            if (modele == null)
            {
                return NotFound();
            }
            ViewData["MarqueId"] = new SelectList(_context.Marque.OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", modele.MarqueId);
            return View(modele);
        }

        // POST: Modeles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibelleModele,MarqueId")] Modele modele)
        {
            if (id != modele.Id)
            {
                return NotFound();
            }

            // ignore navigation property
            ModelState.Remove(nameof(Modele.Marque));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modele);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeleExists(modele.Id))
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
            ViewData["MarqueId"] = new SelectList(_context.Marque.OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", modele.MarqueId);
            return View(modele);
        }

        // GET: Modeles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modele = await _context.Modele
                .Include(m => m.Marque)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modele == null)
            {
                return NotFound();
            }

            return View(modele);
        }

        // POST: Modeles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modele = await _context.Modele.FindAsync(id);
            if (modele != null)
            {
                _context.Modele.Remove(modele);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeleExists(int id)
        {
            return _context.Modele.Any(e => e.Id == id);
        }
    }
}
