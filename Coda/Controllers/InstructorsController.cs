using Coda.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Coda.Controllers
{
    public class InstructorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: Instructors
        public ActionResult InstructorsNearYou()
        {
            List<Instructor> instructors = db.Instructor.Select(x => x).ToList();

            List<UserViewModel> instructorViewModel = new List<UserViewModel>();

            ViewBag.Long = db.MemberProfiles.Select(x => x.Latitude);
            ViewBag.Lat = db.MemberProfiles.Select(x => x.Longitude);

            //var numberPosts = db.Instructor.Select(x => x.NumberOfTabPosts);

            instructors.ForEach(x => instructorViewModel.Add(new UserViewModel
            {
                Image = x.MemberProfile.Image,
                UserName = x.MemberProfile.UserName,
                Email = x.MemberProfile.Email,
                Content = x.Content,
                UserId = x.Id,
                NumberOfPosts = x.NumberOfTabPosts
            }));

            return View(instructorViewModel);
        }

        // GET: Instructors/Details/5
        public ActionResult InstructorDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult NewInstructor()
        {

            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewInstructor(
            [Bind(Include = "Id,Content,InstructorSince,MemberId,PricePerHour,Instrument")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                List<string> instruments = new List<string> { "Guitar", "Bass", "Drums", "Vocals" };

                ViewBag.Instruments = new SelectList(instruments);

                ApplicationUser user =
                    System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                Instructor profileToAdd = new Instructor
                {
                    MemberProfile = db.MemberProfiles.Select(x => x).FirstOrDefault(t => t.Email == user.Email),
                    PricePerHour = instructor.PricePerHour,
                    InstructorSince = DateTime.Today,
                    Content = instructor.Content,
                };

                db.Instructor.Add(profileToAdd);
                db.SaveChanges();

                return RedirectToAction("Home", "Home");
            }

            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,InstructorSince,MemberId")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("InstructorsNearYou");
            }
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructor.Find(id);
            db.Instructor.Remove(instructor);
            db.SaveChanges();
            return RedirectToAction("InstructorsNearYou");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult Index(int? id)
        //{

        //    ApplicationUser user =
        //        System.Web.HttpContext.Current.GetOwinContext()
        //            .GetUserManager<ApplicationUserManager>()
        //            .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        //    Instructor profile = db.Instructor.Select(x => x).FirstOrDefault(t => t.MemberProfile.Email == user.Email);

        //    if (profile != null && profile.NumberOfTabPosts > 5)
        //    {
        //        List<Instructor> profiles = db.Instructor.Select(x => x).ToList();
        //        profiles =
        //            profiles.Select(x => x)
        //                .Where(
        //                    t =>
        //                        DistanceFinder.FindDistanceBetweenCoordinates(profile.MemberProfile.Latitude, profile.MemberProfile.Longitude,
        //                            t.MemberProfile.Latitude, t.MemberProfile.Latitude) <= 20)
        //                .ToList();

        //        //profiles = profiles.Select(x => x).Where(t => t.Alternative == profile.Alternative &&
        //        //                                              t.Blues == profile.Blues &&
        //        //                                              t.ClassicRock == profile.ClassicRock &&
        //        //                                              t.Grunge == profile.Grunge &&
        //        //                                              t.Metal == profile.Metal &&
        //        //                                              t.Pop == profile.Pop &&
        //        //                                              t.PunkRock == profile.PunkRock &&
        //        //                                              t.RAndB == profile.RAndB &&
        //        //                                              t.Rock == profile.Rock).ToList();
        //        profiles.Remove(profile);

        //        List<InstructorViewModel> instructorNearby = new List<InstructorViewModel>();

        //        profiles.ForEach(x => instructorNearby.Add(new InstructorViewModel
        //        {
        //            Image = x.MemberProfile.Image,
        //            Content = x.Content,
        //            UserName = x.MemberProfile.UserName,
        //            Email = x.MemberProfile.Email,
        //            ZipCode = x.MemberProfile.ZipCode,
        //            UserId = x.Id
        //        }));

        //        return View(instructorNearby);
        //    }

        //    return View();
        //}

    }
}















//        public ActionResult FindInstructors()
//        {
//            ApplicationUser user =
//                System.Web.HttpContext.Current.GetOwinContext()
//                    .GetUserManager<ApplicationUserManager>()
//                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


//            Instructor profile = db.Instructor.Select(x => x).FirstOrDefault(t => t.MemberProfile.Email == user.Email);

//            List<Instructor> profiles = db.Instructor.Select(x => x).ToList();
//            //profiles =
//            //    profiles.Select(x => x)
//            //        .Where(
//            //            t =>
//            //                profile != null &&
//            //                DistanceFinder.FindDistanceBetweenCoordinates(profile.MemberProfile.Latitude,
//            //                    profile.MemberProfile.Longitude, t.MemberProfile.Latitude,
//            //                    t.MemberProfile.Longitude) <= 20)
//            //        .ToList();
//            //profile.NumberOfTabPosts = db.Tabulatures.Count(t => t.MemberProfile.Id == profile.Id);

//            if (profile.NumberOfTabPosts < 5)
//            {
//                profiles.Remove(profile);
//            }
//            return View(profiles);
//        }
//        public ActionResult InfoWindow()
//        {
//            return View();
//        }
//    }
//}





//public class InfoWindowModel
//{
//    public InfoWindowModel()
//    {
//        this.MaxWidth = 500;
//    }
//    public int MaxWidth { get; set; }
//    public bool DisableAutoPan { get; set; }
//    public bool OpenOnRightClick { get; set; }
//}



