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
using System.Media;


namespace QUANLYCHUYENNGANH
{
    public partial class frmQuan_ly_thong_tin_nganh : Form
    {
        SqlCommand cm;
        public SoundPlayer error = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Error.wav");

        public frmQuan_ly_thong_tin_nganh()
        {
            InitializeComponent();
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "Select Distinct MANGANH as 'Mã Ngành',TENKHOA as 'Tên Khoa',TENNGANH as 'Tên Ngành' from KHOA, NGANH where KHOA.MAKHOA = NGANH.MAKHOA";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        //public DataTable danhsachkhoa()
        //{
        //    Connection cn = new Connection();
        //    SqlDataAdapter da = new SqlDataAdapter("select MANGANH  as 'Mã Ngành',MAKHOA as 'Mã Khoa',TENNGANH as 'Tên Ngành' from NGANH", cn.con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}
        public void Loadcbmakhoa()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select MAKHOA,TENKHOA from KHOA", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmakhoa.DataSource = ds.Tables[0];
            cbmakhoa.ValueMember = "MAKHOA";
            cbmakhoa.DisplayMember = "TENKHOA";
        }

        private void Quan_ly_thong_tin_nganh_Load(object sender, EventArgs e)
        {
            hienthi();
            Loadcbmakhoa();
            //dgvthongtin.DataSource = danhsachkhoa();
            //cbmakhoa.Text = ("---------Chọn mã khoa----------");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string manganh = txtmanganh.Text;
            string makhoa = cbmakhoa.SelectedValue.ToString();
            string tennganh = txttennganh.Text;
            cm = new SqlCommand("select MANGANH from NGANH where MANGANH='" + manganh + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if(txtmanganh.TextLength < 8)
            {
                errorProvider1.SetError(txtmanganh, "Mã ngành gồm 8 chữ số!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            if (manganh == "" || tennganh == "")
            {
                MessageBox.Show("Thông tin ngành không được bỏ trống!!!");
            }
            else
            if (manganh == ma)
            {
                error.Play();
                MessageBox.Show("Trùng mã ngành, thêm thất bại");
            }
            else
            {
                string sqlthem = "insert into NGANH values('" + manganh + "',N'" + makhoa + "',N'" + tennganh + "')";
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
            //dgvthongtin.DataSource = danhsachkhoa();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string manganh = txtmanganh.Text;
            string makhoa = cbmakhoa.SelectedValue.ToString();
            string tennganh = txttennganh.Text;
            cm = new SqlCommand("select MANGANH from NGANH where MANGANH='" + manganh + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (manganh == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update NGANH set MANGANH='" + manganh + "',MAKHOA=N'" + makhoa + "',TENNGANH=N'" + tennganh + "' where  MANGANH='" + manganh + "'";
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
                MessageBox.Show("Không trùng mã ngành!");
            hienthi();
            //dgvthongtin.DataSource = danhsachkhoa();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string manganh = txtmanganh.Text;
            Connection cn = new Connection();
            cn.OpenConn();
            if (manganh == "")
            {
                MessageBox.Show("Thông tin cần xóa hiện không tồn tại! ");
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete NGANH where  MANGANH='" + manganh + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmanganh.Clear();
                        txttennganh.Clear();
                        cbmakhoa.Text = "-----Chọn mã khoa-----";
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
                hienthi();
                //dgvthongtin.DataSource = danhsachkhoa();
            }
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                txtmanganh.Text = row.Cells[0].Value.ToString();
                cbmakhoa.Text = row.Cells[1].Value.ToString();
                txttennganh.Text = row.Cells[2].Value.ToString();

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
        //        string manganh = txtTK.Text;
        //        string sqltk = "select *from NGANH where MANGANH ='" + manganh + "'";
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
            dgvthongtin.DataSource = hienthiTK("select * from NGANH where MANGANH like '%" + txtTK.Text.Trim() + "%'");
        }

        private void txtmanganh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
