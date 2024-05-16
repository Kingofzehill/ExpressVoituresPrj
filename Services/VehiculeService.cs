using ExpressVoitures.Controllers;
using ExpressVoitures.Data;
using ExpressVoitures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Services
{
    [Authorize(Roles = "Admin")]
    public class VehiculeService
    {        
        public VehiculeService()
        {
        }

        /// <summary>
        /// CarToSaleButtonDisplay Method ==> define if "Mettre en vente" button has to be displayed.
        /// </summary>      
        /// <return>True if button "Mettre en vente" has to be displayed.</return> 
        /// <remarks></remarks>
        public bool CarToSaleButtonDisplay(Vehicule vehicule)
        {
            bool bToSaleButtonDisplay = false;                                  

            if ((vehicule.DateMisEnVente != null) && (vehicule.PrixDeVente > 0) && (vehicule.MisEnVente == false) && (vehicule.Statut == VehiculeStatuts.Achat))
            {
                bToSaleButtonDisplay = true;
            }

            return bToSaleButtonDisplay;
        }

        /// <summary>
        /// CarSoldButtonDisplay Method  ==> define if "Vendu" button has to be displayed.      
        /// </summary>
        /// <return>True if button "Vendu" has to be displayed.</return>
        /// <remarks></remarks>        
        public bool CarSoldButtonDisplay(Vehicule vehicule)
        {
            bool bSoldButtonDisplay = false;                           

            if ((vehicule.DateVente != null) && (vehicule.Vendu == false) && (vehicule.Statut == VehiculeStatuts.EnVente))
            {
                bSoldButtonDisplay = true;
            }

            return bSoldButtonDisplay;
        }

        /// <summary>
        /// CarUnavailableButtonDisplay Method  ==> define if "Non disponible" 
        /// or "Disponible" buttons has to be displayed.    
        /// </summary>
        /// <return>0: nothing to do. 1: "Non disponible" button. 2: "Disponible" button.</return>
        /// <remarks></remarks>        
        public int CarUnavailableButtonDisplay(Vehicule vehicule)
        {
            int returnValue = 0;

            if (vehicule.Statut == VehiculeStatuts.EnVente)
            {
                returnValue = 1;
            }
            if (vehicule.Statut == VehiculeStatuts.Indisponible)
            {
                returnValue = 2;
            }
            return returnValue;
        }
    }
}
