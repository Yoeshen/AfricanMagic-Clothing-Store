using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfricanMagicSystem.Models
{
    [Bind(Exclude="CustomSalesID")]
    public class CustomDesignSales
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [Key]
        public int CustomSalesID { get; set; }

        public string CustomSalesName { get; set; }

        public string Email { get; set; }

        public int DesignID { get; set; }

        public string ShirtText { get; set; }

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

        public string Size { get; set; }

        public string Colour { get; set; }

        public double TotalAmount { get; set; }

    }
}