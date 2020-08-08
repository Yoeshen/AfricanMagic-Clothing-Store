using AfricanMagicSystem.Models;
using AfricanMagicSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Controllers
{
    public class AnalyticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        AnalyticsViewModel vm = new AnalyticsViewModel();

        public async Task<ActionResult> Index()
        {
            var data = (from Sale in db.Sales
                        group Sale by Sale.SaleDate into dateGroup
                        select new SaleDateGroup()
                        {
                            SaleDate = dateGroup.Key,
                            SaleCount = dateGroup.Count()
                        }).Take(10);

            var alldata = (from sale in db.Sales
                           group sale by sale.SaleDate into dateGroup
                           select new SaleDateGroup()
                           {
                               SaleDate = dateGroup.Key,
                               SaleCount = dateGroup.Count()
                           });

            vm.SaleData = await data.ToListAsync();
            vm.SaleDateForToday = await alldata.ToListAsync();

            return View(vm);



        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        public JsonResult GetDataAsJson()
        {
            var data = GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private List<SaleDateGroup> GetData()
        {
            var alldata = (from sales in db.Sales
                           group sales by sales.SaleDate into dateGroup
                           select new SaleDateGroup()
                           {
                               SaleDate = dateGroup.Key,
                               SaleCount = dateGroup.Count()
                           });

            var SaleData = alldata.ToList();

            return SaleData;
        }

        public dynamic StopsByMonth()
        {
            var resultSet = GetData();

            return resultSet;
        }
    }
}