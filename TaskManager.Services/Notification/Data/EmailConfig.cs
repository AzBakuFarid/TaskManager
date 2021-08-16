using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.Notification.Data
{
    public class EmailConfig
    {
        public string Sender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public Dictionary<string, EmailMessage> Messages { get; set; }

    }
    public class EmailMessage {
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
