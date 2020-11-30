using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AfricanMagicSystem.Models;

namespace AfricanMagicSystem.Controllers
{
    public class CustomDesignSalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomDesignSales
        public ActionResult Index()
        {
            return View(db.CustomDesignSales.ToList());
        }

        // GET: CustomDesignSales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDesignSales customDesignSales = db.CustomDesignSales.Find(id);
            if (customDesignSales == null)
            {
                return HttpNotFound();
            }
            return View(customDesignSales);
        }

        // GET: CustomDesignSales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomDesignSales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomDesignSales customDesignSales)
        {
            if (ModelState.IsValid)
            {
                db.CustomDesignSales.Add(customDesignSales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customDesignSales);
        }

        // GET: CustomDesignSales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDesignSales customDesignSales = db.CustomDesignSales.Find(id);
            if (customDesignSales == null)
            {
                return HttpNotFound();
            }
            return View(customDesignSales);
        }

        // POST: CustomDesignSales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomDesignSales customDesignSales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customDesignSales).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customDesignSales);
        }

        // GET: CustomDesignSales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDesignSales customDesignSales = db.CustomDesignSales.Find(id);
            if (customDesignSales == null)
            {
                return HttpNotFound();
            }
            return View(customDesignSales);
        }

        // POST: CustomDesignSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomDesignSales customDesignSales = db.CustomDesignSales.Find(id);
            db.CustomDesignSales.Remove(customDesignSales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            CustomDesignSales customDesignSales = await db.CustomDesignSales.FindAsync(id);

            byte[] photoBack = customDesignSales.InternalImage;

            return File(photoBack, "image/png");
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
