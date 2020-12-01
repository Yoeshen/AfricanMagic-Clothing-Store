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
using System.Security.Cryptography;

namespace AfricanMagicSystem.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PurchaseOrders
        public async Task<ActionResult> Index()
        {
            return View(await db.PurchaseOrders.ToListAsync());
        }

        // GET: PurchaseOrders
        public async Task<ActionResult> ViewLow()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: PurchaseOrders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = await db.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrders.Add(purchaseOrder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = await db.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = await db.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseOrder = await db.PurchaseOrders.FindAsync(id);
            db.PurchaseOrders.Remove(purchaseOrder);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GenerateLow(int id)
        {
            Product product = db.Products.Find(id);

            string productName = product.Name;

            var Quantity = Request.Form["Quantity"].ToString();
            var Supplier = Request.Form["Supplier"].ToString();

            SupplierShipping supplierShipping = new SupplierShipping();
            {
                supplierShipping.Description = productName;
                supplierShipping.Notes = Quantity;
                supplierShipping.Subject = Supplier;
                supplierShipping.Time = "";
                supplierShipping.Date = "";
                supplierShipping.Confirmed = false;             
            };

            ViewBag.Message = "Successfully Ordered: " + Quantity + " " + productName + ".";
            db.supplierShippings.Add(supplierShipping);
            await db.SaveChangesAsync();
            return View();
        }

        public ActionResult ViewLowStock()
        {

            List<Product> products = (from q in db.Products
                                      select q).ToList();

            var LowProducts = "";
            //var LowProductID = new List<int>();

            List<String> Suppliers = (from x in db.Suppliers
                                      select x.SupplierName).ToList();
            
            foreach (var item in products)
            {
                if (item.Stock <= 15)
                {
                    LowProducts = item.Name;
                }
            }

            //ViewData["ProductID"] = LowProductID;
            ViewData["Products"] = LowProducts;
            ViewData["Suppliers"] = Suppliers;
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
