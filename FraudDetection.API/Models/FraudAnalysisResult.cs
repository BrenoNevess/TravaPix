namespace FraudDetection.API.Models
{
    public class FraudAnalysisResult
    {
        public string RiskLevel { get; set; }
            = "";

        public List<string> Alerts
        {
            get;
            set;
        }
            = new();
    }
}