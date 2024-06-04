using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace PasswordlessDemo.Api.Filters
{
    public class RequestValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> errors = context.ModelState
                    .ToDictionary(entry => entry.Key, entry => entry.Value.Errors
                        .Select(modelError => modelError.ErrorMessage)
                        .ToList());

                context.Result = new JsonResult(new { errors })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
