using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coda.Models
{
    public class TablatureWithRating
    {
        public int TablatureId { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
    }
}