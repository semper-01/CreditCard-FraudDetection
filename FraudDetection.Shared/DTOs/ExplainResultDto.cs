using System.Text.Json.Serialization; // Ensure this using statement is at the top!

namespace FraudDetection.Shared.DTOs
{
    public class ExplainResultDto
    {
        [JsonPropertyName("probability")]
        public double Probability { get; set; }
        
        [JsonPropertyName("prediction")]
        public int Prediction { get; set; }
        
        [JsonPropertyName("shap_values")] //Add attribute for snake_case match
        public double[] ShapValues { get; set; } = new double[0];
        
        [JsonPropertyName("base_value")] 
        public double BaseValue { get; set; }
    }
}