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
    public partial class frmTimkiem : Form
    {
        public frmTimkiem()
        {
            InitializeComponent();
        }

        public DataTable hienthi(string sqlht)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            SqlDataAdapter da = new SqlDataAdapter(sqlht, cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
            cn.CloseConn();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (cbthongtin.Text == "Cán bộ")
                dgvthongtin.DataSource = hienthi("select * from CANBO where MACB like '%" + textBox1.Text.Trim() + "%' or HOTEN like '%" + textBox1.Text.Trim() + "%'");
            if (cbthongtin.Text == "Sinh viên")
                dgvthongtin.DataSource = hienthi("select * from SINHVIEN where MSSV like '%" + textBox1.Text.Trim() + "%' or HOTEN like '%" + textBox1.Text.Trim() + "%'");
            if (cbthongtin.Text == "Khoa")
                dgvthongtin.DataSource = hienthi("select * from KHOA where MAKHOA like '%" + textBox1.Text.Trim() + "%' or TENKHOA like '%" + textBox1.Text.Trim() + "%'");
            if (cbthongtin.Text == "Ngành")
                dgvthongtin.DataSource = hienthi("select * from NGANH where MANGANH like '%" + textBox1.Text.Trim() + "%' or TENNGANH like '%" + textBox1.Text.Trim() + "%'");
            if (cbthongtin.Text == "Chuyên ngành")
                dgvthongtin.DataSource = hienthi("select * from CHUYENNGANH where MACN like '%" + textBox1.Text.Trim() + "%' or TENCN like '%" + textBox1.Text.Trim() + "%'");
            if (cbthongtin.Text == "Lớp")
                dgvthongtin.DataSource = hienthi("select * from LOP where MALOP like '%" + textBox1.Text.Trim() + "%' or TENLOP like '%" + textBox1.Text.Trim() + "%'");
            if (cbthongtin.Text == "Môn học")
                dgvthongtin.DataSource = hienthi("select * from MONHOC where MAMH like '%" + textBox1.Text.Trim() + "%' or TENMH like '%" + textBox1.Text.Trim() + "%'");
        }

        private void frmTimkiem_Load(object sender, EventArgs e)
        {
            cbthongtin.Text = "---------Chọn--------";
        }
    }
}
