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
    public class BulkOrderImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BulkOrderImages
        public async Task<ActionResult> Index()
        {
            return View(await db.BulkOrderImages.ToListAsync());
        }

        // GET: BulkOrderImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulkOrderImages bulkOrderImages = await db.BulkOrderImages.FindAsync(id);
            if (bulkOrderImages == null)
            {
                return HttpNotFound();
            }
            return View(bulkOrderImages);
        }

        // GET: BulkOrderImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BulkOrderImages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BulkOrderImages bulkOrderImages)
        {
            if (ModelState.IsValid)
            {
                db.BulkOrderImages.Add(bulkOrderImages);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bulkOrderImages);
        }

        // GET: BulkOrderImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulkOrderImages bulkOrderImages = await db.BulkOrderImages.FindAsync(id);
            if (bulkOrderImages == null)
            {
                return HttpNotFound();
            }
            return View(bulkOrderImages);
        }

        // POST: BulkOrderImages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BulkOrderImages bulkOrderImages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bulkOrderImages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bulkOrderImages);
        }

        // GET: BulkOrderImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulkOrderImages bulkOrderImages = await db.BulkOrderImages.FindAsync(id);
            if (bulkOrderImages == null)
            {
                return HttpNotFound();
            }
            return View(bulkOrderImages);
        }

        // POST: BulkOrderImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BulkOrderImages bulkOrderImages = await db.BulkOrderImages.FindAsync(id);
            db.BulkOrderImages.Remove(bulkOrderImages);
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
