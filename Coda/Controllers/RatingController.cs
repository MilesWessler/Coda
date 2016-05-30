using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coda.Models;

namespace Coda.Controllers
{
    public class RatingController : Controller
    {
       ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Rate()
        {
            return View();
        }

        //[HttpPost]
//        public string Rate([Bind(Include = "Id,TablatureID,Rating")] TablatureRating model, int? id)
//        {
            
///*db.Tabulatures.Select(x => x).FirstOrDefault(t => t.Id == model.TablatureID)*/

//                Tablature tab = db.Tabulatures.Find(id);

//                TablatureRating rating = new TablatureRating
//                {
//                    Rating = model.Rating,
//                    TablatureID = tab.Id,
//                    Tablature = tab


//                };

//                db.TablatureRatings.Add(rating);

//                db.SaveChanges();

//                return View()
//            }
            
        }
    }
