using AfricanMagicSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PromotionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Promotions
        public ActionResult Index()
        {
            return View();
        }
        
        // Email Promotions To Users in user table.
        
        public ActionResult EmailPromotion()
        {
            var Users = db.Users.Include(u => u.Roles);
            MailMessage mail = new MailMessage();
            MailAddress from = new MailAddress("africanmagicsystem@gmail.com");

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential("africanmagicsystem@gmail.com", "Afmag19@");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            List<string> Addresses = new List<string>();
            foreach (var user in Users)
            {
                Addresses.Add(user.UserName);
            }

            foreach (var mailAddress in Addresses)
            {
                mail.To.Add(mailAddress);
            }
            mail.From = from;
            mail.Subject = "AfricanMagic Clearance Sale";
            mail.Body = @"AfricanMagic is now offering you up to 90% off all your favorite clothes and underwear. <br /><br /> <img src=""https://csb10033fffa956f677.blob.core.windows.net/promo/AfricanMagicPromo.jpg"" /> <br /><br /> Limited Time Only! While Stocks Last.";
            smtp.Send(mail);
            //Dispose of email.
            mail.Dispose();
            return RedirectToAction("EmailPromotionSuccess", "Promotions");
        }

        //Successful Email        
        public ActionResult EmailPromotionSuccess()
        {
            return View();
        }
    }
}