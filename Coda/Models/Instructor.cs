using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coda.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public int PricePerHour { get; set; }
        public string Content { get; set; }
        public DateTime InstructorSince { get; set; }
        public virtual List<Instrument> Instruments { get; set; }
        public int MemberId { get; set; }
        public virtual MemberProfile MemberProfile { get; set; }

    }
}