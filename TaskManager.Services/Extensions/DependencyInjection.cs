using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using TaskManager.Notification;
using TaskManager.Notification.Data;
using TaskManager.Services.HttpRequest;
using TaskManager.Services.Notification;
using TaskManager.Services.Notification.Data;
using TaskManager.Services.Organizations;
using TaskManager.Services.Tasks;
using TaskManager.Services.UiServices;
using TaskManager.Services.Users;


namespace TaskManager.Services.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddScoped<UiUserService>()
                .AddScoped<UiTaskService>()
                .AddScoped<UiOrgService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IOrganizationService, OrganizationService>()
                .AddScoped<ITaskService, TaskService>()
                .AddScoped<IHttpContextService, HttpContextService>()
                .AddTransient<IAppEmailService, AppEmailService>()
                ;

            var emailConfig = config.GetSection("Email").Get<EmailConfig>();
            var emailSettings = new EmailSettings(emailConfig.Sender, emailConfig.Username, emailConfig.Password, emailConfig.Server, emailConfig.Port);
            services.RegisterNotificationServices(emailSettings);
            services.AddSingleton(emailConfig.Messages);


            return services;
        }
    }
}
