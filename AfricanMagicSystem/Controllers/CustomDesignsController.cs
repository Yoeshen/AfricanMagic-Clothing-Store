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
    public class CustomDesignsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomDesigns
        public async Task<ActionResult> Index()
        {
            return View(await db.CustomDesigns.ToListAsync());
        }

        public ActionResult MakeCustom()
        {
            return View();
        }

        // GET: CustomDesigns/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDesign customDesign = await db.CustomDesigns.FindAsync(id);
            if (customDesign == null)
            {
                return HttpNotFound();
            }
            return View(customDesign);
        }

        // GET: CustomDesigns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomDesigns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomDesign customDesign)
        {
            if (ModelState.IsValid)
            {
                db.CustomDesigns.Add(customDesign);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customDesign);
        }

        // GET: CustomDesigns/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDesign customDesign = await db.CustomDesigns.FindAsync(id);
            if (customDesign == null)
            {
                return HttpNotFound();
            }
            return View(customDesign);
        }

        // POST: CustomDesigns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomDesign customDesign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customDesign).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customDesign);
        }        

        // POST: CustomDesigns/MakeCustom
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeCustom(CustomDesignSales customDesignSales, int? id)
        {
            if (ModelState.IsValid)
            {                                
                customDesignSales.DesignID = Convert.ToInt32(id);
                db.CustomDesignSales.Add(customDesignSales);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customDesignSales);
        }

        // GET: CustomDesigns/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDesign customDesign = await db.CustomDesigns.FindAsync(id);
            if (customDesign == null)
            {
                return HttpNotFound();
            }
            return View(customDesign);
        }

        // POST: CustomDesigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomDesign customDesign = await db.CustomDesigns.FindAsync(id);
            db.CustomDesigns.Remove(customDesign);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            CustomDesign customDesign = await db.CustomDesigns.FindAsync(id);

            byte[] photoBack = customDesign.InternalImage;

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
