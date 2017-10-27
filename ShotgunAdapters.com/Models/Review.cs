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

        [StringLength(30)]
        public string Person { get; set; }

        public DateTime? Date { get; set; }

        public string Location { get; set; }

        [StringLength(2000)]
        public string Text { get; set; }
        
        public string GetReviewer()
        {
            string reviewerText = "";
            if (Person != null & Person != "")
            {
                reviewerText += "By " + Person + " ";
            }
            if (Location != null & Location != "")
            {
                reviewerText += "(" + Location + ")";
            }

            return reviewerText;
        }
    }
}