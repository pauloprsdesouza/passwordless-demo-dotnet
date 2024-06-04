using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace PasswordlessDemo.Domain.Users
{
    public interface IUserService
    {
        Task<User> GetProfile(string userEmail);
        Task<User> SignIn(User user, string token);
        Task<User> SignUp(User user);
        Task<User> Update(User user);
        Task<User> UploadProfileImage(Guid userId, IFormFile image);
    }
}
