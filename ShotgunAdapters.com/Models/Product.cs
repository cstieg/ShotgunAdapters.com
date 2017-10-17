using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cstieg.Image;

namespace ShotgunAdapters.Models
{
    public class Product : Cstieg.ShoppingCart.ProductBase
    {
        [ForeignKey("GunCaliber")]
        public int GunCaliberId { get; set; }
        public virtual Caliber GunCaliber { get; set; }

        [ForeignKey("AmmunitionCaliber")]
        public int AmmunitionCaliberId { get; set; }
        public virtual Caliber AmmunitionCaliber { get; set; }

        [StringLength(2000)]
        public string ProductInfo { get; set; }

        [InverseProperty("Product")]
        public IEnumerable<WebImage> WebImages { get; set; }
    }

}
