using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_ABC_Phone_Company
{
    class DatabaseConnection
    {
        public SqlConnection getConnection()
        {
            SqlConnection conn = null; ;
            try
            {
                conn = new SqlConnection("data source = 10.0.0.4; initial catalog = 7447_C#; user id = hnd; password = hnd");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't Open Connection !" + ex);
            }
            return conn;
        }
    }
}
