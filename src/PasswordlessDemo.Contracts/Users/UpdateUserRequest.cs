using System.ComponentModel.DataAnnotations;

namespace PasswordlessDemo.Contracts.Users
{
    public class UpdateUserRequest
    {
        [Required, MaxLength(200)]
        public string FirstName { get; set; }

        [Required, MaxLength(200)]
        public string LastName { get; set; }

        [Required, MaxLength(15)]
        public string Phone { get; set; }
    }
}
