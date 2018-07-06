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
            SqlCommand cm = new SqlCommand("select HOTEN from CANBO where MACB= '" + MACB + "' ", cn.con);
            string hoten = cm.ExecuteScalar() as string;
            cn.CloseConn();
            return hoten;
        }

        public string TangMSSV()
        {
            string sql = "select MSSV from SINHVIEN";
            da = new SqlDataAdapter(sql, cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string ma = "";
            if(dt.Rows.Count <= 0 )
            {
                ma = "00000001";
            }
            else
            {
                int k;
                ma = "";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString());
                k = k + 1;
                if(k < 10)
                {
                    ma = ma + "0000000";
                }
                else if (k < 100)
                {
                    ma = ma + "000000";
                }
                else if(k < 1000)
                {
                    ma = ma + "00000";
                }
                else if(k < 10000)
                {
                    ma = ma + "0000";
                }
                else if(k < 100000)
                {
                    ma = ma + "000";
                }
                else if(k < 1000000)
                {
                    ma = ma + "00";
                }
                else if(k < 10000000)
                {
                    ma = ma + "0";
                }
                ma = ma + k.ToString();
            }
            return ma;
        }

        //public string TangMCB()
        //{
        //    string sql = "select MACB from CANBO";
        //    da = new SqlDataAdapter(sql, cn.con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    string ma = "";
        //    if (dt.Rows.Count <= 0)
        //    {
        //        ma = "CB0001";
        //    }
        //    else
        //    {
        //        int k;
        //        ma = "CB";
        //        k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 3));
        //        k = k + 1;
        //        if (k < 10)
        //        {
        //            ma = ma + "000";
        //        }
        //        else if (k < 100)
        //        {
        //            ma = ma + "00";
        //        }
        //        else if (k < 1000)
        //        {
        //            ma = ma + "0";
        //        }               
        //        ma = ma + k.ToString();
        //    }
        //    return ma;
        //}
    }
}
