using System.Collections.Generic;
using FraudDetection.Models;

namespace FraudDetection.Repositories
{
    public class FakeFraudRepository
        : IFraudRepository
    {
        private static readonly
            List<FraudRecord>
                frauds = new();

        public void Add(FraudRecord fraud)
        {
            frauds.Add(fraud);
        }

        public List<FraudRecord> GetAll()
        {
            return frauds;
        }
    }
}