using System.ComponentModel.DataAnnotations;

namespace ShotgunAdapters.Models
{
    public class Caliber
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Diameter in Inches")]
        public decimal? Diameter { get; set; }
    }
}