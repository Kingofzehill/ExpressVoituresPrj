using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Data;
using ExpressVoitures.Models;
using Microsoft.AspNetCore.Authorization;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
            ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", 0);
            ViewData["Statut"] = defaultStatus; // 0 = VehiculeStatuts.Achat
            ViewData["AnneeMinimumVehicule"] = carYearMinimum;
            ViewData["MargeMinimum"] = margisMinimum;
            ViewData["ActionEnVente"] = false;
            ViewData["ActionVendu"] = false;
            ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", 0);
            ViewData["Modele"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", 0);
            return View();
        }        

        // POST: Vehicules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Vin,Statut,Information,DateAchat,PrixAchat,AnneeVehicule,PrixDeVente,DateMisEnVente,DateVente,FinitionId,Marge,Image,MisEnVente,Vendu,ActionEnVente,AnneeMinimumVehicule,MargeMinimum,ActionVendu,Marque,Modele")] Vehicule vehicule)
        {
            // ignore navigation property
            ModelState.Remove(nameof(Vehicule.Finition));            

            if (ModelState.IsValid)
            {                                
                vehicule.DateMisAJour = DateTime.Now;
                try
                {
                    // UPD13.4 create image support
                    if (vehicule.Image.File != null)
                    {
                        vehicule.Image = await _imageService.UploadAsync(vehicule.Image);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Image failure...");
                    Console.WriteLine(e.ToString());
                    throw;
                }
                _context.Add(vehicule);
                await _context.SaveChangesAsync();
                bool bDisplayVehiculeActionForSale = false;
                bool bDisplayVehiculeActionSold = false;
                ViewData["ActionEnVente"] = false;
                ViewData["ActionVendu"] = false;
                // Displays "Mettre en vente" action
                if ((vehicule.MisEnVente == false) && (_vehiculeService.CarToSaleButtonDisplay(vehicule) == true))
                {
                    bDisplayVehiculeActionForSale = true;
                    ViewData["ActionEnVente"] = true;                    
                }                
                // Displays "Vendu" action
                if ((vehicule.Vendu == false) && (_vehiculeService.CarSoldButtonDisplay(vehicule) == true))
                {
                    bDisplayVehiculeActionSold = true;                    
                    ViewData["ActionVendu"] = true;
                }
                if ((bDisplayVehiculeActionForSale == true) || (bDisplayVehiculeActionSold == true))
                {
                    // Cascading dropdownlist Marque / Modele.
                    if (vehicule.FinitionId != 0)
                    {
                        //var modelId = vehicule.Finition.ModeleId;
                        var modelId = _context.Finition.Single(c => c.Id == vehicule.FinitionId).ModeleId;
                        var brandId = _context.Modele.Single(c => c.Id == modelId).MarqueId;
                        ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", brandId);
                        ViewData["Modele"] = new SelectList(_context.Set<Modele>().Where(m => m.MarqueId == brandId).OrderBy(x => x.LibelleModele), "Id", "LibelleModele", modelId);
                        ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().Where(m => m.Id == vehicule.FinitionId).OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
                    }
                    else
                    {
                        ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", 0);
                        ViewData["Modele"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", 0);
                        ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", 0);
                    }                    
                    return View(vehicule);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }                
            }
            // Cascading dropdownlist Marque / Modele.
            if (vehicule.FinitionId != 0)
            {
                var modelId = vehicule.Finition.ModeleId;                
                var brandId = _context.Modele.Single(c => c.Id == modelId).MarqueId;
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", brandId);
                ViewData["Modele"] = new SelectList(_context.Set<Modele>().Where(m => m.MarqueId == brandId).OrderBy(x => x.LibelleModele), "Id", "LibelleModele", modelId);
                ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().Where(m => m.Id == vehicule.FinitionId).OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
            }
            else
            {
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", 0);
                ViewData["Modele"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", 0);
                ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", 0);
            }                        
            return View(vehicule);
        }

        /// <summary>
        /// setDropDrownList Method
        /// Cascading DropDrownLists Marques / Modeles / Finitions 
        /// </summary>
        /// <remarks></remarks>
        [HttpPost]
        public JsonResult setDropDrownList(string type, int value)
        {
            switch (type)
            {
                case "Marque":
                    var modelsList = new SelectList(new List<SelectListItem>());
                    if (value == 0)
                    {
                        modelsList = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", 0);
                    }
                    else
                    {
                        modelsList = new SelectList(_context.Set<Modele>().Where(m => m.MarqueId == value).OrderBy(x => x.LibelleModele), "Id", "LibelleModele", 0);
                    }
                    ViewData["Modele"] = modelsList;
                    return Json(modelsList);
                case "Modele":
                    var finishingList = new SelectList(new List<SelectListItem>());                    
                    if (value == 0)
                    {
                        finishingList = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", 0);
                    }
                    else
                    {
                        finishingList = new SelectList(_context.Set<Finition>().Where(m => m.ModeleId == value).OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", 0);
                    }
                    ViewData["FinitionId"] = finishingList;
                    return Json(finishingList);
                case "FinitionId":
                    var brandList = new SelectList(new List<SelectListItem>());                    
                    //Search Modeles of the selection finition
                    if (value == 0)
                    {
                        brandList = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", 0);
                    }
                    else
                    {
                        var modelId = _context.Finition.Single(c => c.Id == value).ModeleId;
                        var brandId = _context.Modele.Single(c => c.Id == modelId).MarqueId;
                        brandList = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", brandId);
                    }
                    ViewData["Marque"] = brandList;
                    return Json(brandList);
                default:
                    return Json(new SelectList(null));
            }
            /*            
                for (int i = 0; i < modelesList.Count(); i++)
                {
                    modeleId = Int32.Parse(modelesList.DataValueField.ToString());
                }
            */
        }

        /// <summary>
        /// SetCarToSell Vehicule Controller Method  ==> set a Car in Sale.         
        /// </summary>
        /// <remarks>Car displayed into Public Vehicule index page.</remarks> 
        [Authorize(Roles = "Admin")]        
        public async Task<IActionResult> SetCarToSell(int id)
        {
            var myVehicule = await _context.Vehicule.FindAsync(id);
            if (myVehicule == null)
            {
                return NotFound();
            }            
            myVehicule.DateMisAJour = DateTime.Now;
            myVehicule.Statut = VehiculeStatuts.EnVente;
            myVehicule.MisEnVente = true;            
            _context.Update(myVehicule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));            
        }

        /// <summary>
        /// SetCarSold Vehicule Controller Method  ==> set a Car as sold.           
        /// </summary>
        /// <remarks>Car is not displayed into Public Vehicule index page anymore.</remarks> 
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetCarSold(int id)
        {
            var myVehicule = await _context.Vehicule.FindAsync(id);
            if (myVehicule == null)
            {
                return NotFound();
            }
            myVehicule.DateMisAJour = DateTime.Now;
            myVehicule.Statut = VehiculeStatuts.Vendu;
            //myVehicule.MisEnVente = false;
            myVehicule.Vendu = true;
            _context.Update(myVehicule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicules/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, int defaultStatus = 0, int carYearMinimum = Vehicule.carYearMinimum, decimal margisMinimum = Vehicule.margisMinimum)
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
            ViewData["AnneeMinimumVehicule"] = carYearMinimum;
            ViewData["MargeMinimum"] = margisMinimum;            
            ViewData["ActionEnVente"] = false;
            ViewData["ActionVendu"] = false;
            // Displays "Mettre en vente" action
            if ((vehicule.MisEnVente == false) && (_vehiculeService.CarToSaleButtonDisplay(vehicule) == true))
            {
                ViewData["ActionEnVente"] = true;
            }
            // Displays "Vendu" action
            if ((vehicule.Vendu == false) && (_vehiculeService.CarSoldButtonDisplay(vehicule) == true))
            {
                ViewData["ActionVendu"] = true;
            }            
            // Cascading dropdownlist Marque / Modele.
            if (vehicule.FinitionId != 0)
            {
                var modelId = vehicule.Finition.ModeleId;
                var brandId = _context.Modele.Single(c => c.Id == modelId).MarqueId;
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", brandId);
                ViewData["Modele"] = new SelectList(_context.Set<Modele>().Where(m => m.MarqueId == brandId).OrderBy(x => x.LibelleModele), "Id", "LibelleModele", modelId);
                ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().Where(m => m.Id == vehicule.FinitionId).OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
            }
            else
            {
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", 0);
                ViewData["Modele"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", 0);
                ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", 0);
            }
            return View(vehicule);                                 
        }

        // POST: Vehicules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Vin,Statut,Information,DateAchat,PrixAchat,AnneeVehicule,PrixDeVente,DateMisEnVente,DateVente,FinitionId,Marge,Image,MisEnVente,Vendu,ActionEnVente,AnneeMinimumVehicule,MargeMinimum,ActionVendu,Marque,Modele")] Vehicule vehicule)
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
                    try
                    {
                        // UPD13.4 create image support
                        if (vehicule.Image.File != null)
                        {
                            vehicule.Image = await _imageService.UploadAsync(vehicule.Image);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Image failure...");
                        Console.WriteLine(e.ToString());
                        throw;
                    }

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
                bool bDisplayVehiculeActionForSale = false;
                bool bDisplayVehiculeActionSold = false;

                ViewData["ActionEnVente"] = false;
                ViewData["ActionVendu"] = false;
                // Displays "Mettre en vente" action
                if ((vehicule.MisEnVente == false) && (_vehiculeService.CarToSaleButtonDisplay(vehicule) == true))
                {
                    bDisplayVehiculeActionForSale = true;
                    ViewData["ActionEnVente"] = true;
                }
                // Displays "Vendu" action
                if ((vehicule.Vendu == false) && (_vehiculeService.CarSoldButtonDisplay(vehicule) == true))
                {
                    bDisplayVehiculeActionSold = true;
                    ViewData["ActionVendu"] = true;
                }
                if ((bDisplayVehiculeActionForSale == true) || (bDisplayVehiculeActionSold == true))
                {
                    ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
                    return View(vehicule);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            // Cascading dropdonwlists Marque / Modele.
            if (vehicule.FinitionId != 0)
            {
                var modelId = vehicule.Finition.ModeleId;
                var brandId = _context.Modele.Single(c => c.Id == modelId).MarqueId;
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", brandId);
                ViewData["Modele"] = new SelectList(_context.Set<Modele>().Where(m => m.MarqueId == brandId).OrderBy(x => x.LibelleModele), "Id", "LibelleModele", modelId);
                ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().Where(m => m.Id == vehicule.FinitionId).OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", vehicule.FinitionId);
            }
            else
            {
                ViewData["Marque"] = new SelectList(_context.Set<Marque>().OrderBy(x => x.LibelleMarque), "Id", "LibelleMarque", 0);
                ViewData["Modele"] = new SelectList(_context.Set<Modele>().OrderBy(x => x.LibelleModele), "Id", "LibelleModele", 0);
                ViewData["FinitionId"] = new SelectList(_context.Set<Finition>().OrderBy(x => x.LibelleFinition), "Id", "LibelleFinition", 0);
            }            
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
