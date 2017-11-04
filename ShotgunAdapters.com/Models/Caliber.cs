using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShotgunAdapters.Models
{
    public class Caliber
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Diameter in Inches")]
        [Index]
        public decimal? Diameter { get; set; }

        [Display(Name = "Display in Menu")]
        [Index]
        public bool DisplayInMenu { get; set; }
    }
}