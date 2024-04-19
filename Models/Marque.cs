using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    // 
    public class Marque
    {
        // IdMarque (clé primaire)
        //[Key()]
        //[Required, Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Saisir une Marque"), Display(Name = "Marque"), StringLength(50)]
        // ***SET ==> [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string LibelleMarque { get; set; }

        // liste des modèles de la marque
        public virtual ICollection<Modele> Modeles { get; set; } = new List<Modele>(); // Collection navigation containing dependents
    }
}
