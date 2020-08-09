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
            var returnItems = db.ReturnItems.Include(r => r.Product).Include(r => r.Sale);
            return View(returnItems.ToList());
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
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name");
            ViewBag.SaleID = new SelectList(db.Sales, "SaleId", "Username");
            return View();
        }

        // POST: ReturnItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReturnCode,ProductID,SaleID,Name,ReturnDate,ReturnReason")] ReturnItem returnItem)
        {
            if (ModelState.IsValid)
            {
                db.ReturnItems.Add(returnItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", returnItem.ProductID);
            ViewBag.SaleID = new SelectList(db.Sales, "SaleId", "Username", returnItem.SaleID);
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
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", returnItem.ProductID);
            ViewBag.SaleID = new SelectList(db.Sales, "SaleId", "Username", returnItem.SaleID);
            return View(returnItem);
        }

        // POST: ReturnItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReturnCode,ProductID,SaleID,Name,ReturnDate,ReturnReason")] ReturnItem returnItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", returnItem.ProductID);
            ViewBag.SaleID = new SelectList(db.Sales, "SaleId", "Username", returnItem.SaleID);
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
