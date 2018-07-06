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
using System.Media;

namespace QUANLYCHUYENNGANH
{
    public partial class frmDangnhap : Form
    {
        public SoundPlayer error = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Error.wav");
        public SoundPlayer warning = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Ding.wav");
        public SoundPlayer success = new SoundPlayer(@"e:\Âm thanh thông báo\tada.wav");
        public SoundPlayer start = new SoundPlayer(@"e:\Âm thanh thông báo\Ring06.wav");

        Connection cn = new Connection();
        SqlCommand cm;
        public static string quyenhan = "";
        public static string aidangdangnhap = "";
        public static string UsertName = "";

        public frmDangnhap()
        {
            InitializeComponent();

            checkBox1.Checked = Properties.Settings.Default.CO;

            if (checkBox1.Checked)
            {
                txtuser.Text = Properties.Settings.Default.TENDANGNHAP;
                txtpass.Text = Properties.Settings.Default.MATKHAU;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDangky1 f = new frmDangky1();
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
                warning.Play();
                MessageBox.Show("Thông tin đang nhập không hợp lệ !");
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
                        error.Play();
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
                        //
                        frmQuanlysinhvien.UsertName = txtuser.Text;
                        frmForm1.UsertName = txtuser.Text;
                    }
                    else
                    {
                        error.Play();
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
                        //
                        frmQuanlysinhvien.UsertName = txtuser.Text;
                        frmForm1.UsertName = txtuser.Text;
                    }
                    else
                    {
                        error.Play();
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
            label5.Parent = pictureBox1;
            label5.BackColor = Color.Transparent;


            //txtuser.Text = "CB0002";
            //txtpass.Text = "123";
            //cbquyenhan.Text = "covan";
        }

        public int i = 10;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label5.Left += i;
            if (label5.Left >= this.Width - label5.Width || label5.Left <= 0)
                i = -i;
        }

        private void frmDangnhap_Click(object sender, EventArgs e)
        {
            label5.Text = @"GGGGGGGGGGGGGG";
        }

        private void btndangnhap_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.TENDANGNHAP = txtuser.Text;
                Properties.Settings.Default.MATKHAU = txtpass.Text;
            }
            else
            {
                Properties.Settings.Default.TENDANGNHAP = "";
                Properties.Settings.Default.MATKHAU = "";
            }

            Properties.Settings.Default.CO = checkBox1.Checked;

            Properties.Settings.Default.Save();


            xuly xl = new xuly();
            Connection cn = new Connection();
            string USER = txtuser.Text.Replace(" ", " ");
            string PASSWORD = txtpass.Text.Replace(" ", " ");

            if (USER == "" || PASSWORD == "" || cbquyenhan.Text == "")
            {
                warning.Play();
                MessageBox.Show("Thông tin đăng nhập không bỏ trống");
                txtuser.Clear();
                txtpass.Clear();
                txtuser.Focus();
            }
            
            //Connection cn = new Connection();
            //cn.OpenConn();
            //cm = new SqlCommand("select MACB from CANBO where MACB='" + txtuser.Text + "' ", cn.con);
            //string ma = cm.ExecuteScalar() as string;
            //if (txtuser.Text != ma)
            //{
            //    MessageBox.Show("Tên đăng nhập này không tồn tại!");
            //}

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
                        error.Play();
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
                        //
                        frmQuanlysinhvien.UsertName = txtuser.Text;
                        frmForm1.UsertName = txtuser.Text;
                    }
                    else
                    {
                        error.Play();
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
                        //
                        frmQuanlysinhvien.UsertName = txtuser.Text;
                        frmForm1.UsertName = txtuser.Text;
                    }
                    else
                    {
                        error.Play();
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            frmDangky1 f = new frmDangky1();
            f.Show();
            this.Hide();
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            label5.Text = @"Chào mừng bạn đến với phần mềm:
        -Quản lý lớp chuyên ngành theo học chế tín chỉ của Trường Đại học SPKTVL-";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            new frmQuenMatKhau().Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
