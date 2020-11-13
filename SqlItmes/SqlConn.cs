using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlItmes
{
    public static class SqlConn
    {
        public static string ConnectionString
        {
            get
            { 
                string con = ConfigurationManager.ConnectionStrings["TanmiahClone"].ConnectionString;
                return con;
            }


        }
    }
}
