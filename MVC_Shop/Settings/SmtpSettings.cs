namespace MVC_Shop.Settings
{
    public class SmtpSettings
    {
        public int Port { get; set; } = 0;
        public string Host { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
