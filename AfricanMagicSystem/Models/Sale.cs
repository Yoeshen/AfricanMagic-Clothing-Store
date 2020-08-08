using AfricanMagicSystem.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{
    [Bind(Exclude = "SaleId")]
    public partial class Sale
    {

        [ScaffoldColumn(false)]
        public int SaleId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime SaleDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(40)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10)] //CHANGE----------------------------------------------------------------- 
        public string PhoneNumber { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Credit/Debit Card Number")]
        [NotMapped]
        [Required]
        [DataType(DataType.CreditCard)]
        public String CardNumber { get; set; }

        [Display(Name = "Credit Card Type")]
        [NotMapped]
        public String CardType { get; set; }

        public bool SaveUserInfo { get; set; }


        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }



        public string ToString(Sale sale)
        {
            StringBuilder bob = new StringBuilder();

            bob.Append("<p>Order Information for Order: " + sale.SaleId + "<br>Placed at: " + sale.SaleDate + "</p>").AppendLine();
            bob.Append("<p>Name: " + sale.FirstName + " " + sale.LastName + "<br>");
            bob.Append("Address: " + sale.Address + " " + sale.City + " " + sale.State + " " + sale.PostalCode + "<br>");
            bob.Append("Contact: " + sale.Email + "     " + sale.PhoneNumber + "</p>");
            bob.Append("<p>Charge: " + sale.CardNumber + " " + sale.ExpiryDate.ToString("dd-MM-yyyy") + "</p>");
            bob.Append("<p>Credit Card Type: " + sale.CardType + "</p>");

            bob.Append("<br>").AppendLine();
            bob.Append("<Table>").AppendLine();
            // Display header 
            string header = "<tr> <th>Item Name</th>" + "<th>Quantity</th>" + "<th>Price</th> <th></th> </tr>";
            bob.Append(header).AppendLine();

            String output = String.Empty;
            try
            {
                foreach (var item in sale.SaleDetails)
                {
                    bob.Append("<tr>");
                    output = "<td>" + item.Product.Name + "</td>" + "<td>" + item.Quantity + "</td>" + "<td>" + item.Quantity * item.UnitPrice + "</td>";
                    bob.Append(output).AppendLine();
                    Console.WriteLine(output);
                    bob.Append("</tr>");
                }
            }
            catch (Exception ex)
            {
                output = "No items ordered.";
            }
            bob.Append("</Table>");
            bob.Append("<b>");
            // Display footer 
            string footer = String.Format("{0,-12}{1,12}\n",
                                          "Total", sale.Total);
            bob.Append(footer).AppendLine();
            bob.Append("</b>");

            return bob.ToString();
        }
    }
}