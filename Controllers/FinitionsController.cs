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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;

namespace ExpressVoitures.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FinitionsController : Controller
    {
        private readonly ExpressVoituresContext _context;

        public FinitionsController(ExpressVoituresContext context)
        {
            _context = context;
        }

        // GET: Finitions
        public async Task<IActionResult> Index()
        {
            var expressVoituresContext = _context.Finition.Include(f => f.Modele);
            return View(await expressVoituresContext.OrderBy(x => x.LibelleFinition).ToListAsync());
        }

        // GET: Finitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finition = await _context.Finition
                .Include(f => f.Modele)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finition == null)
            {
                return NotFound();
            }

            return View(finition);
        }

        // GET: Finitions/Create
        public IActionResult Create()
        {
            // TD14 cascading dropdownlist Marque / Modele            
            // UPD11.1 (controller create) cascading dropdownlist Marque / Modele                
            //ViewData["MarqueDefault"] = "Sélectionner..."; //default value
            ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque");
            ViewData["ModeleId"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele");
            return View();
        }

        /// <summary>
        /// setDropDrownList Method
        /// Cascading DropDrownLists Marques / Modeles for Modele View
        /// </summary>
        /// <remarks></remarks>
        [HttpPost]
        public JsonResult setDropDrownList(string type, int value)
        {
            switch (type)
            {
                case "Marque":
                    //Select Modeles for the selectect MarqueId
                    var modelesList = new SelectList(_context.Set<Modele>().Where(m => m.MarqueId == value).OrderBy(x => x.LibelleModele), "Id", "LibelleModele");
                    ViewData["ModeleId"] = modelesList;
                    if (value == 0)
                    {
                        modelesList = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele");
                    }
                    return Json(modelesList);
                case "ModeleId":
                    //Select Marques for the selected ModeleId
                    //var marqueDefault = "Sélectionner...";
                    var marquesList = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque");
                    if (value != 0)
                    {
                        var MarqueId = _context.Modele.Single(c => c.Id == value).MarqueId;
                        //marqueDefault = _context.Marque.Single(c => c.Id == MarqueId).LibelleMarque;
                        //marquesList = new SelectList(_context.Set<Marque>().Where(m => m.Id == MarqueId).OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", MarqueId);
                        marquesList = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", MarqueId);
                    }                    
                    //ViewData["MarqueDefault"] = marqueDefault;
                    ViewData["Marque"] = marquesList;
                    return Json(marquesList);
                default:
                    return Json(new SelectList(null));
            }            
        }

        // POST: Finitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibelleFinition,ModeleId")] Finition finition)
        {
            // FIX6 finition : ignore navigation property on create validation
            ModelState.Remove(nameof(Finition.Modele));
            // UPD11.3 Fintion update model validation for managing "Selectionner" default list options
            if (finition.ModeleId == 0) // ModeleId == 0 for "Sélectionner..." option
            {
                ModelState.AddModelError("ModeleId", "Veuillez sélectionner le Modèle de la Finition.");
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(finition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (finition.ModeleId != 0)
            {
                //var MarqueDefault = finition.Modele.Marque.LibelleMarque;
                var MarqueId = _context.Modele.Single(c => c.Id == finition.ModeleId).MarqueId;
                //var MarqueDefault = _context.Marque.Single(c => c.Id == MarqueId).LibelleMarque;
                //ViewData["MarqueDefault"] = MarqueDefault;
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", MarqueId);
            }
            else
            {
                //ViewData["MarqueDefault"] = "Sélectionner...";
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque");

            }
            ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque");
            ViewData["ModeleId"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", finition.ModeleId);
            return View(finition);
        }

        // GET: Finitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finition = await _context.Finition.FindAsync(id);
            if (finition == null)
            {
                return NotFound();
            }
            // UPD11 : FinitionView, add MarquesList which filter ModeleList            
            if (finition.ModeleId != 0)
            {
                //var MarqueDefault = finition.Modele.Marque.LibelleMarque;
                var MarqueId = _context.Modele.Single(c => c.Id == finition.ModeleId).MarqueId;
                //var MarqueDefault = _context.Marque.Single(c => c.Id == MarqueId).LibelleMarque;
                //ViewData["MarqueDefault"] = MarqueDefault;
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", MarqueId);
            }
            else
            {
                //ViewData["MarqueDefault"] = "Sélectionner...";
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque");

            }           
            ViewData["ModeleId"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", finition.ModeleId);
            return View(finition);
        }

        // POST: Finitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibelleFinition,ModeleId")] Finition finition)
        {
            if (id != finition.Id)
            {
                return NotFound();
            }

            // FIX6 finition : ignore navigation property on create validation
            ModelState.Remove(nameof(Finition.Modele));
            // UPD11.3 Fintion update model validation for managing "Selectionner" default list options
            if (finition.ModeleId == 0) // ModeleId == 0 for "Sélectionner..." option
            {
                ModelState.AddModelError("ModeleId", "Veuillez sélectionner le Modèle de la Finition.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinitionExists(finition.Id))
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
            
            // UPD11 : FinitionView, add MarquesList which filter ModeleList            
            if (finition.ModeleId != 0)
            {
                //var MarqueDefault = finition.Modele.Marque.LibelleMarque;
                var MarqueId = _context.Modele.Single(c => c.Id == finition.ModeleId).MarqueId;
                //var MarqueDefault = _context.Marque.Single(c => c.Id == MarqueId).LibelleMarque;
                //ViewData["MarqueDefault"] = MarqueDefault;
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", MarqueId);
            }
            else
            {
                //ViewData["MarqueDefault"] = "Sélectionner...";
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque");

            }
            ViewData["ModeleId"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", finition.ModeleId);
            return View(finition);
        }

        // GET: Finitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finition = await _context.Finition
                .Include(f => f.Modele)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finition == null)
            {
                return NotFound();
            }

            return View(finition);
        }

        // POST: Finitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var finition = await _context.Finition.FindAsync(id);
            if (finition != null)
            {
                _context.Finition.Remove(finition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinitionExists(int id)
        {
            return _context.Finition.Any(e => e.Id == id);
        }
    }
}
