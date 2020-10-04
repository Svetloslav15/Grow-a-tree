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
                MailMessage mailMsg = new MailMessage();

                mailMsg.To.Add(new MailAddress(receiver.Email, receiver.FirstName + " " + receiver.LastName));
                mailMsg.From = new MailAddress(Constants.GeneralEmail, subject);

                mailMsg.Subject = subject;
                string text = mailDescription;
                mailMsg.Body = text;

                SmtpClient smtpClient = new SmtpClient(Settings.SendGridServerName, Convert.ToInt32(Settings.SendGridPortNotSecured));
                string username = Settings.SendGridUsername;
                string password = Settings.SendGridAPIKEY;
                NetworkCredential credentials = new NetworkCredential(username, password);
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}