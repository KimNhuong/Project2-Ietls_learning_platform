using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IeltsWeb.api.Services
{
    public class DeepSeekService
    {
        private readonly string? _apiKey;
        private readonly HttpClient _httpClient;

        public DeepSeekService(IConfiguration config)
        {
            _apiKey = config["DeepSeek:ApiKey"];
            _httpClient = new HttpClient();
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException("DeepSeek API key is missing in configuration.");
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            // Optional headers for OpenRouter rankings
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://your-site.com"); // <- thay thế
            _httpClient.DefaultRequestHeaders.Add("X-Title", "IeltsWeb"); // <- thay thế
        }

        public async Task<string> CallDeepSeekAsync(string prompt)
        {
            var payload = new
            {
                model = "deepseek/deepseek-chat-v3-0324:free",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"DeepSeek API error: {response.StatusCode} - {error}");
            }

            var responseString = await response.Content.ReadAsStringAsync();

            // Extract nội dung trả về từ message
            using var jsonDoc = JsonDocument.Parse(responseString);
            var messageContent = jsonDoc
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return messageContent ?? "No response content";
        }
    }
}
