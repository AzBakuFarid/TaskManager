
using TaskManager.Notification.Data;

namespace TaskManager.Notification.Interfaces
{
    public interface IEmailNotification : INotification<EmailData>
    {
        EmailSettings Settings { get; set; }
    }

}
