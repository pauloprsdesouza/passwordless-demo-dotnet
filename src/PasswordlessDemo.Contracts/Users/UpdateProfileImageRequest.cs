using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PasswordlessDemo.Contracts.Users
{
    public class UpdateProfileImageRequest
    {
        [Required]
        public IFormFile ProfileImage { get; set; }
    }
}
