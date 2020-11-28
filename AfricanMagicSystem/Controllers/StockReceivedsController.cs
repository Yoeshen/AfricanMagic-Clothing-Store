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
    public class StockReceivedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StockReceiveds
        public async Task<ActionResult> Index()
        {
            return View(await db.StockReceived.ToListAsync());
        }

        // GET: StockReceiveds/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockReceived stockReceived = await db.StockReceived.FindAsync(id);
            if (stockReceived == null)
            {
                return HttpNotFound();
            }
            return View(stockReceived);
        }

        // GET: StockReceiveds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockReceiveds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StockReceived stockReceived)
        {
            if (ModelState.IsValid)
            {
                db.StockReceived.Add(stockReceived);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stockReceived);
        }

        // GET: StockReceiveds/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockReceived stockReceived = await db.StockReceived.FindAsync(id);
            if (stockReceived == null)
            {
                return HttpNotFound();
            }
            return View(stockReceived);
        }

        // POST: StockReceiveds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StockReceived stockReceived)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockReceived).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stockReceived);
        }

        // GET: StockReceiveds/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockReceived stockReceived = await db.StockReceived.FindAsync(id);
            if (stockReceived == null)
            {
                return HttpNotFound();
            }
            return View(stockReceived);
        }

        // POST: StockReceiveds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StockReceived stockReceived = await db.StockReceived.FindAsync(id);
            db.StockReceived.Remove(stockReceived);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult ReceiveStock()
        {
            return View("~/SupplierShippings/ReceiveStock");
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
