﻿namespace Common.Services
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Common.Constants;
    using Common.Interfaces;
    using GrowATree.Domain.Entities;
    using Serilog;

    /// <summary>
    /// Class that process email sending.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public async Task<bool> SendEmail(User receiver, string mailDescription, string subject)
        {
            try
            {
                Log.Logger.Error("Email Sender user receiver: " + receiver.Email);

                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(Settings.GmailServerName);

                Log.Logger.Error("Email Sender our email: " + Settings.GmailEmail);

                mail.From = new MailAddress(Settings.GmailEmail);
                mail.To.Add(receiver.Email);
                mail.Subject = subject;
                mail.Body = mailDescription;

                smtpServer.Port = Settings.SendGridPortNotSecured;
                smtpServer.Credentials = new NetworkCredential(Settings.GmailEmail, Settings.GmailPassword);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Log.Logger.Error("Email Sender Catch " + ex.Message);
                return false;
            }
        }
    }
}