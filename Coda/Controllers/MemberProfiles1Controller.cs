using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coda.Models;
using Coda.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ZipCodeCoords;

namespace Coda.Controllers
{
    public class MemberProfiles1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MemberProfiles1
        public ActionResult Index()
        {
            ApplicationUser user =
               System.Web.HttpContext.Current.GetOwinContext()
                   .GetUserManager<ApplicationUserManager>()
                   .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            MemberProfile profile = db.MemberProfiles.Select(x => x).FirstOrDefault(t => t.Email == user.Email);
            if (profile.ConnectWithOtherMembers)
            {
                List<MemberProfile> profiles = db.MemberProfiles.Select(x => x).ToList();
                profiles =
                    profiles.Select(x => x)
                        .Where(
                            t => DistanceFinder.FindDistanceBetweenCoordinates(profile.Latitude, profile.Longitude, t.Latitude, t.Longitude) <= 20)
                        .ToList();
                profiles = profiles.Select(x => x).Where(t => t.Alternative == profile.Alternative && 
                t.Blues == profile.Blues && 
                t.ClassicRock == profile.ClassicRock &&
                t.Grunge == profile.Grunge &&
                t.Metal == profile.Metal &&
                t.Pop == profile.Pop &&
                t.PunkRock == profile.PunkRock &&
                t.RAndB == profile.RAndB &&
                t.Rock == profile.Rock).ToList();
                profiles.Remove(profile);
                List<UserViewModel> usersNearby = new List<UserViewModel>();
                profiles.ForEach(x => usersNearby.Add(new UserViewModel
                {
                    Email = x.Email,
                    ZipCode = x.ZipCode
                }));
                return View(usersNearby);
            }
            return View();
            //return View(db.MemberProfiles.ToList());
        }

        // GET: MemberProfiles1/Details/5
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

        // GET: MemberProfiles1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberProfiles1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,ZipCode,Rock,ClassicRock,PunkRock,Grunge,Metal,Blues,RAndB,Pop,Alternative,ConnectWithOtherMembers,UserId")] MemberProfile memberProfile)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                double latitude = Spatial.Search(memberProfile.ZipCode.ToString()).Latitude;
                double longitude = Spatial.Search(memberProfile.ZipCode.ToString()).Longitude;
                MemberProfile profileToAdd = new MemberProfile
                {
                    Address = memberProfile.Address,
                    ZipCode = memberProfile.ZipCode,
                    Rock = memberProfile.Rock,
                    ClassicRock = memberProfile.ClassicRock,
                    PunkRock = memberProfile.PunkRock,
                    Grunge = memberProfile.Grunge,
                    Metal = memberProfile.Grunge,
                    Blues = memberProfile.Blues,
                    RAndB = memberProfile.RAndB,
                    Pop = memberProfile.Pop,
                    Alternative = memberProfile.Alternative,
                    ConnectWithOtherMembers = memberProfile.ConnectWithOtherMembers,
                    UserId = user.UserName,
                    Email = user.Email,
                    Longitude = longitude,
                    Latitude = latitude

                };
                db.MemberProfiles.Add(profileToAdd);
                //db.MemberProfiles.Add(memberProfile);
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }

            return View(memberProfile);
        }
        
        // GET: MemberProfiles1/Edit/5
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

        // POST: MemberProfiles1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,ZipCode,Rock,ClassicRock,PunkRock,Grunge,Metal,Blues,RAndB,Pop,Alternative,ConnectWithOtherMembers")] MemberProfile memberProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberProfile);
        }

        // GET: MemberProfiles1/Delete/5
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

        // POST: MemberProfiles1/Delete/5
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

        //public ActionResult Connect()
        //{
        //    ApplicationUser user =
        //        System.Web.HttpContext.Current.GetOwinContext()
        //            .GetUserManager<ApplicationUserManager>()
        //            .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        //    MemberProfile profile = db.MemberProfiles.Select(x => x).FirstOrDefault(t => t.Email == user.Email);
        //    if (profile.ConnectWithOtherMembers)
        //    {
        //        //IQueryable query = from memberProfiles in db.MemberProfiles
        //        //    let x = memberProfiles.Coordinate
        //        //    let distance = DistanceFinder.FindDistanceBetweenCoordinates(profile.Coordinate, x)
        //        //    where distance <= 20
        //        //    select x;
        //        List<MemberProfile> profiles = db.MemberProfiles.Select(x => x).ToList();
        //        profiles =
        //            profiles.Select(x => x)
        //                .Where(
        //                    t => DistanceFinder.FindDistanceBetweenCoordinates(profile.Latitude, profile.Longitude, t.Latitude, t.Longitude) <= 20)
        //                .ToList();
        //        //List<MemberProfile> profiles = 
        //        //List<MemberProfile> profiles =
        //        //    db.MemberProfiles.Select(x => x)
        //        //        .Where(
        //        //            t => DistanceFinder.FindDistanceBetweenCoordinates(profile.Coordinate, t.Coordinate) <= 20)
        //        //        .ToList();
        //        List <UserViewModel> usersNearby = new List<UserViewModel>();
        //        profiles.ForEach(x => usersNearby.Add(new UserViewModel
        //        {
        //            Email = x.Email,
        //            ZipCode = x.ZipCode
        //        }));
        //        return View(usersNearby);
        //    }
        //    return View();
        //}
    }
}
