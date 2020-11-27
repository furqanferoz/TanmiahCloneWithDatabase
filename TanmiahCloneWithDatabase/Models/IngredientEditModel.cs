using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TanmiahCloneWithDatabase.Models
{
    public class IngredientEditModel
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string TitleOne { get; set; }

        [Required]
        public string DescriptionOne { get; set; }

        [Required]
        public string DescriptionSec { get; set; }

        [Required]
        public string Protein { get; set; }

        [Required]
        public string Carbohydrate { get; set; }

        [Required]
        public string Fat { get; set; }

        [Required]
        public string OrderTitle { get; set; }


    }
}