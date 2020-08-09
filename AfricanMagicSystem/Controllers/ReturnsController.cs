using AfricanMagicSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Controllers
{
    public class ReturnsController : Controller
    {
        private ApplicationDbContext dB = new ApplicationDbContext();

        public ActionResult SaleValidCheck(int? saleid)
        {
            List<Sale> chck = (from x in dB.Sales
                               select x).ToList();
            DateTime currentDate = DateTime.Now;


            foreach (var x in chck)
            {
                DateTime SaleDate = x.SaleDate;
                TimeSpan diff = currentDate.Subtract(SaleDate);

                if (saleid == x.SaleId && diff.TotalDays < 7)
                {

                }
            }
        }
    }
}