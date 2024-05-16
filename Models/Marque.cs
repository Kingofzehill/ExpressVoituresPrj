using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    /// <summary>
    /// Marque Data Model Class.
    /// </summary>    
    /// <remarks></remarks>
    public class Marque
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "Saisir une Marque"), Display(Name = "Marque"), StringLength(50)] 
        public string LibelleMarque { get; set; } = "";

        public virtual ICollection<Modele> Modeles { get; set; } = new List<Modele>(); // Collection navigation containing dependents.
    }
}
