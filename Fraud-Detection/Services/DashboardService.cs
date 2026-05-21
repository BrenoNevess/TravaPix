using FraudDetection.Core;

namespace FraudDetection.Services
{
    public class DashboardService
    {
        public int GetTotalTransactions()
        {
            return DataStore
                .Transactions
                .Count;
        }

        public int GetTotalFrauds()
        {
            return DataStore
                .Frauds
                .Count;
        }

        public decimal GetTotalAmount()
        {
            return DataStore
                .Transactions
                .Sum(
                    t => t.Amount
                );
        }
    }
}