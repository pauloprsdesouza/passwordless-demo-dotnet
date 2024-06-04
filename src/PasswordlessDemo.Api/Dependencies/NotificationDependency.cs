using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PasswordlessDemo.Domain.Notifications;

namespace PasswordlessDemo.Api.Dependencies
{
    public static class NotificationDependency
    {
        public static void AddNotifications(this IServiceCollection services)
        {
            _ = services.AddScoped<INotificationContext, NotificationContext>();
        }
    }
}
