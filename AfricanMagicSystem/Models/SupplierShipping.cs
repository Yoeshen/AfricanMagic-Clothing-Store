using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GrapeCity.DataVisualization.TypeScript;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{
    [Bind(Exclude = "ShippingID")]
    public class SupplierShipping
    {
        [Key]
        public int ShippingID { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public System.DateTime Date { get; set; }

        public string Time { get; set; }


    }
}