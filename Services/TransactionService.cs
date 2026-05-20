using FraudDetection.Core;
using FraudDetection.Models;
using FraudDetection.Repositories;

namespace FraudDetection.Services
{
    public class TransactionService
    {
        private readonly
            ITransactionRepository
            _transactionRepository;

        private readonly
            IFraudRepository
            _fraudRepository;

        public TransactionService()
        {
            _transactionRepository =
                new FakeTransactionRepository();

            _fraudRepository =
                new FakeFraudRepository();
        }

        public bool CreateTransaction(
            TransactionRecord transaction
        )
        {
            _transactionRepository.Add(
                transaction
            );

            bool suspicious =
                FraudDetectionService
                    .AnalyzeTransaction(
                        transaction
                    );

            if (suspicious)
            {
                FraudRecord fraud =
                    new FraudRecord
                    {
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
                            "Fraude detectada automaticamente."
                    };

                _fraudRepository.Add(
                    fraud
                );
            }

            EventBus.NotifyDataChanged();

            return suspicious;
        }
    }
}