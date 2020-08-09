using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AfricanMagicSystem.Models
{
    public class ReturnItem
    {
        [Key]
        public int ReturnCode { get; set; }

        public int ProductID { get; set; }

        [Required(ErrorMessage ="Please enter a valid Sale ID. Find this on your Invoice.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please use numbers only")]
        public int SaleID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use letters only")]
        public string Name { get; set; }

        public DateTime ReturnDate { get; set; }

        public string ReturnReason { get; set; }

        public virtual Product Product { get; set; }

        public virtual Sale Sale { get; set; }
    }
}