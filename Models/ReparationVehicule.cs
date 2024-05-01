using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        [Display(Name = "Coût réparation")]
        // UPD14 change attribut Precision(7, 2) for [Column(TypeName = "decimal(7, 2)")]
        // UPD07 update property ReparationVehicule.CoutReparation ==> from Precision(4, 2) to Precision(7, 2)
        [Column(TypeName = "decimal(7, 2)"), DataType(DataType.Currency), Range(1, 99999), RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir un prix au format XXXX,XX")]
        public decimal CoutReparation { get; set; }
    }
}
