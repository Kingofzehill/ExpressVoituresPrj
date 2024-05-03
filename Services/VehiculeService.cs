using ExpressVoitures.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpressVoitures.Services
{
    [Authorize(Roles = "Admin")]
    public class VehiculeService
    {
        public VehiculeService()
        {

        }

        /// <summary>
        /// CarToSaleButtonDisplay Method
        /// Evaluate if mandatories Vehicule properties DateMisEnVente & DateVente are filled for displaying "Mettre en vente" button. 
        /// This button set a Vehicule to be sale and displays it the Public Vehicule index page.
        /// </summary>
        /// <remarks>Vehicules properties defined in the Data Model Class are not evaluated</remarks>
        /// <remarks></remarks>
        public bool CarToSaleButtonDisplay(Vehicule vehicule)
        {
            bool bToSaleButtonDisplay = false;                                  

            if ((vehicule.DateMisEnVente != null) && (vehicule.PrixDeVente > 0))
            {
                bToSaleButtonDisplay = true;
            }

            return bToSaleButtonDisplay;
        }

        /// <summary>
        /// CarSoldButtonDisplay Method
        /// Evaluate if mandatory Vehicule property DateVente is filled for displaying "Vendu" button. 
        /// This button set a Vehicule as sold and removes it from the Public Vehicule index page.
        /// </summary>
        /// <remarks></remarks>        
        public bool CarSoldButtonDisplay(Vehicule vehicule)
        {
            bool bSoldButtonDisplay = false;                           

            if (vehicule.DateVente != null) 
            {
                bSoldButtonDisplay = true;
            }

            return bSoldButtonDisplay;
        }
    }
}
