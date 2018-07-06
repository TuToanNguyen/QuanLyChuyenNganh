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
    public partial class frmCovanlophocphan : Form
    {
        SqlCommand cm;
        //public static string UsertName = "";

        public frmCovanlophocphan()
        {
            InitializeComponent();

            guiMail();
        }

        public void guiMail()
        {

            Connection cn = new Connection();
            cn.OpenConn();

            string query = @"select CANBO.MACB, HOTEN, EMAIL, THOIGIANBD, THOIGIANKT, MALOP from COVAN,CANBO 
                                where COVAN.MACB = CANBO.MACB and THOIGIANKT < GETDATE() AND DAGOIMAIL = 0";
            SqlCommand cmd = new SqlCommand(query, cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            // Gửi mail
            Email email = new Email();
            foreach (DataRow dr in dt.Rows)
            {
                string b = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
  <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
  <meta name='viewport' content='width=320, initial-scale=1' />
  <title>Airmail Invoice</title>
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

                <table cellspacing='0' cellpadding='0' width='100%'>
                  <tr>
                    <td class='left' style='padding-bottom:40px; text-align:left;'>
                    Hi {0},<br>
                    Lớp học phần của bạn phụ trách đã kết thúc:
                    </td>
                  </tr>
                </table>

                <table border='1' cellspacing='1' cellpadding='0' width='100%'>
                  <tr>
                    <td>
                      <b>Mã lớp</b>
                    </td>
                    <td>
                      <b>Thời gian</b>
                    </td>
                    <td>
                      <b>Trạng thái</b>
                    </td>
                  </tr>
                  <tr>
                    <td class='border-bottom' height='5'></td>
                    <td class='border-bottom' height='5'></td>
                    <td class='border-bottom' height='5'></td>
                  </tr>
                  <tr>
                    <td style='padding-top:5px;'>
                      {1}
                    </td>
                    <td style='padding-top:5px;'>
                      {2} - {3}
                    </td>
                    <td style='padding-top:5px;' class='mobile'>
                      Kết thúc
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
                b = b.Replace("{0}", dr["HOTEN"].ToString());
                b = b.Replace("{1}", dr["MALOP"].ToString());
                b = b.Replace("{2}", dr["THOIGIANBD"].ToString());
                b = b.Replace("{3}", dr["THOIGIANKT"].ToString());


                email.GuiEmail(dr["EMAIL"].ToString(), "Lớp học phần hết hạn", b);

                // Cập nhật trang thái
                query = String.Format(@"UPDATE COVAN set DAGOIMAIL = 1 WHERE MACB = '{0}'", dr["MACB"].ToString());
                cmd = new SqlCommand(query, cn.con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.CloseConn();

                //MessageBox.Show("Đã gởi email thông báo");
            }

        }

        private void Covanlophocphan_Load(object sender, EventArgs e)
        {
            txtmalop.Text = frmQuanlythongtinlophoc.malop;
            hienthi();
            LoadcbmaCB();
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            //CANBO.MACB = '" + frmForm1.UsertName + "'
            string sqlht = @"select HOTEN as 'Tên CB', MALOP as 'Mã lớp', THOIGIANBD as 'TG Bắt đầu', THOIGIANKT as 'TG Kết thúc', ID from COVAN,CANBO where COVAN.MACB = CANBO.MACB ";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }

        public void LoadcbmaCB()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand(@"select DISTINCT MACB, HOTEN from CANBO where QUYENHAN = 'covan' ", cn.con);
            //cm = new SqlCommand(@"select  MACB from CANBO where QUYENHAN = 'covan' ", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmacb.DataSource = ds.Tables[0];
            cbmacb.ValueMember = "MACB";
            cbmacb.DisplayMember = "HOTEN";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();

            string macb = cbmacb.SelectedValue.ToString();
            string malop = txtmalop.Text;
            string tgbd = dptgbd.Value.ToString("yyyy-MM-dd");
            //string tgkt = "";

            DateTime bd = Convert.ToDateTime(dptgbd.Text);
            DateTime Ngt = DateTime.Today;
            TimeSpan day = Ngt.Subtract(bd);

            string kq = day.Days.ToString();
            //MessageBox.Show(kq.ToString());

            if ( kq != "0")
            {
                errorProvider1.SetError(dptgbd, "Ngày bắt đầu phải là ngày hiện tại!");
                return;
            }

            if (KiemTraCVHT(malop) > 0)
            {
                MessageBox.Show("Mã lớp " + malop + " đã có giáo viên cố vấn");
            }

            else
            {
                string sqlthem = "insert into COVAN values('" + macb + "','" + malop + "','" + tgbd + "',NULL,0)";
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
            errorProvider1.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();

            string macb = cbmacb.SelectedValue.ToString();
            string malop = txtmalop.Text;
            string tgbd = dptgbd.Value.ToString("yyyy-MM-dd");
            string tgkt = dptgkt.Value.ToString("yyyy-MM-dd");

            DateTime bd = Convert.ToDateTime(dptgbd.Value.ToString());
            DateTime kt = Convert.ToDateTime(dptgkt.Value.ToString());
            int b = int.Parse(bd.Year.ToString());
            int k = int.Parse(kt.Year.ToString());
            //int nam = k - b;

            if(k < b)
            {
                errorProvider1.SetError(dptgkt, "Năm kết thúc phải sau năm bắt đầu!");
                return;
            }

            //cm = new SqlCommand("select MALOP from COVAN where MALOP='" + malop + "'", cn.con);
            //string ma1 = cm.ExecuteScalar() as string;
            //if (malop == ma1)
            //{
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update COVAN set MACB='" + macb + "',MALOP='" + malop + "',THOIGIANBD='" + 
                    tgbd + "',THOIGIANKT='" + tgkt + "' where ID=" + dgvthongtin.CurrentRow.Cells["ID"].Value.ToString();
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
            //}
            //else
            //    MessageBox.Show("Lỗi!");
            hienthi();
            errorProvider1.Clear();
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                    cbmacb.Text = row.Cells[0].Value.ToString();
                    txtmalop.Text = row.Cells[1].Value.ToString();
                    dptgbd.Text = row.Cells[2].Value.ToString();
                    dptgkt.Text = row.Cells[3].Value.ToString();
                }
            }
            catch(Exception)
            { }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string macb = cbmacb.SelectedValue.ToString();
            string malop = txtmalop.Text;
            Connection cn = new Connection();
            cn.OpenConn();
            if (malop == "")
            {
                MessageBox.Show("Thông tin cần xóa hiện không tồn tại! ");
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete COVAN where ID=" + dgvthongtin.CurrentRow.Cells["ID"].Value.ToString();
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmalop.Clear();
                        dptgbd.Value = DateTime.Today;
                        dptgkt.Value = DateTime.Today;
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
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
            dgvthongtin.DataSource = hienthiTK("select * from COVAN where MACB like '%" + txtTK.Text.Trim() + "%' OR MALOP like '%" + txtTK.Text.Trim() + "%'");
        }

        private void dgvthongtin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public int KiemTraCVHT(string maLop)
        {
            String sqlht = @"SELECT * FROM COVAN WHERE MALOP = '" + maLop + "'AND GETDATE() BETWEEN THOIGIANBD AND THOIGIANKT UNION ALL SELECT* FROM COVAN WHERE THOIGIANKT iS NULL AND MALOP = '" + maLop + "'";
            Connection cn = new Connection();
            cn.OpenConn();
            SqlDataAdapter da = new SqlDataAdapter(sqlht, cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count;
            cn.CloseConn();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
