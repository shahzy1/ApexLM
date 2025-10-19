using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;
using System.Text.Json;
using Toolbelt.Blazor;

namespace T.ApexLM.App.AppSettings
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly ISnackbar _snackbar;
        private readonly NavigationManager _navigation;
        private readonly ILogger _logger;
        private int _errorCount = 0;
        private bool _eventsRegistered = false;
        private bool _disposed = false;

        public HttpInterceptorService(
            HttpClientInterceptor interceptor,
            ILogger<HttpInterceptorService> logger,
            ISnackbar snackbar,
            NavigationManager navigation)
        {
            _interceptor = interceptor;
            _logger = logger;
            _snackbar = snackbar;
            _navigation = navigation;
        }

        public void RegisterEvent()
        {
            if (_eventsRegistered) return;

            _interceptor.BeforeSendAsync += InterceptBeforeSend;
            _interceptor.AfterSendAsync += InterceptAfterSend;
            _eventsRegistered = true;
        }

        private Task InterceptBeforeSend(object sender, HttpClientInterceptorEventArgs e)
        {
            _logger.LogInformation("Intercepting request: {Method} {RequestUri}", e.Request.Method, e.Request.RequestUri);
            return Task.CompletedTask;
        }

        public int GetErrorCount() => _errorCount;
        public void ResetErrorCount() => Interlocked.Exchange(ref _errorCount, 0);

        private async Task InterceptAfterSend(object sender, HttpClientInterceptorEventArgs e)
        {
            if (e.Response is null)
            {
                throw new HttpResponseException("Server not available.");
            }

            if (e.Response?.IsSuccessStatusCode == true) return;

            try
            {
                // Clone the response to allow multiple reads
                using var clonedResponse = await CloneResponseAsync(e.Response!);

                if (IsProblemDetailsResponse(clonedResponse))
                {
                    await HandleJsonResponse(clonedResponse);
                }
                else
                {
                    HandleNonJsonResponse(clonedResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing response");
                _snackbar.Add($"Error processing response: {ex.Message}", MudBlazor.Severity.Error);
            }
        }

        private async Task<HttpResponseMessage> CloneResponseAsync(HttpResponseMessage response)
        {
            var cloned = new HttpResponseMessage(response.StatusCode)
            {
                Content = await CloneContentAsync(response.Content),
                RequestMessage = response.RequestMessage,
                ReasonPhrase = response.ReasonPhrase
            };

            foreach (var header in response.Headers)
            {
                cloned.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return cloned;
        }

        private async Task<HttpContent> CloneContentAsync(HttpContent content)
        {
            if (content == null) return null!;

            var ms = new MemoryStream();
            await content.CopyToAsync(ms);
            ms.Position = 0;

            var cloned = new StreamContent(ms);
            foreach (var header in content.Headers)
            {
                cloned.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return cloned;
        }

        private bool IsProblemDetailsResponse(HttpResponseMessage response)
        {
            return response.Content?.Headers?.ContentType?.MediaType?.Contains("application/problem+json") == true;
        }

        private async Task HandleJsonResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            Interlocked.Increment(ref _errorCount);

            try
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (problemDetails != null)
                {
                    await HandleProblemDetails(problemDetails, response.StatusCode);
                    return;
                }
            }
            catch { /* Not a ProblemDetails response */ }

            _logger.LogError("Request failed: {StatusCode}", response.StatusCode);
            _snackbar.Add($"Request failed: {response.StatusCode}", MudBlazor.Severity.Error);
        }

        private void HandleNonJsonResponse(HttpResponseMessage response)
        {
            Interlocked.Increment(ref _errorCount);
            _logger.LogError("Request failed with status: {StatusCode}", response.StatusCode);
            _snackbar.Add($"Request failed: {response.StatusCode}", MudBlazor.Severity.Error);
        }

        private async Task HandleProblemDetails(ProblemDetails problem, HttpStatusCode statusCode)
        {
            var message = problem.Detail ?? problem.Title ?? "An error occurred";
            _snackbar.Add(message, MudBlazor.Severity.Error);

            // Show validation errors if available
            if (problem.Errors.Any())
            {
                foreach (var error in problem.Errors)
                {
                    foreach (var errorMessage in error.Value)
                    {
                        _logger.LogError(errorMessage);
                        _snackbar.Add($"{error.Key}: {errorMessage}", MudBlazor.Severity.Error);
                    }
                }
            }

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    _navigation.NavigateTo("/error");
                    break;
            }

            await Task.CompletedTask;
        }

        public void DisposeEvent()
        {
            if (_disposed)
            {
                return;
            }

            if (_eventsRegistered)
            {
                _interceptor.BeforeSendAsync -= InterceptBeforeSend;
                _interceptor.AfterSendAsync -= InterceptAfterSend;
                _eventsRegistered = false;
            }

            _disposed = true;
        }
    }
}