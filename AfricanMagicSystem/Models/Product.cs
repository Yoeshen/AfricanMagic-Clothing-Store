using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web.Mvc.Html;

namespace AfricanMagicSystem.Models
{
    
    public class Product
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [Key]
        public int ID { get; set; }

        [DisplayName("Category")]
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        [StringLength(160)]
        public string Name { get; set; }

        [DisplayName("Stock left in inventory.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be between R0.01 and R99999.99")]
        public decimal Price { get; set; }

        public byte[] InternalImage { get; set; }

        [Display(Name = "Local file")]
        [NotMapped]
        public HttpPostedFileBase File
        {
            get
            {
                return null;
            }

            set
            {
                try
                {
                    MemoryStream target = new MemoryStream();

                    if (value.InputStream == null)
                        return;

                    value.InputStream.CopyTo(target);
                    InternalImage = target.ToArray();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
        }

        [DisplayName("Item Picture URL")]
        [StringLength(1024)]
        public string ItemPictureUrl { get; set; }

        [DisplayName("Point Store Product")]
        public bool isExclusive { get; set; }

        [Required(ErrorMessage = "Points Required")]
        [Range(0.01, 99999.99, ErrorMessage = "Points must be between 100 and 10000")]
        public decimal exclusivePrice { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual List<SaleDetail> SaleDetails { get; set; }
    }
}