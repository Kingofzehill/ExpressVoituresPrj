using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models;

namespace ExpressVoitures.Controllers
{
    public class VehiculesController : Controller
    {
        private readonly ExpressVoituresContext _context;

        public VehiculesController(ExpressVoituresContext context)
        {
            _context = context;
        }

        // GET: Vehicules
        public async Task<IActionResult> Index()
        {
            var expressVoituresContext = _context.Vehicule.Include(v => v.Finition);
            return View(await expressVoituresContext.ToListAsync());
        }

        // GET: Vehicules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicule
                .Include(v => v.Finition)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // GET: Vehicules/Create        
        public IActionResult Create(int StatutDefault = 0, int AnneeVehiculeDefault = Vehicule.AnneeAchatMinimum)
        {
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>(), "Id", "LibelleFinition");
            ViewData["Statut"] = StatutDefault; // 0 = VehiculeStatuts.Achat
            ViewData["AnneeVehiculeMin"] = AnneeVehiculeDefault;
            return View();
        }
        /*public IActionResult Create()
        {
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>(), "Id", "LibelleFinition");            
            return View();
        }*/

        // POST: Vehicules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Statut,Information,Photo,DateAchat,PrixAchat,AnneeVehicule,MisEnVente,PrixDeVente,DateMisEnVente,Vendu,DateVente,FinitionId")] Vehicule vehicule)
        {
            if (ModelState.IsValid)
            {
                // TD Vérif si DateVente > DateMisEnVente > DateAchat + Statut OK en fonction
                // TD définir MisEnVente true si DateMisEnVente et PrixVente définis
                // TD définir Vendu true si DateVente défini
                // Display validation errors in view (summary) https://stackoverflow.com/questions/61153326/display-model-state-errors-in-view

                _context.Add(vehicule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>(), "Id", "LibelleFinition", vehicule.FinitionId);
            return View(vehicule);
        }

        // GET: Vehicules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicule.FindAsync(id);
            if (vehicule == null)
            {
                return NotFound();
            }
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>(), "Id", "LibelleFinition", vehicule.FinitionId);
            return View(vehicule);
        }

        // POST: Vehicules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Statut,Information,Photo,DateAchat,PrixAchat,AnneeVehicule,MisEnVente,PrixDeVente,DateMisEnVente,Vendu,DateVente,FinitionId")] Vehicule vehicule)
        {
            if (id != vehicule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculeExists(vehicule.Id))
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
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>(), "Id", "LibelleFinition", vehicule.FinitionId);
            return View(vehicule);
        }

        // GET: Vehicules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicule = await _context.Vehicule
                .Include(v => v.Finition)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicule == null)
            {
                return NotFound();
            }

            return View(vehicule);
        }

        // POST: Vehicules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicule = await _context.Vehicule.FindAsync(id);
            if (vehicule != null)
            {
                _context.Vehicule.Remove(vehicule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculeExists(int id)
        {
            return _context.Vehicule.Any(e => e.Id == id);
        }
    }
}
