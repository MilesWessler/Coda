using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coda.Models;

namespace Coda.Controllers
{
    public class MemberProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MemberProfiles
        public ActionResult Index()
        {
            return View(db.MemberProfiles.ToList());
        }

        // GET: MemberProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberProfile memberProfile = db.MemberProfiles.Find(id);
            if (memberProfile == null)
            {
                return HttpNotFound();
            }
            return View(memberProfile);
        }

        // GET: MemberProfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,ZipCode,ConnectWithOtherMembers")] MemberProfile memberProfile)
        {
            if (ModelState.IsValid)
            {
                db.MemberProfiles.Add(memberProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberProfile);
        }

        // GET: MemberProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberProfile memberProfile = db.MemberProfiles.Find(id);
            if (memberProfile == null)
            {
                return HttpNotFound();
            }
            return View(memberProfile);
        }

        // POST: MemberProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,ZipCode,ConnectWithOtherMembers")] MemberProfile memberProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberProfile);
        }

        // GET: MemberProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberProfile memberProfile = db.MemberProfiles.Find(id);
            if (memberProfile == null)
            {
                return HttpNotFound();
            }
            return View(memberProfile);
        }

        // POST: MemberProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberProfile memberProfile = db.MemberProfiles.Find(id);
            db.MemberProfiles.Remove(memberProfile);
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
    }
}
