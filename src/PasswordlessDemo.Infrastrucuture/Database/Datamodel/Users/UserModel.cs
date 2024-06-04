using Amazon.DynamoDBv2.DataModel;
using PasswordlessDemo.Infrastructure.Database.Datamodel.BaseModels;

namespace PasswordlessDemo.Infrastructure.Database.Datamodel.Users
{
    public class UserModel : DynamoBaseModel
    {
        public UserModel()
        {
            InitializePK(Email);
            SK = $"Name#{Name}";
        }

        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public string Email { get; set; }
    }
}