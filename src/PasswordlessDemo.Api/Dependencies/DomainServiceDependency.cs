using Microsoft.Extensions.DependencyInjection;
using PasswordlessDemo.Application.Users;
using PasswordlessDemo.Domain.Users;

namespace PasswordlessDemo.Api.Dependencies
{
    public static class DomainServiceDependency
    {
        public static void AddServices(this IServiceCollection services)
        {
            _ = services.AddScoped<IUserService, UserService>();
        }
    }
}
