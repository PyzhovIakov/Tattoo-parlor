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

        public static DataTable SELECTWorkDB()
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM [work]", con);
                ada.Fill(dt);
                return dt;
            }
            catch { return dt; }
        }

        public static DataTable SELECTClientDB()
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM client", con);
                ada.Fill(dt);
                return dt;
            }
            catch { return dt; }
        }

        public static DataTable SELECTMasterDB()
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM master", con);
                ada.Fill(dt);
                return dt;
            }
            catch { return dt; }
        }

        public static bool INSERTClientDB(string F, string N, string P, string T, string A)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("INSERT INTO client ( surname, name, patronymic, telephone, age )  VALUES ( '" + F + "', '" + N + "', '" + P + "', '" + T + "', '" + A + "');", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }

        public static bool INSERTWorkDB(string C, string M, string D, string T, string S)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt = new DataTable();
            try
            {
                string[] mc=C.Split(' ');
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM client WHERE (((client.surname)='" + mc[0] + "') AND ((client.name)='" + mc[1] + "') AND ((client.patronymic)='" + mc[2] + "'));", con);
                ada1.Fill(dt1);
                string[] mm = M.Split(' ');
                OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM master WHERE (((master.surname)='" + mm[0] + "') AND ((master.name)='" + mm[1] + "') AND ((master.patronymic)='" + mm[2] + "'));", con);
                ada2.Fill(dt2);
                OleDbDataAdapter ada3 = new OleDbDataAdapter("SELECT * FROM [work] WHERE (((work.date)=#" + D.Replace('.','/') + "#));", con);
                ada3.Fill(dt3);
                if (dt3.Rows.Count <= 0)
                {
                    OleDbDataAdapter ada = new OleDbDataAdapter("INSERT INTO  [work] ( id_client, id_maste, [date], type_work, price )  VALUES ( " + Convert.ToInt32(dt1.Rows[0][0]) + ", " + Convert.ToInt32(dt2.Rows[0][0]) + ", '" + Convert.ToDateTime(D) + "', '" + T + "', " + S + ");", con);
                    ada.Fill(dt);
                    return true;
                }
                else
                { return false; }
                
            }
            catch { return false; }
        }

        public static bool INSERTMasterDB(string F, string N, string P, string T, string S,string D)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("INSERT INTO master ( surname, name, patronymic, work_experience, job_title, telephone )  VALUES ( '" + F + "', '" + N + "', '" + P + "', '" + S + "', '"+ D + "', '" + T + "');", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }
    }
}
