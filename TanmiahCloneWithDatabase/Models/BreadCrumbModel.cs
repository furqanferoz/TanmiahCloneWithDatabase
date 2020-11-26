using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TanmiahCloneWithDatabase.Models
{
    public class BreadCrumbModel
    {

        public int ID { get; set; }

        [Required (ErrorMessage = "Please Provide it")]
        [Display(Name = "MainLink")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} character long.", MinimumLength = 4)]
        public string MainLink { get; set; }

        [Required]
        [Display(Name = "Link")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} character long.", MinimumLength = 4)]
        public string Link { get; set; }

        [Required]
        [Display(Name = "SubLink")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} character long.", MinimumLength = 4)]
        public string SubLink { get; set; }

    }
}