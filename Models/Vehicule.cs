using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //pour [ForeignKey(<nom champs>)]

namespace ExpressVoitures.Models
{
    public class Vehicule
    {
        // TD002.1 use a validate CustomAttribute
        //    https://stackoverflow.com/questions/22256925/range-attribute-using-dynamic-values-instead-of-fixed
        private double _MaxYearValue; // no readonly keyword


        //****DataAnnotations : https://www.completecsharptutorial.com/asp-net-mvc5/data-annotation-validation-with-example-in-asp-net-mvc.php
        // Identifiant VIN du vehicule (clé primaire)
        //[Key()]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        /*
            * ’attribut DatabaseGenerated avec le paramètre None spécifie 
            * que les valeurs de clé primaire sont fournies par l’utilisateur 
            * plutôt que générées par la base de données.
         */        
        public int Id { get; set; }

        [Required(ErrorMessage = "Indiquer un code VIN"), Display(Name = "VIN"), StringLength(17, MinimumLength = 17)]
        // [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string Vin { get; set; } = "";

        // ***SET ==> Private 
        // list values : Achat, Réparation à faire, Réparation en cours, Mis en vente, Vendu; Non disponible
        [Required(ErrorMessage = "Indiquer un statut"), StringLength(30)]
        public VehiculeStatuts Statut { get; set; }

        [StringLength(200, ErrorMessage = "Information est limité à 200 caractères")]
        //***SET ==> [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string? Information { get; set; } = "";

        // ***TOCHECK ==> MaxLength(4000) for byte[] type could be mapped to image SQL type
        [DataType("image"), MaxLength(4000, ErrorMessage = "l'image est limitée à 4000 bytes")]
        public byte[]? Photo { get; set; }
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

        // ***SET ==> Private 
        [Required(ErrorMessage = "Indiquer une date d'achat"), Display(Name = "Date d'achat")]
        [DataType(DataType.Date)]
        // FIX004 : add DisplayFormat to date fields
        //      Le paramètre ApplyFormatInEditMode indique que la mise en forme doit également être appliquée quand la valeur est affichée dans une zone de texte à des fins de modification. 
        //      il est cependant préférable d'utiliser datatype qui a le mérite d'etre prise en charge par HTML5 et de s'adapter aux paramètres régionaux
        // UPD002 Date format dd MMMM yyyy, 10 avril 2024
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        //TD001 Control date ok in a dates range
        // Use a variable for the range end = now
        /*
            https://stackoverflow.com/questions/1406046/data-annotation-ranges-of-dates
                [Range(typeof(DateTime), "1/2/2004", "3/4/2004",
                ErrorMessage = "Value for {0} must be between {1} and {2}")]
                But Jquery doesnot works
            See the way to control it with A validationController and dedicated method of control for the date.
        */
        public DateTime DateAchat { get; set; }

