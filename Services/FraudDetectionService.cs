using FraudDetection.Models;

namespace FraudDetection.Services
{
    public static class FraudDetectionService
    {
        public static bool AnalyzeTransaction(
            TransactionRecord transaction
        )
        {
            if (transaction.Amount >= 5000)
            {
                return true;
            }

            if (
                transaction.Location
                    .ToLower()
                    .Contains("russia")
            )
            {
                return true;
            }

            return false;
        }
    }
}