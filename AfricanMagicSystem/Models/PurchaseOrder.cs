using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{
    [Bind(Exclude = "PurchaseOrderID")]
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderID { get; set; }

        public int ProductNeeded{ get; set; }

        public List<Product> Products  { get; set; }
    }
}