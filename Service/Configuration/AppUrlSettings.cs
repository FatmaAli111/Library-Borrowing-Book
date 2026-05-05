namespace Service.Configuration
{
    public class AppUrlSettings
    {
        public const string SectionName = "AppUrls";

        public string PublicApiUrl { get; set; } = "http://localhost:5113";
        public string ClientAppUrl { get; set; } = "http://localhost:4200";
    }
}
