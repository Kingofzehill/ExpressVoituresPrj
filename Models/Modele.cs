using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    // required one to many : un modèle doit être associée à une marque.
    //      ==> https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#required-one-to-many

    /// <summary>
    /// Vehicule Data Model Class.
    /// </summary>
    /// <remarks>ForeignKey with Marque (Many To One).</remarks>
    /// <remarks>Linked to Finition (One to Many).</remarks>
    public class Modele
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "Saisir un modèle"), Display(Name = "Modèle"), StringLength(50)]
// ***SET ==>  [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string LibelleModele { get; set; }

        public int MarqueId { get; set; } // Required foreign key property
        public Marque Marque { get; set; } = null!; // Required reference navigation to principal

        public virtual ICollection<Finition> Finitions { get; set; } = new List<Finition>(); // Collection navigation containing dependents
    }
}
