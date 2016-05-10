using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGrease;

namespace Coda.Models
{
    [Table("RateLogs")]
    public class RateLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Int16 SectionId { get; set; }
        public int VoteForId { get; set; }
        public string UserName { get; set; }
        public Int16 Vote { get; set; }
        public bool Active { get; set; }

    }
}
