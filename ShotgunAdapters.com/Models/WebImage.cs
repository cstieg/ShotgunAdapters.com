﻿using Cstieg.Image;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShotgunAdapters.Models
{
    public class WebImage : WebImageBase
    {
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}