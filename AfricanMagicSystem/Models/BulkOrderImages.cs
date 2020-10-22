using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using NLog;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{

    [Bind(Exclude = "ID")]
    public class BulkOrderImages
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Product is required")]
        [StringLength(160)]
        public string Name { get; set; }

        [DisplayName("Stock left in inventory.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be between R0.01 and R99999.99")]
        public decimal price { get; set; }


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
                catch(Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
        }

        public string Size { get; set; }

        public string Colour { get; set; }




    }
}
