using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace T.ApexLM.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextAnalyticsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TextAnalyticsController> _logger;

        public TextAnalyticsController(IHttpClientFactory clientFactory, ILogger<TextAnalyticsController> logger)
        {
            _httpClient = clientFactory.CreateClient("PythonAIService");
            _logger = logger;
        }

        [HttpPost("sentiment")]
        public async Task<IActionResult> AnalyzeSentiment([FromBody] TextRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Text))
                    return BadRequest(new { error = "Text cannot be null or empty" });

                _logger.LogInformation("Sending sentiment analysis request to Python service");

                var response = await _httpClient.PostAsJsonAsync("/analyze/sentiment", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Python service returned {StatusCode}: {Error}", response.StatusCode, errorContent);
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to connect to Python AI service");
                return StatusCode(503, new { error = "AI service unavailable", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in sentiment analysis");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost("language")]
        public async Task<IActionResult> DetectLanguage([FromBody] TextRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Text))
                    return BadRequest(new { error = "Text cannot be null or empty" });

                var response = await _httpClient.PostAsJsonAsync("/analyze/language", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in language detection");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost("key-phrases")]
        public async Task<IActionResult> ExtractKeyPhrases([FromBody] TextRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/analyze/key-phrases", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in key phrase extraction");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost("entities")]
        public async Task<IActionResult> RecognizeEntities([FromBody] TextRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/analyze/entities", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in entity recognition");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost("pii")]
        public async Task<IActionResult> DetectPii([FromBody] TextRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/analyze/pii", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PII detection");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost("all")]
        public async Task<IActionResult> AnalyzeAll([FromBody] TextRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/analyze/all", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in comprehensive analysis");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet("health")]
        public async Task<IActionResult> HealthCheck()
        {
            try
            {
                var response = await _httpClient.GetAsync("/health");

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, "Python service is unhealthy");

                var health = await response.Content.ReadFromJsonAsync<JsonElement>();
                return Ok(health);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Health check failed");
                return StatusCode(503, new { status = "unhealthy", error = ex.Message });
            }
        }
    }

    public class TextRequest
    {
        public string Text { get; set; } = string.Empty;
    }
}
