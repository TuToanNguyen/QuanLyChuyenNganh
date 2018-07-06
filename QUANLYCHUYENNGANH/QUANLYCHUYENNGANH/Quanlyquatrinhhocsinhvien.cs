using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;//important
using System.Data.OleDb;//important
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Media;

namespace QUANLYCHUYENNGANH
{
    public partial class frmQuanlyquatrinhhocsinhvien : Form
    {
        public SoundPlayer error = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Error.wav");
        public SoundPlayer warning = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Ding.wav");
        public SoundPlayer success = new SoundPlayer(@"e:\Âm thanh thông báo\tada.wav");
        public SoundPlayer start = new SoundPlayer(@"e:\Âm thanh thông báo\Ring06.wav");

        SqlCommand cm;
        public static string UsertName = "";

        public frmQuanlyquatrinhhocsinhvien()
        {
            InitializeComponent();
        }

        private void frmQuanlyquatrinhhocsinhvien_Load(object sender, EventArgs e)
        {
            txtmssv.Text = frmQuanlysinhvien.ma;
            Loadcbmamon();
            hienthi();
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = @"select distinct MSSV, TENMH as 'Tên MH', DIEMCC, DIEMGK, DIEMCK, DIEMHE10, DIEMHE4, DIEMCHU as 'Điểm chữ', LANHOC as 'Lần học' from COVAN,CANBO,LOP,NGANH,CHUYENNGANH,MONHOC,HOC 
            where CANBO.MACB=COVAN.MACB and COVAN.MALOP = LOP.MALOP and LOP.MANGANH = NGANH.MANGANH and NGANH.MANGANH=CHUYENNGANH.MANGANH 
            and CHUYENNGANH.MACN = MONHOC.MACN and MONHOC.MAMH = HOC.MAMH and CANBO.MACB='" + frmForm1.UsertName + "' AND  HOC.MAMH=MONHOC.MAMH AND MSSV='" + txtmssv.Text + "' and  chuyennganh.macn='" + frmQuanlysinhvien.cn + "'";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }

        public void Loadcbmamon()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand(@"select distinct MONHOC.MAMH, TENMH from COVAN, CANBO, LOP, NGANH, CHUYENNGANH, MONHOC
            where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and LOP.MANGANH = NGANH.MANGANH and NGANH.MANGANH = CHUYENNGANH.MANGANH
            and CHUYENNGANH.MACN = MONHOC.MACN and CANBO.MACB = '" + frmForm1.UsertName + "' and  chuyennganh.macn='"+frmQuanlysinhvien.cn+"'", cn.con);

            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmamh.DataSource = ds.Tables[0];
            cbmamh.ValueMember = "MAMH";
            cbmamh.DisplayMember = "TENMH";
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

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                txtmssv.Text = row.Cells[0].Value.ToString();
                cbmamh.Text = row.Cells[1].Value.ToString();
                txtdiemcc.Text = row.Cells[2].Value.ToString();
                txtdiemgk.Text = row.Cells[3].Value.ToString();
                txtdiemck.Text = row.Cells[4].Value.ToString();
                txthe10.Text = row.Cells[5].Value.ToString();
                txthe4.Text = row.Cells[6].Value.ToString();
                txtchu.Text = row.Cells[7].Value.ToString();
                txtlanhoc.Text = row.Cells[8].Value.ToString();
            }
        }       

