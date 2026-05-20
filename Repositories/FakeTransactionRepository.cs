using System.Collections.Generic;
using FraudDetection.Models;

namespace FraudDetection.Repositories
{
    public class FakeTransactionRepository
        : ITransactionRepository
    {
        private static readonly
            List<TransactionRecord>
                transactions = new();

        public void Add(
            TransactionRecord transaction
        )
        {
            transactions.Add(transaction);
        }

        public List<TransactionRecord>
            GetAll()
        {
            return transactions;
        }
    }
}