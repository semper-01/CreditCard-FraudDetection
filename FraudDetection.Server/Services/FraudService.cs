using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FraudDetection.Shared.Contracts;
using FraudDetection.Shared.DTOs;

namespace FraudDetection.Server.Services
{
    public class FraudService : IFraudService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FraudService(HttpClient http)
        {
            _http = http;
            // Options for deserializing Python responses (which contain snake_case keys like shap_values)
            _jsonSerializerOptions = new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true 
            }; 
        }

        // Helper method to serialize the DTO, respecting the [JsonPropertyName] attributes
        // (which we only keep for amount_log, time_hours, day_of_week)
        private StringContent MakeJsonContent(TransactionDto transaction)
        {
            // Default C# serialization: V1 becomes "V1" (matching Python Pydantic)
            // Attributes ensure: AmountLog becomes "amount_log" (matching Python Pydantic)
            var json = JsonSerializer.Serialize(transaction);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<FraudResultDto?> ScoreAsync(TransactionDto transaction)
        {
            try
            {
                using var content = MakeJsonContent(transaction); 
                using var resp = await _http.PostAsync("score", content);
                
                resp.EnsureSuccessStatusCode(); 
                
                var stream = await resp.Content.ReadAsStreamAsync();
                
                var result = await JsonSerializer.DeserializeAsync<FraudResultDto>(stream, _jsonSerializerOptions);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ScoreAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<ExplainResultDto?> ExplainAsync(TransactionDto transaction)
        {
            try
            {
                using var content = MakeJsonContent(transaction); 
                using var resp = await _http.PostAsync("explain", content);
                
                resp.EnsureSuccessStatusCode();

                var responseJson = await resp.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseJson))
                {
                    Console.WriteLine("Warning: Explain endpoint returned a success status but no content.");
                    return null; 
                }

                // Use options for deserialization (handles shap_values, base_value, etc.)
                var result = JsonSerializer.Deserialize<ExplainResultDto>(responseJson, _jsonSerializerOptions);
                
                return result;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization Error in ExplainAsync: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error in ExplainAsync: {ex.Message}");
                return null;
            }
        }
    }
}