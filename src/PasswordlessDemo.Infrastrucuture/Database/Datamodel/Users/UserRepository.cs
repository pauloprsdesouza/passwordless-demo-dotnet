using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using PasswordlessDemo.Domain.Users;
using PasswordlessDemo.Infrastructure.Database.Datamodel.BaseModels;
using System.Threading.Tasks;

namespace PasswordlessDemo.Infrastructure.Database.Datamodel.Users
{
    public class UserRepository : BaseRepository<User, UserModel>, IUserRepository
    {
        public UserRepository(IDynamoDBContext dynamoDbClient, IMapper mapper) : base(dynamoDbClient, mapper)
        {
        }

        public Task<User> GetProfile(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
