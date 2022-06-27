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

        public static bool DELETEClientDB(string F, string N, string P, string T, string A)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("DELETE * FROM client WHERE (((client.surname)='"+F+"') AND ((client.name)='"+N+"') AND ((client.patronymic)='"+P+"') AND ((client.telephone)='"+T+"') AND ((client.age)="+Convert.ToInt32(A)+"));", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }

        public static bool DELETEWorkDB(string C, string M, string T, string S) /////////////
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt = new DataTable();
            try
            {
                string[] mc = C.Split(' ');
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM client WHERE (((client.surname)='" + mc[0] + "') AND ((client.name)='" + mc[1] + "') AND ((client.patronymic)='" + mc[2] + "'));", con);
                ada1.Fill(dt1);
                string[] mm = M.Split(' ');
                OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM master WHERE (((master.surname)='" + mm[0] + "') AND ((master.name)='" + mm[1] + "') AND ((master.patronymic)='" + mm[2] + "'));", con);
                ada2.Fill(dt2);
                OleDbDataAdapter ada = new OleDbDataAdapter("DELETE * FROM [work] WHERE (((work.id_client)=" + Convert.ToInt32(dt1.Rows[0][0]) + ") AND ((work.id_maste)=" + Convert.ToInt32(dt2.Rows[0][0]) + ") AND ((work.type_work)='" + T + "') AND ((work.price)=" + S + "));", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }

        public static bool DELETEMasterDB(string F, string N, string P, string S, string D, string T)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada = new OleDbDataAdapter("DELETE * FROM master WHERE (((master.surname)='" + F + "') AND ((master.name)='" + N + "') AND ((master.patronymic)='" + P + "')  AND ((master.work_experience)=" + S + ")AND ((master.job_title)='" + D + "') AND ((master.telephone)='" + T + "'));", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }

        public static bool UPDATEClientDB(string pF, string pN, string pP, string pT, string pA, string F, string N, string P, string T, string A)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {

                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM client WHERE (((client.surname)='" + pF + "') AND ((client.name)='" + pN + "') AND ((client.patronymic)='" + pP + "') AND ((client.age)=" + pA + ") AND ((client.telephone)='" + pT + "'));", con);
                ada1.Fill(dt1);
                OleDbDataAdapter ada = new OleDbDataAdapter("UPDATE client SET client.surname = '"+F+"', client.name = '"+N+"', client.patronymic = '"+P+"', client.telephone = '"+T+"', client.age = "+A+" WHERE (((client.id)="+Convert.ToInt32(dt1.Rows[0][0])+"));", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }

        public static bool UPDATEMasterDB(string pF, string pN, string pP, string pS, string pD, string pT, string F, string N, string P, string S, string D, string T)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM master WHERE (((master.surname)='" + pF + "') AND ((master.name)='" + pN + "') AND ((master.patronymic)='" + pP + "') AND ((master.work_experience)=" + pS + ") AND ((master.job_title)='" + pD + "') AND ((master.telephone)='" + pT + "'));", con);
                ada1.Fill(dt1);
                OleDbDataAdapter ada = new OleDbDataAdapter("UPDATE master SET master.surname = '" + F + "', master.name = '" + N + "', master.patronymic = '" + P + "', master.telephone = '" + T + "', master.job_title = '" + D + "', master.work_experience = " + S + " WHERE (((master.id)=" + Convert.ToInt32(dt1.Rows[0][0]) + "));", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }

        public static bool UPDATEWorkDB(string pC, string pM, string pD, string pT, string pS, string C, string M, string D, string T, string S)
        {
            DataTable dt1 = new DataTable();
            DataTable pdt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable pdt2 = new DataTable();
            DataTable pdt = new DataTable();
            DataTable dt = new DataTable();
            try
            {
                string[] pmc = pC.Split(' ');
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = db.mdb");
                OleDbDataAdapter pada1 = new OleDbDataAdapter("SELECT * FROM client WHERE (((client.surname)='" + pmc[0] + "') AND ((client.name)='" + pmc[1] + "') AND ((client.patronymic)='" + pmc[2] + "'));", con);
                pada1.Fill(pdt1);
                string[] mc = C.Split(' ');
                OleDbDataAdapter ada1 = new OleDbDataAdapter("SELECT * FROM client WHERE (((client.surname)='" + mc[0] + "') AND ((client.name)='" + mc[1] + "') AND ((client.patronymic)='" + mc[2] + "'));", con);
                ada1.Fill(dt1);

                string[] mm = M.Split(' ');
                OleDbDataAdapter ada2 = new OleDbDataAdapter("SELECT * FROM master WHERE (((master.surname)='" + mm[0] + "') AND ((master.name)='" + mm[1] + "') AND ((master.patronymic)='" + mm[2] + "'));", con);
                ada2.Fill(dt2);
                string[] pmm = pM.Split(' ');
                OleDbDataAdapter pada2 = new OleDbDataAdapter("SELECT * FROM master WHERE (((master.surname)='" + pmm[0] + "') AND ((master.name)='" + pmm[1] + "') AND ((master.patronymic)='" + pmm[2] + "'));", con);
                pada2.Fill(pdt2);

                OleDbDataAdapter pada = new OleDbDataAdapter("SELECT * FROM [work]  WHERE (((work.id_client)=" + Convert.ToInt32(pdt1.Rows[0][0]) + ") AND ((work.id_maste)=" + Convert.ToInt32(pdt2.Rows[0][0]) + ") AND ((work.type_work)='" + pT + "') AND ((work.price)=" + pS + "));", con);
                pada.Fill(pdt);

                OleDbDataAdapter ada = new OleDbDataAdapter("UPDATE [work] SET work.id_client = '" + Convert.ToInt32(dt1.Rows[0][0]) + "', work.id_maste = '" + Convert.ToInt32(dt2.Rows[0][0]) + "', work.type_work = '" + T + "', work.price = '" + S + "', work.date = #" + D.Replace('.', '/') + "# WHERE (((work.id)=" + Convert.ToInt32(pdt.Rows[0][0]) + "));", con);
                ada.Fill(dt);
                return true;
            }
            catch { return false; }
        }
    
    }
}
