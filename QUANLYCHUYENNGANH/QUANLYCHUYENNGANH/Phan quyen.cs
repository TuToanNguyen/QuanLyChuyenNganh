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
    public partial class frmPhan_quyen : Form
    {
        xuly xl = new xuly();
        SqlCommand cm = new SqlCommand();
        Connection cn = new Connection();
       
        public frmPhan_quyen()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select distinct MACB  as 'Mã Cán Bộ',TENKHOA as 'Tên Khoa',HOTEN as 'Họ Tên',NGAYSINH as 'Ngày Sinh',GIOITINH as 'Giới Tính',CHUCVU as 'Chức vụ',CANBO.EMAIL as 'Email',QUYENHAN as 'Quyền Hạn' from CANBO,KHOA where CANBO.MAKHOA = KHOA.MAKHOA and QUYENHAN!='admin'";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        //private void hienthi()
        //{
        //    Connection cn = new Connection();
        //    cn.OpenConn();
        //    string sqlht = "select MACB,MAKHOA,HOTEN,NGAYSINH,GIOITINH,CHUCVU,EMAIL,QUYENHAN from CANBO where QUYENHAN!='admin'" ;
        //    SqlCommand cmd = new SqlCommand(sqlht, cn.con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    dgvthongtin.DataSource = dt;
        //    cn.CloseConn();
        //}
        //public DataTable danhsachCB()
        //{
        //    Connection cn = new Connection();
        //    SqlDataAdapter da = new SqlDataAdapter("select MACB  as 'Mã Cán Bộ',MAKHOA as 'Mã Khoa',HOTEN as 'Họ Tên',NGAYSINH as 'Ngày Sinh',GIOITINH as 'Giới Tính',CHUCVU as 'Chức vụ',EMAIL as 'Email',QUYENHAN as 'Quyền Hạn' from CANBO where QUYENHAN!='admin'", cn.con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}

        private void frmPhan_quyen_Load(object sender, EventArgs e)
        {
            hienthi();
            //dgvthongtin.DataSource = danhsachCB();
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                lbmacb.Text = row.Cells[0].Value.ToString();
                lbmakhoa.Text = row.Cells[1].Value.ToString();
                lbhoten.Text = row.Cells[2].Value.ToString();
                lbngsinh.Text = row.Cells[3].Value.ToString();
                lbgioitinh.Text = row.Cells[4].Value.ToString();
                lbchucvu.Text = row.Cells[5].Value.ToString();
                lbemail.Text = row.Cells[6].Value.ToString();
                lbqhan.Text = row.Cells[7].Value.ToString();
                cbquyen.Text = row.Cells[7].Value.ToString();
            }
        }

        private void btnxacnhan_Click(object sender, EventArgs e)
        {
            bool st = false;
            Connection cn = new Connection();
            cn.OpenConn();
            string mcb = lbmacb.Text;
            string quyen = cbquyen.Text;


            //KIEM TRA PHAN QUYEN 
            cm = new SqlCommand("select Count(QUYENHAN)  from CANBO where QUYENHAN= 'admin'  ", cn.con);
            int qh = (int)cm.ExecuteScalar();
            //MessageBox.Show(qh.ToString());
            ////for(int i =1; i>=3; i++)
            ////{
            //    if (qh+1 > 1 )
            //    {
            //        MessageBox.Show("Chỉ tồn tại 1 admin! Vui lòng chọn lại quyền hạn.", "THÔNG BÁO", MessageBoxButtons.OK);
            //    }
            //    //else
            //if (qh - 1 ==0)
            //{
            //    MessageBox.Show("can 1 admin .", "THÔNG BÁO", MessageBoxButtons.OK);
            //}
            ////}
            ////}
            //if(qh < 1)
            //{
            //    MessageBox.Show("phai co it nhat 1 admin");
            //}
            //if (qh > 3)
            //{
            //    MessageBox.Show("co khong qua 3 admin");
            //}

            //if (cbquyen.Text == "admin" && lbqhan.Text == "covan")
            //{
            //        MessageBox.Show("");                
            //}


            //if (cbquyen.Text == "admin" && lbqhan.Text != "admin")
            //{
            //    if (qh + 1 > 1)
            //    {
            //        MessageBox.Show("Chỉ tồn tại 1 admin! Vui lòng chọn lại quyền hạn.", "THÔNG BÁO", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //        if (DialogResult.OK == dlr)
            //        {
            //            string sqlsua = "update CANBO set QUYENHAN='" + quyen + "' where  MACB='" + mcb + "'";
            //            SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
            //            try
            //            {
            //                cmd.ExecuteNonQuery();
            //                cmd.Dispose();
            //                cn.CloseConn();
            //                MessageBox.Show("Thay đổi quyền thành công");
            //                frmPhan_quyen_Load(sender, e);
            //                hienthi();
            //                dgvthongtin.DataSource = danhsachCB();
            //            }
            //            catch
            //            {
            //                cmd.Dispose();
            //                cn.CloseConn();
            //                MessageBox.Show("Thay đổi quyền thất bại!");
            //            }
            //        }
            //    }

            //}
            ////////////////////////
            //else
            //if (cbquyen.Text == "admin" && lbqhan.Text != "admin")
            //{
            //    if (qh -1 == 0)
            //    {
            //        MessageBox.Show("phai co it nhat 1 admin ", "THÔNG BÁO", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //        if (DialogResult.OK == dlr)
            //        {
            //            string sqlsua = "update CANBO set QUYENHAN='" + quyen + "' where  MACB='" + mcb + "'";
            //            SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
            //            try
            //            {
            //                cmd.ExecuteNonQuery();
            //                cmd.Dispose();
            //                cn.CloseConn();
            //                MessageBox.Show("Thay đổi quyền thành công");
            //                frmPhan_quyen_Load(sender, e);
            //                hienthi();
            //                dgvthongtin.DataSource = danhsachCB();
            //            }
            //            catch
            //            {
            //                cmd.Dispose();
            //                cn.CloseConn();
            //                MessageBox.Show("Thay đổi quyền thất bại!");
            //            }
            //        }
            //    }

            //}     

            ////////////ngoi thuong
            //else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update CANBO set QUYENHAN='" + quyen + "' where  MACB='" + mcb + "'";
                    SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Thay đổi quyền thành công");
                        frmPhan_quyen_Load(sender, e);
                        hienthi();
                        //dgvthongtin.DataSource = danhsachCB();
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Thay đổi quyền thất bại!");
                    }
                }

            }


            //UPDATE QUYEN HAN
            //else
            //{
            //    DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //    if (DialogResult.OK == dlr)
            //    {
            //        string sqlsua = "update CANBO set QUYENHAN='" + quyen + "' where  MACB='" + mcb + "'";
            //        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
            //        try
            //        {
            //            cmd.ExecuteNonQuery();
            //            cmd.Dispose();
            //            cn.CloseConn();
            //            MessageBox.Show("Thay đổi quyền thành công");
            //            frmPhan_quyen_Load(sender, e);
            //            hienthi();
            //            dgvthongtin.DataSource = danhsachCB();
            //        }
            //        catch
            //        {
            //            cmd.Dispose();
            //            cn.CloseConn();
            //            MessageBox.Show("Thay đổi quyền thất bại!");
            //        }
            //    }
            //}
            hienthi();
            //dgvthongtin.DataSource = danhsachCB();

            string query = "SELECT MACB, HOTEN, CHUCVU FROM CANBO WHERE MACB = '" + lbmacb.Text.Trim() + "'";



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

                mail.GuiEmail(lbemail.Text, "Cập nhật quyền hạn", a);
                MessageBox.Show("Đã gửi mail thông báo");

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
