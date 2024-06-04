using System.Threading.Tasks;
using PasswordlessDemo.Domain.Base;

namespace PasswordlessDemo.Domain.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetProfile(string email);
    }
}
