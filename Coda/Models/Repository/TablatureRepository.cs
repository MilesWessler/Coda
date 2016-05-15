using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coda.Models.Repository
{
    public class TablatureRepository : Repository<Tablature>
    {

        ApplicationDbContext db = new ApplicationDbContext();
        public IQueryable<IGrouping<int, Tablature>> FlagSong()
        {

            var flagSong = from tab in db.Tabulatures
                           group tab by tab.Song.Id
                           into groupedSongs
                           where groupedSongs.Select(x => x.Rating).Average() <= 4
                           select groupedSongs;

            return flagSong;

        }
    }
}
