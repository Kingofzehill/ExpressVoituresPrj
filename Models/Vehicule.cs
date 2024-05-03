using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //pour [ForeignKey(<nom champs>)]

/* Memento
 * TD05 Use ressource for [Display] [Display(ResourceType = typeof(MyResources), Name = "Name")]
 *      https://stackoverflow.com/questions/35384987/what-is-a-simple-explanation-for-displayfor-and-displaynamefor-in-asp-net
 * Use ressource for Error message [Required(ErrorMessageResourceName = "<MessageKey>", ErrorMessageResourceType = typeof(Resources.ProductService))]
 * */

/* Entity Framework, Model classes relationships (primarykey and foreignkeys) : 
 *        https://learn.microsoft.com/en-us/ef/core/modeling/relationships
 *         https://learn.microsoft.com/en-us/ef/core/modeling/relationships/foreign-and-principal-keys
 *         Entity Framework, One-to-many relationships : https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
 *         Entity Framework, One-to-one relationships : https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-one
 *         Entity Framework, Many-to-many relationships : https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
 *         */

// Images mngt ==>
// ***** https://www.binaryintellect.net/articles/2f55345c-1fcb-4262-89f4-c4319f95c5bd.aspx
// *** https://dev.to/karenpayneoregon/sql-server-working-with-images-dp3
// *** https://www.aspsnippets.com/Articles/2342/Save-Insert-Image-file-in-Database-using-Entity-Framework-in-ASPNet-MVC/
// *** https://learn.microsoft.com/en-us/answers/questions/682240/best-practice-for-saving-image-in-database
// https://stackoverflow.com/questions/11208229/image-data-type-in-sql-server-comapct-4-code-first-entity-framewwork-5
// ** https://www.c-sharpcorner.com/UploadFile/0c1bb2/uploading-images-to-database-using-Asp-Net-C-Sharp/
// https://learn.microsoft.com/fr-fr/dotnet/api/system.windows.forms.datavisualization.charting.imageannotation?view=netframework-4.8.1
// ** https://stackoverflow.com/questions/73680444/how-can-i-save-image-file-to-sql-database-with-net-core-and-ef
// https://www.codeproject.com/Articles/658522/Storing-Images-in-SQL-Server-using-EF-and-ASP-NET

/// <summary>
/// Vehicule Data Model Class
/// </summary>
/// <remarks>ForeignKey with Finition (One to Many).</remarks>
/// <remarks>Association with Reparation (Many to Many (ReparationVehicule)).</remarks>
/// <remarks>Association with Image (One to One).</remarks>
namespace ExpressVoitures.Models
{
    public class Vehicule
    {        
        public const int carYearMinimum = 1990;
        public const decimal margisMinimum = 500M;

        public int Id { get; set; }

        //[Required(ErrorMessage = "Saisir code VIN"), Display(Name = "VIN"), StringLength(17, MinimumLength = 17)]
        //[Required(ErrorMessage = "Saisir {0}")]
        [Display(Name = "VIN")]
        /* Bug001 : MinimumLength = 17 cause validation error
                [StringLength(17, MinimumLength = 17)]
         */
        [StringLength(17, ErrorMessage = "{0} doit être de {1} caractères")]
        public string Vin { get; set; } = "";

        [Required(ErrorMessage = "Sélectionner {0}")]
        public VehiculeStatuts Statut { get; set; }

        [StringLength(200, ErrorMessage = "{0} est limité à {1} caractères")]        
        public string? Information { get; set; }
        
        [Required(ErrorMessage = "Saisir {0}")]
        [Display(Name = "Date d'achat")]
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]        
        public DateTime DateAchat { get; set; }

        [Required(ErrorMessage = "Saisir {0}")]
        [Display(Name = "Prix d'achat")]
        // FIX03 : update precision from (2, 2) to (7, 2) as Precision(p, s) : p means both left and right of the decimal
        [Column(TypeName = "decimal(7, 2)")]
        [DataType(DataType.Currency)]
        [Range(1, 99999, ErrorMessage = "Saisir un {0} au format XXXXX.XX")]
        [RegularExpression(@"^[0-9]+((\.)[0-9]+)*$", ErrorMessage = "Saisir un {0} au format XXXXX.XX")]
        public decimal PrixAchat { get; set; }       

        [Required(ErrorMessage = "Saisir {0}")]
        [Display(Name = "Année")]        
        [Range(1990, 2024, ErrorMessage = "L'{0} doit être entre {1} et {2}")]        
        public int AnneeVehicule { get; set; }

        [Display(Name = "Prix de vente")]
        [Column(TypeName = "decimal(7, 2)")]
        [DataType(DataType.Currency)]
        [Range(1, 99999, ErrorMessage = "Saisir un {0} au format XXXXX.XX")]
        [RegularExpression(@"^[0-9]+((\.)[0-9]+)*$", ErrorMessage = "Saisir un {0} au format XXXXX.XX")]
        public decimal? PrixDeVente { get; set; }

        [Display(Name = "En vente")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateMisEnVente { get; set; }

        [Display(Name = "Vendue le")]
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]        
        public DateTime? DateVente { get; set; }

        // TD11 : Vehicule class, add DateMisAJour & Marge to class properties
        // FIX04 : Vehicule class, add DateMisAJour & Marge
        [Display(Name = "Actualisé le")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateMisAJour { get; set; } = DateTime.Now;

        // UPD14 change attribut Precision(7, 2) for [Column(TypeName = "decimal(7, 2)")]
        [Column(TypeName = "decimal(7, 2)")]
        [DataType(DataType.Currency)]
        [Range(1, 99999, ErrorMessage = "Saisir une {0} au format XXXXX.XX")]
        [RegularExpression(@"^[0-9]+((\.)[0-9]+)*$", ErrorMessage = "Saisir une {0} au format XXXXX.XX")]
        public decimal Marge { get; set; }

        [Required(ErrorMessage = "Sélectionner {0}")]
        [Display(Name = "Finition")]
        public int FinitionId { get; set; } // Required foreign key property
        public virtual Finition Finition { get; set; } = null!; // Required reference navigation to principal                                                                        

        public List<Reparation> Reparations { get; } = [];

        //UPD13 image support (vehicule), navigation property to image
        public virtual Image? Image { get; set; } //navigation property for 1/1 relationship with Image(CHILD)
        
        public bool MisEnVente { get; set; } = false;
        
        public bool Vendu { get; set; } = false;
    }
}
