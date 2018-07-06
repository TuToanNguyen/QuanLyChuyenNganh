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
    public partial class frmLoading : Form
    {
        public frmLoading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            progressBar1.Increment(1);
            if(progressBar1.Value == 100)
            {
                timer1.Stop();
                this.Hide();
                frmKetnoiSQL f = new frmKetnoiSQL();
                f.Show();

            }
        }

    }
}
