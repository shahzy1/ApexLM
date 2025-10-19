using Microsoft.JSInterop;

namespace T.ApexLM.App.AppSettings
{
    public interface IJsInteropAppInsightService
    {
        Task InitializeAsync(string instrumentationKey);
        Task TrackEventAsync(string eventName, Dictionary<string, object>? properties = null);
        Task TrackExceptionAsync(string message, int severityLevel = 3, Dictionary<string, object>? properties = null);
        Task TrackPageViewAsync(string name, string? uri = null, Dictionary<string, object>? properties = null);
    }

    public class JsInteropAppInsightService : IJsInteropAppInsightService
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _module;

        public JsInteropAppInsightService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync(string instrumentationKey)
        {
            _module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/appInsights.js");
            await _module.InvokeVoidAsync("initializedAppInsights", instrumentationKey);
        }

        public async Task TrackEventAsync(string eventName, Dictionary<string, object>? properties = null)
        {
            if (_module == null)
            {
                return;
            }

            await _jsRuntime.InvokeVoidAsync("appInsights.trackEvent", eventName, properties ?? new Dictionary<string, object>());
        }

        public async Task TrackExceptionAsync(string message, int severityLevel = 3, Dictionary<string, object>? properties = null)
        {
            if (_module == null)
            {
                return;
            }

            await _jsRuntime.InvokeVoidAsync("appInsights.trackException", message, severityLevel, properties ?? new Dictionary<string, object>());
        }

        public async Task TrackPageViewAsync(string name, string? uri = null, Dictionary<string, object>? properties = null)
        {
            if (_module == null)
            {
                return;
            }

            await _jsRuntime.InvokeVoidAsync("appInsights.trackPageView", name, uri, properties ?? new Dictionary<string, object>());
        }
    }
}
