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
using System.Media;


namespace QUANLYCHUYENNGANH
{
    public partial class frmQuan_tri_nguoi_dung : Form
    {
        SqlCommand cm;
        SqlCommand cm1;
        public SoundPlayer error = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Error.wav");

        public frmQuan_tri_nguoi_dung()
        {
            InitializeComponent();           
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = @"select distinct MACB  as 'Mã Cán Bộ',TENKHOA as 'Tên Khoa',HOTEN as 'Họ Tên',NGAYSINH as 'Ngày Sinh',
            GIOITINH as 'Giới Tính',CHUCVU as 'Chức vụ',CANBO.EMAIL as 'Email',MATKHAU as 'Mật Khẩu',QUYENHAN as 'Quyền Hạn' 
            from CANBO,KHOA where CANBO.MAKHOA = KHOA.MAKHOA and QUYENHAN!='admin'";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        //public DataTable danhsachCB()
        //{
        //    Connection cn = new Connection();
        //    SqlDataAdapter da = new SqlDataAdapter("select MACB  as 'Mã Cán Bộ',MAKHOA as 'Mã Khoa',HOTEN as 'Họ Tên',NGAYSINH as 'Ngày Sinh',GIOITINH as 'Giới Tính',CHUCVU as 'Chức vụ',EMAIL as 'Email',MATKHAU as 'Mật Khẩu',QUYENHAN as 'Quyền Hạn' from CANBO", cn.con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}
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
        //public void Reset()
        //{
        //    txtmacb.Clear();
        //    cbmakhoa.Text = "";
        //    txthoten.Clear();
        //    dpngaysinh.Value = DateTime.Today;
        //    radioNam.Checked = true;
        //    txtchucvu.Clear();
        //    txtemail.Clear();
        //    txtmk.Clear();
        //    cbquyen.Text = "";
        //}        

        private void frmQuan_tri_nguoi_dung_Load(object sender, EventArgs e)
        {
            hienthi();
            Loadcbmakkhoa();
            //dgvthongtin.DataSource = danhsachCB();
            radioNam.Checked = true;
            cbquyen.Text = "covan";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
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
                error.Play();
                errorProvider1.SetError(txtmacb, "Trùng mã cán bộ, thêm thất bại");
                return;
            }
            if (!Regex.IsMatch(macb, @"^CB\d{4}$"))
            {
                error.Play();

                errorProvider1.SetError(txtmacb, "Mã bắt buộc phải có 2 ký tự CB và có 4 chữ số! Không chứa khoảng trắng!");
                return;
            }
            ////KIEM TRA MA KHOA
            if (makhoa == " ")
            {
                error.Play();

                errorProvider1.SetError(cbmakhoa, "Chưa chọn mã khoa!");
                return;
            }
            //KIEM TRA HO TEN
            if (hoten.IndexOf(" ") < 0)
            {
                error.Play();

                errorProvider1.SetError(txthoten, "Tên gồm 2 từ trở lên!");
                return;
            }
            //KIEM TRA NGAY SINH 
            if (tuoi < 18)
            {
                error.Play();

                errorProvider1.SetError(dpngaysinh, "Đọc giả phải đủ 18 tuổi");
                return;
            }
            else
            if (tuoi > 100)
            {
                error.Play();

                errorProvider1.SetError(dpngaysinh, "Đọc giả phải nhỏ hơn 100 tuổi");
                return;
            }
            //KIEM TRA EMAIL 
            if (email.IndexOf(" ") >= 0)
            {
                error.Play();

                errorProvider1.SetError(txtemail, "Email không chứa khoảng trắng!");
                return;
            }
            //KIEM TRA MAT KHAU


            //KIEM TRA QUYEN HAN
            //cm = new SqlCommand("select Count(QUYENHAN)  from CANBO where QUYENHAN= 'admin'  ", cn.con);
            //int qh = (int)cm.ExecuteScalar();
            //if (qh >= 1)
            //{
            //    errorProvider1.SetError(cbquyen, "Chỉ tồn tại 1 admin! Chọn quyền hạn khác");
            //    return;
            //}
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
                //MessageBox.Show(sqlthem);
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                    lbChuy.Text = " ";
                    //frmQuan_tri_nguoi_dung_Load(sender, e);
                    //Reset();
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
                    //frmQuan_tri_nguoi_dung_Load(sender, e);
                    //Reset();
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thất bại!");
                }
            }      
            hienthi();
            //dgvthongtin.DataSource = danhsachCB();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            bool st = false;
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
            string quyen = cbquyen.Text;


