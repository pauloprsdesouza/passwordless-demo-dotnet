using PasswordlessDemo.Domain.Base;

namespace PasswordlessDemo.Domain.Users
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfileImageUrl { get; set; } = null;

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
