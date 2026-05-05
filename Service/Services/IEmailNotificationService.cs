namespace Service.Services
{
    public interface IEmailNotificationService
    {
        Task SendEmailAsync(string toEmail, string subject, string plainTextBody, string? htmlBody = null);
    }
}
