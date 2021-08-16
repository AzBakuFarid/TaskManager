using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Notification.Data
{
    public class EmailSettings
    {
        public string Sender { get; }
        public string Username { get; }
        public string Password { get; }
        public string Server { get; }
        public int Port { get; }

        public EmailSettings(string sender, string username, string password, string server, int port)
        {
            this.Sender = sender;
            this.Username = username;
            this.Password = password;
            this.Server = server;
            this.Port = port;
        }
    }
}
