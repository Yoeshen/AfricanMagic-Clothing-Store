using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{
    [Bind(Exclude = "ReceiveID")]
    public class StockReceived
    {
        [Key]
        public int ReceivedID { get; set; }

        [Required]
        public string ProductsReceived { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Supplier { get; set; }

        [Required]
        public string DateReceived { get; set; }

        [Required]
        public string Notes { get; set; }

    }
}