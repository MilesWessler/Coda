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
        

    }
}