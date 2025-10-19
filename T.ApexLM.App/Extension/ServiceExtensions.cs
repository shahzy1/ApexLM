using FluentValidation;
using T.ApexLM.App.Services;

namespace T.ApexLM.App.Extension
{
    public static class ServiceExtensions
    {
        public static void ConfigureHttpServices(this IServiceCollection services)
        {
            // Register T.ApexLM.App specific services
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<IFilterStorageService, FilterStorageService>();

            // Register T.ApexLM.Api services
            //services.AddScoped<IProjectActionService, ProjectActionService>();

        }
    }
}
