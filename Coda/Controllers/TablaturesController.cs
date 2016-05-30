using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Coda.Models;
using Coda.Models.Repository;
using Coda.ViewModels;
using DevExpress.DashboardCommon.DataProcessing.InMemoryDataProcessor.Executors;
using DevExpress.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace Coda.Controllers
{
    public class TablaturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tabulatures
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return View(db.Tabulatures.ToList());
            }
            else
            {
                return View(db.Tabulatures.Select(x => x).Where(t => t.SongId == id).ToList());
            }
        }

        //public ActionResult Details()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Details([Bind(Include = "Id,TablatureID,Rating")] TablatureRating model, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tablature tab = db.Tabulatures.Find(id);
        //    if (tab == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    TablatureRating rating = new TablatureRating
        //    {
        //        Rating = model.Rating,
        //        TablatureID = tab.Id,
        //        Tablature = tab,

        //    };

        //    tab.TotalRaters = db.TablatureRatings.Count(t => t.TablatureID == rating.TablatureID);
        //    tab.Rating += rating.Rating;


        //    TablatureWithRating twr = new TablatureWithRating
        //    {
        //        Id = tab.Id,
        //        Rating = rating.Rating,
        //        TotalRaters = tab.TotalRaters,
        //        AverageRating = tab.Rating / tab.TotalRaters
        //    };
        //    //tab.PageViews++;

        //    db.TablatureRatings.Add(rating);
        //    db.SaveChanges();
        //    return View(twr);

        //}

        public ActionResult Details(int? id, TablatureWithRating twr)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tablature tab = db.Tabulatures.Find(id);

            tab.TotalRaters = db.TablatureRatings.Count(t => t.TablatureID == tab.Id);
            //tab.Rating += rating.Rating;
            if (tab == null)
            {
                return HttpNotFound();
            }
            twr.Id = tab.Id;
            twr.AverageRating  = tab.Rating/tab.TotalRaters;
           
            return View(twr);
        }

        //public ActionResult _Rating()
        //{
        //    return View("Details"); 
        //}

        [HttpPost]
      public ActionResult _Rating(TablatureRating model, int? id)
        {
            Tablature tab = db.Tabulatures.Find(id);
            TablatureRating rating = new TablatureRating
            {
                Rating = model.Rating,
                TablatureID = tab.Id,
                Tablature = tab,

            };
            tab.TotalRaters = db.TablatureRatings.Count(t => t.TablatureID == rating.TablatureID);
            tab.Rating += rating.Rating;
            TablatureWithRating twr = new TablatureWithRating
            {
                Id = rating.TablatureID,
                Rating = rating.Rating,
                TotalRaters = tab.TotalRaters,
                //AverageRating = tab.Rating / tab.TotalRaters
            };

            db.TablatureRatings.Add(rating);
            db.SaveChanges();

            return RedirectToAction("Details", new {id = twr.Id});

        }







        //[HttpPost]
        //public string Details([Bind(Include = "Id,TablatureID,Rating")] TablatureRating model, int? id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        /*db.Tabulatures.Select(x => x).FirstOrDefault(t => t.Id == model.TablatureID)*/

        //        Tablature tab = db.Tabulatures.Find(id);

        //        TablatureRating rating = new TablatureRating
        //        {
        //            Rating = model.Rating,
        //            TablatureID = tab.Id,
        //            Tablature = tab
        //        };

        //        db.TablatureRatings.Add(rating);

        //        db.SaveChanges();
        //        return View("")
        //    }

        //}

        // GET: Tabulatures/Create
        public ActionResult Create(int? id)
        {
            ViewBag.SongId = new SelectList(db.Songs, "Id", "Name");
            //ViewBag.SongId = id;
            return View();
        }

        // POST: Tabulatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Content,SongId")] Tablature tablature)
        {
            ApplicationUser user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            MemberProfile profile = db.MemberProfiles.Select(x => x).FirstOrDefault(t => t.Email == user.Email);
            if (ModelState.IsValid)
            {
                Tablature tabToAdd = new Tablature
                {
                    Content = tablature.Content,
                    Song = db.Songs.Select(x => x).FirstOrDefault(t => t.Id == tablature.SongId),
                    TimeCreated = DateTime.Now,
                    MemberProfile = profile,

                };
                db.Tabulatures.Add(tabToAdd);
                //db.Tabulatures.Add(Tablature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tablature);
        }

        // GET: Tabulatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tablature tablature = db.Tabulatures.Find(id);
            if (tablature == null)
            {
                return HttpNotFound();
            }
            return View(tablature);
        }

        // POST: Tabulatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content")] Tablature tablature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tablature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tablature);
        }

        // GET: Tabulatures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tablature tablature = db.Tabulatures.Find(id);
            if (tablature == null)
            {
                return HttpNotFound();
            }
            return View(tablature);
        }

        // POST: Tabulatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tablature tablature = db.Tabulatures.Find(id);
            db.Tabulatures.Remove(tablature);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Top10()
        {
            List<Tablature> top10 = db.Tabulatures.Select(x => x).OrderByDescending(t => t.PageViews).Take(10).ToList();
            return View(top10);
        }

        //[HttpPost]
        //public ActionResult Rate(Tablature model)
        //{
        //    model.Rating = DevExpress.Web.Mvc.RatingControlExtension.GetValue(model.Rating);
        //    return View("Details", model);
        //}

        public ActionResult FlaggedTabs()
        {
            List<Tablature> flag =
                db.Tabulatures.Select(x => x).Where(x => x.Rating < 4).OrderByDescending(x => x.Rating).ToList();
            return View(flag);
        }

        public ActionResult TopRatedTabs()
        {
            List<Tablature> top10 =
                db.Tabulatures.Select(x => x).Where(x => x.Rating >= 4).OrderByDescending(x => x.Rating).ToList();
            return View(top10);
        }


        //[HttpPost]
        //public ActionResult HtmlPost([Bind(Include = "Id,TablatureID,Rating")] TablatureRating model, int? id)
        //{
        //    ViewBag.Average = db.TablatureRatings.GroupBy(x => x.TablatureID).Select()
        //    if (ModelState.IsValid)
        //    {
        //        //model.Message = string.Format("Post new value = {0}", model.Rating);
        //        Tablature tab = db.Tabulatures.Find(id);
        //        TablatureRating rating = new TablatureRating
        //        {
        //            Rating = model.Rating,
        //            TablatureID = tab.Id,
        //            Tablature = tab
        //        };

        //        db.TablatureRatings.Add(rating);

        //        db.SaveChanges();
        //    }

        //}
    }
}

//  [HttpPost]
//public ActionResult AjaxPost([Bind(Include = "Id,TablatureID,Rating")]TablatureRating model, int? id)
//{
//    if (ModelState.IsValid)
//    {/*db.Tabulatures.Select(x => x).FirstOrDefault(t => t.Id == model.TablatureID)*/

//        Tablature tab = db.Tabulatures.Find(id);

//        TablatureRating rating = new TablatureRating
//        {
//            Rating = model.Rating,
//         TablatureID = tab.Id,
//         Tablature = tab


//        };

//        db.TablatureRatings.Add(rating);

//        db.SaveChanges();

//        return View("Rating", model);
//    }
//    return Content("Invalid model!");
//}




