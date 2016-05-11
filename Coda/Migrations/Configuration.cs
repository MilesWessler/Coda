using System.Collections.Generic;
using Coda.Controllers;
using Coda.Models;

namespace Coda.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Coda.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Coda.Models.ApplicationDbContext context)
        {
        //    List<Genre> genres = new List<Genre>
        //        {
        //            new Genre
        //            {
        //                Name = "Rock"
        //            },
        //            new Genre
        //            {
        //                Name = "Metal"
        //            },
        //            new Genre
        //            {
        //                Name = "Classic Rock"
        //            },
        //            new Genre
        //            {
        //                Name = "Punk Rock"
        //            },
        //            new Genre
        //            {
        //                Name = "Grunge"
        //            },
        //            new Genre
        //            {
        //                Name = "Blues"
        //            },
        //            new Genre
        //            {
        //                Name = "R&B"
        //            },
        //            new Genre
        //            {
        //                Name = "Pop"
        //            }

        //        };
        //    genres.ForEach(b => context.Genres.Add(b));

        //    Artist ledZepplin = new Artist
        //    {
        //        Name = "Led Zeppelin"
        //    };
        //    Artist queen = new Artist
        //    {
        //        Name = "Queen"
        //    };
        //    Artist ccr = new Artist
        //    {
        //        Name = "Creedence Clearwater Revival"
        //    };
        //    Artist prince = new Artist
        //    {
        //        Name = "Prince"
        //    };
        //    Artist pearlJam = new Artist
        //    {
        //        Name = "Pearl Jam"
        //    };
        //    Artist clapton = new Artist
        //    {
        //        Name = "Eric Clapton"
        //    };
        //    Artist tool = new Artist
        //    {
        //        Name = "Tool"
        //    };
        //    Artist pinkFloyd = new Artist
        //    {
        //        Name = "Pink Floyd"
        //    };
        //    Artist zzTop = new Artist
        //    {
        //        Name = "ZZ Top"
        //    };
        //    Artist aChains = new Artist
        //    {
        //        Name = "Alice in Chains"
        //    };
        //    Artist Jimi = new Artist
        //    {
        //        Name = "Jim Hendrix"
        //    };
        //    List<Instrument> instruments = new List<Instrument>()
        //{
        //    new Instrument()
        //    {
        //        Name = "Guitar"
        //    },
        //    new Instrument()
        //    {
        //        Name = "Drums"
        //    },
        //    new Instrument()
        //    {
        //        Name = "Bass"
        //    },
        //    new Instrument()
        //    {
        //        Name = "Vocals"
        //    },
        //};
        //    instruments.ForEach(b => context.Instruments.Add(b));


        //    List<Song> songs = new List<Song>
        //        {
        //            new Song
        //            {
        //                Artist = ledZepplin,
        //                Name = "StairWay to Heaven"
        //            },
        //            new Song
        //            {
        //                Artist = ledZepplin,
        //                Name = "Over the Hills and Far Away"
        //            },
        //            new Song
        //            {
        //                Artist = queen,
        //                Name = "Bohemian Rhapsody"
        //            },
        //            new Song
        //            {
        //                Artist = ccr,
        //                Name = "Fortunate Son"
        //            },
        //                new Song
        //            {
        //                Artist = prince,
        //                Name = "1999"
        //            },
        //            new Song
        //            {
        //                Artist = prince,
        //                Name = "Purple Rain"
        //            },
        //            new Song
        //            {
        //                Artist = aChains,
        //                Name = "Rooster"
        //            },
        //            new Song
        //            {
        //                Artist = clapton,
        //                Name = "Tears in Heaven"
        //            },
        //                new Song
        //            {
        //                Artist = clapton,
        //                Name = "Crossroads"
        //            },
        //            new Song
        //            {
        //                Artist = pinkFloyd,
        //                Name = "Brick in the Wall"
        //            },
        //            new Song
        //            {
        //                Artist = pinkFloyd,
        //                Name = "Time"
        //            },
        //            new Song
        //            {
        //                Artist = zzTop,
        //                Name = "La Grange"
        //            },
        //                new Song
        //            {
        //                Artist = tool,
        //                Name = "Sober"
        //            },
        //            new Song
        //            {
        //                Artist = Jimi,
        //                Name = "Purple Haze"
        //            },
        //            new Song
        //            {
        //                Artist = Jimi,
        //                Name = "Little Wing"
        //            },
        //            new Song
        //            {
        //                Artist = pearlJam,
        //                Name = "Black"
        //            },

        //        };

        //    songs.ForEach(b => context.Songs.Add(b));
        }

    }
}
