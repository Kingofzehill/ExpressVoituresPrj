using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    // Many to Many relationship modelisation
    //      ==> https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many#many-to-many-with-class-for-join-entity

    /// <summary>
    /// Reparation Vehicule Data Model Class.
    /// </summary>
    /// <remarks>Association between Vehicule and Reparation (Many to Many).</remarks>
    public class ReparationVehicule
    {
        public int VehiculesId { get; set; }

        public int ReparationsId { get; set; }

// ***SET ==> Private 
        [Display(Name = "Coût réparation")]
        [DataType(DataType.Currency), Range(1, 9999), Precision(4, 2), RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir un prix au format XXXX,XX")]
        public decimal CoutReparation { get; set; }
    }
}
