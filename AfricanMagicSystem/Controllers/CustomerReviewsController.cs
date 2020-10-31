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
            if (ModelState.IsValid)
            {
                db.CustomerReview.Add(customerReviews);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
