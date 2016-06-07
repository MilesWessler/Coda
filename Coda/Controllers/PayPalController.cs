using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coda.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Coda.Controllers
{
    public class PayPalController : Controller
    {
       ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Donate()
        {
            ApplicationUser user =
                    System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var model = new IndexViewModel
            {
                DonationAmount = GetDonationAmount(),
                UserId = user.Id,
                HasPassword = HasPassword(),
                };
           
            //if (model.DonationAmount == null)
            //{
            //    return View("PreviousDonation");
            //}

            return View(model);
        }
        public ActionResult NewDonation()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult NewDonation([Bind(Include = "Id,Amount")] Donation donation)
        {

            ApplicationUser user =
                    System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
           

                if (ModelState.IsValid)
            {
               
                    Donation donationToAdd = new Donation
                {
                    Id = donation.Id,
                    Amount = donation.Amount,
                    UserId = user.Id
                };
                    db.Donations.Add(donationToAdd);
                    db.SaveChanges();

                    return RedirectToAction("Donate");
                }

            return View(donation);
        }


        private bool HasPassword()
        {
            ApplicationUser user =
                    System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return user?.PasswordHash != null;
        }
        private double? GetDonationAmount()
        {

            ApplicationUser user =
                    System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                double? donationAmount =
                    db.Donations.Select(y => y).Where(u => u.UserId == user.Id).Select(m => m.Amount).Single();
                return donationAmount;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}