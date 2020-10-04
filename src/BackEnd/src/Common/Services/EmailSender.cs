namespace Common.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Common.Constants;
    using Common.Interfaces;
    using GrowATree.Domain.Entities;

    /// <summary>
    /// Class that process email sending.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public async Task<bool> SendEmail(User receiver, string mailDescription, string subject)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Settings.GmailServerName);

                mail.From = new MailAddress(Settings.GmailEmail);
                mail.To.Add(receiver.Email);
                mail.Subject = subject;
                mail.Body = mailDescription;

                SmtpServer.Port = Settings.SendGridPortNotSecured;
                SmtpServer.Credentials = new NetworkCredential(Settings.GmailEmail, Settings.GmailPassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}