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
    public class SupplierShippingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SupplierShippings
        public async Task<ActionResult> Index()
        {
            return View(await db.supplierShippings.ToListAsync());
        }

        // GET: SupplierShippings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierShipping supplierShipping = await db.supplierShippings.FindAsync(id);
            if (supplierShipping == null)
            {
                return HttpNotFound();
            }
            return View(supplierShipping);
        }

        // GET: SupplierShippings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierShippings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SupplierShipping supplierShipping)
        {
            if (ModelState.IsValid)
            {
                db.supplierShippings.Add(supplierShipping);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(supplierShipping);
        }

        // GET: SupplierShippings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierShipping supplierShipping = await db.supplierShippings.FindAsync(id);
            if (supplierShipping == null)
            {
                return HttpNotFound();
            }
            return View(supplierShipping);
        }

        // POST: SupplierShippings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SupplierShipping supplierShipping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierShipping).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(supplierShipping);
        }

        // GET: SupplierShippings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierShipping supplierShipping = await db.supplierShippings.FindAsync(id);
            if (supplierShipping == null)
            {
                return HttpNotFound();
            }
            return View(supplierShipping);
        }

        // POST: SupplierShippings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SupplierShipping supplierShipping = await db.supplierShippings.FindAsync(id);
            db.supplierShippings.Remove(supplierShipping);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiveStock(string Value)
        {
            List<SupplierShipping> check = (from q in db.supplierShippings
                                            select q).ToList();

            foreach (var item in check)
            {
                if(item.ShippingID == int.Parse(Value))
                {
                    ViewBag.Message = item.Notes;
                }
            }

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
