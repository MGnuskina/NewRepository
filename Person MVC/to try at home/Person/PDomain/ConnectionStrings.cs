using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDomain
{
    public static class ConnectionStrings
    {
        public static string Location = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+@"C:\Users\Mariya\Dropbox\Person MVC\Person\Person\App_Data\PDomain.PersonContextEntitySQL.mdf"+";Integrated Security = True";
        
        public static string LocationNHibernate = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mariya\Documents\NHibernatePerson.mdf;Integrated Security = True; Connect Timeout = 30";
    }
}