            //else
            cm = new SqlCommand("select MACB from CANBO where MACB='" + macb + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (macb == ma)
            {
                //cm = new SqlCommand("select Count(QUYENHAN)  from CANBO where QUYENHAN= 'admin'  ", cn.con);
                //int qh = (int)cm.ExecuteScalar();
                //if (qh == 0)
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
                        string sqlsua = "update CANBO set MACB='" + macb + "',MAKHOA=N'" + makhoa + "',HOTEN=N'" + hoten + "',NGAYSINH='" + ngaysinh + "',GIOITINH='" + nam + "',CHUCVU=N'" + chucvu + "',EMAIL='" + email + "',MATKHAU='" + matkhau + "',QUYENHAN='" + quyen + "' where  MACB='" + macb + "'";
                        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thành công");
                            lbChuy.Text = " ";
                            //frmQuan_tri_nguoi_dung_Load(sender, e);
                            //Reset();
                            st = true;
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
                        string sqlsua = "update CANBO set MACB='" + macb + "',MAKHOA=N'" + makhoa + "',HOTEN=N'" + hoten + "',NGAYSINH='" + ngaysinh + "',GIOITINH='" + nu + "',CHUCVU=N'" + chucvu + "',EMAIL='" + email + "',MATKHAU='" + matkhau + "',QUYENHAN='" + quyen + "' where  MACB='" + macb + "' ";
                        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cn.CloseConn();
                            MessageBox.Show("Sửa thành công");
                            lbChuy.Text = " ";
                            //frmQuan_tri_nguoi_dung_Load(sender, e);
                            //Reset();
                            st = true;
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
                // dgvthongtin.DataSource = danhsachCB();
                //}
            }
            string query = "SELECT MACB, HOTEN, CHUCVU FROM CANBO WHERE MACB = '" + txtmacb.Text.Trim() + "'";



            SqlCommand cmdd = new SqlCommand(query, cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            // gui mil
            Email mail = new Email();
            foreach (DataRow dr in dt.Rows)
            {
                string a = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
  <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
  <meta name='viewport' content='width=320, initial-scale=1' />
  <title>Airmail Confirm</title>
  <style type='text/css'>

    /* ----- Client Fixes ----- */

    /* Force Outlook to provide a 'view in browser' message */
    #outlook a {
      padding: 0;
    }

    /* Force Hotmail to display emails at full width */
    .ReadMsgBody {
      width: 100%;
    }

    .ExternalClass {
      width: 100%;
    }

    /* Force Hotmail to display normal line spacing */
    .ExternalClass,
    .ExternalClass p,
    .ExternalClass span,
    .ExternalClass font,
    .ExternalClass td,
    .ExternalClass div {
      line-height: 100%;
    }


     /* Prevent WebKit and Windows mobile changing default text sizes */
    body, table, td, p, a, li, blockquote {
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
    }

    /* Remove spacing between tables in Outlook 2007 and up */
    table, td {
      mso-table-lspace: 0pt;
      mso-table-rspace: 0pt;
    }

    /* Allow smoother rendering of resized image in Internet Explorer */
    img {
      -ms-interpolation-mode: bicubic;
    }

     /* ----- Reset ----- */

    html,
    body,
    .body-wrap,
    .body-wrap-cell {
      margin: 0;
      padding: 0;
      background: #ffffff;
      font-family: Arial, Helvetica, sans-serif;
      font-size: 14px;
      color: #464646;
      text-align: left;
    }

    img {
      border: 0;
      line-height: 100%;
      outline: none;
      text-decoration: none;
    }

    table {
      border-collapse: collapse !important;
    }

    td, th {
      text-align: left;
      font-family: Arial, Helvetica, sans-serif;
      font-size: 14px;
      color: #464646;
      line-height:1.5em;
    }

    b a,
    .footer a {
      text-decoration: none;
      color: #464646;
    }

    a.blue-link {
      color: blue;
      text-decoration: underline;
    }

    /* ----- General ----- */

    td.center {
      text-align: center;
    }

    .left {
      text-align: left;
    }

    .body-padding {
      padding: 24px 40px 40px;
    }

    .border-bottom {
      border-bottom: 1px solid #D8D8D8;
    }

    table.full-width-gmail-android {
      width: 100% !important;
    }


    /* ----- Header ----- */
    .header {
      font-weight: bold;
      font-size: 16px;
      line-height: 16px;
      height: 16px;
      padding-top: 19px;
      padding-bottom: 7px;
    }

    .header a {
      color: #464646;
      text-decoration: none;
    }

    /* ----- Footer ----- */

    .footer a {
      font-size: 12px;
    }
  </style>

  <style type='text/css' media='only screen and (max-width: 650px)'>
    @media only screen and (max-width: 650px) {
      * {
        font-size: 16px !important;
      }

      table[class*='w320'] {
        width: 320px !important;
      }

      td[class='mobile-center'],
      div[class='mobile-center'] {
        text-align: center !important;
      }

      td[class*='body-padding'] {
        padding: 20px !important;
      }

      td[class='mobile'] {
        text-align: right;
        vertical-align: top;
      }
    }
  </style>

</head>
<body style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none'>
<table border='0' cellpadding='0' cellspacing='0' width='100%'>
<tr>
 <td valign='top' align='left' width='100%' style='background:repeat-x url(https://www.filepicker.io/api/file/al80sTOMSEi5bKdmCgp2) #f9f8f8;'>
 <center>

   <table class='w320 full-width-gmail-android' bgcolor='#f9f8f8' background='https://www.filepicker.io/api/file/al80sTOMSEi5bKdmCgp2' style='background-color:transparent' cellpadding='0' cellspacing='0' border='0' width='100%'>
      <tr>
        <td width='100%' height='48' valign='top'>
            <!--[if gte mso 9]>
            <v:rect xmlns:v='urn:schemas-microsoft-com:vml' fill='true' stroke='false' style='mso-width-percent:1000;height:49px;'>
              <v:fill type='tile' src='https://www.filepicker.io/api/file/al80sTOMSEi5bKdmCgp2' color='#ffffff' />
              <v:textbox inset='0,0,0,0'>
            <![endif]-->
              <table class='full-width-gmail-android' cellspacing='0' cellpadding='0' border='0' width='100%'>
                <tr>
                  <td class='header center' width='100%'>
                    <a href='#'>
                      Hệ thống quản lý lớp chuyên ngành theo học chế tín chỉ của Trường DHSPKTVL
                    </a>
                  </td>
                </tr>
              </table>
            <!--[if gte mso 9]>
              </v:textbox>
            </v:rect>
            <![endif]-->
        </td>
      </tr>
    </table>

    <table cellspacing='0' cellpadding='0' width='100%' bgcolor='#ffffff'>
      <tr>
        <td align='center'>
          <center>
            <table class='w320' cellspacing='0' cellpadding='0' width='500'>
              <tr>
                <td class='body-padding mobile-padding'>

                <table cellpadding='0' cellspacing='0' width='100%'>
                  <tr>
                    <td style='text-align:center; font-size:30px; padding-bottom:20px;'>
                      Quyền hạn của bạn đã được thay đổi!
                    </td>
                  </tr>
                  <tr>
                    <td style='padding-bottom:20px;'>
                      Xin chào {0}, <br>
                      <br>
                     Admin đã thay đổi quyền hạn của bạn thành {1}.<br>
                      <br>
                    </td>
                  </tr>
                </table>

                <table cellspacing='0' cellpadding='0' width='100%'>
                  <tr>
                    <td class='left' style='padding-bottom:20px; text-align:left;'>
                      <b>Lưu ý:</b> Thư này được gửi từ địa chỉ email chỉ để thông báo và không chấp nhận email đến. Vui lòng không trả lời thư này.!

                    </td>
                  </tr>
                </table>

           

           

                <table cellspacing='0' cellpadding='0' width='100%'>
                  <tr>
                    <td class='left' style='text-align:left;'>
                      Thanks so much,
                    </td>
                  </tr>
                  <tr>
                    <td class='left' width='129' height='20' style='padding-top:10px; text-align:left;'>
                      <img class='left' width='129' height='20' src='https://www.filepicker.io/api/file/2R9HpqboTPaB4NyF35xt' alt='Company Name'>
                    </td>
                  </tr>
                </table>

                </td>
              </tr>
            </table>
          </center>
        </td>
      </tr>
    </table>

    <table class='w320' bgcolor='#E5E5E5' cellpadding='0' cellspacing='0' border='0' width='100%'>
      <tr>
        <td style='border-top:1px solid #B3B3B3;' align='center'>
          <center>
            <table class='w320' cellspacing='0' cellpadding='0' width='500' bgcolor='#E5E5E5'>
              <tr>
                <td>
                  <table cellpadding='0' cellspacing='0' width='100%' bgcolor='#E5E5E5'>
                    <tr>
                      <td class='center' style='padding:25px; text-align:center;'>
                        <b><a href='#'>Get in touch</a></b> if you have any questions or feedback
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </center>
        </td>
      </tr>
      <tr>
        <td style='border-top:1px solid #B3B3B3; border-bottom:1px solid #B3B3B3;' align='center'>
          <center>
            <table class='w320' cellspacing='0' cellpadding='0' width='500' bgcolor='#E5E5E5'>
              <tr>
                <td align='center' style='padding:25px; text-align:center'>
                  <table cellspacing='0' cellpadding='0' width='100%' bgcolor='#E5E5E5'>
                    <tr>
                      <td class='center footer' style='font-size:12px;'>
                        <a href='#'>Contact Us</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                        <span class='footer-group'>
                          <a href='#'>Facebook</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                          <a href='#'>Twitter</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                          <a href='#'>Support</a>
                        </span>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </center>
        </td>
      </tr>
    </table>

  </center>
  </td>
</tr>
</table>
</body>
</html>";
                a = a.Replace("{0}", dr["HOTEN"].ToString());
                a = a.Replace("{1}", dr["CHUCVU"].ToString());

                mail.GuiEmail(txtemail.Text, "Cập nhật quyền hạn", a);
                MessageBox.Show("Đã gửi mail thông báo");
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
                //dgvthongtin.DataSource = danhsachCB();
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
            dgvthongtin.DataSource = hienthiTK("select * from CANBO where MACB like '%" + txtTK.Text.Trim() + "%' OR HOTEN like '%" + txtTK.Text.Trim() + "%'");
        }

        private void frmQuan_tri_nguoi_dung_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Connection cn = new Connection();
            //cn.OpenConn();
            //cm = new SqlCommand("select Count(QUYENHAN)  from CANBO where QUYENHAN= 'admin'  ", cn.con);
            //int qh = (int)cm.ExecuteScalar();
            //if(qh == 0 )
            //{
            //    e.Cancel = true;
            //    MessageBox.Show("Vui lòng thêm ít nhất 1 admin để duy trì hệ thống! Thank you!");
               
            //}
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
