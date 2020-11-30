using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
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

        // GET: CustomDesignSales
        public ActionResult AdminIndex()
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
                customDesignSales.Completed = true;
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
                
        public async Task<ActionResult> AcceptDesign(int id)
        {
                CustomDesignSales customDesignSales = db.CustomDesignSales.Find(id);
            
                db.Entry(customDesignSales).State = EntityState.Modified;
                customDesignSales.Paid = true;
                await db.SaveChangesAsync();

                double FinalPrice = customDesignSales.TotalAmount * 0.90;

                // Retrieve required values for the PayFast Merchant
                string name = "AfricanMagic Custom Design Remaining Balance For Sale #" + customDesignSales.CustomSalesID;
                string description = "This is a non-refundable payment.";

                string site = "https://sandbox.payfast.co.za/eng/process";
                string merchant_id = "";
                string merchant_key = "";

                string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/process?";
                    merchant_id = "10000100";
                    merchant_key = "46f0cd694581a";
                }

                // Build the query string for payment site

                StringBuilder str = new StringBuilder();
                str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
                str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
                str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
                str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

                str.Append("&m_payment_id=" + HttpUtility.UrlEncode(customDesignSales.CustomSalesID.ToString()));
                str.Append("&amount=" + HttpUtility.UrlEncode(FinalPrice.ToString()));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                return Redirect(site + str.ToString());          
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
