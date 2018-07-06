using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;//Sử dụng thư viện này để làm việc với Stream
using System.Media;

namespace QUANLYCHUYENNGANH
{
    public partial class frmKetnoiSQL : Form
    {
       
        public SoundPlayer error = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Error.wav");
        public SoundPlayer warning = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Ding.wav");
        public SoundPlayer success = new SoundPlayer(@"e:\Âm thanh thông báo\tada.wav");
        public SoundPlayer start = new SoundPlayer(@"e:\Âm thanh thông báo\Ring06.wav");

        public static String sv, db, user, pass;

        private void label5_Click(object sender, EventArgs e)
        {

        }

        ////icon thông báo trên window
        //private void frmKetnoiSQL_Resize(object sender, EventArgs e)
        //{
        //    if(WindowState == FormWindowState.Minimized)
        //    {
        //        notifyIcon1.Visible = true;
        //        notifyIcon1.ShowBalloonTip(500, "Thông báo", "Bấm đúp để mở chương trình", ToolTipIcon.Info);
        //        this.Hide();
        //    }
        //}

        //private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    notifyIcon1.Visible = false;
        //    this.Show();
        //    WindowState = FormWindowState.Normal;
        //}

        //private void mởChươngTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    notifyIcon1.Visible = false;
        //    this.Show();
        //    WindowState = FormWindowState.Normal;
        //}

        //private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    Application.Exit();
        //}

        //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa//
        private void frmKetnoiSQL_Load(object sender, EventArgs e)
        {
            start.Play();        
            txtserver.Text = "DESKTOP-NICOLAI\\SQLEXPRESS";
            txttencsdl.Text = "QLChuyenNganh";
            txtuser.Text = "sa";
            txtpass.Text = "system";
        }

        public frmKetnoiSQL()
        {
            InitializeComponent();
        }

        private void btnketnoi_Click(object sender, EventArgs e)
        {
            sv = txtserver.Text;
            db = txttencsdl.Text;
            user = txtuser.Text;
            pass = txtpass.Text;
            Connection con = new Connection();
            if (txtserver.Text == "" || txttencsdl.Text == "" || txtuser.Text == "" || txtpass.Text == "")
            {
                warning.Play();
                //success.Stop();
                //error.Stop();

                lbChuy.Text = "Thông tin kết nối không được bỏ trống!";
            }
            else
            {
                try
                {
                    lbChuy.Text = "";
                    if (con.OpenConn() == true)
                    {
                        //success.Play();
                        frmDangnhap f = new frmDangnhap();
                        f.Show();
                        this.Hide();
                    }
                    else
                    {
                        error.Play();
                        MessageBox.Show("Kết nối thất bại! Vui lòng kiểm tra lại thông tin kết nối!");
                    }
                }
                catch (Exception ex)
                {
                    error.Play();
                    MessageBox.Show("" + ex);
                }
            }
        }
    }
}
