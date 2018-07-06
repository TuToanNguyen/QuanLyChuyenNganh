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

namespace QUANLYCHUYENNGANH
{
    public partial class frmDangnhap : Form
    {
        Connection cn = new Connection();
        SqlCommand cm;
        public static string quyenhan = "";
        public static string aidangdangnhap = "";
        public static string UsertName = "";

        public frmDangnhap()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDangky f = new frmDangky();
            f.Show();
            this.Hide();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            xuly xl = new xuly();
            Connection cn = new Connection();
            string USER = txtuser.Text.Replace(" ", " ");
            string PASSWORD = txtpass.Text.Replace(" ", " ");

            if (USER == "" || PASSWORD == "")
            {
                MessageBox.Show("Thông tin đang nhập không hợp lệ!!!");
                txtuser.Clear();
                txtpass.Clear();
                txtuser.Focus();
            }
            else
            if (cbquyenhan.Text.CompareTo("admin") == 0)
            {
                string sql = "SELECT Count(*) FROM CANBO WHERE MACB='" + txtuser.Text + "'and MATKHAU='" + txtpass.Text + "' and QUYENHAN='" + cbquyenhan.Text + "'";
                try
                {
                    cn.OpenConn();
                    if (cn.executeScala(sql) == 1)
                    {
                        quyenhan = xl.quyenhan(USER);
                        aidangdangnhap = USER;
                        this.Hide();
                        quyenhan = "admin";
                        frmForm1 f = new frmForm1();
                        f.Show();
                        frmdoimk.UsertName = txtuser.Text;
                        frmForm1.UsertName = txtuser.Text;
                        //
                        frmQuanlysinhvien.UsertName = txtuser.Text;
                        frmForm1.UsertName = txtuser.Text;
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập không thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtuser.Clear();
                        txtpass.Clear();
                        txtuser.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
            else if (cbquyenhan.Text.CompareTo("covan") == 0)
            {
                string sql = "SELECT Count(*) FROM CANBO WHERE MACB='" + txtuser.Text + "'and MATKHAU='" + txtpass.Text + "' and QUYENHAN='" + cbquyenhan.Text + "'";
                try
                {
                    cn.OpenConn();
                    if (cn.executeScala(sql) == 1)
                    {
                        quyenhan = xl.quyenhan(USER);
                        aidangdangnhap = USER;
                        this.Hide();
                        quyenhan = "covan";
                        frmForm1 f = new frmForm1();
                        f.Show();
                        frmdoimk.UsertName = txtuser.Text;
                        frmForm1.UsertName = txtuser.Text;
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập không thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtuser.Clear();
                        txtpass.Clear();
                        txtuser.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
            else if (cbquyenhan.Text.CompareTo("truongphong") == 0)
            {
                string sql = "SELECT Count(*) FROM CANBO WHERE MACB='" + txtuser.Text + "'and MATKHAU='" + txtpass.Text + "' and QUYENHAN='" + cbquyenhan.Text + "'";
                try
                {
                    cn.OpenConn();
                    if (cn.executeScala(sql) == 1)
                    {
                        quyenhan = xl.quyenhan(USER);
                        aidangdangnhap = USER;
                        this.Hide();
                        quyenhan = "truongphong";
                        frmForm1 f = new frmForm1();
                        f.Show();
                        frmdoimk.UsertName = txtuser.Text;
                        frmForm1.UsertName = txtuser.Text;
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập không thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtuser.Clear();
                        txtpass.Clear();
                        txtuser.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }

        }

        private void frmDangnhap_Load(object sender, EventArgs e)
        {
            txtuser.Text = "CB002";
            txtpass.Text = "123";
            cbquyenhan.Text = "admin";
        }

    }
}
