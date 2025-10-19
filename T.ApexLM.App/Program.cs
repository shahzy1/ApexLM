using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Polly;
using T.ApexLM.App.AppSettings;
using T.ApexLM.App.Extension;
using T.ApexLM.Shared;
using T.ApexLM.App;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load appsettings.json
builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<AppConfig>>().Value);

var appConfig = builder.Configuration.GetSection("AppConfig").Get<AppConfig>();

string apiScopeSetting = appConfig?.Scopes?.ApexLmApiScope ?? string.Empty;
//builder.Services.AddMsalAuthentication(options =>
//{
//    builder.Configuration.Bind("AppConfig:AzureAd", options.ProviderOptions.Authentication);
//    options.ProviderOptions.DefaultAccessTokenScopes.Add(apiScopeSetting);
//    options.ProviderOptions.LoginMode = "redirect";
//});

builder.Services.AddTransient<CustomAuthorizationMessageHandler>();

string baseAddressSetting = appConfig?.BaseAddress ?? string.Empty;
builder.Services.AddHttpClient(AppConstants.Auth.ServerSideHttpClientName, (sp, client) =>
{
    client.BaseAddress = new Uri(baseAddressSetting);
    client.EnableIntercept(sp);

})
    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
    {
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(5)
    }))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient(AppConstants.Auth.ServerSideHttpClientName));

// Register the AutoMapper services.
builder.Services.AddAutoMapper(typeof(Program));

// Add application insights
builder.Services.AddScoped<IJsInteropAppInsightService, JsInteropAppInsightService>();

// Register the MudBlazor services.
builder.Services.AddMudServices();

// Register application services.
builder.Services.ConfigureHttpServices();

// Register HTTP Interceptor services.
builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInterceptorService>();

await builder.Build().RunAsync();
