using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    /// <summary>
    /// Reparation Data Model Class.
    /// </summary>
    /// <remarks>many to many : une Réparation est réalisée sur des Vehicules.</remarks>
    /// <remarks>==>  https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many</remarks>
    public class Reparation
    {
        public int Id { get; set; }

        [Required, Display(Name = "Réparation"), StringLength(50)]
        public string? LibelleReparation { get; set; }

        public List<Vehicule> Vehicules { get; } = [];
    }
}
