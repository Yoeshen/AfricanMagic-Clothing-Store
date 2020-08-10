using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AfricanMagicSystem.Models;

namespace AfricanMagicSystem.Controllers
{
    public class ReturnItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReturnItems
        public ActionResult Index()
        {
            return View(db.ReturnItems.ToList());
        }

        // GET: ReturnItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);
            if (returnItem == null)
            {
                return HttpNotFound();
            }
            return View(returnItem);
        }

        // GET: ReturnItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReturnItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReturnItem returnItem)
        {
            List<Sale> chck = (from x in db.Sales
                               select x).ToList();
            DateTime currentdate = DateTime.Now;
            if (ModelState.IsValid)
            {
                foreach(var d in chck)
                {
                    DateTime saledate = d.SaleDate;
                    TimeSpan diff = currentdate.Subtract(saledate);

                    if(d.SaleId == returnItem.InvoiceNumber && diff.TotalDays < 7)
                    {
                        db.ReturnItems.Add(returnItem);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                
            }
            return View(returnItem);
        }

        // GET: ReturnItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);
            if (returnItem == null)
            {
                return HttpNotFound();
            }
            return View(returnItem);
        }

        // POST: ReturnItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReturnItem returnItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(returnItem);
        }

        // GET: ReturnItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);
            if (returnItem == null)
            {
                return HttpNotFound();
            }
            return View(returnItem);
        }

        // POST: ReturnItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReturnItem returnItem = db.ReturnItems.Find(id);
            db.ReturnItems.Remove(returnItem);
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
    }
}
