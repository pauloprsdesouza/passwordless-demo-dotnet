using System;

namespace PasswordlessDemo.Domain.Base
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
