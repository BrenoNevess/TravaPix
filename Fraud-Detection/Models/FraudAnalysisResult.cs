namespace FraudDetection.Models
{
    public class FraudAnalysisResult
    {
        public bool IsFraud { get; set; }

        public int RiskScore { get; set; }

        public string RiskLevel { get; set; } = "";

        public string Reason { get; set; } = "";
    }
}