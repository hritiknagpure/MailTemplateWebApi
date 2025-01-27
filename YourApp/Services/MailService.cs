using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using _101SendEmailNotificationDoNetCoreWebAPI.Settings;
using _101SendEmailNotificationDoNetCoreWebAPI.Model;
using System.IO;
using System.Threading.Tasks;

namespace _101SendEmailNotificationDoNetCoreWebAPI.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            // Load and process the email template
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailTemplate.html");
            var emailTemplate = await File.ReadAllTextAsync(templatePath);

            // Replace placeholders in the template with actual values
            emailTemplate = emailTemplate
                .Replace("{{UserName}}", mailRequest.ToEmail) // Add user email as the name
                .Replace("{{UserMessage}}", mailRequest.Body) // Message from the request
                .Replace("{{AppName}}", "YourAppName") // Your app name
                .Replace("{{Year}}", DateTime.Now.Year.ToString()) // Current year
                .Replace("{{AppUrl}}", "https://francinaconsulting.com/") // App URL
                .Replace("{{SocialMediaUrl}}", "https://francinaconsulting.com/"); // Social media URL (e.g., GitHub)

            // Create the email
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = mailRequest.Subject
            };

            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

            // Create the email body
            var builder = new BodyBuilder { HtmlBody = emailTemplate };

            // Handle attachments
            if (mailRequest.Attachments != null)
            {
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        builder.Attachments.Add(file.FileName, ms.ToArray(), MimeKit.ContentType.Parse(file.ContentType));
                    }
                }
            }

            email.Body = builder.ToMessageBody();

            // Send the email
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
