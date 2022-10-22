using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NahpiWebsite.General
{
    public class AppConnection
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["nahpii"].ConnectionString;
        }
    }
}
