using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coda.Models.Repository
{
    public class ArtistRepository : Repository<Artist>
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public List<Artist> GetByName(string name)
        {
            return DbSet.Where(a => a.Name.Contains(name)).ToList();
        }

        //public List<SoloArtist> GetSoloArtists()
        //{
        //    return DbSet.OfType<SoloArtist>().ToList();
        //}
    }
}
