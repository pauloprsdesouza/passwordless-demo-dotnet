namespace PasswordlessDemo.Domain.Users
{
    public enum UserError
    {
        USER_NOT_FOUND,
        INVALID_CREDENTIALS,
        THIS_USER_ALREADY_EXISTS,
        NEED_CHANGE_PASSWORD
    }
}
