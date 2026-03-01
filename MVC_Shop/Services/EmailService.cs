using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MVC_Shop.Settings;
using System.Net;
using System.Net.Mail;

namespace MVC_Shop.Services
{
    public class EmailService : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _email;
        private readonly string _password;

        private readonly SmtpClient _smtpClient;

        public EmailService(IOptions<SmtpSettings> options)
        {
            try
            {
                var settings = options.Value;

                _email = settings.Email;
                _password = settings.Password;
                _host = settings.Host;
                _port = settings.Port;

                _smtpClient = new SmtpClient(_host, _port);
                _smtpClient.Credentials = new NetworkCredential(_email, _password);
                _smtpClient.EnableSsl = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\x1b[31m{ex.Message}\x1b[0m"); ;
            }

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_email);
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessage;
                mailMessage.IsBodyHtml = true;
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\x1b[31m{ex.Message}\x1b[0m"); ;
            }

        }
    }
}
