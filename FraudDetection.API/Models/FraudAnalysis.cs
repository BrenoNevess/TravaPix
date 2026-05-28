namespace FraudDetection.API.Models
{
    public class FraudAnalysis
    {
        public Guid Id { get; set; }

        public Guid TransactionId
        {
            get;
            set;
        }

        public int RiskScore
        {
            get;
            set;
        }

        public string RiskLevel
        {
            get;
            set;
        } = "";

        public string Reason
        {
            get;
            set;
        } = "";

        public bool IsFraud
        {
            get;
            set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }
    }
}