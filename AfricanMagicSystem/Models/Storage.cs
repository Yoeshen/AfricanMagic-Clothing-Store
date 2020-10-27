using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AfricanMagicSystem.Models
{
    public class Storage
    {
        [Key]
        public int StorageID { get; set; }

        public int BulkOrderImagesID { get; set; }

        public int Quantity { get; set; }

        public virtual BulkOrderImages BulkOrderImages { get; set; }



    }
}