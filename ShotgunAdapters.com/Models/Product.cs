﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
    }

}
