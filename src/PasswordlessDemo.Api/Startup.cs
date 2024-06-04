using Aws.Services.Lib;
using Aws.Services.Lib.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PasswordlessDemo.Api.Authorization;
using PasswordlessDemo.Api.Configuration;
using PasswordlessDemo.Api.Dependencies;
using PasswordlessDemo.Api.Filters;
using PasswordlessDemo.Domain.EmailConfiguration;

namespace PasswordlessDemo.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddDefaultAWSOptions(_configuration.GetAWSOptions());
            _ = services.AddControllers(options =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
                _ = options.Filters.Add(typeof(ExceptionFilter));
                _ = options.Filters.Add(typeof(RequestValidationFilter));
                _ = options.Filters.Add(typeof(NotificationFilter));
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.Default());

            services.AddDefaultCorsPolicy();
            services.AddAWSToolsLib(_configuration);
            services.AddNotifications();
            services.AddServices();
            services.AddRepositories();
            services.AddMapperProfiles();
            services.AddSwaggerDocumentation();
            services.AddDbContextDependency(_configuration);
            services.AddMemoryCache();

            services.Configure<InstitutionalEmailOptions>(_configuration.GetSection("InstitutionalEmails"));
            services.AddJwtAuthentication(_configuration.GetSection("JWTConfigurations"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerDocumentation();

            _ = app.UseRouting();

            _ = app.UseCors();
            _ = app.UseAuthentication();
            _ = app.UseAuthorization();

            _ = app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}