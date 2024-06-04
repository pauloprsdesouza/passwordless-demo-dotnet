using Microsoft.Extensions.DependencyInjection;
using PasswordlessDemo.Infrastructure.Mappers;

namespace PasswordlessDemo.Api.Dependencies
{
    public static class MapperProfileDependency
    {
        public static void AddMapperProfiles(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
