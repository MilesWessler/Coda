using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coda.Models;
using System.Data.Entity;
using System.IO;
using Coda.Models.Repository;

namespace Coda.Controllers
{
    public class RatingController : Controller
    {
        SongRepository sp = new SongRepository();

        public ActionResult Details()
        {
            return View(sp.FlagSong());
        }

    }
}
