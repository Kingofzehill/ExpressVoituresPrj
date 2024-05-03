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
        /// CarToSaleButtonDisplay Method ==> Displays "Mettre en vente" button. 
        /// Evaluate if mandatories Vehicule properties DateMisEnVente & DateVente are correctly filled 
        /// and evaluates vehicule workflow.        
        /// </summary>      
        /// <remarks></remarks>
        public bool CarToSaleButtonDisplay(Vehicule vehicule)
        {
            bool bToSaleButtonDisplay = false;                                  

            if ((vehicule.DateMisEnVente != null) && (vehicule.PrixDeVente > 0) && (vehicule.MisEnVente == false))
            {
                bToSaleButtonDisplay = true;
            }

            return bToSaleButtonDisplay;
        }

        /// <summary>
        /// CarSoldButtonDisplay Method  ==> Displays "Vendu" button. 
        /// Evaluate if mandatory Vehicule property DateVente is filled
        /// and evaluates vehicule workflow.        
        /// </summary>
        /// <remarks></remarks>        
        public bool CarSoldButtonDisplay(Vehicule vehicule)
        {
            bool bSoldButtonDisplay = false;                           

            if ((vehicule.DateVente != null) && (vehicule.Vendu == false))
            {
                bSoldButtonDisplay = true;
            }

            return bSoldButtonDisplay;
        }
    }
}
