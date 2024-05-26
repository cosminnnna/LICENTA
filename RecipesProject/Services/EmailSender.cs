using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;



namespace RecipesProject.Services
{

    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
/*        public class SmtpSettings
        {
            public SmtpConfig Gmail { get; set; }
            public SmtpConfig Yahoo { get; set; }
        }

        public class SmtpConfig
        {
            public string Server { get; set; }
            public int Port { get; set; }
            public string User { get; set; }
            public string Pass { get; set; }
        }*/
        public EmailSender(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email address cannot be null or empty.", nameof(email));
            }

            try
            {
                var client = new SmtpClient(_smtpServer, _smtpPort)
                {
                    Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUser),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);  // Verifică dacă `email` este valid aici

                await client.SendMailAsync(mailMessage);
            }
            catch (FormatException fe)
            {
                // Log the exception for invalid email format
                throw new ArgumentException("Email format is invalid.", nameof(email), fe);
            }
            catch (Exception ex)
            {
                // Handle other exceptions and log accordingly
                throw new InvalidOperationException("Failed to send email.", ex);
            }
        }

    }

}

