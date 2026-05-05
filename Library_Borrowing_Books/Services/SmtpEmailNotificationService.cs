using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Service.Configuration;
using Service.Services;

namespace Library_Borrowing_Books.Api.Services
{
    public class SmtpEmailNotificationService : IEmailNotificationService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<SmtpEmailNotificationService> _logger;

        public SmtpEmailNotificationService(IOptions<EmailSettings> options, ILogger<SmtpEmailNotificationService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string plainTextBody, string? htmlBody = null)
        {
            if (!_settings.SendRealEmail)
            {
                _logger.LogInformation(
                    "[Email skipped — SendRealEmail=false] To={To}\nSubject={Subject}\n{Body}",
                    toEmail,
                    subject,
                    plainTextBody);
                return;
            }

            if (string.IsNullOrWhiteSpace(_settings.SmtpHost))
            {
                _logger.LogWarning("SMTP host is not configured; email not sent to {To}", toEmail);
                return;
            }

            using var message = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = subject,
                Body = htmlBody ?? plainTextBody,
                IsBodyHtml = htmlBody != null
            };
            message.To.Add(toEmail);

            if (htmlBody != null)
            {
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plainTextBody, null, "text/plain"));
            }

#pragma warning disable SYSLIB0014
            using var client = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
            {
                EnableSsl = _settings.UseSsl,
                Credentials = string.IsNullOrEmpty(_settings.SmtpUserName)
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(_settings.SmtpUserName, _settings.SmtpPassword)
            };
#pragma warning restore SYSLIB0014

            await client.SendMailAsync(message);
        }
    }
}
