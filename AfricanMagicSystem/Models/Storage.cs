using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AfricanMagicSystem.Models
{
    public class Storage
    {
        [Key]
        public int PKey { get; set; }

        public int BOImageID { get; set; }

        public virtual BulkOrderImages BulkOrderImages { get; set; }
    }
}