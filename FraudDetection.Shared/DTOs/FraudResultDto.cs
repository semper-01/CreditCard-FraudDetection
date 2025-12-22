using System.Text.Json.Serialization;

namespace FraudDetection.Shared.DTOs
{
    public class FraudResultDto
    {
        [JsonPropertyName("probability")]
        public double Probability { get; set; }

        [JsonPropertyName("prediction")]
        public int Prediction { get; set; }

        [JsonPropertyName("threshold")]
        public double Threshold { get; set; }

        [JsonPropertyName("risk_level")] 
        public string RiskLevel { get; set; } = "";
    }
}