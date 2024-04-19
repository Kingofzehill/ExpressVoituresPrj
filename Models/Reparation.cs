using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    public class Reparation
    {
        // IdReparation (clé primaire)
        //[Key()]
        //[Required, Display(Name = "Id")]
        public int Id { get; set; }

        [Required, Display(Name = "Réparation"), StringLength(50)]
        // ***SET ==>  [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string? LibelleReparation { get; set; }

        // Navigation avec ReparationsVehicule        
        public List<Vehicule> Vehicules { get; } = [];
        //public List<ReparationVehicule> ReparationVehicules { get; } = [];
    }
}
