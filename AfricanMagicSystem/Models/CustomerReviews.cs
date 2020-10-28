using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{
    [Bind(Exclude = "CustomerReviewID")]
    public class CustomerReviews
    {
        [Key]
        public int CustomerReviewID { get; set; }

        public int SaleID { get; set; }

        public string Username { get; set; }

        public int Vote { get; set; }

        public string Comment { get; set; }

        public bool Flagged { get; set; }

        public bool Approved { get; set; }

        public virtual Sale Sale { get; set; }


    }
}