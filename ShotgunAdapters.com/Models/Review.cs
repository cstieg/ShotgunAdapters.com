using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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

        [Index]
        public DateTime? Date { get; set; }

        public string Location { get; set; }

        [StringLength(2000)]
        [AllowHtml]
        public string Text { get; set; }
        
        public string GetReviewer()
        {
            string reviewerText = "";
            if (Person != null & Person != "")
            {
                reviewerText += "by " + Person + " ";
            }
            if (Location != null & Location != "")
            {
                reviewerText += "(" + Location + ")";
            }

            return reviewerText;
        }
    }
}