using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coda.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double Amount { get; set; }
    }
}