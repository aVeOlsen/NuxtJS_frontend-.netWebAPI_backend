using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace wellbeing_api.Data
{
    public class EmailSender : IEmailSender
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public EmailSender() 
        {
        }

        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this.Host = host;
            this.Port = port;
            this.EnableSSL = enableSSL;
            this.UserName = userName;
            this.Password = password;
        }


        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(Host, Port)
            {
                Credentials = new NetworkCredential(UserName, Password),
                EnableSsl = EnableSSL
            };
            return client.SendMailAsync(new MailMessage(UserName, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }
    }
}
