using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace POS_System
{
    class Connection
    {
        string s;
        public string connections()
        {
            //s= "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = "+ System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Database\\Shop.mdf; Integrated Security = True";
            s = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            return s;
        }
    }
}
