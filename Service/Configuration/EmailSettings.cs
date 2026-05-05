namespace Service.Configuration
{
    public class EmailSettings
    {
        public const string SectionName = "EmailSettings";

        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 587;
        public bool UseSsl { get; set; } = true;
        public string SmtpUserName { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;
        public string FromEmail { get; set; } = "noreply@localhost";
        public string FromName { get; set; } = "Library";
        public bool SendRealEmail { get; set; }
        public bool SendConfirmationOnRegister { get; set; } = true;
        public bool RequireConfirmationForLogin { get; set; } = true;
    }
}
