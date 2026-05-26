namespace FraudDetection.Models
{
    public class FraudAnalysisResult
    {
        public int RiskScore { get; set; }

        public FraudRiskLevel RiskLevel { get; set; }

        public bool IsFraud { get; set; }

        public string Reason { get; set; } = "";
    }
}