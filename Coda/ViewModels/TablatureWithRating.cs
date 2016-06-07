using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coda.Models;

namespace Coda.ViewModels
{
    public class TablatureWithRating
    {
        public int? Id { get; set; }
        public double Rating { get; set; }
        public double AverageRating { get; set; }
        public double TotalRaters { get; set; }
        public double UserRating { get; set; }
        public string UserId { get; set; }
        public bool HasRated { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public int PageViews { get; set; }

        public string Staff1 { get; set; }
        public string Staff2 { get; set; }
        public string Staff3 { get; set; }
        public string Staff4 { get; set; }
        public string Staff5 { get; set; }
        public string Staff6 { get; set; }
        public string Staff7 { get; set; }
        public string Staff8 { get; set; }
        public string Staff9 { get; set; }
        public string Staff10 { get; set; }




    }
}