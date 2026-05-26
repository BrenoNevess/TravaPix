using FraudDetection.API.Models;

namespace FraudDetection.API.Services
{
    public class FraudDetectionService
    {
        public FraudAnalysisResult AnalyzeTransaction(
            decimal amount,
            DateTime transactionDate
        )
        {
            List<string> alerts =
                new();

            string riskLevel =
                "SEGURA";

            if (amount >= 10000)
            {
                riskLevel =
                    "ALTO RISCO";

                alerts.Add(
                    "Valor extremamente elevado!"
                );
            }
            else if (amount >= 5000)
            {
                riskLevel =
                    "SUSPEITA";

                alerts.Add(
                    "Valor acima do normal!"
                );
            }

            int hour =
                transactionDate.Hour;

            if (
                hour <= 5
                ||
                hour >= 23
            )
            {
                riskLevel =
                    "SUSPEITA";

                alerts.Add(
                    "Horário suspeito!"
                );
            }

            return new FraudAnalysisResult
            {
                RiskLevel =
                    riskLevel,

                Alerts =
                    alerts
            };
        }
    }
}