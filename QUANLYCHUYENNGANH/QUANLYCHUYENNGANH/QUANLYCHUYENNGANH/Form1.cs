using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYCHUYENNGANH
{
    public partial class frmForm1 : Form
    {
        public static string UsertName = "";
        public static string quyenhan = "";
        public frmForm1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            xuly xl = new xuly();     

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = Color.LightBlue;
                }
                if (frmDangnhap.quyenhan == "covan")
                {
                    quantrihethongToolStripMenuItem.Visible = false;
                    qUẢNLÝKHOAoolStripMenuItem.Visible = false;
                    qUẢNLÝNGÀNHToolStripMenuItem.Visible = false;
                    qUẢNLÝLỚPHỌCToolStripMenuItem.Visible = false;
                    nguoidungToolStripMenuItem.Text = xl.XinChao(frmDangnhap.aidangdangnhap);
                }
                if (frmDangnhap.quyenhan == "admin")
                {
                    qUẢNLÝKHOAoolStripMenuItem.Visible = false;
                    qUẢNLÝNGÀNHToolStripMenuItem.Visible = false;
                    qUẢNLÝLỚPHỌCToolStripMenuItem.Visible = false;
                    tHỐNGKÊBÁOCÁOToolStripMenuItem.Visible = false;
                    QuanlysinhvientoolStripMenuItem2.Visible = false;
                    nguoidungToolStripMenuItem.Text = xl.XinChao(frmDangnhap.aidangdangnhap);
                }
                if (frmDangnhap.quyenhan == "truongphong")
                {
                    QuanlysinhvientoolStripMenuItem2.Visible = false;
                    quantrihethongToolStripMenuItem.Visible = false;
                    nguoidungToolStripMenuItem.Text = xl.XinChao(frmDangnhap.aidangdangnhap);
                }
            }
        }

        private void đỔIMẬTKHẨUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmdoimk f = new frmdoimk();
            f.MdiParent = this;
            f.Show();
           
        }

        private void tHÔNGTINCÁNHÂNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongtincanhan f = new frmThongtincanhan();
            f.UsertName = UsertName;
            f.MdiParent = this;
            //f.Dock = DockStyle.Fill;
            f.Show();
        }

        private void đĂNGXUẤTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult.OK == dlr)
            {
                frmDangnhap f = new frmDangnhap();
                f.Show();
                this.Close();
            }
        }

        private void qUẢNLÝTHÔNGTINKHÓAHỌCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanlythongtinkhoa f = new frmQuanlythongtinkhoa();
            f.MdiParent = this;
            f.Show();
           
        }

        private void qUẢNLÝBẬCĐÀOTẠOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanlybacdaotao f = new frmQuanlybacdaotao();
            f.MdiParent = this;
            f.Show();
        }

        private void qUALÝTHÔNGTINNGÀNHHỌCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuan_ly_thong_tin_nganh f = new frmQuan_ly_thong_tin_nganh();
            f.MdiParent = this;
            f.Show();
        }

        private void qUẢNLÝTHÔNGTINCHUYÊNNGÀNHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuan_ly_thong_tin_chuyen_nganh f = new frmQuan_ly_thong_tin_chuyen_nganh();
            f.MdiParent = this;
            f.Show();
        }

        private void qUẢNTRỊNGƯỜIDÙNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuan_tri_nguoi_dung f = new frmQuan_tri_nguoi_dung();
            f.MdiParent = this;
            f.Show();
        }

        private void pHÂNQUYỀNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPhan_quyen f = new frmPhan_quyen();
            f.MdiParent = this;
            f.Show();
        }

        private void tÌMKIẾMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimkiem f = new frmTimkiem();
            f.MdiParent = this;
            f.Show();
        }

        private void qUẢNLÝTHÔNGTINLỚPHỌCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanlythongtinlophoc f = new frmQuanlythongtinlophoc();
            f.MdiParent = this;
            f.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmQuanlymonhoc f = new frmQuanlymonhoc();
            f.MdiParent = this;
            f.Show();
        }

        private void qUẢNLÝSINHVIÊNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanlysinhvien f = new frmQuanlysinhvien();
            f.MdiParent = this;
            f.Show();
        }

        private void qUẢNLÝQUÁTRÌNHHỌCSINHVIÊNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanlyquatrinhhocsinhvien f = new frmQuanlyquatrinhhocsinhvien();
            f.MdiParent = this;
            f.Show();
        }
    }
}
