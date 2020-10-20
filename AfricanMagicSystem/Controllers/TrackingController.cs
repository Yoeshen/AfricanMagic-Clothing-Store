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

            var orderDetail = db.SalesDetails.Where(order => order.SaleId == id);

            decimal total = 0;
            
            List<string> ListOfItems = new List<string>();

            var products = from product in db.Products
                           join sale in orderDetail on 
                           product.ID equals sale.ProductId
                           select new { oName = product.Name , oQuantity = sale.Quantity, oPrice = sale.UnitPrice};

            foreach (var item in products)
            {
                string productName = item.oName;
                int productQuantity = item.oQuantity;
                decimal productPrice = item.oPrice;
                total = total + item.oPrice ;
                string itemConCatted = productQuantity.ToString() + " " + productName + " | R" + item.oPrice.ToString();
                ListOfItems.Add(itemConCatted);
            }
            string final = "R" + total.ToString();
            ViewBag.Products = ListOfItems;
            ViewBag.Total =  final;

            ViewBag.DeliveryAddress = delivery.sale.Address;
            ViewBag.CurrentLocation = delivery.CurrentLocation;

            return View(delivery);
        }


    }
}
