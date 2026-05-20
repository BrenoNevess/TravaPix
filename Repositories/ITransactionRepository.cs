using System.Collections.Generic;
using FraudDetection.Models;

namespace FraudDetection.Repositories
{
    public interface ITransactionRepository
    {
        void Add(
            TransactionRecord transaction
        );

        List<TransactionRecord> GetAll();
    }
}