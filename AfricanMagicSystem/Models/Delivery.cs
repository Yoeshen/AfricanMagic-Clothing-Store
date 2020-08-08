using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AfricanMagicSystem.Models
{
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }

        public int SaleId { get; set; }

        public string OrderStatus { get; set; }

        public virtual Sale sale { get; set; }

    }
}