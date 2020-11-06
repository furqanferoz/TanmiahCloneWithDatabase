using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TanmiahCloneWithDatabase.Models
{
    public class HeaderCartEditModel
    {
        public int ID { get; set; }

        public string Tile { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}