using System.ComponentModel.DataAnnotations;

namespace PasswordlessDemo.Contracts.Users
{
    public class SignUpRequest
    {
        [Required, MaxLength(200)]
        public string FirstName { get; set; }

        [Required, MaxLength(200)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone, MaxLength(15)]
        public string Phone { get; set; }
    }
}