        // ***SET ==> Private 
        [Required(ErrorMessage = "Saisir un tarif")]
        [Display(Name = "Prix d'achat")]
        //BUG003 : add precison to decimal type field [Precision(XX, X)]
        //      exception : Microsoft.EntityFrameworkCore.Model.Validation[30000]       No store type was specified for the decimal property         
        [DataType(DataType.Currency), Range(1, 99999), Precision(5, 2), RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir un prix au format XXXX,XX")]
        //FIX004 : add format to date fields        
        public decimal PrixAchat { get; set; }

        // ***CHANGE ==> for year only
        //    ==> https://stackoverflow.com/questions/36228477/use-current-year-as-range-validation-in-dataannotations        
        //[BindProperty, DataType("year")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        // Le paramètre ApplyFormatInEditMode indique que la mise en forme doit également être appliquée quand la valeur est affichée dans une zone de texte à des fins de modification. 
        // il est cependant préférable d'utiliser datatype qui a le mérite d'etre prise en charge par HTML5 et de s'adapter aux paramètres régionaux

        [Required(ErrorMessage = "Saisir une année de mise en circulation"), Display(Name = "Année")]
        // UPD003 changed for int datatype with range values for controlling year between 1990 and 2024
        [Range(1990, 2024, ErrorMessage = "La valeur pour {0} doit être entre {1} et {2}")]
        // TD002 Set max range value with a private readonly variable corresponding the actual year from the now date
        public int AnneeVehicule { get; set; }

        // Vehicule mis en vente, MisEnVente (bool) définit l'accessibilité aux informations PrixDeVente et Date de mise Vente
        //      Vu avec ehod en mentorat (10/04) implémentation dans la classe. Un boolean sert de marqueur
        public bool MisEnVente { get; set; }

        // ***SET ==> Private
        [Display(Name = "Prix de vente")]
        //BUG003 : add precison to decimal type field [Precision(XX, X)]
        //      exception : Microsoft.EntityFrameworkCore.Model.Validation[30000]       No store type was specified for the decimal property         
        [DataType(DataType.Currency), Range(1, 99999), Precision(5, 2), RegularExpression(@"^[0-9]+((\,)[0-9]+)*$", ErrorMessage = "Saisir un prix au format XXXXX,XX")]
        public decimal? PrixDeVente { get; set; }
        [Display(Name = "Mis en vente"), DataType(DataType.Date)]
        // FIX004 : add DisplayFormat to date fields
        // UPD002 Date format dd MMMM yyyy, 10 avril 2024
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        // Le paramètre ApplyFormatInEditMode indique que la mise en forme doit également être appliquée quand la valeur est affichée dans une zone de texte à des fins de modification. 
        // il est cependant préférable d'utiliser datatype qui a le mérite d'etre prise en charge par HTML5 et de s'adapter aux paramètres régionaux
        public DateTime? DateMisEnVente { get; set; }

        // Vehicule vendu, Vendu (bool) définit l'accessibilité à l'information Date vente
        //      Vu avec ehod en mentorat (10/04) implémentation dans la classe. Un boolean sert de marqueur
        public bool Vendu { get; set; }
        [Display(Name = "Date vente"), DataType(DataType.Date)]
        //FIX004 : add DisplayFormat to date fields
        // UPD002 Date format dd MMMM yyyy, 10 avril 2024
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        // Le paramètre ApplyFormatInEditMode indique que la mise en forme doit également être appliquée quand la valeur est affichée dans une zone de texte à des fins de modification. 
        // il est cependant préférable d'utiliser datatype qui a le mérite d'etre prise en charge par HTML5 et de s'adapter aux paramètres régionaux
        public DateTime? DateVente { get; set; }


        // BUG002 ==> Error Number:1785,State:0,Class:16. Introducing FOREIGN KEY constraint 'FK_Vehicule_Marque_MarqueidMarque' on table 'Vehicule'
        //      may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION,
        //      or modify other FOREIGN KEY constraints.
        //      Could not create constraint or index.See previous errors.
        //      *****
        //      BUG002 RESOLUTION : remove Navigation fields properties to Marque and Modele
        //              assuming these informations can be found in the Finition of the Vehicule
        //public int MarqueId { get; set; } // ???? for storing MarqueId from Finition set to the Vehicule
        //public int ModeleId { get; set; } // ???? for storing MarqueId from Finition set to the Vehicule
        public int FinitionId { get; set; } // Required foreign key property
        public virtual Finition Finition { get; set; } = null!; // Required reference navigation to principal                                                                        

        /* Before BUG002 fix updates ==> 
                //[ForeignKey("MarqueId")]
                //idMarque exprime la propriété de clé étrangère et Marque la propriété de navigation (permet d'acceder aux propriétés de la marque)
                public int MarqueId { get; set; } // Required foreign key property
                public virtual Marque Marque { get; set; } = null!; // Required reference navigation to principal


                //[ForeignKey("ModeleId")]
                //ModeleId exprime la propriété de clé étrangère et Modele la propriété de navigation (permet d'acceder aux propriétés du modèle)
                public int ModeleId { get; set; } // Required foreign key property
                public virtual Modele Modele { get; set; } = null!; // Required reference navigation to principal

                //[ForeignKey("FinitionId")]
                //idFinition exprime la propriété de clé étrangère et Finition la propriété de navigation (permet d'acceder aux propriétés de la finition)
                public int FinitionId { get; set; } // Required foreign key property
                public virtual Finition Finition { get; set; } = null!; // Required reference navigation to principal
        */

        // Navigation avec ReparationsVehicule        
        public List<Reparation> Reparations { get; } = [];
        //public List<ReparationVehicule> ReparationVehicules { get; } = [];
    }
}
