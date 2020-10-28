using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AfricanMagicSystem.Models;
using System.IO;
using System.Text.RegularExpressions;
using RestSharp;

namespace AfricanMagicSystem.Controllers
{
    public class CustomerReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerReviews
        public async Task<ActionResult> Index()
        {
            var customerReview = db.CustomerReview.Include(c => c.Sale);
            return View(await customerReview.ToListAsync());
        }

        // GET: CustomerReviews/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerReviews customerReviews = await db.CustomerReview.FindAsync(id);
            if (customerReviews == null)
            {
                return HttpNotFound();
            }
            return View(customerReviews);
        }

        // GET: CustomerReviews/Create
        public ActionResult Create()
        {
            ViewBag.SaleID = new SelectList(db.Sales, "SaleId", "Username");
            return View();
        }

        // POST: CustomerReviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerReviews customerReviews)
        {
            var regex = new Regex(@"\b[\s,\.-:;]*");

            var phrase = customerReviews.Comment;

            var words = regex.Split(phrase).Where(x => !string.IsNullOrEmpty(x));

            int count = 0;

            using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/Photos/swearWords.txt")))
            {
                foreach (var x in words)
                {
                    while (!reader.EndOfStream)
                    {
                        string currentLine = reader.ReadLine();

                        if (currentLine == x)
                        {
                            count++;
                        }
                    }
                   
                }
            } 

            


            var rating = Request.Form["Rating"].ToString();

            CustomerReviews flag = new CustomerReviews
            {
                SaleID = customerReviews.SaleID,
                Comment = customerReviews.Comment,
                Vote = int.Parse(rating),
                Flagged = true,
                Username = "Anonymous"
            };

            if (ModelState.IsValid)
            {
                db.CustomerReview.Add(flag);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SaleID = new SelectList(db.Sales, "SaleId", "Username", customerReviews.SaleID);
            return View(customerReviews);
        }

        // GET: CustomerReviews/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerReviews customerReviews = await db.CustomerReview.FindAsync(id);
            if (customerReviews == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleID = new SelectList(db.Sales, "SaleId", "Username", customerReviews.SaleID);
            return View(customerReviews);
        }

        // POST: CustomerReviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerReviews customerReviews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerReviews).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SaleID = new SelectList(db.Sales, "SaleId", "Username", customerReviews.SaleID);
            return View(customerReviews);
        }

        // GET: CustomerReviews/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerReviews customerReviews = await db.CustomerReview.FindAsync(id);
            if (customerReviews == null)
            {
                return HttpNotFound();
            }
            return View(customerReviews);
        }

        // POST: CustomerReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomerReviews customerReviews = await db.CustomerReview.FindAsync(id);
            db.CustomerReview.Remove(customerReviews);
            await db.SaveChangesAsync();
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
