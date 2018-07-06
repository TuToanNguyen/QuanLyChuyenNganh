using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace QUANLYCHUYENNGANH
{
    public partial class frmDangky : Form
    {
        public static string ms = "";
        public static string hoten = "";
        public static string ngaysinh = "";
        public static string gioitinh = "";
        public static string tenkhoa = "";
        public static string chucvu = "";

        public frmDangky()
        {
            InitializeComponent();
        }

        private void btndangky_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage(txtnguoigoi.Text, txtnguoinhan.Text, txtsub.Text, rtxtmess.Text);
                SmtpClient client = new SmtpClient(cbstmp.Text);

                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential(txtuser.Text, txtpass.Text);
                client.EnableSsl = true;
                client.Send(mail);

                MessageBox.Show("Mail Sent!", "Success", MessageBoxButtons.OK);
                this.Close();
            }
            catch(Exception )
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin! Đảm bảo không bỏ trống!");
            }
        }

        private void frmDangky_Load(object sender, EventArgs e)
        {
            rtxtmess.Text = frmDangky1.ms;
            //rtxtmess.Text = frmDangky1.hoten;
            //rtxtmess.Text = frmDangky1.ngaysinh;
            //rtxtmess.Text = frmDangky1.gioitinh;
            //rtxtmess.Text = frmDangky1.tenkhoa;
            //rtxtmess.Text = frmDangky1.chucvu;

            txtnguoinhan.Text = "tutoannguyen105@gmail.com";
            txtsub.Text = "YÊU CẦU CẤP QUYỀN";
            //MessageBox.Show(ms);
        }

    }
}
