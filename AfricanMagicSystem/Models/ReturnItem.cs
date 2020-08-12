using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{
    [Bind(Exclude = "ReturnCode")]
    public class ReturnItem
    {
        [Key]
        public int ReturnCode { get; set; }

        [Required(ErrorMessage ="Please enter a valid Sale ID. Find this on your Invoice.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please use numbers only")]
        public int InvoiceNumber { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please use letters only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter a reason for the return.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please use letters only")]
        public string ReturnReason { get; set; }

        public string Status { get; set; }

        public List<SaleDetail> saleDetails { get; set; }
    }
}