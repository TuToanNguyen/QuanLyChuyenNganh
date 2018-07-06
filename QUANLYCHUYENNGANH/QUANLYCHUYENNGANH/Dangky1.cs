using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace QUANLYCHUYENNGANH
{
    public partial class frmDangky1 : Form
    {
        SqlCommand cm;
        public static string ms = "";
        public static string hoten = "";
        public static string ngaysinh = "";
        public static string gioitinh = "";
        public static string tenkhoa = "";
        public static string chucvu = "";

        public frmDangky1()
        {
            InitializeComponent();
        }

        public void Loadcbmakkhoa()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select distinct KHOA.MAKHOA,TENKHOA from KHOA,CANBO", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmakhoa.DataSource = ds.Tables[0];
            cbmakhoa.ValueMember = "MAKHOA";
            cbmakhoa.DisplayMember = "TENKHOA";

        }

        private void btnDK_Click_1(object sender, EventArgs e)
        {
            ms = txtmacb.Text;
           // ms = txthoten.Text;

            Connection cn = new Connection();
            cn.OpenConn();
            string macb = txtmacb.Text;
            string makhoa = cbmakhoa.SelectedValue.ToString();
            string hoten = txthoten.Text;
            string ngaysinh = dpngaysinh.Value.ToString("yyyy/MM/dd");
            string nam = radioNam.Text;
            string nu = radioNu.Text;
            string chucvu = txtchucvu.Text;
            string email = txtemail.Text;
            string matkhau = txtmk.Text;
            string xnmk = txtmkxn.Text;
            string quyen = "";

            DateTime nams = Convert.ToDateTime(dpngaysinh.Value.ToString());
            int Ngt = int.Parse(DateTime.Now.Year.ToString());
            int nn = int.Parse(nams.Year.ToString());
            int tuoi = Ngt - nn;

            //KIEM TRA MA CAN BO 
            cm = new SqlCommand("select MACB from CANBO where MACB='" + macb + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (macb == ma)
            {
                errorProvider1.SetError(txtmacb, "Mã cán bộ này đã tồn tại!");
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
                errorProvider1.SetError(dpngaysinh, "Cán bộ phải đủ 18 tuổi");
                return;
            }
            else
            if (tuoi > 100)
            {
                errorProvider1.SetError(dpngaysinh, "Cán bộ phải nhỏ hơn 100 tuổi");
                return;
            }
            //KIEM TRA EMAIL 
            if (email.IndexOf(" ") >= 0)
            {
                errorProvider1.SetError(txtemail, "Email không chứa khoảng trắng!");
                return;
            }
            //KIEM TRA MAT KHAU 
            if(txtmk.Text.CompareTo(txtmkxn.Text) != 0)
            {
                errorProvider1.SetError(txtmkxn, "Mật khẩu không trùng khớp!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //KIEM TRA RỖNG
            if (macb == "" || makhoa == "" || hoten == "" || ngaysinh == "" || chucvu == "" || email == "" || matkhau == "" || xnmk == "")
            {
                lbChuy.Text = "Thông tin đăng ký không được bỏ trống!!!";
            }
            else
            //KIEM TRA GIOI TINH 
            if (radioNam.Checked == true)
            {
                radioNam.Text = "Nam";
                string sqlthem = "insert into CANBO values('" + macb + "','" + makhoa + "',N'" + hoten + "','" + ngaysinh + "',N'" + nam + "',N'" + chucvu + "','" + email + "','" + xnmk + "','" + quyen + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Đăng ký thành công");
                    lbChuy.Text = " ";

                    //chuyen form
                    frmDangky f = new frmDangky();
                    f.ShowDialog();
                    frmDangky.ms = txtmacb.Text;
                    frmDangky.hoten = txthoten.Text;
                    frmDangky.ngaysinh = dpngaysinh.Text;
                    frmDangky.gioitinh = "Nam";
                    frmDangky.tenkhoa = cbmakhoa.Text;
                    frmDangky.chucvu = txtchucvu.Text;
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Đăng ký thất bại!");

                    //string soundfile = 
                }

            }
            else
            {
                if (radioNu.Checked == true)
                    radioNu.Text = "Nu";
                string sqlthem = "insert into CANBO values('" + macb + "','" + makhoa + "',N'" + hoten + "','" + ngaysinh + "',N'" + nu + "',N'" + chucvu + "','" + email + "','" + xnmk + "','" + quyen + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Đăng ký thành công");
                    lbChuy.Text = " ";

                    frmDangky f = new frmDangky();
                    f.ShowDialog();
                    frmDangky.ms = txtmacb.Text;
                    frmDangky.hoten = txthoten.Text;
                    frmDangky.ngaysinh = dpngaysinh.Text;
                    frmDangky.gioitinh = "Nam";
                    frmDangky.tenkhoa = cbmakhoa.Text;
                    frmDangky.chucvu = txtchucvu.Text;

                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Đăng ký thất bại!");
                }
            }
        }

        private void frmDangky1_Load(object sender, EventArgs e)
        {
            Loadcbmakkhoa();
            radioNam.Checked = true;
        }

        private void btnCAPQUYEN_Click(object sender, EventArgs e)
        {
            frmDangnhap f = new frmDangnhap();
            f.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void radioNam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioNu_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
