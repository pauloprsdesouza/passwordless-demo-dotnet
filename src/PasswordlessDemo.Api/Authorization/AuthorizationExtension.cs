using System;
using System.Security.Claims;

namespace PasswordlessDemo.Api.Authorization
{
    public static class AuthorizationExtension
    {
        public static Guid GetId(this ClaimsPrincipal claim)
        {
            return Guid.Parse(claim.FindFirstValue("UserId"));
        }

        public static string GetEmail(this ClaimsPrincipal claim)
        {
            return claim.FindFirstValue("UserEmail");
        }
    }
}
