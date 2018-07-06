using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QUANLYCHUYENNGANH
{
    public partial class frmQuanlymonhoc : Form
    {
        SqlCommand cm;
        public frmQuanlymonhoc()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select * from MONHOC";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        public DataTable danhsachmonhoc()
        {
            Connection cn = new Connection();
            SqlDataAdapter da = new SqlDataAdapter("select MAMH as 'Mã Môn Học',MACN as 'Mã Chuyên Ngành',TENMH as 'Tên Môn Học', SOTC as 'Số Tín Chỉ' from MONHOC", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void Loadcbmacn()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select MACN from CHUYENNGANH", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmacn.DataSource = ds.Tables[0];
            cbmacn.ValueMember = "MACN";
        }

        private void Quanlymonhoc_Load(object sender, EventArgs e)
        {
            hienthi();
            Loadcbmacn();
            dgvthongtin.DataSource = danhsachmonhoc();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string mamh = txtmamon.Text;
            string macn = cbmacn.Text;
            string tenmh = txttenmon.Text;
            string stc = numsotc.Text;
            cm = new SqlCommand("select MAMH from MONHOC  where MAMH='" + mamh + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (mamh == "" || macn == "" || tenmh == "" || stc == "")
            {
                MessageBox.Show("Thông tin môn học không được bỏ trống!!!");
            }
            else
            if (mamh == ma)
            {
                MessageBox.Show("Trùng mã môn học, thêm thất bại");
            }
            else
            {
                string sqlthem = "insert into MONHOC values('" + mamh + "',N'" + macn + "',N'" + tenmh + "',N'" + stc + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            hienthi();
            Loadcbmacn();
            dgvthongtin.DataSource = danhsachmonhoc();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string mamh = txtmamon.Text;
            string macn = cbmacn.Text;
            string tenmh = txttenmon.Text;
            string stc = numsotc.Text;
            cm = new SqlCommand("select MAMH from MONHOC where MAMH='" + mamh + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (mamh == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update MONHOC set MAMH='" + mamh + "',MACN='" + macn + "',TENMH='" + tenmh + "',SOTC='" + stc + "' where  MAMH='" + mamh + "'";
                    SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Sửa thành công");
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Sửa thất bại!");
                    }
                }
            }
            else
                MessageBox.Show("Không trùng mã môn học!");
            hienthi();
            Loadcbmacn();
            dgvthongtin.DataSource = danhsachmonhoc();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mamh = txtmamon.Text;
            Connection cn = new Connection();
            cn.OpenConn();
            if (mamh == "")
            {
                MessageBox.Show("Thông tin cần xóa hiện không tồn tại! ");
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete MONHOC where  MAMH='" + mamh + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmamon.Clear();
                        txttenmon.Clear();
                        numsotc.Value = 0;
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
                hienthi();
                Loadcbmacn();
                dgvthongtin.DataSource = danhsachmonhoc();
            }
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                txtmamon.Text = row.Cells[0].Value.ToString();
                cbmacn.Text = row.Cells[1].Value.ToString();
                txttenmon.Text = row.Cells[2].Value.ToString();
                numsotc.Text = row.Cells[3].Value.ToString();
            }
        }

        public DataTable hienthiTK(string sqlht)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            SqlDataAdapter da = new SqlDataAdapter(sqlht, cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
            cn.CloseConn();
        }
        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            dgvthongtin.DataSource = hienthiTK("select * from MONHOC where MAMH like '%" + txtTK.Text.Trim() + "%' or TENMH like '%" + txttenmon.Text.Trim() + "%'");
        }
    }
}
//WHERE manhanvien LIKE '" + txttk.Text + "%' OR hoten LIKE '" + txttk.Text + "%'";