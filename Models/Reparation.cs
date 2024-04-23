using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    /// <summary>
    /// Reparation Data Model Class.
    /// </summary>
    /// <remarks>ForeignKey with Marque (Many To One).</remarks>
    /// <remarks>Assocition with Vehicule (Many to Many (ReparationVehicule)).</remarks>
    public class Reparation
    {
        public int Id { get; set; }

        [Required, Display(Name = "Réparation"), StringLength(50)]
// ***SET ==>  [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string? LibelleReparation { get; set; }

        public List<Vehicule> Vehicules { get; } = [];
    }
}
