using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;


namespace MQSRequestData
{
    class SendEmail
    {
        public void sendEmail(string mailUser, string mailPassword, string emailTo, string emailFrom, string emailSubject, string emailBody, string smtpAddress, int port)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(emailTo);
                mail.From = new MailAddress(emailFrom);
                mail.Subject = emailSubject;
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(smtpAddress, port);
                //smtp.EnableSsl = true;               
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(mailUser, mailPassword);
                smtp.Send(mail);
                MessageBox.Show("E-mail sent successfully!");
            }
            catch (SmtpException ex)
            {
                MessageBox.Show("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception has occured: " + ex.Message);
            }
        }        
  
    }
}
