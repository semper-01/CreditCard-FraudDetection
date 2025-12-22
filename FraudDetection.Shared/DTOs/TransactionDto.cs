using System.Text.Json.Serialization;

namespace FraudDetection.Shared.DTOs
{
    public class TransactionDto
    {
        public double V1 { get; set; }
        public double V2 { get; set; }
        public double V3 { get; set; }
        public double V4 { get; set; }
        public double V5 { get; set; }
        public double V6 { get; set; }
        public double V7 { get; set; }
        public double V8 { get; set; }
        public double V9 { get; set; }
        public double V10 { get; set; }
        public double V11 { get; set; }
        public double V12 { get; set; }
        public double V13 { get; set; }
        public double V14 { get; set; }
        public double V15 { get; set; }
        public double V16 { get; set; }
        public double V17 { get; set; }
        public double V18 { get; set; }
        public double V19 { get; set; }
        public double V20 { get; set; }
        public double V21 { get; set; }
        public double V22 { get; set; }
        public double V23 { get; set; }
        public double V24 { get; set; }
        public double V25 { get; set; }
        public double V26 { get; set; }
        public double V27 { get; set; }
        public double V28 { get; set; }

        [JsonPropertyName("amount_log")]
        public double AmountLog { get; set; }
        
        [JsonPropertyName("time_hours")]
        public double TimeHours { get; set; }
        
        [JsonPropertyName("day_of_week")]
        public int DayOfWeek { get; set; }
    }
}