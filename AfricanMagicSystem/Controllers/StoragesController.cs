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
    public class StoragesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Storages
        public async Task<ActionResult> Index()
        {
            var storages = db.Storages.Include(s => s.BulkOrderImages);
            return View(await storages.ToListAsync());
        }

        // GET: Storages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storage storage = await db.Storages.FindAsync(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            return View(storage);
        }

        // GET: Storages/Create
        public ActionResult Create()
        {
            ViewBag.BulkOrderImagesID = new SelectList(db.BulkOrderImages, "ID", "Name");
            return View();
        }

        // POST: Storages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Storage storage)
        {
            if (ModelState.IsValid)
            {
                db.Storages.Add(storage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BulkOrderImagesID = new SelectList(db.BulkOrderImages, "ID", "Name", storage.BulkOrderImagesID);
            return View(storage);
        }

        // GET: Storages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storage storage = await db.Storages.FindAsync(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            ViewBag.BulkOrderImagesID = new SelectList(db.BulkOrderImages, "ID", "Name", storage.BulkOrderImagesID);
            return View(storage);
        }

        // POST: Storages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Storage storage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BulkOrderImagesID = new SelectList(db.BulkOrderImages, "ID", "Name", storage.BulkOrderImagesID);
            return View(storage);
        }

        // GET: Storages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storage storage = await db.Storages.FindAsync(id);
            if (storage == null)
            {
                return HttpNotFound();
            }
            return View(storage);
        }

        // POST: Storages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Storage storage = await db.Storages.FindAsync(id);
            db.Storages.Remove(storage);
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
