using Newtonsoft.Json;
using System.Collections.Generic;
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

        [Index]
        [Display(Name = "Diameter in Inches")]
        [DisplayFormat(DataFormatString = "{0:#.###}")]
        public decimal? Diameter { get; set; }

        [Display(Name = "Display in Menu")]
        [Index]
        public bool DisplayInMenu { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<Product> ProductsAmmunition { get; set; }
    }
}