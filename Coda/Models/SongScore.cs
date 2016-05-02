using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coda.Models
{
    public class SongScore
    {

        public int Id { get; set; }
        public int SongId { get; set; }
        public int Score { get; set; }


    }
}
