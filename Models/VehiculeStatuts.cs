using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    /// <summary>
    /// VehiculeStatuts enum Class.
    /// Status of a Vehicule (see Statut field).
    /// </summary>    
    public enum VehiculeStatuts
    {
        [Display(Name = "Achat")]
        Achat = 0,        
        [Display(Name = "En vente")]
        EnVente = 1,
        [Display(Name = "Vendu")]
        Vendu = 2,
        [Display(Name = "Indisponible")]
        Indisponible = 3
    }
}
