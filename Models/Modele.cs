using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ExpressVoitures.Models
{
    // required one to many : un modèle doit être associée à une marque.
    //      ==> https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#required-one-to-many
    public class Modele
    {
        // IdModele (clé primaire)
        //[Key()]
        //[Required, Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Saisir un modèle"), Display(Name = "Modèle"), StringLength(50)]
        // ***SET ==>  [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string LibelleModele { get; set; }

        // Marque associée au modèle
        public int MarqueId { get; set; } // Required foreign key property
        public Marque Marque { get; set; } = null!; // Required reference navigation to principal

        // liste des finitions du modèle
        public virtual ICollection<Finition> Finitions { get; set; } = new List<Finition>(); // Collection navigation containing dependents
    }
}
