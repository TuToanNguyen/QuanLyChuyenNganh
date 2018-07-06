using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace QUANLYCHUYENNGANH
{
    public class Email
    {
        private string email;
        private string matKhau;

        public Email()
        {
            email = "14004096@student.vlute.edu.vn";
            matKhau = "toankhung96";
        }

        public string GuiEmail(string emailNguoiNhan, string tieuDe, string noiDung)
        {
            try
            {
                MailMessage mail = new MailMessage(email, emailNguoiNhan);

                mail.IsBodyHtml = true;
                mail.Body = noiDung;
                mail.Subject = tieuDe;
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential(email, matKhau);
                client.EnableSsl = true;
                client.Send(mail);
  ;

                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
