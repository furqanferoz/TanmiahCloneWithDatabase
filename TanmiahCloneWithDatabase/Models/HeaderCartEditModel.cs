using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TanmiahCloneWithDatabase.Models
{
    public class HeaderCartEditModel
    {
        public int ID { get; set; }

        [Required]
        public string Tile { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string Image { get; set; }
    }
}