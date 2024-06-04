namespace PasswordlessDemo.Domain.EmailTemplates
{
    public class MfaTemplateModel
    {
        public string UserName { get; set; }
        public string MfaCode { get; set; }
    }
}
