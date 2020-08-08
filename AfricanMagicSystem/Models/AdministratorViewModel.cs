using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AfricanMagicSystem.Models
{
    
        public class RoleViewModel
        {
            public string Id { get; set; }
            [Required(AllowEmptyStrings = false)]
            [Display(Name = "RoleName")]
            public string Name { get; set; }
        }

        public class EditUserViewModel
        {
            public string Id { get; set; }

            [Required(AllowEmptyStrings = false)]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            public IEnumerable<SelectListItem> RolesList { get; set; }
        }
    
}