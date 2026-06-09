using System.Collections.Generic;

using FraudDetection.Models;

namespace FraudDetection.Core
{
    public static class DataStore
    {
        public static List<User> Users =
            new List<User>();

        public static List<TransactionRecord> Transactions =
            new List<TransactionRecord>();

        public static List<FraudRecord> Frauds =
            new List<FraudRecord>();
    }
}