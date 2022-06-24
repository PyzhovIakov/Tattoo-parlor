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
        public static DataTable SELECTUsersDB()
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
        public static bool INSERTUsersDB(string N, string L, string P, string S)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("INSERT INTO users (user_name ,login_name,passwd,status)  VALUES ( '" + N + "', '" + L + "', '" + P + "', '" + S + "');", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }
    }
}