        private void btnTinh_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            try
            {
                //if (CheckSo(txtdiemcc.Text) == false)
                //{
                //    errorProvider1.SetError(txtdiemcc, "Chỉ được nhập số!");
                //    return;
                //}
                //else if (CheckSo(txtdiemcc.Text) == true)
                //{
                //    errorProvider1.Clear();
                //}
                //if (CheckSo(txtdiemgk.Text) == false)
                //{
                //    errorProvider1.SetError(txtdiemgk, "Chỉ được nhập số!");
                //    return;
                //}
                //else if (CheckSo(txtdiemgk.Text) == true)
                //{
                //    errorProvider1.Clear();
                //}                
                //if (CheckSo(txtdiemck.Text) == false)
                //{
                //    errorProvider1.SetError(txtdiemck, "Chỉ được nhập số!");
                //    return;
                //}
                //else if (CheckSo(txtdiemck.Text) == false)
                //{
                //    errorProvider1.Clear();
                //}
                /////////////////////////////////
                if (txtdiemcc.Text == "")
                {
                    errorProvider1.SetError(txtdiemcc, "Chưa nhập điểm chuyên cần!");
                    return;
                }
                if ( txtdiemgk.Text == "")
                {
                    errorProvider1.SetError(txtdiemgk, "Chưa nhập điểm giữa kỳ!");
                    return;
                }
                if (txtdiemck.Text == "")
                {
                    errorProvider1.SetError(txtdiemck, "Chưa nhập điểm cuối kỳ!");
                    return;
                }
                else
                {
                    //TINH DIEM TB
                    float cc = float.Parse(txtdiemcc.Text);
                    float gk = float.Parse(txtdiemgk.Text);
                    float ck = float.Parse(txtdiemck.Text);

                    double he10 = (cc + gk + ck) / 3;
                    he10 = Math.Round(he10, 1);
                    txthe10.Text = he10.ToString();

                    // QUY DOI DIEM HE 4
                    if (he10 >= 8.5)
                    {
                        txthe4.Text = "4";
                    }
                    else if (he10 >= 7)
                    {
                        txthe4.Text = "3";
                    }
                    else if (he10 >= 5.5)
                    {
                        txthe4.Text = "2";
                    }
                    else if (he10 >= 4)
                    {
                        txthe4.Text = "1";
                    }
                    else
                        txthe4.Text = "0";

                    //QUY DOI DIEM HE 10
                    if (he10 >= 8.5)
                    {
                        txtchu.Text = "A";
                    }
                    else if (he10 <= 8.4 && he10 >= 7.8 )
                    {
                        txtchu.Text = "B+";
                    }
                    else if (he10 <= 7.7 && he10 >= 7)
                    {
                        txtchu.Text = "B";
                    }
                    else if (he10 <= 6.9 && he10 >= 6.3)
                    {
                        txtchu.Text = "C+";
                    }
                    else if (he10 <= 6.2 && he10 >= 5.5)
                    {
                        txtchu.Text = "C";
                    }
                    else if (he10 <= 5.4 && he10 >= 4.8)
                    {
                        txtchu.Text = "D+";
                    }
                    else if (he10 <= 4.7 && he10 >= 4)
                    {
                        txtchu.Text = "D";
                    }
                    else
                        txtchu.Text = "F";
                }
                errorProvider1.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" +ex);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string mssv = txtmssv.Text;
            string mamon = cbmamh.SelectedValue.ToString();

            string cc = txtdiemcc.Text;
            string gk = txtdiemgk.Text;
            string ck = txtdiemck.Text;
            string he10 = txthe10.Text;

            //float cc = float.Parse(txtdiemcc.Text);
            //float gk = float.Parse(txtdiemgk.Text);
            //float ck = float.Parse(txtdiemck.Text);
            //float he10 = float.Parse(txthe10.Text);

            //double cc = Convert.ToDouble(txtdiemcc.Text);
            //double gk = Convert.ToDouble(txtdiemgk.Text);
            //double ck = Convert.ToDouble(txtdiemck.Text);
            //double he10 = Convert.ToDouble(txthe10.Text);

            string he4 = txthe4.Text;
            string chu = txtchu.Text;
            string lanhoc = txtlanhoc.Text;

            cm = new SqlCommand("select MSSV from SINHVIEN where MSSV='" + mssv + "'", cn.con);
            string ma1 = cm.ExecuteScalar() as string;
            if (mssv != ma1)
            {
                errorProvider1.SetError(txtmssv, "Sinh viên này không tồn tại!");
                return;
            }

            cm = new SqlCommand("select MAMH from HOC where MAMH='" + mamon + "' and MSSV='" + mssv + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (mamon == ma)
            {
                errorProvider1.SetError(cbmamh, "Trùng môn học, thêm thất bại!");
                return;
            }

            if(txtlanhoc.Value == 0)
            {
                errorProvider1.SetError(txtlanhoc, "Lần học phải > hơn 0!");
                return;
            }

            if (cc == "" || gk == "" || ck == "" || he10 == "" || he4 == "" || chu == "" || lanhoc == "")
            {
                warning.Play();
                lbChuy.Text = "Thông tin không được bỏ trống!!!";
            }
            else
            {
                string sqlthem = "insert into HOC values('" + mssv + "',N'" + mamon + "',N'" + cc + "','" + gk + "','" + ck + "',N'" + he10 + "',N'" + he4 + "','" + chu + "','" + lanhoc + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy.Text = "";
                }
                catch
                {
                    error.Play();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            hienthi();
            errorProvider1.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string mssv = txtmssv.Text;
            string mamon = cbmamh.SelectedValue.ToString();
            string cc = txtdiemcc.Text;
            string gk = txtdiemgk.Text;
            string ck = txtdiemck.Text;
            string he10 = txthe10.Text;
            string he4 = txthe4.Text;
            string chu = txtchu.Text;
            string lanhoc = txtlanhoc.Text;

            cm = new SqlCommand("select MAMH from HOC where MAMH='" + mamon + "' and MSSV='" + mssv + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (mamon != ma)
            {
                errorProvider1.SetError(cbmamh, "Không trùng môn học, sửa thất bại!");
                return;
            }

            if (txtlanhoc.Value == 0)
            {
                errorProvider1.SetError(txtlanhoc, "Lần học phải > hơn 0!");
                return;
            }

            if (cc == "" || gk == "" || ck == "" || he10 == "" || he4 == "" || chu == "" || lanhoc == "")
            {
                warning.Play();
                lbChuy.Text = "Thông tin không được bỏ trống!!!";
            }
            else

            ////cm = new SqlCommand("select DIEMCC,DIEMGK,DIEMCK from HOC where DIEMCC='" + cc + "' and DIEMGK='" + gk + "' and DIEMCK='" + ck + "' ", cn.con);
            ////string ma = cm.ExecuteScalar() as string;
            ////if (cc != ma || gk != ma || ck != ma)
            ////{
            ////    btnTinh_Click(sender, e);
            ////}
            //else

            //cm = new SqlCommand("select MSSV from HOC where MSSV='" + mssv + "'", cn.con);
            //string ma1 = cm.ExecuteScalar() as string;
            //if (mamon == ma1)
            {
                    DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (DialogResult.OK == dlr)
                    {
                        string sqlsua = "update HOC set  MAMH='" + mamon + "',DIEMCC='" + cc + "',DIEMGK='" + gk + "',DIEMCK='" + ck + "',DIEMHE10 = '" + he10 + "',DIEMHE4 = '" + he4 + "',DIEMCHU = '" + chu + "',LANHOC = '" + lanhoc + "' where  MSSV='" + mssv + "' and MAMH='" + mamon + "'  ";
                        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thành công");
                            lbChuy.Text = "";
                        }
                        catch
                        {
                            error.Play();
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thất bại!");
                        }
                    }
             }
             //else
             //   MessageBox.Show("Đã xảy ra lỗi!");
                hienthi();
            errorProvider1.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string mssv = txtmssv.Text;
            string mamon = cbmamh.SelectedValue.ToString();
            string cc = txtdiemcc.Text;
            string gk = txtdiemgk.Text;
            string ck = txtdiemck.Text;
            string he10 = txthe10.Text;
            string he4 = txthe4.Text;
            string chu = txtchu.Text;
            string lanhoc = txtlanhoc.Text;

            if (cc == "" || gk == "" || ck == "" || he10 == "" || he4 == "" || chu == "" || lanhoc == "")
            {
                warning.Play();
                lbChuy.Text = "Vui lòng chọn thông tin cần xóa!!!";
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete HOC where MSSV='" + mssv + "' and MAMH='" + mamon + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        lbChuy.Text = "";
                        txtdiemcc.Clear();
                        txtdiemgk.Clear();
                        txtdiemck.Clear();
                        txthe10.Clear();
                        txthe4.Clear();
                        txtchu.Clear();
                        //txtlanhoc.Clear();
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        error.Play();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
                hienthi();
            }

        private void txtdiemcc_MouseClick(object sender, MouseEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
        }

        private void txtdiemgk_MouseClick(object sender, MouseEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
        }

        private void txtdiemck_MouseClick(object sender, MouseEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
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
            dgvthongtin.DataSource = hienthiTK("select * from HOC where MSSV like '%" + txtTK.Text.Trim() + "%' OR TENMH like '%" + txtTK.Text.Trim() + "%'");
        }

        private void txtdiemcc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtdiemcc.Text[txtdiemcc.Text.Length - 1] == ',')
                {
                    errorProvider1.SetError(txtdiemcc, "Không sử dụng dấu phẩy trong trường hợp này!");
                    return;
                }
                else if (txtdiemcc.Text[txtdiemcc.Text.Length - 1] == '.')
                {
                    errorProvider1.Clear();
                }
            }
            catch(Exception ex)
            {
               // MessageBox.Show(""+ex);
            }
        }

        private void txtdiemgk_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtdiemgk.Text[txtdiemgk.Text.Length - 1] == ',')
                {
                    errorProvider1.SetError(txtdiemgk, "Không sử dụng dấu phẩy trong trường hợp này!");
                    return;
                }
                else if (txtdiemgk.Text[txtdiemgk.Text.Length - 1] == '.')
                {
                    errorProvider1.Clear();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void txtdiemck_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtdiemck.Text[txtdiemck.Text.Length - 1] == ',')
                {
                    errorProvider1.SetError(txtdiemck, "Không sử dụng dấu phẩy trong trường hợp này!");
                    return;
                }
                else if (txtdiemck.Text[txtdiemck.Text.Length - 1] == '.')
                {
                    errorProvider1.Clear();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnmofile_Click(object sender, EventArgs e)
        {

        }
    }
    }

