using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Models
{
    // Entity Framework, Model classes relationships (primarykey and foreignkeys) : https://learn.microsoft.com/en-us/ef/core/modeling/relationships
    //                          https://learn.microsoft.com/en-us/ef/core/modeling/relationships/foreign-and-principal-keys
    // Entity Framework, One-to-many relationships : https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
    // Entity Framework, One-to-one relationships : https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-one
    // Entity Framework, Many-to-many relationships : https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
    public class Finition
    {
        // IdFinition (clé primaire)
        //[Key()]
        //[Required, Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Saisir une finition"), Display(Name = "Finition"), StringLength(50)]
        // ***SET ==>  [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string LibelleFinition { get; set; }

        //[ForeignKey("MarqueId")]
        //idMarque exprime la propriété de clé étrangère et Marque la propriété de navigation (permet d'acceder aux propriétés de la marque)
        // ***FIX ==> la marque se retrouve via le modèle (un modèle est lié à une marque)
        //public int MarqueId { get; set; }
        //public virtual Marque Marque { get; set; } = null!; // Required reference navigation to principal

        //[ForeignKey("ModeleId")]
        //idModele exprime la propriété de clé étrangère et Modele la propriété de navigation (permet d'acceder aux propriétés du modèle)
        public int ModeleId { get; set; }
        public virtual Modele Modele { get; set; } = null!; // Required reference navigation to principal

        // liste des véhicules pour une finition
        public virtual ICollection<Vehicule> Vehicules { get; set; } = new List<Vehicule>(); // Collection navigation containing dependents
    }
}
