using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GrapeCity.DataVisualization.TypeScript;

namespace AfricanMagicSystem.Models
{
    public class SupplierShipping
    {
        [Key]
        public int ShippingID { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ThemeColour { get; set; }
    }
}