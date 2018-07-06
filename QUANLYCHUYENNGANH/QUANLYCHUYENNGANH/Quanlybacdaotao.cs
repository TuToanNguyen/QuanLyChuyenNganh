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
    public partial class frmQuanlybacdaotao : Form
    {
        SqlCommand cm;
        public frmQuanlybacdaotao()
        {
            InitializeComponent();
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select * from BACDAOTAO";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        public DataTable danhsachkhoa()
        {
            Connection cn = new Connection();
            SqlDataAdapter da = new SqlDataAdapter("select MADT as 'Mã Bậc',TENDT as 'Tên Bậc ' from BACDAOTAO", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private void frmQuanlybacdaotao_Load(object sender, EventArgs e)
        {
            hienthi();
            dgvthongtin.DataSource = danhsachkhoa();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string makhoa = txtmakhoa.Text;
            string tenkhoa = txttenkhoa.Text;
            cm = new SqlCommand("select MADT from BACDAOTAO where MADT='" + makhoa + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (makhoa == "" || tenkhoa == "")
            {
                MessageBox.Show("Thông tin loại không được bỏ trống!!!");
            }
            else
            if (makhoa == ma)
            {
                MessageBox.Show("Trùng mã bậc đào tạo, thêm thất bại");
            }
            else
            {
                string sqlthem = "insert into BACDAOTAO values('" + makhoa + "',N'" + tenkhoa + "')";
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
            dgvthongtin.DataSource = danhsachkhoa();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string makhoa = txtmakhoa.Text;
            string tenkhoa = txttenkhoa.Text;
            cm = new SqlCommand("select MADT from BACDAOTAO where MADT='" + makhoa + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (makhoa == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update BACDAOTAO set MADT='" + makhoa + "',TENDT=N'" + tenkhoa + "' where  MADT='" + makhoa + "'";
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
                MessageBox.Show("Không trùng mã đào tạo!");
            hienthi();
            dgvthongtin.DataSource = danhsachkhoa();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string makhoa = txtmakhoa.Text;
            Connection cn = new Connection();
            cn.OpenConn();
            if (makhoa == "")
            {
                MessageBox.Show("Thông tin cần xóa hiện không tồn tại! ");
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete BACDAOTAO where MADT='" + makhoa + "' ";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmakhoa.Clear();
                        txttenkhoa.Clear();
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
                hienthi();
                dgvthongtin.DataSource = danhsachkhoa();
            }
        }

        //private void btnTK_Click(object sender, EventArgs e)
        //{
        //    if (txtTK.Text == "")
        //    {
        //        MessageBox.Show("Chưa nhập thông tin cần tìm!");
        //    }
        //    else
        //    {
        //        Connection cn = new Connection();
        //        cn.OpenConn();
        //        string MAKHOA = txtTK.Text;
        //        string sqltk = "select *from BACDAOTAO where MADT ='" + MAKHOA + "'";
        //        SqlCommand cmd = new SqlCommand(sqltk, cn.con);
        //        cmd.ExecuteNonQuery();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        DataTable dt = new DataTable();
        //        dt.Load(dr);
        //        dgvthongtin.DataSource = dt;
        //    }
        //}

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                txtmakhoa.Text = row.Cells[0].Value.ToString();
                txttenkhoa.Text = row.Cells[1].Value.ToString();

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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
            dgvthongtin.DataSource = hienthiTK("select * from BACDAOTAO where MADT like '%" + txtTK.Text.Trim() + "%'");
        }

        private void txtmakhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= 65 && e.KeyChar <= 122) || (e.KeyChar == 8));
        }
    }
}
