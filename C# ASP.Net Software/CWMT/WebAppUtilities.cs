using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CWMasterTeacher3
{
    public static class WebAppUtilities
    {
        public static bool IsRunningInAzure
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CWMT_DB"].ConnectionString;
                return connectionString.StartsWith("Data Source=tcp:") || connectionString.StartsWith("Server=tcp:");
            }
        }


    }
}