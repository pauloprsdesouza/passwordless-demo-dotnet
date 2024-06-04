using System.ComponentModel.DataAnnotations;

namespace PasswordlessDemo.Contracts.Users
{
    public class SignInRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
