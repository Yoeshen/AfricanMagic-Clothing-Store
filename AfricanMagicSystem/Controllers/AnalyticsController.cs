using AfricanMagicSystem.Models;
using AfricanMagicSystem.ViewModels;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

        public ActionResult ExportSales()
        {          
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                    

                //Instantiate the Excel application object
                IApplication application = excelEngine.Excel;

                //Assigns default application version
                application.DefaultVersion = ExcelVersion.Excel2013;

                //A new workbook is created equivalent to creating a new workbook in Excel
                //Create a workbook with 1 worksheet
                IWorkbook workbook = application.Workbooks.Create(1);

                //Access first worksheet from the workbook
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet.Range["A1"].Text = "Sale ID";
                worksheet.Range["B1"].Text = "Sale Date";
                worksheet.Range["C1"].Text = "Customer Username";
                worksheet.Range["D1"].Text = "Customer First Name";
                worksheet.Range["E1"].Text = "Customer Last Name";
                worksheet.Range["F1"].Text = "Customer Address";
                worksheet.Range["G1"].Text = "Customer Contact Number";
                worksheet.Range["H1"].Text = "Customer Email";
                worksheet.Range["I1"].Text = "Sale Total";

                int i = 2;
                foreach (var item in db.Sales)
                {                    
                   
                    worksheet.Range["A" + i].Text = item.SaleId.ToString();
                    worksheet.Range["B" + i].DateTime = item.SaleDate;
                    worksheet.Range["C" + i].Text = item.Username;
                    worksheet.Range["D" + i].Text = item.FirstName;
                    worksheet.Range["E" + i].Text = item.LastName;
                    worksheet.Range["F" + i].Text = item.Address;
                    worksheet.Range["G" + i].Text = item.PhoneNumber;
                    worksheet.Range["H" + i].Text = item.Email;
                    worksheet.Range["I" + i].Text = item.Total.ToString();
                    i++;
                }


                //Format Columns
                worksheet.UsedRange.AutofitColumns();

                //Save the workbook as stream
                MemoryStream outputStream = new MemoryStream();
                outputStream.Position = 0;
                workbook.SaveAs(outputStream);
                byte[] bytesInStream = outputStream.ToArray(); 
                outputStream.Close();

                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment;    filename=ExportSales.xlsx");
                Response.BinaryWrite(bytesInStream);
                Response.End();
                
            }
            return View();
        }

        public ActionResult ExportProducts()
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {


                //Instantiate the Excel application object
                IApplication application = excelEngine.Excel;

                //Assigns default application version
                application.DefaultVersion = ExcelVersion.Excel2013;

                //A new workbook is created equivalent to creating a new workbook in Excel
                //Create a workbook with 1 worksheet
                IWorkbook workbook = application.Workbooks.Create(1);

                //Access first worksheet from the workbook
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet.Range["A1"].Text = "Product ID";
                worksheet.Range["B1"].Text = "Product Name";
                worksheet.Range["C1"].Text = "Product Price (In Rands)";
                worksheet.Range["D1"].Text = "Current Stock";
             

                int i = 2;
                foreach (var item in db.Products)
                {
                    
                    worksheet.Range["A" + i].Text = item.ID.ToString();
                    worksheet.Range["B" + i].Text = item.Name;
                    worksheet.Range["C" + i].Text = item.Price.ToString();
                    worksheet.Range["D" + i].Text = item.Stock.ToString();                   
                    i++;
                }


                //Format Columns
                worksheet.UsedRange.AutofitColumns();

                //Save the workbook as stream
                MemoryStream outputStream = new MemoryStream();
                outputStream.Position = 0;
                workbook.SaveAs(outputStream);
                byte[] bytesInStream = outputStream.ToArray();
                outputStream.Close();

                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment;    filename=ExportProducts.xlsx");
                Response.BinaryWrite(bytesInStream);
                Response.End();

            }
            return View();
        }

        public ActionResult ExportDeliveries()
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {


                //Instantiate the Excel application object
                IApplication application = excelEngine.Excel;

                //Assigns default application version
                application.DefaultVersion = ExcelVersion.Excel2013;

                //A new workbook is created equivalent to creating a new workbook in Excel
                //Create a workbook with 1 worksheet
                IWorkbook workbook = application.Workbooks.Create(1);

                //Access first worksheet from the workbook
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet.Range["A1"].Text = "Sale ID Of Delivery";
                worksheet.Range["B1"].Text = "Delivery ID";
                worksheet.Range["C1"].Text = "Current Status";
                

                int i = 2;
                foreach (var item in db.Deliveries)
                {                    
                    worksheet.Range["A" + i].Text = item.SaleId.ToString();
                    worksheet.Range["B" + i].Text = item.DeliveryId.ToString();
                    worksheet.Range["C" + i].Text = item.OrderStatus;                                      
                    i++;
                }


                //Format Columns
                worksheet.UsedRange.AutofitColumns();

                //Save the workbook as stream
                MemoryStream outputStream = new MemoryStream();
                outputStream.Position = 0;
                workbook.SaveAs(outputStream);
                byte[] bytesInStream = outputStream.ToArray();
                outputStream.Close();

                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment;    filename=ExportDeliveries.xlsx");
                Response.BinaryWrite(bytesInStream);
                Response.End();

            }
            return View();
        }
    }
}