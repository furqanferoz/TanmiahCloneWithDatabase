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

        
        
        [Required]
        public string MainLink { get; set; }

       
        
        [Required]
        public string Link { get; set; }

        
        
        [Required]
        public string SubLink { get; set; }

    }
}