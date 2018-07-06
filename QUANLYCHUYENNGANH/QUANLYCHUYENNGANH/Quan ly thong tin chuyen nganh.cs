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
    public partial class frmQuan_ly_thong_tin_chuyen_nganh : Form
    {
        SqlCommand cm;
        public frmQuan_ly_thong_tin_chuyen_nganh()
        {
            InitializeComponent();
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select DISTINCT macn as 'Mã CN',tennganh as 'Tên ngành', tencn as 'Tên CN' from NGANH,chuyennganh where nganh.manganh = chuyennganh.manganh";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        //public DataTable danhsachCN()
        //{
        //    Connection cn = new Connection();
        //    SqlDataAdapter da = new SqlDataAdapter("select MACN  as 'Mã Chuyên Ngành',MANGANH as 'Mã Ngành',TENCN as 'Tên Chuyên Ngành' from CHUYENNGANH", cn.con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}
        public void Loadcbmanganh()         {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select DISTINCT nganh.manganh,TENNGANH from NGANH ", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmanganh.DataSource = ds.Tables[0];
            cbmanganh.DisplayMember = "tennganh";
            cbmanganh.ValueMember = "manganh";
           
        }

        private void frmQuan_ly_thong_tin_chuyen_nganh_Load(object sender, EventArgs e)
        {
            hienthi();
            Loadcbmanganh();          
            //dgvthongtin.DataSource = danhsachCN();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string macn = txtmacn.Text;
            string manganh = cbmanganh.SelectedValue.ToString();
            string tencn = txttencn.Text;
            cm = new SqlCommand("select MACN from CHUYENNGANH where MACN='" + macn + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (macn == "" || tencn == "")
            {
                MessageBox.Show("Thông tin loại không được bỏ trống!!!");
            }
            else
            if (manganh == ma)
            {
                MessageBox.Show("Trùng mã chuyên ngành, thêm thất bại");
            }
            else
            {
                string sqlthem = "insert into CHUYENNGANH values('" + macn + "',N'" + manganh + "',N'" + tencn + "')";
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
            //dgvthongtin.DataSource = danhsachCN();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string macn = txtmacn.Text;
            string manganh = cbmanganh.SelectedValue.ToString();
            string tencn = txttencn.Text;
            cm = new SqlCommand("select MACN from CHUYENNGANH where MACN='" + macn + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (macn == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update CHUYENNGANH set MACN='" + macn + "',MANGANH=N'" + manganh + "',TENCN=N'" + tencn + "' where  MACN='" + macn + "'";
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
                MessageBox.Show("Không trùng mã chuyên ngành!");
            hienthi();
            //dgvthongtin.DataSource = danhsachCN();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string macn = txtmacn.Text;
            Connection cn = new Connection();
            cn.OpenConn();
            if (macn == "")
            {
                MessageBox.Show("Thông tin cần xóa hiện không tồn tại! ");
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete CHUYENNGANH where  MACN='" + macn + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmacn.Clear();
                        txttencn.Clear();
                        cbmanganh.Text = "-----Chọn mã khoa-----";
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
                hienthi();
                //dgvthongtin.DataSource = danhsachCN();
            }
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                txtmacn.Text = row.Cells[0].Value.ToString();
                cbmanganh.Text = row.Cells[1].Value.ToString();
                txttencn.Text = row.Cells[2].Value.ToString();

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
        //        string macn = txtTK.Text;
        //        string sqltk = "select *from CHUYENNGANH where MACN ='" + macn + "'";
        //        SqlCommand cmd = new SqlCommand(sqltk, cn.con);
        //        cmd.ExecuteNonQuery();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        DataTable dt = new DataTable();
        //        dt.Load(dr);
        //        dgvthongtin.DataSource = dt;
        //    }
        //}

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
            dgvthongtin.DataSource = hienthiTK("select * from CHUYENNGANH where MACN like '%" + txtTK.Text.Trim() + "%' or TENCN like '%" + txtTK.Text.Trim() + "%'");
        }
    }
}
