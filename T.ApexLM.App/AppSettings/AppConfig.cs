namespace T.ApexLM.App.AppSettings
{
    public class AppConfig
    {
        public string? BaseAddress { get; set; }
        public List<string> AuthorizedUrls { get; set; } = new List<string>();
        public Scopes Scopes { get; set; } = new Scopes();
        public ApplicationInsights ApplicationInsights { get; set; } = new ApplicationInsights();

    }


    public class ApplicationInsights
    {
        public string? ConnectionString { get; set; }
    }

    public class Scopes
    {
        public string? ApexLmApiScope { get; set; }
    }
}
