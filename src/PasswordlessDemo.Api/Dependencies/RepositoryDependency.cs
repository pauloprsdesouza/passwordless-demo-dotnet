using Microsoft.Extensions.DependencyInjection;
using PasswordlessDemo.Domain.Users;
using PasswordlessDemo.Infrastructure.Database.Datamodel.Users;

namespace PasswordlessDemo.Api.Dependencies
{
    public static class RepositoryDependency
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            _ = services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
