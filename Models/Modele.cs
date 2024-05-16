using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{    
    /// <summary>
    /// Vehicule Data Model Class.
    /// </summary>    
    /// <remarks>one to many : un Modèle est associé à une Marque.</remarks>
    /// <remarks>==> https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#required-one-to-many</remarks>
    public class Modele
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "Saisir un modèle"), Display(Name = "Modèle"), StringLength(50)]
        public string LibelleModele { get; set; } = "";
        
        public int MarqueId { get; set; } // Required foreign key property.
        public Marque Marque { get; set; } = null!; // Required reference navigation to principal.

        public virtual ICollection<Finition> Finitions { get; set; } = new List<Finition>(); // Collection navigation containing dependents.
    }
}
