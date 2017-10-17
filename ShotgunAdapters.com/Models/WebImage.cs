using System.ComponentModel.DataAnnotations.Schema;

namespace ShotgunAdapters.com.Models
{
    public class WebImage : Cstieg.Image.WebImageBase
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } 
    }
}