using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AfricanMagicSystem.Models;

namespace AfricanMagicSystem.Controllers
{
    public class ReturnItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReturnItems
        public ActionResult Index()
        {
            return View(db.ReturnItems.ToList());
        }

        // GET: ReturnItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);

            var orderDetails = db.SalesDetails.Where(x => x.SaleId == returnItem.InvoiceNumber);

            returnItem.saleDetails = orderDetails.ToList();

            if (returnItem == null)
            {
                return HttpNotFound();
            }
            return View(returnItem);


        }

        // GET: ReturnItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReturnItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReturnItem returnItem)
        {
            List<Sale> chck = (from x in db.Sales
                               select x).ToList();
            DateTime currentdate = DateTime.Now;

            if (ModelState.IsValid)
            {
                foreach (var d in chck)
                {
                    DateTime saledate = d.SaleDate;
                    TimeSpan diff = currentdate.Subtract(saledate);

                    if (d.SaleId == returnItem.InvoiceNumber && diff.TotalDays < 7)
                    {
                        returnItem.Status = "Pending";
                        db.ReturnItems.Add(returnItem);
                        db.SaveChanges();
                        return View("Success");
                    }
                    
                }
                return View("Invalid");
            }
            return View(returnItem);
        }

        // GET: ReturnItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);
            if (returnItem == null)
            {
                return HttpNotFound();
            }
            return View(returnItem);
        }

        // POST: ReturnItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReturnItem returnItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(returnItem);
        }

        // GET: ReturnItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);
            if (returnItem == null)
            {
                return HttpNotFound();
            }
            return View(returnItem);
        }

        // POST: ReturnItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReturnItem returnItem = db.ReturnItems.Find(id);
            db.ReturnItems.Remove(returnItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: ReturnItems/Refund/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Refund(string value)
        {

            //string detailId = "";
            //detailId = form["SaleDetailsId"];

            string[] id = value.Split(',');
            int[] ids = new int[id.Length];

            for (int i = 0; i < id.Length; i++)
            {
                ids[i] = int.Parse(id[i]);
            }

            List<SaleDetail> saleDetails = (from x in db.SalesDetails
                                            select x).ToList();
            List<ReturnItem> returnItems = (from z in db.ReturnItems
                                            select z).ToList();

            decimal RefundPrice = 0;
            for (int y = 0; y < ids.Length; y++)
            {
                foreach (var check in saleDetails)
                {
                    if (check.SaleDetailId == ids[y])
                    {
                        RefundPrice += (check.Product.Price * check.Quantity);
                        foreach (var look in returnItems)
                        {
                            if (look.InvoiceNumber == check.SaleId)
                            {
                                look.Status = "Completed";
                                db.Entry(look).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }                  

                }
            }
            string name = "Refund";
            string description = "This is a once-off payment";

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
            str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));
            str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
            str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));


            str.Append("&amount=" + HttpUtility.UrlEncode(RefundPrice.ToString()));
            str.Append("&item_name=" + HttpUtility.UrlEncode(name));
            str.Append("&item_description=" + HttpUtility.UrlEncode(description));
            // Redirect to PayFast
            return Redirect(site + str.ToString());
                   
           // return View();
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
