using System.Collections.Generic;
using FraudDetection.Models;

namespace FraudDetection.Repositories
{
    public interface IFraudRepository
    {
        void Add(FraudRecord fraud);

        List<FraudRecord> GetAll();
    }
}