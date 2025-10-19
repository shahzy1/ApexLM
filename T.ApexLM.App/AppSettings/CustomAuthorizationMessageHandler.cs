using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;


namespace T.ApexLM.App.AppSettings
{
    //-- Assign token automatically to outbound HttpRequest
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
                                            NavigationManager navigationManager,
                                            IConfiguration configuration) :
                                                base(provider, navigationManager)
        {
            var authUrls = configuration.GetSection("AppConfig:AuthorizedUrls").Get<List<string>>() ?? [];
            var scopes = configuration.GetValue<string>("AppConfig:Scopes:UctApiScope") ?? string.Empty;

            ConfigureHandler(authorizedUrls: authUrls, scopes: [scopes]);
        }
    }
}
