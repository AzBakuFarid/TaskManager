using System;
using System.Net.Mail;
using TaskManager.Notification.Data;
using TaskManager.Notification.Exceptions;
using TaskManager.Notification.Interfaces;

namespace TaskManager.Notification.Services
{
    public class EmailService : IEmailNotification
    {
        public EmailSettings Settings { get; set; }
        public EmailData Data { get; set; }

        public EmailService(EmailSettings settings)
        {
            this.Settings = settings;
        }
        public void Notify()
        {
            if (this.Data == null)
            {
                throw new NotificationServiceException($"{nameof(this.Data)} parameter is not provided");
            }

            MailAddress to = new MailAddress(Data.To);
            MailAddress from = new MailAddress(Settings.Sender);
            MailMessage message = new MailMessage(from, to)
            {
                Subject = Data.Subject,
                Body = Data.Text,
            };
            //SmtpClient client = new SmtpClient(Settings.Server, Settings.Port)
            SmtpClient client = new SmtpClient(Settings.Server)
            {
                Credentials = new System.Net.NetworkCredential(Settings.Username, Settings.Password),
                EnableSsl = true,
            };
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw new NotificationServiceException(ex.Message);
            }
            finally
            {
                client?.Dispose();
                message?.Dispose();
            }
        }
    }

}
