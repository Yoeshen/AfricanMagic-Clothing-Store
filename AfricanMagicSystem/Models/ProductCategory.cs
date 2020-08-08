using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace AfricanMagicSystem.Models
{
    public class ProductCategory
    {
        [Key]
        [DisplayName("Category ID")]
        public int ID { get; set; }

        [DisplayName("Category")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}