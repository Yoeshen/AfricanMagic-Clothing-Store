using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AfricanMagicSystem.ViewModels;

namespace AfricanMagicSystem.Models
{
    public class AnalyticsViewModel
    {

        public List<SaleDateGroup> SaleData { get; set; }

        public List<SaleDateGroup> SaleDateForToday { get; set; }

    }
}