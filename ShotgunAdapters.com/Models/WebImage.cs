using Cstieg.Image;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShotgunAdapters.Models
{
    public class WebImage : WebImageBase
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}