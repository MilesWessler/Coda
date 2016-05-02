using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Coda.Models.Repository
{
    public class SongRepository : Repository<SongScore>
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IQueryable<IGrouping<int, SongScore>> FlagSong()
        {

            var flagSong = from song in db.SongScores

                           group song by song.SongId
                            into groupedSongs
                           where groupedSongs.Select(x => x.Score).Average() <= 4
                           select groupedSongs;

            return flagSong;

        }



    }
}





