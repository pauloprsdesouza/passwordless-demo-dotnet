using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace PasswordlessDemo.Api.Authorization
{
    public static class JwtExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.Configure<JwtOptions>(configuration);

            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration[nameof(JwtOptions.Issuer)],
                    ValidAudience = configuration[nameof(JwtOptions.Audience)],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        key: Encoding.ASCII.GetBytes(
                            configuration[nameof(JwtOptions.Secret)]
                        )
                    ),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
