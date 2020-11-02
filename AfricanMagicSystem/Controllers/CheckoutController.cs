using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AfricanMagicSystem.Configuration;
using AfricanMagicSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Drawing;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Parsing;
using System.Data;
using Syncfusion.Pdf.Graphics;
using Syncfusion.XPS;
using Nexmo.Api;

namespace AfricanMagicSystem.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext dB = new ApplicationDbContext();
        AppConfigurations appConfig = new AppConfigurations();
        // ProductsController ProductsController = new ProductsController();

        public List<String> CreditCardTypes { get { return appConfig.CreditCardType; } }

        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            ViewBag.CreditCardTypes = CreditCardTypes;
            var previousOrder = dB.Sales.FirstOrDefault(x => x.Username == User.Identity.Name);

            if (previousOrder != null)
                return View(previousOrder);
            else
                return View();
        }


        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
            decimal deliveryFee = 0;
            decimal points = 0;
            int oldPoints = 0;
            int roundPoints = 0;

            if (ModelState.IsValid)
                ViewBag.CreditCardTypes = CreditCardTypes;
            string results = values[9];

            var sale = new Sale();
            TryUpdateModel(sale);
            sale.CardNumber = results;
            var cart = ShoppingCart.GetCart(this.HttpContext);


            int numProducts = 0;
            numProducts = cart.GetCount();
            if (numProducts == 1)
            {
                deliveryFee = 60;
            }
            else if (numProducts <= 3)
            {
                deliveryFee = 80;
            }
            else
            {
                deliveryFee = 100;
            }

            int pointTotal = cart.getTotalPoints();

            try
            {
                sale.Username = User.Identity.Name;
                sale.Email = User.Identity.Name;
                sale.SaleDate = DateTime.Now;
                sale.Total = cart.GetTotal() + deliveryFee;

                //Calculate Points From Cart
                points = cart.GetTotal();
                points = points / 10;
                Math.Round(points);
                roundPoints = Convert.ToInt32(points);  //Rounded-up to subtract from current points              
                //roundPoints is int.

                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var ctx = store.Context;
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var currentUserId = User.Identity.GetUserId();

                if (sale.SaveUserInfo)
                {
                    currentUser.Address = sale.Address;
                    currentUser.City = sale.City;
                    currentUser.Country = sale.Country;
                    currentUser.State = sale.State;
                    currentUser.Phone = sale.PhoneNumber;
                    currentUser.PostalCode = sale.PostalCode;
                    currentUser.CustomerFirstName = sale.FirstName;
                }

                oldPoints = Convert.ToInt32(currentUser.Points);
                //oldPoints is int. Current amount of points the current user owns.

                if (pointTotal > 0 && oldPoints < pointTotal) //Checks if sale has items that use points. If user does not have enough points in his account then redirect to error page.
                {
                    return View("Insufficient");
                }
                else if (pointTotal > 0 && oldPoints > pointTotal) //Same as above but continues to subtract and update points if all requirements are met.
                {
                    int finalPoints = oldPoints - pointTotal;
                    currentUser.Points = finalPoints.ToString();
                }
                else if (pointTotal == 0) // If sale does not have point items, continue to reward user.
                {
                    int finalInt = oldPoints + roundPoints;
                    currentUser.Points = finalInt.ToString();
                }

                var result = await manager.UpdateAsync(currentUser);


                var x = cart.GetCartItems();
                List<Product> chck = (from q in dB.Products
                                      select q).ToList();

                foreach (var items in x)
                {
                    foreach (Product c in chck)
                    {
                        if (items.ProductId == c.ID)
                        {
                            c.Stock -= items.Count;
                        }
                    }
                }

                //Save Order    
                var delivery = new Delivery
                {
                    SaleId = sale.SaleId,
                    OrderStatus = "Pending",
                    CurrentLocation = "95 Monty Naicker Rd, Durban Central, Durban, 4001, South Africa"
                };
                dB.Deliveries.Add(delivery);
                dB.Sales.Add(sale);
                await dB.SaveChangesAsync();


                List<Product> emailNotif = new List<Product>();
                String emailBody = "";
                foreach (var checkStock in chck)
                {
                    if (checkStock.Stock == 20)
                    {
                        emailNotif.AddRange(chck);
                        emailBody = "Stock ID - " + checkStock.ID.ToString() + " (" + checkStock.Name.ToString() + ", " + checkStock.Stock.ToString() + ")" + "<br/>";
                        MailMessage mailcheckStock = new MailMessage();
                        string emailToAdmin = "africanmagicsystem@gmail.com";
                        MailAddress from1 = new MailAddress("africanmagicsystem@gmail.com");
                        mailcheckStock.From = from1;
                        mailcheckStock.Subject = "Your Stock Update ";

                        mailcheckStock.Body = "Dear Administrator " + "<br/>" + emailBody;
                        mailcheckStock.To.Add(emailToAdmin);


                        mailcheckStock.IsBodyHtml = true;
                        SmtpClient smtp1 = new SmtpClient();
                        smtp1.Host = "smtp.gmail.com";
                        smtp1.EnableSsl = true;
                        NetworkCredential networkCredential1 = new NetworkCredential("africanmagicsystem@gmail.com", "Afmag19@");
                        smtp1.UseDefaultCredentials = true;
                        smtp1.Credentials = networkCredential1;
                        smtp1.Port = 587;
                        smtp1.Send(mailcheckStock);
                        //Clean-up.
                        //Close the document.

                        //Dispose of email.
                        mailcheckStock.Dispose();
                    }
                }
                //Process the order
                decimal amountTotal = cart.GetTotal() + deliveryFee;
                int amount = Convert.ToInt32(amountTotal);
                string orderId = sale.SaleId.ToString();
                sale = cart.CreateOrder(sale);


                //New Email.
                //Creates a new PDF document
                PdfDocument document = new PdfDocument();
                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;
                document.PageSettings.Margins.All = 50;
                //Adds a page to the document
                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                //Loads the image from disk
                PdfImage image = PdfImage.FromFile(Server.MapPath("~/Photos/EmailLogo.png"));
                RectangleF bounds = new RectangleF(10, 10, 200, 200);
                //Draws the image to the PDF page
                page.Graphics.DrawImage(image, bounds);
                PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
                bounds = new RectangleF(0, bounds.Bottom + 90, graphics.ClientSize.Width, 30);
                //Draws a rectangle to place the heading in that region.
                graphics.DrawRectangle(solidBrush, bounds);
                //Creates a font for adding the heading in the page
                PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
                //Creates a text element to add the invoice number
                PdfTextElement element = new PdfTextElement("Invoice " + sale.SaleId.ToString() + " for" + " " + sale.FirstName + " " + sale.LastName, subHeadingFont);
                element.Brush = PdfBrushes.White;

                //Draws the heading on the page
                PdfLayoutResult res = element.Draw(page, new PointF(10, bounds.Top + 8));
                string currentDate = "Date Purchased " + sale.SaleDate.ToString();
                //Measures the width of the text to place it in the correct location
                SizeF textSize = subHeadingFont.MeasureString(currentDate);
                PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, res.Bounds.Y);
                //Draws the date by using DrawString method
                graphics.DrawString(currentDate, subHeadingFont, element.Brush, textPosition);
                PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);
                //Creates text elements to add the address and draw it to the page.
                element = new PdfTextElement("Bill To " + sale.Address.ToString() + ", " + sale.City + ", " + sale.State, timesRoman);
                element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
                res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 25));
                element = new PdfTextElement("Total Price R " + sale.Total.ToString(), timesRoman);
                element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
                res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 25));
                string cardNumFour = sale.CardNumber.Substring(12, 4);
                element = new PdfTextElement("Card Billed: **** **** **** " + cardNumFour, timesRoman);
                element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
                res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 25));
                PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
                PointF startPoint = new PointF(0, res.Bounds.Bottom + 3);
                PointF endPoint = new PointF(graphics.ClientSize.Width, res.Bounds.Bottom + 3);
                //Draws a line at the bottom of the address
                graphics.DrawLine(linePen, startPoint, endPoint);

                //Creates the datasource for the table
                DataTable invoiceDetails = new DataTable();

                //Add columns to the DataTable
                invoiceDetails.Columns.Add("Product Name");
                invoiceDetails.Columns.Add("Category");
                invoiceDetails.Columns.Add("Quantity");
                invoiceDetails.Columns.Add("Price");

                //Add rows to the DataTable
                foreach (var item in sale.SaleDetails)
                {
                    invoiceDetails.Rows.Add(new object[] { item.Product.Name, item.Product.ProductCategory.Name, item.Quantity, item.Product.Price });
                }


                //Creates text elements to add the address and draw it to the page.



                //Creates a PDF grid
                PdfGrid grid = new PdfGrid();
                //Adds the data source
                grid.DataSource = invoiceDetails;
                //Creates the grid cell styles
                PdfGridCellStyle cellStyle = new PdfGridCellStyle();
                cellStyle.Borders.All = PdfPens.White;
                PdfGridRow header = grid.Headers[0];
                //Creates the header style
                PdfGridCellStyle headerStyle = new PdfGridCellStyle();
                headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
                headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
                headerStyle.TextBrush = PdfBrushes.White;
                headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

                //Adds cell customizations
                for (int i = 0; i < header.Cells.Count; i++)
                {
                    if (i == 0 || i == 1)
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                }

                //Applies the header style
                header.ApplyStyle(headerStyle);
                cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
                cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
                cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
                //Creates the layout format for grid
                PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
                // Creates layout format settings to allow the table pagination
                layoutFormat.Layout = PdfLayoutType.Paginate;
                //Draws the grid to the PDF page.
                PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, res.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

                MemoryStream outputStream = new MemoryStream();
                document.Save(outputStream);
                outputStream.Position = 0;

                var invoicePdf = new System.Net.Mail.Attachment(outputStream, System.Net.Mime.MediaTypeNames.Application.Pdf);
                string docname = "Invoice.pdf";
                invoicePdf.ContentDisposition.FileName = docname;

                MailMessage mail = new MailMessage();
                string emailTo = sale.Email;
                MailAddress from = new MailAddress("africanmagicsystem@gmail.com");
                mail.From = from;
                mail.Subject = "Your invoice for order number #" + sale.SaleId;
                mail.Body = "Dear " + sale.FirstName + ", find your invoice in the attached PDF document.";
                mail.To.Add(emailTo);

                mail.Attachments.Add(invoicePdf);

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential("africanmagicsystem@gmail.com", "Afmag19@");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mail);
                //Clean-up.
                //Close the document.
                document.Close(true);
                //Dispose of email.
                mail.Dispose();

                //Nexmo SMS Service
                string SMS = "27" + sale.PhoneNumber.Substring(1, 9);
                var client = new Client(creds: new Nexmo.Api.Request.Credentials
                {
                    ApiKey = "52a7c941",
                    ApiSecret = "Z9wbGIsEHzi96qDp"
                });
                var resultss = client.SMS.Send(request: new SMS.SMSRequest
                {
                    from = "AfricanMagic",
                    to = SMS,
                    text = "Dear " + sale.FirstName + ", Your Order #" + sale.SaleId + " has been successfully received. Delivery will be confirmed upon successful payment and will take place between 3-5 working days."
                });

                // Retrieve required values for the PayFast Merchant
                string name = "AfricanMagic Order Number #" + orderId;
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
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
                str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
                str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

                str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
                str.Append("&amount=" + HttpUtility.UrlEncode(amount.ToString()));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                return Redirect(site + str.ToString());
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(sale);
            }
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = dB.Sales.Any(
                o => o.SaleId == id &&
                o.Username == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        //GET: /Checkout/Insufficient
        public ActionResult Insufficient()
        {
            return View("Insufficient");
        }
        
        public async Task<ActionResult> PointsCheckout()
        {
            decimal points = 0;
            int oldPoints = 0;
            int roundPoints = 0;
            decimal deliveryFee = 0;

            int numProducts = 0;
            var cart = ShoppingCart.GetCart(this.HttpContext);
            numProducts = cart.GetCount();
            if (numProducts == 1)
            {
                deliveryFee = 60;
            }
            else if (numProducts <= 3)
            {
                deliveryFee = 80;
            }
            else
            {
                deliveryFee = 100;
            }

            int pointTotal = cart.getTotalPoints();

            //Calculate Points From Cart
            points = cart.GetTotal();
            points = points / 10;
            Math.Round(points);
            roundPoints = Convert.ToInt32(points);  //Rounded-up to subtract from current points              
                                                    //roundPoints is int.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var ctx = store.Context;
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var currentUserId = User.Identity.GetUserId();

            oldPoints = Convert.ToInt32(currentUser.Points);
            //oldPoints is int. Current amount of points the current user owns.

            if (pointTotal > 0 && oldPoints < pointTotal) //Checks if sale has items that use points. If user does not have enough points in his account then redirect to error page.
            {
                return View("Insufficient");
            }
            else if (pointTotal > 0 && oldPoints > pointTotal) //Same as above but continues to subtract and update points if all requirements are met.
            {
                int finalPoints = oldPoints - pointTotal;
                currentUser.Points = finalPoints.ToString();
            }
            else if (pointTotal == 0) // If sale does not have point items, continue to reward user.
            {
                int finalInt = oldPoints + roundPoints;
                currentUser.Points = finalInt.ToString();
            }

            

            var result = await manager.UpdateAsync(currentUser);

            ViewBag.NewPoints = oldPoints + " --> " + currentUser.Points;

            return View("PointsCheckout");

        }
    }

 }

       
   
