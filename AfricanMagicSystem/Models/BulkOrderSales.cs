using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AfricanMagicSystem.Models
{
    public class BulkOrderSales
    {

        [Key]
        public int BOSaleID { get; set; }

        public System.DateTime BOSaleDate { get; set; }

        public string BOEmail { get; set; }

        public decimal Total { get; set; }

        public string BOStatus { get; set; }

        public string BOPhoneNumber { get; set; }

        public string AdditionalNotes { get; set; }

        public List<BulkOrderSaleDetails> BulkOrderSaleDetails { get; set; }


    }
}