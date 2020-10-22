using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AfricanMagicSystem.Models
{
    public class BulkOrderSaleDetails
    {
        [Key]
        public int BOSaleDetailID { get; set; }

        public int BOSaleID { get; set; }

        public int BOImageID { get; set; }

        public int BOQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual BulkOrderSales BulkOrderSales { get; set; }

        public virtual BulkOrderImages BulkOrderImages { get; set; }


    }
}