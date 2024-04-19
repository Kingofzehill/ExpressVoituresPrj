using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //pour [ForeignKey(<nom champs>)]

namespace ExpressVoitures.Models
{
    //[PrimaryKey(nameof(VehiculeId), nameof(ReparationId))]
    // Many to Many relationship modelisation
    //      ==> https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many#many-to-many-with-class-for-join-entity
    public class ReparationVehicule
    {
        // Code VIN (clé primaire composée)                
        //[Key, Column(Order = 0)]        
        //[ForeignKey("VehiculesId")]
        //[PrimaryKey]
        public int VehiculesId { get; set; }

        // ReparationId (clé primaire composée)
        //[Key, Column(Order = 1)]
        //[ForeignKey("ReparationsId")]
        //[PrimaryKey]
        public int ReparationsId { get; set; }

        //public Vehicule Vehicule { get; set; } = null!;
        //public Reparation Reparation { get; set; } = null!;

        // ***SET ==> Private 
        [Display(Name = "Coût réparation")]
        //BUG003 : add precison to decimal type field [Precision(XX, X)]
        //      exception : Microsoft.EntityFrameworkCore.Model.Validation[30000]       No store type was specified for the decimal property         
        [DataType(DataType.Currency), Range(1, 9999), Precision(4, 2), RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir un prix au format XXXX,XX")]
        public decimal CoutReparation { get; set; }
    }
}
