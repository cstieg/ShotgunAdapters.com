using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cstieg.Image;

namespace ShotgunAdapters.Models
{
    public class Product : Cstieg.ShoppingCart.ProductBase
    {
        [ForeignKey("GunCaliber")]
        [Display(Name = "Gun Caliber")]
        public int GunCaliberId { get; set; }
        public virtual Caliber GunCaliber { get; set; }

        [ForeignKey("AmmunitionCaliber")]
        [Display(Name = "Ammunition Caliber")]
        public int AmmunitionCaliberId { get; set; }
        public virtual Caliber AmmunitionCaliber { get; set; }

        [StringLength(2000)]
        public string ProductInfo { get; set; }

        [InverseProperty("Product")]
        public IEnumerable<WebImage> WebImages { get; set; }
    }

}
