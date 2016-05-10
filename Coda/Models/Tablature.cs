using System;
using System.Collections.Generic;
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
        public string Rating { get; set; }
        public int PageViews { get; set; }
    }
}