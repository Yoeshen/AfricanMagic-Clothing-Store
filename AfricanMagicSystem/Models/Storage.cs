using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{   
    [Bind(Exclude = "StorageID")]
    public class Storage
    {
        [Key]
        public int StorageID { get; set; }

        public int BulkOrderImagesID { get; set; }

        public int Quantity { get; set; }

        public string Colour { get; set; }

        public string Size { get; set; }

        public virtual BulkOrderImages BulkOrderImages { get; set; }
    }
}