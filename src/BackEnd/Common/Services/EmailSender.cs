namespace Common.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using Common.Constants;
    using Common.Interfaces;
    using GrowATree.Domain.Entities;

    /// <summary>
    /// Class that process email sending.
    /// </summary>
    public class EmailSender : IEmailSender
    {

        /// <summary>
        /// Send email to a user.
        /// </summary>
        /// <param name="receiver">User.</param>
        /// <param name="mailDescription">Content of the email</param>
        /// <param name="subject">Title of the email.</param>
        public void SendEmail(User receiver, string mailDescription, string subject)
        {
            MailMessage mailMsg = new MailMessage();

            mailMsg.To.Add(new MailAddress(receiver.Email, receiver.FirstName + " " + receiver.LastName));
            mailMsg.From = new MailAddress(Constants.GeneralEmail, subject);

            mailMsg.Subject = subject;
            string text = mailDescription;
            mailMsg.Body = text;

            SmtpClient smtpClient = new SmtpClient(Settings.EmalHost, Convert.ToInt32(587));
            string username = Settings.SendGridUsername;
            string password = Settings.SendGridPassword;
            NetworkCredential credentials = new NetworkCredential(username, password);
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }
    }
}