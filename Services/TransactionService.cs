using FraudDetection.Models;
using FraudDetection.Repositories;
using FraudDetection.Core;

namespace FraudDetection.Services
{
    public class TransactionService
    {
        private readonly FakeTransactionRepository
            transactionRepository = new();

        private readonly FakeFraudRepository
            fraudRepository = new();

        public FraudAnalysisResult ProcessTransaction(
            TransactionRecord transaction
        )
        {
            FraudAnalysisResult analysis =
                FraudDetectionService
                    .AnalyzeTransaction(
                        transaction
                    );

            transaction.RiskScore =
                analysis.RiskScore;

            transaction.RiskLevel =
                analysis.RiskLevel;

            transaction.IsFraud =
                analysis.IsFraud;

            transactionRepository.Add(
                transaction
            );

            if (analysis.IsFraud)
            {
                FraudRecord fraud =
                    new FraudRecord
                    {
                        Id = transaction.Id,

                        SenderCpf =
                            transaction.SenderCpf,

                        ReceiverCpf =
                            transaction.ReceiverCpf,

                        Amount =
                            transaction.Amount,

                        Location =
                            transaction.Location,

                        Date =
                            transaction.Date,

                        Reason =
                            analysis.Reason,

                        RiskScore =
                            analysis.RiskScore,

                        RiskLevel =
                            analysis.RiskLevel
                    };

                fraudRepository.Add(
                    fraud
                );
            }

            EventBus.NotifyDataChanged();

            return analysis;
        }
    }
}