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
    public partial class frmQuanlyquatrinhhocsinhvien : Form
    {
        public frmQuanlyquatrinhhocsinhvien()
        {
            InitializeComponent();
        }

        private void frmQuanlyquatrinhhocsinhvien_Load(object sender, EventArgs e)
        {
            txtemail.Text = frmQuanlysinhvien.ma;
        }
    }
}
