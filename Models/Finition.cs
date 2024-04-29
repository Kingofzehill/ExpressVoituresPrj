using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Models
{
    /// <summary>
    /// Finition Data Model Class.
    /// </summary>
    /// <remarks>ForeignKey with Modele (Many To one).</remarks>
    /// <remarks>Linked to Vehicule (One to Many).</remarks>
    public class Finition
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "Saisir une finition"), Display(Name = "Finition"), StringLength(50)]
// ***SET ==>  [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string LibelleFinition { get; set; } = "";

        //[Display(Name = "Modèle")]
        public int ModeleId { get; set; } // Required foreign key property
        public virtual Modele Modele { get; set; } = null!; // Required reference navigation to principal
        
        public virtual ICollection<Vehicule> Vehicules { get; set; } = new List<Vehicule>(); // Collection navigation containing dependents

        /*  selectList for modelView
            //[NotMapped] Attribute in Properties
            //      For the following Property, no database column will be created 
            //      https://dotnettutorials.net/lesson/notmapped-attribute-in-entity-framework-core/#:~:text=The%20NotMapped%20Attribute%20in%20Entity,accessors)%20in%20an%20entity%20class.
            
            [NotMapped]
            public SelectList Marques { get; set; }  
            [NotMapped]
            public SelectList Modeles { get; set; }           
         */
    }
}
