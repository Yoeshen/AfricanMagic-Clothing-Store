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
using PagedList;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;

namespace AfricanMagicSystem.Controllers
{
    public class BulkOrderImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BulkOrderImages
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in db.BulkOrderImages
                        select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.price);
                    break;
                default:  // Sort By Name ASC
                    items = items.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(items.ToPagedList(pageNumber, pageSize));

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

        public async Task<ActionResult> RenderImage(int id)
        {
           BulkOrderImages bulkOrderImages = await db.BulkOrderImages.FindAsync(id);

            byte[] photoBack = bulkOrderImages.InternalImage;

            return File(photoBack, "image/png");
        }

        // GET: BulkOrderImages/Delete/5
        public async Task<ActionResult> AddToQuote(int? id)
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

        //AddToBulkORderSale Table
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToQuote(int id)
        {
           
            var chckdb = db.Storages.FirstOrDefault();

            if(chckdb == null)
            {
                var crt = new Storage
                {
                    StorageID = 1,
                    BulkOrderImagesID = id,
                    Quantity = 1
                };

                if (ModelState.IsValid)
                {
                    db.Storages.Add(crt);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Details");
                }
            }
            else
            {
                int check = int.Parse(db.Storages.OrderByDescending(p => p.StorageID).Select(r => r.StorageID).First().ToString());
                var crt = new Storage
                {
                    StorageID = 1 + check,
                    BulkOrderImagesID = id,
                    Quantity = 1
                };

                if (ModelState.IsValid)
                {
                    db.Storages.Add(crt);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Details");
                }

            }
            return View("Index");
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
