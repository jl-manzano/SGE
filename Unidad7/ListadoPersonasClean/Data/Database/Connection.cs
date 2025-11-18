using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    internal class Connection
    {
        public static string getConnectionString()
        {
            return "server=josemnzano.database.windows.net;database=PersonasDB;uid=josemanzano;pwd=abc12345_;trustServerCertificate=true;";

        }
    }
}
