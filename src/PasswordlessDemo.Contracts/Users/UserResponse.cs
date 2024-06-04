using System;
namespace PasswordlessDemo.Contracts.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
