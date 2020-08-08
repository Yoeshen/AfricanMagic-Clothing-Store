using AfricanMagicSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Controllers
{
    public class StoreController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Store/

        public ActionResult Index()
        {
            var catagories = db.ProductCategories.ToList();

            return View(catagories);
        }

        //
        // GET: /Store/Browse/5

        public ActionResult Browse(string productCategory)
        {
            // Retrieve Genre and its Associated Items from database
            var categoryModel = db.ProductCategories.Include("Products")
                .Single(g => g.Name == productCategory);

            return View(categoryModel);
        }

        //i
        // GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            var product = db.Products.Find(id);

            return View(product);
        }

        //
        // GET: /Store/CategoryMenu/
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var catagories = db.ProductCategories.ToList();

            return PartialView(catagories);
        }
    }
}