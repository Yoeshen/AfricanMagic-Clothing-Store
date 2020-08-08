using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfricanMagicSystem.Models
{
    public class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual Sale Sale { get; set; }
    }
}