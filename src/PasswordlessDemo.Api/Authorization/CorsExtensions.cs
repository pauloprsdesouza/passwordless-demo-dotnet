using Microsoft.Extensions.DependencyInjection;

namespace PasswordlessDemo.Api.Authorization
{
    public static class CorsExtensions
    {
        public static void AddDefaultCorsPolicy(this IServiceCollection services)
        {
            _ = services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
    }
}
