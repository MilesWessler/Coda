using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Coda.Models
{
    public class MyUser : IdentityUser
    {
        public virtual ICollection<MemberProfile> MemberProfiles { get; set; } 
    }
}
