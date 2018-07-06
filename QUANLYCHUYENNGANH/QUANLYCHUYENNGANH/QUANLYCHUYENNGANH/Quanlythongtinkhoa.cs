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
using System.Text.RegularExpressions;

namespace QUANLYCHUYENNGANH
{
    public partial class frmQuanlythongtinkhoa : Form
    {
        SqlCommand cm;
        public frmQuanlythongtinkhoa()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select * from KHOA";
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
            SqlDataAdapter da = new SqlDataAdapter("select MAKHOA as 'Mã Khoa',TENKHOA as 'Tên Khoa',DIACHI as 'Địa chỉ',EMAIL as 'Email',SDT as 'Số điện thoại' from KHOA", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static bool CheckSo(String str)
        {
            int i;
            bool check = true;
            try
            {
                i = Int32.Parse(str);
                if (i >= 0)
                {
                    check = true;
                }
            }
            catch
            {
                check = false;
            }
            return check;
        }     
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string makhoa = txtmakhoa.Text.Trim();
            string tenkhoa = txttenkhoa.Text;
            string diachi = txtdiachi.Text;
            string email = txtemail.Text;
            string sdt = txtsdt.Text;

            if (makhoa == "" || tenkhoa == "" || diachi == "" || sdt == "" || email == "")
            {
                lbChuy1.Text = "Thông tin khoa không được bỏ trống!!!";
            }
            //else
            if (makhoa.IndexOf(" ") >= 0)
            {
                errorProvider1.SetError(txtmakhoa, "Mã không chứa khoảng trắng!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //
            if (tenkhoa.IndexOf(" ") < 0)
            {
                errorProvider1.SetError(txttenkhoa, "Tên gồm 2 từ trở lên!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //else 
            if (CheckSo(txtsdt.Text) == false)
            {
                errorProvider1.SetError(txtsdt, "Chỉ được nhập số!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //else
            if (txtsdt.Text.Length < 10 || txtsdt.Text.Length > 11)
            {
                errorProvider1.SetError(txtsdt, "Số điện thoại gồm 10 hoặc 11 số!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            cm = new SqlCommand("select MAKHOA from KHOA where MAKHOA='" + makhoa + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (makhoa == ma)
            {

                lbChuy1.Text = "Trùng mã khoa, thêm thất bại!";
            }
            else
            {
                string sqlthem = "insert into KHOA values('" + makhoa + "',N'" + tenkhoa + "',N'" + diachi + "','" + email + "','" + sdt + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy1.Text = "";
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
            string diachi = txtdiachi.Text;
            string email = txtemail.Text;
            string sdt = txtsdt.Text;

            if (makhoa == "" || tenkhoa == "" || diachi == "" || sdt == "" || email == "")
            {
                lbChuy1.Text = "Thông tin khoa không được bỏ trống!!!";
            }
            else
            if (makhoa.IndexOf(" ") >= 0)
            {
                errorProvider1.SetError(txtmakhoa, "Mã không chứa khoảng trắng!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //
            if (tenkhoa.IndexOf(" ") < 0)
            {
                errorProvider1.SetError(txttenkhoa, "Tên gồm 2 từ trở lên!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //else 
            if (CheckSo(txtsdt.Text) == false)
            {
                errorProvider1.SetError(txtsdt, "Chỉ được nhập số!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //else
            if (txtsdt.Text.Length < 10 || txtsdt.Text.Length > 11)
            {
                errorProvider1.SetError(txtsdt, "Số điện thoại gồm 10 hoặc 11 số!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            cm = new SqlCommand("select MAKHOA from KHOA where makhoa='" + makhoa + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (makhoa == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update KHOA set MAKHOA='" + makhoa + "',TENKHOA='" + tenkhoa + "',DIACHI='" + diachi + "',EMAIL='" + email + "',SDT='" + sdt + "' where  MAKHOA='" + makhoa + "'";
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
                MessageBox.Show("Không trùng mã khoa!");
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
                    string sqlxoa = "delete KHOA where  MAKHOA='" + makhoa + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmakhoa.Clear();
                        txttenkhoa.Clear();
                        txtdiachi.Clear();
                        txtsdt.Clear();
                        txtemail.Clear();
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

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                txtmakhoa.Text = row.Cells[0].Value.ToString();
                txttenkhoa.Text = row.Cells[1].Value.ToString();
                txtdiachi.Text = row.Cells[2].Value.ToString();
                txtemail.Text = row.Cells[3].Value.ToString();
                txtsdt.Text = row.Cells[4].Value.ToString();
            }
        }

        private void frmQuanlythongtinkhoa_Load(object sender, EventArgs e)
        {
            hienthi();
            dgvthongtin.DataSource = danhsachkhoa();
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
            dgvthongtin.DataSource = hienthiTK("select * from KHOA where MAKHOA like '%" + txtTK.Text.Trim() + "%' or TENKHOA like '%" + txtTK.Text.Trim() + "%'");
        }

        private void txtmakhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= 65 && e.KeyChar <= 122) || (e.KeyChar == 8));
        }
    }
}
