using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TanmiahCloneWithDatabase.Models
{
    public class IngredientEditModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string TitleOne { get; set; }

        public string DescriptionOne { get; set; }

        public string DescriptionSec { get; set; }

        public string Protein { get; set; }

        public string Carbohydrate { get; set; }

        public string Fat { get; set; }

        public string OrderTitle { get; set; }


    }
}