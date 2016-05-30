using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coda.Models
{
    public class TablatureRating
    {
        public int Id { get; set; }
        public int? TablatureID { get; set; }
        public double Rating { get; set; }
        //public int TotalRaters { get; set; }
        //public double AverageRating { get; set; }
        public virtual Tablature Tablature { get; set; }
    }
}
