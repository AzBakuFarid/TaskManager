using System;

namespace TaskManager.Notification.Exceptions
{
    public class NotificationServiceException : Exception
    {
        public NotificationServiceException() { }
        public NotificationServiceException(string message) : base(message) { }
    }

}
