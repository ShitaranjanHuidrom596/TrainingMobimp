using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class GlobalConstant
    {
        public static string ConnectionString;
        static GlobalConstant()
        {
            // Connection for SQL Server Database
            ConnectionString = ConfigurationManager.ConnectionStrings["SQLConString"].ConnectionString;
        }
    }
}
