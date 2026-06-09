using System.Threading.Tasks;

using FraudDetection.Services;

namespace FraudDetection.Services
{
    public class TransactionService
    {
        private readonly ApiService
            apiService =
                new ApiService();

        public async Task<string>
            CreateTransaction(
                object transactionData
            )
        {
            return await apiService
                .CreateTransaction(
                    transactionData
                );
        }
    }
}