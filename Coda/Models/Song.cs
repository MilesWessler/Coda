using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coda.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Tabulature> Tabulature  { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
    }
}