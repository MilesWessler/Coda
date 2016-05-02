using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coda.Models;

namespace Coda.Controllers
{
    public class TabulaturesController : Controller
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
            Tabulature tabulature = db.Tabulatures.Find(id);
            if (tabulature == null)
            {
                return HttpNotFound();
            }
            tabulature.PageViews++;
            db.SaveChanges();
            return View(tabulature);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,SongId")] Tabulature tabulature)
        {
            if (ModelState.IsValid)
            {
                Tabulature tabToAdd = new Tabulature
                {
                    Content = tabulature.Content,
                    Song = db.Songs.Select(x => x).Where(t => t.Id == tabulature.SongId).FirstOrDefault()
                };
                db.Tabulatures.Add(tabToAdd);
                //db.Tabulatures.Add(tabulature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tabulature);
        }

        // GET: Tabulatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tabulature tabulature = db.Tabulatures.Find(id);
            if (tabulature == null)
            {
                return HttpNotFound();
            }
            return View(tabulature);
        }

        // POST: Tabulatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content")] Tabulature tabulature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabulature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tabulature);
        }

        // GET: Tabulatures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tabulature tabulature = db.Tabulatures.Find(id);
            if (tabulature == null)
            {
                return HttpNotFound();
            }
            return View(tabulature);
        }

        // POST: Tabulatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tabulature tabulature = db.Tabulatures.Find(id);
            db.Tabulatures.Remove(tabulature);
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
            List<Tabulature> top10 = db.Tabulatures.Select(x => x).OrderByDescending(t => t.PageViews).Take(10).ToList();
            return View(top10);
        }
    }
}
