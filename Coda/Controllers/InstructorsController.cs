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
    public class InstructorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructors
        public ActionResult Index()
        {
            List<Instructor> instructors = db.Instructor.Select(x => x).ToList();

            List<UserViewModel> instructorViewModel = new List<UserViewModel>();

            instructors.ForEach(x => instructorViewModel.Add(new UserViewModel
            {
                Image = x.MemberProfile.Image,
                UserName = x.MemberProfile.UserName,
                Email = x.MemberProfile.Email,
                UserId = x.Id
            }));
            return View(instructorViewModel);
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create()
        {

            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(
            [Bind(Include = "Id,Content,InstructorSince,MemberId,PricePerHour,Instrument")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                List<string> instruments = new List<string> {"Guitar", "Bass", "Drums", "Vocals"};

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

                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index");
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

        public ActionResult FindInstructors()
        {
            ApplicationUser user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Instructor profile = db.Instructor.Select(x => x).FirstOrDefault(t => t.MemberProfile.Email == user.Email);

            List<Instructor> profiles = db.Instructor.Select(x => x).ToList();
            profiles =
                profiles.Select(x => x)
                    .Where(
                        t =>
                            profile != null &&
                            DistanceFinder.FindDistanceBetweenCoordinates(profile.MemberProfile.Latitude,
                                profile.MemberProfile.Longitude, t.MemberProfile.Latitude,
                                t.MemberProfile.Longitude) <= 20)
                    .ToList();
            profiles.Remove(profile);
            return View(profiles);
        }
        public ActionResult InfoWindow()
        {
            return View();
        }
    }
}



    

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



