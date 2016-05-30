using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coda.Models
{
    public class Tablature
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public virtual Song Song { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime TimeCreated { get; set; }
        public int TotalRaters { get; set; }
        public IQueryable<double> AverageRating { get; set; }
        public int PageViews { get; set; }
        public string Message { get; set; }
        //public int? TablatureRatingId { get; set; }
        //public virtual TablatureRating TablatureRating { get; set; }
        public virtual MemberProfile MemberProfile { get; set; }

        public virtual List<TablatureRating> TablatureRatings { get; set; }
    }
}