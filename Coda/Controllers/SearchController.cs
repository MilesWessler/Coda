using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coda.Models;
using Coda.Models.Repository;

namespace Coda.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public PartialViewResult SearchArtists(string keyword)
        {
            // System.Threading.Thread.Sleep(2000);
            var data = db.Artists.Where(f => f.Name.Contains(keyword)).ToList();
            return PartialView(data);
        }

    }
}