using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FraudDetection.Shared.DTOs;
using FraudDetection.Shared.Contracts;

namespace FraudDetection.Client.Services
{
    public class ServerFraudClient
    {
        private readonly HttpClient _http;

        public ServerFraudClient(HttpClient http)
        {
            _http = http;
        }
        public async Task<FraudResultDto?> ScoreAsync(TransactionDto transaction)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync("api/fraud/score", transaction);
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadFromJsonAsync<FraudResultDto>();
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Score request failed: {ex.Message}");
                return null;
            }
        }
        public async Task<ExplainResultDto?> ExplainAsync(TransactionDto transaction)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync("api/fraud/explain", transaction);
                if (!resp.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine($"Explain not available: {resp.StatusCode}");
                    return null;
                }
                return await resp.Content.ReadFromJsonAsync<ExplainResultDto>();
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Explain request failed: {ex.Message}");
                return null;
            }
        }
    }
}