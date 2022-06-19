using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace tattoo_parlor
{
    class Requests
    {
        public static DataTable UsersDB()
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM users", con);
                ada.Fill(dt);
                return dt;
            }
            catch { return dt; }
        }
    }
}
