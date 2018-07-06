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
    public partial class frmQuan_tri_nguoi_dung : Form
    {
        SqlCommand cm;
        SqlCommand cm1;
        public frmQuan_tri_nguoi_dung()
        {
            InitializeComponent();           
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select * from CANBO";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        public DataTable danhsachCB()
        {
            Connection cn = new Connection();
            SqlDataAdapter da = new SqlDataAdapter("select MACB  as 'Mã Cán Bộ',MAKHOA as 'Mã Khoa',HOTEN as 'Họ Tên',NGAYSINH as 'Ngày Sinh',GIOITINH as 'Giới Tính',CHUCVU as 'Chức vụ',EMAIL as 'Email',MATKHAU as 'Mật Khẩu',QUYENHAN as 'Quyền Hạn' from CANBO", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void Loadcbmakkhoa()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select MAKHOA from KHOA", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmakhoa.DataSource = ds.Tables[0];
            cbmakhoa.ValueMember = "MAKHOA";
        }
        public void Reset()
        {
            txtmacb.Clear();
            cbmakhoa.Text = "";
            txthoten.Clear();
            dpngaysinh.Value = DateTime.Today;
            radioNam.Checked = true;
            txtchucvu.Clear();
            txtemail.Clear();
            txtmk.Clear();
            cbquyen.Text = "";
        }        

        private void frmQuan_tri_nguoi_dung_Load(object sender, EventArgs e)
        {
            hienthi();
            Loadcbmakkhoa();
            dgvthongtin.DataSource = danhsachCB();
            radioNam.Checked = true;
            cbquyen.Text = "covan";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string macb = txtmacb.Text;
            string makhoa = cbmakhoa.Text;
            string hoten = txthoten.Text;
            string ngaysinh = dpngaysinh.Text;
            string nam = radioNam.Text;
            string nu = radioNu.Text;
            string chucvu = txtchucvu.Text;
            string email = txtemail.Text;
            string matkhau = txtmk.Text;
            string quyen = cbquyen.Text;

            DateTime nams = Convert.ToDateTime(dpngaysinh.Value.ToString());
            int Ngt = int.Parse(DateTime.Now.Year.ToString());
            int nn = int.Parse(nams.Year.ToString());
            int tuoi = Ngt - nn;

            //KIEM TRA MA CAN BO 
            cm = new SqlCommand("select MACB from CANBO where MACB='" + macb + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (macb == ma)
            {
                errorProvider1.SetError(txtmacb, "Trùng mã cán bộ, thêm thất bại");
                return;
            }
            if (!Regex.IsMatch(macb, @"^CB\d{4}$"))
            {
                errorProvider1.SetError(txtmacb, "Mã bắt buộc phải có 2 ký tự CB và có 4 chữ số! Không chứa khoảng trắng!");
                return;
            }
            ////KIEM TRA MA KHOA
            if (makhoa == " ")
            {
                errorProvider1.SetError(cbmakhoa, "Chưa chọn mã khoa!");
                return;
            }
            //KIEM TRA HO TEN
            if (hoten.IndexOf(" ") < 0)
            {
                errorProvider1.SetError(txthoten, "Tên gồm 2 từ trở lên!");
                return;
            }
            //KIEM TRA NGAY SINH 
            if (tuoi < 18)
            {
                errorProvider1.SetError(dpngaysinh, "Đọc giả phải đủ 18 tuổi");
                return;
            }
            else
            if (tuoi > 100)
            {
                errorProvider1.SetError(dpngaysinh, "Đọc giả phải nhỏ hơn 100 tuổi");
                return;
            }
            //KIEM TRA EMAIL 
            if (email.IndexOf(" ") >= 0)
            {
                errorProvider1.SetError(txtemail, "Email không chứa khoảng trắng!");
                return;
            }
            //KIEM TRA MAT KHAU


            //KIEM TRA QUYEN HAN
            if (quyen == "admin")
            {
                errorProvider1.SetError(cbquyen, "Chỉ tồn tại 1 admin! Chọn quyền hạn khác");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }           
            //KIEM TRA RỖNG
            if (macb == "" || makhoa == "" || hoten == "" || ngaysinh == "" || chucvu == "" || email == "" || matkhau == "" || quyen == "")
            {
                lbChuy.Text = "Thông tin cán bộ không được bỏ trống!!!";
            }
            else
            //KIEM TRA GIOI TINH 
            if (radioNam.Checked == true )
            {
                radioNam.Text = "Nam";
                string sqlthem = "insert into CANBO values('" + macb + "','" + makhoa + "',N'" + hoten + "','" + ngaysinh + "',N'" + nam + "',N'" + chucvu + "','" + email + "','" + matkhau + "','" + quyen + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy.Text = " ";
                    frmQuan_tri_nguoi_dung_Load(sender, e);
                    Reset();
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thất bại!");
                }

            }
            else
            {
                if (radioNu.Checked == true)
                radioNu.Text = "Nu";
                string sqlthem = "insert into CANBO values('" + macb + "','" + makhoa + "',N'" + hoten + "','" + ngaysinh + "',N'" + nu + "',N'" + chucvu + "','" + email + "','" + matkhau + "','" + quyen + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy.Text = " ";
                    frmQuan_tri_nguoi_dung_Load(sender, e);
                    Reset();
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thất bại!");
                }
            }      
            hienthi();
            dgvthongtin.DataSource = danhsachCB();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string macb = txtmacb.Text;
            string makhoa = cbmakhoa.Text;
            string hoten = txthoten.Text;
            string ngaysinh = dpngaysinh.Text;

            string nam = radioNam.Text;
            string nu = radioNu.Text;

            string chucvu = txtchucvu.Text;
            string email = txtemail.Text;
            string matkhau = txtmk.Text;
            string quyen = cbquyen.Text;

           
            //else
            cm = new SqlCommand("select MACB from CANBO where MACB='" + macb + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (macb == ma)           
            {
                //cm1 = new SqlCommand("select QUYENHAN from CANBO where QUYENHAN='" + quyen + "'", cn.con);
                //string qh = cm.ExecuteScalar() as string;
                //if (qh != quyen)
                //{
                //    errorProvider1.SetError(cbquyen, "Không thể thay đổi quyền hạn");
                //    return;
                //}
                //else
                //{
                    DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                { 
                    if (radioNam.Checked == true)
                    {
                        radioNam.Text = "Nam";
                        string sqlsua = "update CANBO set MACB='" + macb + "',MAKHOA='" + makhoa + "',HOTEN='" + hoten + "',NGAYSINH='" + ngaysinh + "',GIOITINH='" + nam + "',CHUCVU='" + chucvu + "',EMAIL='" + email + "',MATKHAU='" + matkhau + "',QUYENHAN='" + quyen + "' where  MACB='" + macb + "'";
                        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thành công");
                            lbChuy.Text = " ";
                            frmQuan_tri_nguoi_dung_Load(sender, e);
                            Reset();
                        }
                        catch
                        {
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thất bại!");
                        }
                    }
                    else
                    {
                        if (radioNu.Checked == true)
                            radioNu.Text = "Nu";
                        string sqlsua = "update CANBO set MACB='" + macb + "',MAKHOA='" + makhoa + "',HOTEN='" + hoten + "',NGAYSINH='" + ngaysinh + "',GIOITINH='" + nu + "',CHUCVU='" + chucvu + "',EMAIL='" + email + "',MATKHAU='" + matkhau + "',QUYENHAN='" + quyen + "' where  MACB='" + macb + "'";
                        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thành công");
                            lbChuy.Text = " ";
                            frmQuan_tri_nguoi_dung_Load(sender, e);
                            Reset();
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
                    MessageBox.Show("Không trùng mã cán bộ!");
                hienthi();
                dgvthongtin.DataSource = danhsachCB();
            //}
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string macb = txtmacb.Text;
            Connection cn = new Connection();
            cn.OpenConn();
            if (macb == "")
            {
                MessageBox.Show("Thông tin cần xóa hiện không tồn tại! ");
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete CANBO where MACB='" + macb + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmacb.Clear();
                        cbmakhoa.Text = "";
                        txthoten.Clear();
                        dpngaysinh.Value = DateTime.Today;
                        radioNam.Checked = true;
                        txtchucvu.Clear();
                        txtemail.Clear();
                        txtmk.Clear();
                        cbquyen.Text = "";
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
                hienthi();
                dgvthongtin.DataSource = danhsachCB();
            }
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                txtmacb.Text = row.Cells[0].Value.ToString();             
                cbmakhoa.Text = row.Cells[1].Value.ToString();
                txthoten.Text = row.Cells[2].Value.ToString();
                dpngaysinh.Text = row.Cells[3].Value.ToString();

                if (row.Cells[4].Value.ToString() == "Nam")
                {
                    radioNam.Checked = true;
                }
                else
                    radioNu.Checked = true;

                txtchucvu.Text = row.Cells[5].Value.ToString();
                txtemail.Text = row.Cells[6].Value.ToString();
                txtmk.Text = row.Cells[7].Value.ToString();
                cbquyen.Text = row.Cells[8].Value.ToString();
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
            dgvthongtin.DataSource = hienthiTK("select * from CANBO where MACB like '%" + txtTK.Text.Trim() + "%'");
        }


    }
}
