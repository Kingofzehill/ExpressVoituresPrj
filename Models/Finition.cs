using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Models
{
    /// <summary>
    /// Finition Data Model Class.
    /// </summary>        
    /// <remarks>one to many : une Finition est associée à un Modèle..</remarks>
    /// <remarks>==> https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#required-one-to-many</remarks>
    public class Finition
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "Saisir une finition"), Display(Name = "Finition"), StringLength(50)]
        public string LibelleFinition { get; set; } = "";

        public int ModeleId { get; set; } // Required foreign key property.
        public virtual Modele Modele { get; set; } = null!; // Required reference navigation to principal.
        
        public virtual ICollection<Vehicule> Vehicules { get; set; } = new List<Vehicule>(); // Collection navigation containing dependents.
    }
}
