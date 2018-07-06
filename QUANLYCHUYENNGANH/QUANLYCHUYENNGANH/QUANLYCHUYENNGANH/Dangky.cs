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
            catch(Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình gởi mail!");
            }
        }

    }
}
