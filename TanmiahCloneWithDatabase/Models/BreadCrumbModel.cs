using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TanmiahCloneWithDatabase.Models
{
    public class BreadCrumbModel
    {
        public int ID { get; set; }

        public string MainLink { get; set; }

        public string Link { get; set; }

        public string SubLink { get; set; }

    }
}