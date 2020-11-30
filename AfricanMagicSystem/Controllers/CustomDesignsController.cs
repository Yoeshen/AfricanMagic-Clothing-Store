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
using System.Text;

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
                var Colour = Request.Form["Colour"].ToString();
                var Size = Request.Form["Size"].ToString();
                var Quantity = int.Parse(Request.Form["Quantity"].ToString());
                customDesignSales.DesignID = Convert.ToInt32(id);
                customDesignSales.Colour = Colour;
                customDesignSales.Size = Size;
                customDesignSales.Quantity = Quantity;

                if (Size == "Small")
                {
                    customDesignSales.TotalAmount = 130 * Quantity;
                }
                else if (Size == "Medium")
                {
                    customDesignSales.TotalAmount = 160 * Quantity;
                }
                else if (Size == "Large")
                {
                    customDesignSales.TotalAmount = 190 * Quantity;
                }

                db.CustomDesignSales.Add(customDesignSales);
                await db.SaveChangesAsync();

                double DepositPrice = customDesignSales.TotalAmount * 0.10;

                // Retrieve required values for the PayFast Merchant
                string name = "AfricanMagic Custom Design Deposit For Sale #" + customDesignSales.CustomSalesID;
                string description = "This is a non-refundable deposit required for the complete mock-up.";

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
                str.Append("&amount=" + HttpUtility.UrlEncode(DepositPrice.ToString()));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                return Redirect(site + str.ToString());
            }
            else
            {
                return View(customDesignSales);
            }
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
