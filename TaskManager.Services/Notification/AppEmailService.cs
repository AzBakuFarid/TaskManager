using System.Collections.Generic;
using System.Linq;
using TaskManager.Domain.Entites;
using TaskManager.Notification.Data;
using TaskManager.Notification.Interfaces;
using TaskManager.Services.Notification.Data;

namespace TaskManager.Services.Notification
{
    // normalda bu servis ucun ya message queue ya da Hangfire (sonuncunu da ele message queue kimi saymaq olar) qosuram
    // amma onsuz da kifayet qeder cox project elave elemisem....
    // hele, eslinde, serviceler projectinin ozu de bir nece yere bolunmeli idi ( en azi abstraksiyani ayirmaq lazim idi). 
    // amma, yekunda bir test tapsiriqdi... ona gore cox vaxtimi aparmasini istemedim
    // amma dediyim ki, real productionda olan proyektde bu email meselesini Hangfire uzerinden edirem
    
    public interface IAppEmailService {
        void NotifyAboutTaskAssignement(User user, Task task);
        void NotifyMultipleUsersAboutTaskAssignement(List<User> users, Task task);
    }
    public class AppEmailService : IAppEmailService
    {
        private readonly IEmailNotification _emailService;
        private readonly Dictionary<string, EmailMessage> _messages;
        public string Text { get; }

        public AppEmailService(IEmailNotification emailService, Dictionary<string, EmailMessage> messages)
        {
            this._emailService = emailService;
            _messages = messages;
        }

        public void NotifyAboutTaskAssignement(User user, Task task)
        {
            var messageTemplate = _messages["Assigne"];
            var data = new EmailData
            {
                Subject = messageTemplate.Subject,
                Text = string.Format(messageTemplate.Text, user.UserName, task.Title),
                To = user.Email,
            };

            _emailService.Data = data;

            _emailService.Notify();
        }

        public void NotifyMultipleUsersAboutTaskAssignement(List<User> users, Task task)
        {
            for (int i = 0; i < users.Count(); i++)
            {
                var user = users[i];
                NotifyAboutTaskAssignement(user, task);
            }

        }
    }

}
