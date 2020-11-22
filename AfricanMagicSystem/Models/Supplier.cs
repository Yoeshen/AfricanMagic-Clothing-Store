using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AfricanMagicSystem.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        public string SupplierName { get; set; }

        public string SupplierEmail { get; set; }

        public string SupplierPhoneNumber { get; set; }

        public string ProductsSupplied { get; set; }

    }
}