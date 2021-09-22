namespace ODP.Services
{
    public class AppSettings
    {
        public string RestBaseUrl { get; set; } = "https://api.zaius.com/";

        public string RestBaseVersion { get; set; } = "v3";

        public string RestAuthToken { get; set; }
    }
}