using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;

namespace PasswordlessDemo.Api
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            _ = builder.UseStartup<Startup>();
        }
    }
}
