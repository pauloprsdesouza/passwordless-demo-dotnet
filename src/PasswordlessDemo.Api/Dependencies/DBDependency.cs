using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PasswordlessDemo.Api.Dependencies
{
    public static class DBDependency
    {
        public static void AddDbContextDependency(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddAWSService<IAmazonDynamoDB>();
            _ = services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        }
    }
}
