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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,SongId")] Tablature tablature)
        {
            if (ModelState.IsValid)
            {
                Tablature tabToAdd = new Tablature
                {
                    Content = tablature.Content,
                    Song = db.Songs.Select(x => x).Where(t => t.Id == tablature.SongId).FirstOrDefault()
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

        public JsonResult SendRating(string r, string s, string id, string url)
        {
            int autoId = 0;
            Int16 thisVote = 0;
            Int16 sectionId = 0;
            Int16.TryParse(s, out sectionId);
            Int16.TryParse(r, out thisVote);
            int.TryParse(id, out autoId);

            if (!User.Identity.IsAuthenticated)
            {
                return Json("Not authenticated!");
            }

            if (autoId.Equals(0))
            {
                return Json("Sorry, record to vote doesn't exists");
            }


            switch (s)
            {

                case "5": // school voting
                    // check if he has already voted
                    var isIt = db.RateLogs.FirstOrDefault(v => v.SectionId == sectionId &&
                                                      v.UserName.Equals(User.Identity.Name,
                                                          StringComparison.CurrentCultureIgnoreCase) &&
                                                      v.VoteForId == autoId);
                    if (isIt != null)
                    {
                        // keep the school voting flag to stop voting by this member
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                        return Json("<br />You have already rated this post, thanks !");
                    }

                    var tab = db.Tabulatures.FirstOrDefault(sc => sc.Id == autoId);
                    if (tab != null)
                    {
                        object obj = tab.Rating;

                        string updatedVotes = string.Empty;
                        string[] votes = null;
                        if (obj != null && obj.ToString().Length > 0)
                        {
                            string currentVotes = obj.ToString(); // votes pattern will be 0,0,0,0,0
                            votes = currentVotes.Split(',');
                            // if proper vote data is there in the database
                            if (votes.Length.Equals(5))
                            {
                                // get the current number of vote count of the selected vote, always say -1 than the current vote in the array 
                                int currentNumberOfVote = int.Parse(votes[thisVote - 1]);
                                // increase 1 for this vote
                                currentNumberOfVote++;
                                // set the updated value into the selected votes
                                votes[thisVote - 1] = currentNumberOfVote.ToString();
                            }
                            else
                            {
                                votes = new string[] { "0", "0", "0", "0", "0" };
                                votes[thisVote - 1] = "1";
                            }
                        }
                        else
                        {
                            votes = new string[] { "0", "0", "0", "0", "0" };
                            votes[thisVote - 1] = "1";
                        }

                        // concatenate all arrays now
                        updatedVotes = votes.Aggregate(updatedVotes, (current, ss) => current + (ss + ","));
                        updatedVotes = updatedVotes.Substring(0, updatedVotes.Length - 1);

                        db.Entry(tab).State = EntityState.Modified;
                        tab.Rating = updatedVotes;
                        db.SaveChanges();

                        RateLog vm = new RateLog()
                        {
                            Active = true,
                            SectionId = Int16.Parse(s),
                            UserName = User.Identity.Name,
                            Vote = thisVote,
                            VoteForId = autoId
                        };

                        db.RateLogs.Add(vm);

                        db.SaveChanges();

                        // keep the school voting flag to stop voting by this member
                        HttpCookie cookie = new HttpCookie(url, "true");
                        Response.Cookies.Add(cookie);
                    }
                    break;
            }
            return Json("<br />You rated " + r + " star(s), thanks !");
        }
    }
}
