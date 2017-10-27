using System;
using System.ComponentModel.DataAnnotations;

namespace ShotgunAdapters.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Text { get; set; }

        public DateTime? Date { get; set; }

        public string Location { get; set; }
    }
}