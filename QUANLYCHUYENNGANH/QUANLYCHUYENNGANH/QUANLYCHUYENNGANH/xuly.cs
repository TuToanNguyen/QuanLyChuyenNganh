using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace QUANLYCHUYENNGANH
{
    class xuly
    {
        DataSet ds = new DataSet();
        Connection cn = new Connection();
        SqlCommand cm, cm1;
        SqlDataAdapter da;
        SqlDataReader dr;
        public int KTdangnhap(string USER, string PASSWORD)
        {

            cn.OpenConn();
            cm = new SqlCommand("select MACB from CANBO where MACB='" + USER + "' and MATKHAU='" + PASSWORD + "'", cn.con);
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                return 1;
            }
            else
                return 0;
            cm.Dispose();
            dr.Dispose();
            cn.CloseConn();

        }
      
        public string quyenhan(string USER)
        {
            cn.OpenConn();
            string a = "select QUYENHAN from CANBO where MACB='" + USER + "'";
            SqlDataReader dr = cn.executeSQL(a);
            string quyen = "";
            while (dr.Read())
            {
                quyen = dr[0].ToString();
            }
            cn.CloseConn();
            return quyen;
        }

        public string XinChao(string MACB)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            SqlCommand cm = new SqlCommand("select HOTEN from CANBO where MACB='" + MACB + "'", cn.con);
            string hoten = cm.ExecuteScalar() as string;
            cn.CloseConn();
            return hoten;
        }
    }
}
