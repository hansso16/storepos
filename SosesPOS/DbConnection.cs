using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SosesPOS
{
    internal class DbConnection
    {
        public string MyConnection()
        {
            //string con = "Data Source=DESKTOP-2JSSAGN;Initial Catalog=SOSESPOS;Integrated Security=True";
            string dataSource = System.Environment.GetEnvironmentVariable("pos_ds");
            //Console.WriteLine(dataSource);
            string con = "Data Source="+dataSource+";Initial Catalog=SOSESPOS;User id=soses;Password=s0s3sM@rketing;";
            //Console.WriteLine(con);
            return con;
        }

        public SqlCredential credentials()
        {
            SecureString pwd = new System.Net.NetworkCredential("", "myPass").SecurePassword; ;
            pwd.MakeReadOnly();
            SqlCredential cred = new SqlCredential("userId", pwd);
            return cred;
        }
    }
}
