using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coda.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public int PricePerHour { get; set; }
        public string Content { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime InstructorSince { get; set; }
        public string Instrument { get; set; }
        public string BusinessAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int NumberOfTabPosts { get; set; }
        public bool HasPayed { get; set; }
        public virtual MemberProfile MemberProfile { get; set; }

    }
}