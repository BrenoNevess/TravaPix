using FraudDetection.Models;

namespace FraudDetection.Services
{
    public class FraudDetectionService
    {
        public bool IsFraud(decimal amount)
        {
            return amount >= 5000;
        }

        public FraudRecord GenerateFraud(
            TransactionRecord transaction)
        {
            return new FraudRecord
            {
                PurchaseCode = transaction.PurchaseCode,
                Cpf = transaction.DestinationCpf,
                Amount = transaction.Amount,
                Date = DateTime.Now,
                Reason = "Valor elevado que exige análise"
            };
        }
    }
}