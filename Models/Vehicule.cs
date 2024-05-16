using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Vehicule Data Model Class
/// </summary>
/// <remarks>ForeignKey with Finition (One to Many).</remarks>
/// <remarks>Association with Reparation (Many to Many (ReparationVehicule)).</remarks>
/// <remarks>Association with Image (One to One).</remarks>
/// <remarks>one to many : un Vehicule dipose d'une Finition..</remarks>
/// <remarks>==> https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#required-one-to-many</remarks>
/// <remarks>one to one : un Vehicule dispose d'une Image.</remarks>
/// <remarks>==> https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-one</remarks>
/// <remarks>many to many : un Vehicule peut disposer de Réparations.</remarks>
/// <remarks>==>  https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many</remarks>
namespace ExpressVoitures.Models
{
    public class Vehicule
    {        
        public const int carYearMinimum = 1990;
        public const decimal margisMinimum = 500M;

        public int Id { get; set; }

        [Display(Name = "VIN")]        
        [StringLength(17, ErrorMessage = "{0} doit être de {1} caractères")]
        public string Vin { get; set; } = "";

        [Required(ErrorMessage = "Sélectionner {0}")]
        public VehiculeStatuts Statut { get; set; }

        [StringLength(200, ErrorMessage = "{0} est limité à {1} caractères")]        
        public string? Information { get; set; }
        
        [Required(ErrorMessage = "Saisir {0}")]
        [Display(Name = "Date d'achat")]
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAchat { get; set; }

        [Required(ErrorMessage = "Saisir {0}")]
        [Display(Name = "Prix d'achat")]        
        [Column(TypeName = "decimal(7, 2)")] // FIX03 : update precision from (2, 2) to (7, 2) as Precision(p, s) : p means both left and right of the decimal
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir un {0} au format XXXXX,XX")]
        public decimal PrixAchat { get; set; }       

        [Required(ErrorMessage = "Saisir {0}")]
        [Display(Name = "Année")]        
        [Range(1990, 2024, ErrorMessage = "L'{0} doit être entre {1} et {2}")]        
        public int AnneeVehicule { get; set; }

        [Display(Name = "Liste des réparations")]
        [StringLength(400, ErrorMessage = "{0} est limité à {1} caractères")]
        public string? listeReparations { get; set; }

        [Display(Name = "Coût Réparations")]
        [Column(TypeName = "decimal(7, 2)")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir un {0} au format XXXXX,XX")]
        public decimal? CoutReparations { get; set; }

        [Display(Name = "Prix de vente")]
        [Column(TypeName = "decimal(7, 2)")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir un {0} au format XXXXX,XX")]
        public decimal? PrixDeVente { get; set; }

        [Display(Name = "En vente")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateMisEnVente { get; set; }

        [Display(Name = "Vendue le")]
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]        
        public DateTime? DateVente { get; set; }
        
        [Display(Name = "Actualisé le")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateMisAJour { get; set; } = DateTime.Now;
        
        [Column(TypeName = "decimal(7, 2)")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir une {0} au format XXXXX,XX")]
        public decimal Marge { get; set; }

        [Required(ErrorMessage = "Sélectionner {0}")]
        [Display(Name = "Finition")]
        public int FinitionId { get; set; } // Required foreign key property
        public virtual Finition Finition { get; set; } = null!; // Required reference navigation to principal                                                                        

        public List<Reparation> Reparations { get; } = [];
        
        public virtual Image? Image { get; set; } //navigation property for 1/1 relationship with Image(CHILD)
        
        public bool MisEnVente { get; set; } = false;
        
        public bool Vendu { get; set; } = false;

        public bool Indisponible { get; set; } = false;
    }
}
