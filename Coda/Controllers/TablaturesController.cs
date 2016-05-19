using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Coda.Models;
using Coda.Models.Repository;
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

        // GET: Tabulatures/Details/5
        public ActionResult Details(int? id)
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
            tablature.PageViews++;
            db.SaveChanges();
            return View(tablature);
        }

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
            MemberProfile profile = db.MemberProfiles.Select(x => x).FirstOrDefault(t => t.Eamil == user.Email);
            if (ModelState.IsValid)
            {
                Tablature tabToAdd = new Tablature
                {
                    Content = tablature.Content,
                    Song = db.Songs.Select(x => x).Where(t => t.Id == tablature.SongId).FirstOrDefault(),
                    TimeCreated = DateTime.Now,
                    EmailOfCreator = user.Email
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
        [HttpPost]
        public ActionResult Rate(Tablature model)
        {
            model.Rating = DevExpress.Web.Mvc.RatingControlExtension.GetValue("Rating");
            return View("Details", model);
        }
       
        public ActionResult FlaggedTabs()
        {
            List<Tablature> flag = db.Tabulatures.Select(x => x).Where(x => x.Rating < 4).OrderByDescending(x => x.Rating).ToList();
            return View(flag);
        }
        public ActionResult TopRatedTabs()
        {
            List<Tablature> top10 = db.Tabulatures.Select(x => x).Where(x => x.Rating >= 4).OrderByDescending(x => x.Rating).ToList();
            return View(top10);
        }
    }
}
