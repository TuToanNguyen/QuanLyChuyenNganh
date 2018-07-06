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
    public partial class frmQuanlysinhvien : Form
    {
        SqlCommand cm;
        public static string UsertName = "";
        public static string ma = "";
        public frmQuanlysinhvien()
        {
            InitializeComponent();
        }

        public DataTable danhsachSV()
        {
            Connection cn = new Connection();
            SqlDataAdapter da = new SqlDataAdapter("select MSSV,TENLOP as 'Mã Lớp',MACN as 'Mã CN',HOTEN as 'Họ Tên',NGAYSINH as 'Ngày Sinh',GIOITINH as 'Giới Tính',CMND as 'Số CMND',NIENKHOA as 'Niên khóa',DIACHI as 'Địa Chỉ' from SINHVIEN", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            
            string sqlht = "select DISTINCT MSSV,tenlop,tencn as 'Tên chuyên ngành',SINHVIEN.HOTEN,SINHVIEN.NGAYSINH,SINHVIEN.GIOITINH,CMND,NIENKHOA,SINHVIEN.DIACHI from CANBO,KHOA,NGANH,SINHVIEN,LOP,CHUYENNGANH where CHUYENNGANH.MACN=SINHVIEN.MACN AND CANBO.MAKHOA=KHOA.MAKHOA and KHOA.MAKHOA=NGANH.MAKHOA and NGANH.MANGANH=LOP.MANGANH and LOP.MALOP=SINHVIEN.MALOP and CANBO.MACB='"+ frmForm1.UsertName+ "' and sinhvien.macn='"+CBB.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        public void Loadcbmalop()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select DISTINCT LOP.MALOP,tenlop from CANBO,KHOA,NGANH,LOP where CANBO.MAKHOA=KHOA.MAKHOA and KHOA.MAKHOA=NGANH.MAKHOA and NGANH.MANGANH=LOP.MANGANH and CANBO.MACB='" + frmForm1.UsertName + "'", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmalop.DataSource = ds.Tables[0];
            cbmalop.ValueMember = "MALOP";
            cbmalop.DisplayMember = "tenlop";
        }
        public void Loadcbmachuyennganh()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select MACN,TENCN from CHUYENNGANH", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmacn.DataSource = ds.Tables[0];
            cbmacn.ValueMember = "MACN";
            cbmacn.DisplayMember = "TENCN";
        }
        public void Loadcbmachuyennganh1()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select distinct CHUYENNGANH.MACN,TENCN from CANBO,KHOA,NGANH,SINHVIEN,LOP,CHUYENNGANH where CHUYENNGANH.MACN=SINHVIEN.MACN AND CANBO.MAKHOA=KHOA.MAKHOA and KHOA.MAKHOA=NGANH.MAKHOA and NGANH.MANGANH=LOP.MANGANH and LOP.MALOP=SINHVIEN.MALOP and CANBO.MACB='" + frmForm1.UsertName + "' ", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CBB.DataSource = ds.Tables[0];
            CBB.ValueMember = "MACN";
            CBB.DisplayMember = "TENCN";
        }
        public void reset()
        {
            txtmssv.Clear();
            txthoten.Clear();
            radioNam.Checked = true;
            dpngaysinh.Value = DateTime.Today;
            txtcmnd.Clear();
            txtnienkhoa.Clear();
            txtdiachi.Clear();
        }

        private void Quanlysinhvien_Load(object sender, EventArgs e)
        {
            hienthi();
            Loadcbmachuyennganh1();
            Loadcbmachuyennganh();
            Loadcbmalop();
            radioNam.Checked = true;
           // dgvthongtin.DataSource = danhsachSV();
        }

        private void txtemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtmssv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txthoten_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ko nhap so nhung ko nhap dc khoang cach
            //e.Handled = !((e.KeyChar >= 65 && e.KeyChar <= 122) || (e.KeyChar == 8));      
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                ma=txtmssv.Text = row.Cells[0].Value.ToString();
                cbmalop.Text = row.Cells[1].Value.ToString();
                cbmacn.Text = row.Cells[2].Value.ToString();
                txthoten.Text = row.Cells[3].Value.ToString();
                dpngaysinh.Text = row.Cells[4].Value.ToString();

                if (row.Cells[5].Value.ToString() == "nam")
                {
                    radioNam.Checked = true;
                }
                else
                    radioNu.Checked = true;

                txtcmnd.Text = row.Cells[6].Value.ToString();
                txtnienkhoa.Text = row.Cells[7].Value.ToString();
                txtdiachi.Text = row.Cells[8].Value.ToString();

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string mssv = txtmssv.Text;
            string malop = cbmalop.SelectedValue.ToString();
            string macn = cbmacn.SelectedValue.ToString();
            string hoten = txthoten.Text;
            string ngaysinh = dpngaysinh.Text;

            string nam = radioNam.Text;
            string nu = radioNu.Text;

            string cmnd = txtcmnd.Text;
            string nienkhoa = txtnienkhoa.Text;
            string diachi = txtdiachi.Text;

            DateTime nams = Convert.ToDateTime(dpngaysinh.Value.ToString());
            int Ngt = int.Parse(DateTime.Now.Year.ToString());
            int nn = int.Parse(nams.Year.ToString());
            int tuoi = Ngt - nn;

            //KIEM TRA HO TEN
            if (hoten.IndexOf(" ") < 0)
            {
                errorProvider1.SetError(txthoten, "Tên gồm 2 từ trở lên!");
                return;
            }
            //KIEM TRA NGAY SINH 
            if (tuoi < 17)
            {
                errorProvider1.SetError(dpngaysinh, "Sinh viên phải đủ 17 tuổi");
                return;
            }
            else
            if (tuoi > 100)
            {
                errorProvider1.SetError(dpngaysinh, "Sinh viên phải nhỏ hơn 100 tuổi");
                return;
            }

            //KIEM TRA RỖNG
            if (mssv == "" || malop == "" || hoten == "" || ngaysinh == "" || cmnd == "" || nienkhoa == "" || diachi == "" )
            {
                lbChuy.Text = "Thông tin sinh vien không được bỏ trống!!!";
            }
            cm = new SqlCommand("select MSSV from SINHVIEN where MSSV='" + mssv + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (mssv == ma)
            {
                errorProvider1.SetError(txtmssv, "Trùng mã cán bộ, thêm thất bại");
                return;
            }
            else
            //KIEM TRA GIOI TINH 
            if (radioNam.Checked == true)
            {
                radioNam.Text = "Nam";
                string sqlthem = "insert into SINHVIEN values('" + mssv + "','" + malop + "','" + macn + "',N'" + hoten + "','" + ngaysinh + "',N'" + nam + "',N'" + cmnd + "','" + nienkhoa + "','" + diachi + "')";
               
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy.Text = " ";
                    Quanlysinhvien_Load(sender, e);
                    reset();
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
                string sqlthem = "insert into SINHVIEN values('" + mssv + "','" + malop + "','" + macn + "',N'" + hoten + "','" + ngaysinh + "',N'" + nu + "',N'" + cmnd + "','" + nienkhoa + "','" + diachi + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy.Text = " ";
                    Quanlysinhvien_Load(sender, e);
                    reset();
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            //hienthi();
            //Quanlysinhvien_Load(sender, e);
            //dgvthongtin.DataSource = danhsachSV();
        }

        private void cbmalop_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void CBB_SelectedIndexChanged(object sender, EventArgs e)
        {
            hienthi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtmssv.TextLength != 0)
            {
                frmQuanlyquatrinhhocsinhvien f = new frmQuanlyquatrinhhocsinhvien();
                f.ShowDialog();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
