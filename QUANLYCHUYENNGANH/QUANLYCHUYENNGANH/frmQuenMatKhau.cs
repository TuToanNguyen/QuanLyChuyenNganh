using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QUANLYCHUYENNGANH
{
    public partial class frmQuenMatKhau : Form
    {
        SqlCommand cm;
        public frmQuenMatKhau()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select MACB from CANBO where MACB='" + textBox1.Text + "' ", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (textBox1.Text != ma)
            {
                MessageBox.Show("Tên đăng nhập này không tồn tại!");
            }
            else
            {
                string query = "SELECT EMAIL, MACB, HOTEN FROM CANBO WHERE MACB = '" + textBox1.Text.Trim() + "'";

                //Connection cn = new Connection();
                cn.OpenConn();

                SqlCommand cmd = new SqlCommand(query, cn.con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                string a = CreatePassword(10);

                // Gửi mail
                Email email = new Email();
                foreach (DataRow dr in dt.Rows)
                {
                    string b = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
  <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
  <meta name='viewport' content='width=320, initial-scale=1' />
  <title>Airmail Invitation</title>
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

    /* ----- Body ----- */

    .body .body-padded {
      padding-top: 34px;
    }

    .body-thanks-cell {
      padding: 25px 0 10px;
    }

    .body-signature-cell {
      padding: 0 0 30px;
    }

    /* ----- Footer ----- */

    .footer a {
      font-size: 12px;
    }


    /* ----- Soapbox ----- */

    .soapbox .soapbox-title {
      text-align: center;
      font-size: 30px;
      padding-bottom: 20px;
      color: #464646;
    }

    /* ----- Status ----- */

    .status {
      border-collapse: collapse;
      margin-left: 15px;
      color: #656565;
    }

    .status .status-cell {
      border: 1px solid #b3b3b3;
      height: 50px;
      text-align: center;
      font-size: 15px;
      padding: 0 40px;
    }

    .status .status-cell.success,
    .status .status-cell.active {
      height: 65px;
    }

    .status .status-cell.success {
      background: #f2ffeb;
      color: #51da42;
    }

    .status .status-cell.active {
      background: #fffde0;
      width: 135px;
    }

    .status .status-title {
      font-size: 16px;
      font-weight: bold;
      line-height: 23px;
    }

    .status .status-image {
      vertical-align: text-bottom;
    }
  </style>

  <style type='text/css' media='only screen'>
    @media only screen and (max-width: 505px) {

      *[class*='w320'] {
        width: 320px !important;
      }

      table[class='status'] td[class*='status-cell'],
      table[class='status'] td[class*='status-cell'].active {
        display: block !important;
        width: auto !important;
      }

      table[class='status-container single'] table[class='status'] {
        width: 270px !important;
        margin-left: 0;
      }

      table[class='status'] td[class*='status-cell'],
      table[class='status'] td[class*='status-cell'].active,
      table[class='status'] td[class*='status-cell'] [class*='status-title'] {
        line-height: 65px !important;
        font-size: 18px !important;
        padding: 0 15px !important;
      }

      table[class='status-container single'] table[class='status'] td[class*='status-cell'],
      table[class='status-container single'] table[class='status'] td[class*='status-cell'].active,
      table[class='status-container single'] table[class='status'] td[class*='status-cell'] [class*='status-title'] {
        line-height: 51px !important;
      }

      table[class='status'] td[class*='status-cell'].active [class*='status-title'] {
        display: inline !important;
      }

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

                <table class='soapbox' cellpadding='0' cellspacing='0' width='100%'>
                  <tr>
                    <td class='soapbox-title'>
                      Vui lòng đăng nhập lại với mật khẩu mới được cấp bên dưới
                    </td>
                  </tr>
                </table>

                <table class='status-container single' cellpadding='0' cellspacing='0' width='100%'>
                  <tr>
                    <td>
                      <center>
                        <table class='status' bgcolor='#fffeea' cellspacing='0'>
                          <tr>
                            <td class='status-cell'>
                              MẬT KHẨU MỚI: <b>{1}</b>
                            </td>
                          </tr>
                        </table>
                      </center>
                    </td>
                  </tr>
                </table>



                <table class='body'>
                  <tr>
                    <td class='body-padded'>
                      <table class='body-text'>
                        <tr>
                          <td style='vertical-align:middle; padding-bottom:10px; padding-right:20px'>
                            <table cellspacing='0' cellpadding='0' width='100%'>
                              <tr>
                                <td>
                                  <table cellpadding='0' cellspacing='0'>
                                    <tr>
                                      <td width='80' height='80' style='text-align:left;border: 1px solid #888888;'>
                                        <a href=''><img style='display:block' width='80' height='80' src='https://www.filepicker.io/api/file/OqRXT4JuRbmXSgbxccgK'></a>
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                              </tr>
                              <tr>
                                <td style='font-size:12px; padding-top:5px; text-align:left;'>
                                  User: <a style='color:blue;' href=''>{0}</a>
                                </td>
                              </tr>
                            </table>
                          </td>
                          <td class='body-text-cell' width='300' style='padding-bottom:4px;'>
                            <b>Hi {0},</b><br>
                            <br>
                            Hệ thống đã tạo mật khẩu mới cho bạn, vui lòng sử dụng mật khẩu đã cấp sau đó hãy thay đổi mật khẩu bạn mong muốn!<br>
                          </td>
                        </tr>
                      </table>
                      <table class='body-text' cellspacing='0' cellpadding='0' width='100%'>
                        <tr>
                          <td class='body-text-cell' style='text-align:left; padding-bottom:30px;' >
                            Thư này được gửi từ địa chỉ email chỉ để thông báo và không chấp nhận email đến. Vui lòng không trả lời thư này.!
                          </td>
                        </tr>
                      </table>

                   
                      <table class='body-signature-block'>
                        <tr>
                          <td class='body-thanks-cell'>
                            Thanks so much,
                          </td>
                        </tr>
                        <tr>
                          <td class='body-signature-cell'>
                            <img src='https://www.filepicker.io/api/file/2R9HpqboTPaB4NyF35xt' alt='Company Name'>
                          </td>
                        </tr>
                      </table>
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

                    b = b.Replace("{1}", a);


                    email.GuiEmail(dr["EMAIL"].ToString(), "Mật khẩu mới", b);

                    // Cập nhật trang thái
                    query = String.Format(@"UPDATE CANBO set MATKHAU = '{0}' WHERE MACB = '{1}'", a, dr["MACB"].ToString());
                    cmd = new SqlCommand(query, cn.con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();

                    MessageBox.Show("Mật khẩu mới đã được cấp vui lòng check mail của bạn");
                }
            }
            
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

    }
}
