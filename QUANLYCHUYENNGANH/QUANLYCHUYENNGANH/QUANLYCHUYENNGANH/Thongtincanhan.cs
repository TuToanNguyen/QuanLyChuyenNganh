using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace QUANLYCHUYENNGANH
{
    public partial class frmThongtincanhan : Form
    {
        xuly xl = new xuly();
        SqlCommand cm = new SqlCommand();
        Connection cn = new Connection();
        SqlDataReader rdr;
        public string UsertName = "";

        public frmThongtincanhan()
        {
            InitializeComponent();
        }

        private void Thongtincanhan_Load(object sender, EventArgs e)
        {
            try
            {
                cn.OpenConn();
                string sql = "select * from CANBO where MACB = '" + UsertName + "'";
                rdr = cn.executeSQL(sql);
                while(rdr.Read())
                {
                    lbmacb.Text = rdr["MACB"].ToString();
                    lbmakhoa.Text = rdr["MAKHOA"].ToString();
                    lbhoten.Text = rdr["HOTEN"].ToString();
                    lbngsinh.Text = rdr["NGAYSINH"].ToString();
                    lbgioitinh.Text = rdr["GIOITINH"].ToString();
                    lbchucvu.Text = rdr["CHUCVU"].ToString();
                    lbemail.Text = rdr["EMAIL"].ToString();
                    //lbmk.Text = rdr["MATKHAU"].ToString();
                    lbqhan.Text = rdr["QUYENHAN"].ToString();
                }
                
            }
            catch(Exception EX)
            {
                MessageBox.Show("Lỗi "+EX);
            }
            finally
            {
                cn.CloseConn();
            }
        }

    }
}
