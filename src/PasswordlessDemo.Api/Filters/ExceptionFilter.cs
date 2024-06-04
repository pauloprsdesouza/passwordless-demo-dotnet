using Aws.Services.Lib.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace PasswordlessDemo.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            string logId = Guid.NewGuid().ToString();

            var errorLog = new
            {
                Id = logId,
                context.HttpContext.Request.Path,
                ErrorType = exception.GetType().Name,
                ErrorMessage = exception.Message,
                exception.StackTrace
            };


            JsonSerializerOptions options = new JsonSerializerOptions().Default();
            string errorLogJson = JsonSerializer.Serialize(errorLog, options);

            _logger.LogError(errorLogJson);

            context.Result = new JsonResult(new { logId })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
