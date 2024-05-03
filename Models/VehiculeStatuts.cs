using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    //Enumération des statuts possibles d'un Vehicule
    //      https://www.bytehide.com/blog/enum-csharp
    //      ** ENum in model classes https://education.launchcode.org/csharp-web-dev-curriculum/enums/reading/enums-in-models/index.html
    //      var currentStatut = VehiculeStatuts.Achat.ToString(); // renvoie une string du statut

    //FIX001 : Display Enum values in Views 
    //      https://stackoverflow.com/questions/24334761/mvc-5-1-razor-displayfor-not-working-with-enum-displayname
    //      https://stackoverflow.com/questions/13099834/how-to-get-the-display-name-attribute-of-an-enum-member-via-mvc-razor-code

    /// <summary>
    /// VehiculeStatuts enum Class.
    /// Status of a Vehicule (see Statut field).
    /// </summary>
    // TD15 remove statut Reparation à faire/Reparation en cours and re-numerate them.
    // TD16 add Action button in create view for Indisponible.
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
