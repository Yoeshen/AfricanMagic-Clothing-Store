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

        public async Task<ActionResult> SaleValidCheck(ReturnItem returnItem)
        {
            List<Sale> chck = (from x in dB.Sales
                               select x).ToList();
            DateTime currentDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                foreach (var x in chck)
                {
                    DateTime SaleDate = x.SaleDate;
                    TimeSpan diff = currentDate.Subtract(SaleDate);

                    if (returnItem.SaleID == x.SaleId && diff.TotalDays < 7)
                    {
                        dB.ReturnItems.Add(returnItem);
                        await dB.SaveChangesAsync();
                        return View("Success");
                    }
                }
            }
            return View("Invalid");
        } 
    }
}