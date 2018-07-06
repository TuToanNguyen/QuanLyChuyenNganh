using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;


namespace QUANLYCHUYENNGANH
{
    public partial class frmQuanlysinhvien : Form
    {
        SqlCommand cm;
        public SoundPlayer error = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Error.wav");
        public SoundPlayer warning = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Ding.wav");
        public SoundPlayer success = new SoundPlayer(@"e:\Âm thanh thông báo\tada.wav");
        public SoundPlayer start = new SoundPlayer(@"e:\Âm thanh thông báo\Ring06.wav");

        public static string UsertName = "";
        public static string ma = "",cn;
        public frmQuanlysinhvien()
        {
            InitializeComponent();
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = @"select DISTINCT MSSV,tenlop as 'Tên lớp',tencn as 'Tên CN',SINHVIEN.HOTEN as 'Họ tên',SINHVIEN.NGAYSINH as 
                'Ngày sinh',SINHVIEN.GIOITINH as 'Giới tính',CMND,NIENKHOA as 'Niên khóa',SINHVIEN.DIACHI as 'Địa chỉ' 
                from CANBO,COVAN,SINHVIEN,LOP,CHUYENNGANH where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and
                LOP.MALOP = SINHVIEN.MALOP and CHUYENNGANH.MACN=SINHVIEN.MACN and 
                CANBO.MACB='" + frmForm1.UsertName + "' and sinhvien.macn='" + CBB.SelectedValue + "' and sinhvien.MALOP='" +cbtenlop.SelectedValue + "' ";
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
            cm = new SqlCommand(@"select DISTINCT LOP.MALOP,tenlop 
                                    from COVAN, LOP, CANBO
                                    where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and CANBO.MACB = '" + frmForm1.UsertName + "' " +
                                    " AND LOP.MALOP IN( " +
                                    "SELECT MALOP FROM COVAN WHERE MACB = '"+ frmForm1.UsertName + "' AND GETDATE() BETWEEN THOIGIANBD AND THOIGIANKT UNION ALL SELECT MALOP FROM COVAN WHERE MACB = '" + frmForm1.UsertName + "' AND THOIGIANKT IS NULL)", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmalop.DataSource = ds.Tables[0];
            cbmalop.ValueMember = "MALOP";
            cbmalop.DisplayMember = "tenlop";

            Loadcbmachuyennganh();
        }

        public void Loadcbmachuyennganh()
        {
            try
            {
                Connection cn = new Connection();
                cn.OpenConn();
                cm = new SqlCommand(@"select DISTINCT CHUYENNGANH.MACN,TENCN from COVAN,LOP,CANBO,NGANH,CHUYENNGANH where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and LOP.MANGANH = NGANH.MANGANH and NGANH.MANGANH = CHUYENNGANH.MANGANH and CANBO.MACB='" + frmForm1.UsertName + "' AND LOP.MALOP='" + cbmalop.SelectedValue.ToString() + "'  ", cn.con);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cbmacn.DataSource = ds.Tables[0];
                cbmacn.ValueMember = "MACN";
                cbmacn.DisplayMember = "TENCN";
            }
            catch { }
        }

        public void Loadcbmalop1()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand(@"select DISTINCT LOP.MALOP,tenlop 
                                    from COVAN, LOP, CANBO
                                    where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and CANBO.MACB = '" + frmForm1.UsertName + "' " +
                                    " AND LOP.MALOP IN( " +
                                    "SELECT MALOP FROM COVAN WHERE MACB = '" + frmForm1.UsertName + "' AND GETDATE() BETWEEN THOIGIANBD AND THOIGIANKT UNION ALL SELECT MALOP FROM COVAN WHERE MACB = '" + frmForm1.UsertName + "' AND THOIGIANKT IS NULL)", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbtenlop.DataSource = ds.Tables[0];
            cbtenlop.ValueMember = "MALOP";
            cbtenlop.DisplayMember = "tenlop";
        }
        
        public void Loadcbmachuyennganh1()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand(@"select DISTINCT CHUYENNGANH.MACN,TENCN from COVAN,LOP,CANBO,NGANH,CHUYENNGANH where CANBO.MACB = COVAN.MACB 
            and COVAN.MALOP = LOP.MALOP and LOP.MANGANH = NGANH.MANGANH and NGANH.MANGANH = CHUYENNGANH.MANGANH and 
            CANBO.MACB='" + frmForm1.UsertName + "' and LOP.MALOP='"+cbtenlop.SelectedValue+"' ", cn.con);
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

        xuly xl = new xuly();
        private void Quanlysinhvien_Load(object sender, EventArgs e)
        {
            //txtmssv.Text = xl.TangMSSV();
            hienthi();
            Loadcbmalop();
            Loadcbmalop1();
            Loadcbmachuyennganh1();
            radioNam.Checked = true;
            //dgvthongtin.DataSource = danhsachSV();
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
            string ngaysinh = dpngaysinh.Value.ToString("yyyy/MM/dd");

            string nam = radioNam.Text;
            string nu = radioNu.Text;

            string cmnd = txtcmnd.Text;
            string nienkhoa = txtnienkhoa.Text;
            string diachi = txtdiachi.Text;

            DateTime nams = Convert.ToDateTime(dpngaysinh.Value.ToString());
            int Ngt = int.Parse(DateTime.Now.Year.ToString());
            int nn = int.Parse(nams.Year.ToString());
            int tuoi = Ngt - nn;

            //KIEM TRA MSSV
            //if(txtmssv.TextLength != 8)
            //{
            //    errorProvider1.SetError(txtmssv, "MSSV phải đủ 8 chữ số!");
            //    return;
            ////}
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
            //KIEM TRA CMND
            if(txtcmnd.TextLength != 9)
            {
                errorProvider1.SetError(txtcmnd, "Số CMND phải đủ 9 chữ số!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //KIEM TRA RỖNG
            if (malop == "" || hoten == "" || ngaysinh == "" || cmnd == "" || nienkhoa == "" || diachi == "" )
            {
                warning.Play();
                lbChuy.Text = "Thông tin sinh vien không được bỏ trống!!!";
            }

            cm = new SqlCommand("select MSSV from SINHVIEN where MSSV='" + mssv + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (mssv == ma)
            {
                error.Play();
                errorProvider1.SetError(txtmssv, "Trùng mã sinh viên, thêm thất bại");
                return;
            }           

            else
            //KIEM TRA GIOI TINH 
            if (radioNam.Checked == true)
            {
                radioNam.Text = "Nam";
                string sqlthem = "insert into SINHVIEN values('" + mssv + "','" + malop + "','" + macn + "',N'" + hoten + "',Cast('" + ngaysinh + "' as datetime),N'" + nam + "',N'" + cmnd + "','" + nienkhoa + "',N'" + diachi + "')";
               
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy.Text = " ";
                    hienthi();
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    error.Play();
                    MessageBox.Show("Thêm thất bại!");
                }

            }
            else
            {
                if (radioNu.Checked == true)
                radioNu.Text = "Nu";               
                string sqlthem = "insert into SINHVIEN values('" + mssv + "','" + malop + "','" + macn + "',N'" + hoten + "','" + ngaysinh + "',N'" + nu + "',N'" + cmnd + "','" + nienkhoa + "',N'" + diachi + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy.Text = " ";
                    reset();
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    error.Play();
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            hienthi();
        }

        private void CBB_SelectedIndexChanged(object sender, EventArgs e)
        {
            hienthi();                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtmssv.TextLength != 0)
            {
                ma = txtmssv.Text;
                cn = CBB.SelectedValue.ToString();
                frmQuanlyquatrinhhocsinhvien f = new frmQuanlyquatrinhhocsinhvien();
                f.ShowDialog();
            }
            else
                warning.Play();
                MessageBox.Show("Bạn cần chọn sinh viên để nhập điểm");
        }

        private void cbtenlop_SelectedIndexChanged(object sender, EventArgs e)
        {
            hienthi();
            Loadcbmachuyennganh1();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string mssv = txtmssv.Text;
            string malop = cbmalop.SelectedValue.ToString();
            string macn = cbmacn.SelectedValue.ToString();
            string hoten = txthoten.Text;
            string ngaysinh = dpngaysinh.Value.ToString("yyyy/MM/dd");

            string nam = radioNam.Text;
            string nu = radioNu.Text;

            string cmnd = txtcmnd.Text;
            string nienkhoa = txtnienkhoa.Text;
            string diachi = txtdiachi.Text;

            DateTime nams = Convert.ToDateTime(dpngaysinh.Value.ToString());
            int Ngt = int.Parse(DateTime.Now.Year.ToString());
            int nn = int.Parse(nams.Year.ToString());
            int tuoi = Ngt - nn;

            //KIEM TRA MSSV
            if (txtmssv.TextLength != 8)
            {
                errorProvider1.SetError(txtmssv, "MSSV phải đủ 8 chữ số!");
                return;
            }
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
            //KIEM TRA CMND
            if (txtcmnd.TextLength != 9)
            {
                errorProvider1.SetError(txtcmnd, "Số CMND phải đủ 9 chữ số!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            //KIEM TRA RỖNG
            if (mssv == "" || malop == "" || hoten == "" || ngaysinh == "" || cmnd == "" || nienkhoa == "" || diachi == "")
            {
                warning.Play();
                lbChuy.Text = "Thông tin sinh vien không được bỏ trống!!!";
            }
            else
            cm = new SqlCommand("select MSSV from SINHVIEN where MSSV='" + mssv + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (mssv == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    if (radioNam.Checked == true)
                    {
                        radioNam.Text = "Nam";
                        string sqlsua = @"update SINHVIEN set MSSV='" + mssv + "',MALOP=N'" + malop + "',MACN=N'" + macn + "',HOTEN=N'" + hoten + "',NGAYSINH='" + ngaysinh + "',GIOITINH='" + nam + "',CMND=N'" + cmnd + "',NIENKHOA='" + nienkhoa + "',DIACHI=N'" + diachi + "' where  MSSV='" + mssv + "'";
                        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thành công");
                            lbChuy.Text = " ";
                            hienthi();
                        }
                        catch
                        {
                            cmd.Dispose();
                            cn.CloseConn();
                            error.Play();
                            MessageBox.Show("Sửa thất bại!");
                        }
                    }
                    else
                    {
                        if (radioNu.Checked == true)
                            radioNu.Text = "Nu";
                        string sqlsua = @"update SINHVIEN set MSSV='" + mssv + "',MALOP=N'" + malop + "',MACN=N'" + macn + "',HOTEN=N'" + hoten + "',NGAYSINH='" + ngaysinh + "',GIOITINH='" + nu + "',CMND=N'" + cmnd + "',NIENKHOA='" + nienkhoa + "',DIACHI=N'" + diachi + "' where  MSSV='" + mssv + "'";
                        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thành công");
                            lbChuy.Text = " ";
                            hienthi();
                        }
                        catch
                        {
                            cmd.Dispose();
                            cn.CloseConn();
                            error.Play();
                            MessageBox.Show("Sửa thất bại!");
                        }
                    }
                }
                else
                    MessageBox.Show("Không trùng mã cán bộ!");
                hienthi();             
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mssv = txtmssv.Text;
            Connection cn = new Connection();
            cn.OpenConn();
            if (mssv == "")
            {
                warning.Play();
                MessageBox.Show("Thông tin cần xóa hiện không tồn tại! ");
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete from SINHVIEN where MSSV='" + mssv + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmssv.Clear();
                        cbmalop.Text = "";
                        txthoten.Clear();
                        dpngaysinh.Value = DateTime.Today;
                        radioNam.Checked = true;
                        txtnienkhoa.Clear();
                        txtdiachi.Clear();
                        txtcmnd.Clear();
                        cbmacn.Text = "";
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        error.Play();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
                hienthi();
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
            dgvthongtin.DataSource = hienthiTK("select * from SINHVIEN where MSSV like '%" + txtTK.Text.Trim() + "%' OR HOTEN like '%" + txtTK.Text.Trim() + "%'");
        }


        private void txtcmnd_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void btnLoadma_Click(object sender, EventArgs e)
        {
            txtmssv.Text = xl.TangMSSV();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvthongtin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbmalop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadcbmachuyennganh();
        }

    }
}
