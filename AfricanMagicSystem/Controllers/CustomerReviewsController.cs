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
using System.Text.RegularExpressions;
using System.IO;

namespace AfricanMagicSystem.Controllers
{
    public class CustomerReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerReviews
        public async Task<ActionResult> Index()
        {
            return View(await db.CustomerReview.ToListAsync());
        }

        public async Task<ActionResult> CustomerIndex()
        {
            return View(await db.CustomerReview.ToListAsync());
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
            return View();
        }

        // POST: CustomerReviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerReviews customerReviews)
        {

            List<Sale> saleList = (from q in db.Sales
                                   select q).ToList();

            //var regex = new Regex(@"\b[\s,.-:;]*");
            var phrase = customerReviews.Comment;
            //var words = regex.Split(phrase).Where(x => !string.IsNullOrEmpty(x));
            var punctuation = phrase.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = phrase.Split().Select(x => x.Trim(punctuation));
            int count = 0;
            var rating = int.Parse(Request.Form["Vote"].ToString());

            using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/Photos/swearWords.txt")))
            {
                foreach (var x in words)
                {
                    while (!reader.EndOfStream)
                    {
                        string currentLine = reader.ReadLine();

                        if (currentLine == x.ToLower() || currentLine == x.ToUpper())
                        {
                            count++;
                        }
                    }
                    reader.DiscardBufferedData();
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                }
            }

            if(count > 0)
            {
                customerReviews.Flagged = true;
            }
            else
            {
                customerReviews.Flagged = false;
            }

            if (ModelState.IsValid)
            {
                foreach (var item in saleList)
                {
                    if (customerReviews.InvoiceNumber == item.SaleId)
                    {
                        if(count > 0)
                        {
                            customerReviews.InvoiceNumber = item.SaleId;
                            customerReviews.Username = item.Username;
                            customerReviews.Vote = rating;
                            db.CustomerReview.Add(customerReviews);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Flagged");
                        }
                        else
                        {
                            customerReviews.InvoiceNumber = item.SaleId;
                            customerReviews.Username = item.Username;
                            customerReviews.Vote = rating;
                            db.CustomerReview.Add(customerReviews);
                            await db.SaveChangesAsync();
                            return RedirectToAction("ThankYou");
                        }
                        
                    }
                }

               
            }

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

        public ActionResult Verify(int id)
        {
            List<CustomerReviews> customerReviews = (from q in db.CustomerReview
                                                     select q).ToList();

            foreach (var item in customerReviews)
            {
                if(item.CustomerReviewID == id)
                {
                    item.Flagged = true;
                }
            }

            return View("Verified");
        }

        public ActionResult Flagged()
        {
            return View();
        }

        public ActionResult ThankYou()
        {
            return View();
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
