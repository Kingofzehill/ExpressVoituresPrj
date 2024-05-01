using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
    //UPD13 image support (class image)
    public class Image
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Path { get; set; }

        // [NotMapped]: property not to create in DB.
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile File { get; set; }

        public int VehiculeId { get; set; }
        public Vehicule? Vehicule { get; set; } //navigation property for 1/1 relationship with Vehicule (PARENT)
    }
}
