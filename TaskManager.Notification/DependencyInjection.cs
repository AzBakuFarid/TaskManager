using Microsoft.Extensions.DependencyInjection;
using TaskManager.Notification.Data;
using TaskManager.Notification.Interfaces;
using TaskManager.Notification.Services;

namespace TaskManager.Notification
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterNotificationServices(this IServiceCollection services, EmailSettings settings)
        {
            services.AddTransient<IEmailNotification>(s => new EmailService(settings));

            return services;
        }
    }

}
