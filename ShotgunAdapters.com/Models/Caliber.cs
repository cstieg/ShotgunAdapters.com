using System.ComponentModel.DataAnnotations;

namespace ShotgunAdapters.com.Models
{
    public class Caliber
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Diameter { get; set; }
    }
}