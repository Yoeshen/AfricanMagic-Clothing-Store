using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AfricanMagicSystem.Models;
using Microsoft.AspNet.Identity;

namespace AfricanMagicSystem.Controllers
{
    public class TrackingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tracking
        public ActionResult Index()
        {
            var sale = db.Sales.Where(s => s.Username == User.Identity.Name);
            return View(sale);

        }

        // GET: Tracking/Details/5
        public ActionResult Details(int id)
        {
            Delivery delivery = db.Deliveries.Find(id);

            Product product = db.Products.Find(id);

            var orderDetails = db.SalesDetails.Where(x => x.ProductId == product.ID);
                       
            List<string> ListOfItems = new List<string>();
            foreach (var r in orderDetails)
            {
                ListOfItems.Add(r.Product.Name);
            }

            ViewBag.Products = ListOfItems;

            return View(delivery);
        }


    }
}
