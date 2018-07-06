using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QUANLYCHUYENNGANH
{
    public partial class frmKetnoiSQL : Form { 


        public static String sv, db, user, pass;
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
            if (con.OpenConn() == true)
            {
                frmDangnhap f = new frmDangnhap();
                f.Show();
            }
            else
                MessageBox.Show("ket noi that bai");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
