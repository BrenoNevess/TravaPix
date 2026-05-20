using System.Collections.Generic;

using FraudDetection.Models;
using FraudDetection.Repositories;

namespace FraudDetection.Services
{
    public class FraudService
    {
        private readonly
            IFraudRepository
            _fraudRepository;

        public FraudService()
        {
            _fraudRepository =
                new FakeFraudRepository();
        }

        public List<FraudRecord>
            GetAllFrauds()
        {
            return _fraudRepository.GetAll();
        }
    }
}