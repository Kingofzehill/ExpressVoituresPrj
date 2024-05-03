using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models;
using Microsoft.AspNetCore.Authorization;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace ExpressVoitures.Controllers
{
    public class VehiculesController : Controller
    {
        private readonly ExpressVoituresContext _context;
        // UPD13.4 image support
        private readonly ImageService _imageService;
        private readonly VehiculeService _vehiculeService;

        public VehiculesController(ExpressVoituresContext context, ImageService imageService, VehiculeService vehiculeService)
        {
            _context = context;
            _imageService = imageService;
            _vehiculeService = vehiculeService;
        }

        // TD18 Add filter on statut En vente (only for public)
        // GET: Vehicules
        public async Task<IActionResult> Index()
        {
            // UPD13.7 image support in Vehicule Controller method index
            var expressVoituresContext = _context.Vehicule.Include(v => v.Finition).Include(c => c.Image);
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int defaultStatus = 0, int carYearMinimum = Vehicule.carYearMinimum, decimal margisMinimum = Vehicule.margisMinimum)
        {            
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition");
            ViewData["Statut"] = defaultStatus; // 0 = VehiculeStatuts.Achat
            ViewData["CarYearMinimum"] = carYearMinimum;
            ViewData["MargisMinimum"] = margisMinimum;
            return View();
        }
        /* Before UPD13
        public IActionResult Create(int StatutDefault = 0, int AnneeVehiculeDefault = Vehicule.AnneeAchatMinimum)
        {
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition");
            ViewData["Statut"] = StatutDefault; // 0 = VehiculeStatuts.Achat
            ViewData["AnneeVehiculeMin"] = AnneeVehiculeDefault;            
            return View();
        }*/
        /*public IActionResult Create()
        {
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>(), "Id", "LibelleFinition");            
            return View();
        }*/

        // POST: Vehicules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Vin,Statut,Information,DateAchat,PrixAchat,AnneeVehicule,PrixDeVente,DateMisEnVente,DateVente,FinitionId,Marge,Image")] Vehicule vehicule)
        {
            // ignore navigation property
            ModelState.Remove(nameof(Vehicule.Finition));            

            if (ModelState.IsValid)
            {                                
                vehicule.DateMisAJour = DateTime.Now;
                // UPD13.4 create image support
                if (vehicule.Image.File != null)
                {
                    vehicule.Image = await _imageService.UploadAsync(vehicule.Image);                    
                }                
                
                _context.Add(vehicule);
                await _context.SaveChangesAsync();

                bool bDisplayVehiculeActionsForSale = false;

                //To add in index Vehicule View for new admin action button "Mise en vente"
                // Displays "Mettre en vente" action
                if ((vehicule.MisEnVente == false) && (_vehiculeService.CarToSaleButtonDisplay(vehicule) == true))
                {                    
                    bDisplayVehiculeActionsForSale = true;                    
                }

                //To add in index Vehicule View for new admin action buttons "Vendu" / "Indisponible"
                // Displays "Vendu" action
                if ((vehicule.MisEnVente = true) && (vehicule.Vendu = false) && (_vehiculeService.CarSoldButtonDisplay(vehicule) == true))
                {                    
                    bDisplayVehiculeActionsForSale = true;                    
                }

                if (bDisplayVehiculeActionsForSale == true)
                {
                    ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
                    return View(vehicule);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }                
            }
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
            return View(vehicule);
        }

        // GET: Vehicules/Edit/5
        [Authorize(Roles = "Admin")]
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
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
            return View(vehicule);
        }

        // POST: Vehicules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Vin,Statut,Information,DateAchat,PrixAchat,AnneeVehicule,PrixDeVente,DateMisEnVente,DateVente,FinitionId,Marge,Image")] Vehicule vehicule)
        {
            if (id != vehicule.Id)
            {
                return NotFound();
            }

            // ignore navigation property
            ModelState.Remove(nameof(Vehicule.Finition));

            if (ModelState.IsValid)
            {
                try
                {
                    vehicule.DateMisAJour = DateTime.Now;

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
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
            return View(vehicule);
        }

        // GET: Vehicules/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
