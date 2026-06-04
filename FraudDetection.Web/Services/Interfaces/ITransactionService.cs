using FraudDetection.Web.Models.Entities;
using FraudDetection.Web.Models.ViewModels;

namespace FraudDetection.Web.Services.Interfaces
{
    /// <summary>
    /// Criação e consulta de transações.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>Analisa o risco, persiste a transação e devolve o resultado.</summary>
        Task<TransactionResultViewModel> CreateAsync(TransactionViewModel model);

        /// <summary>Transações em que o CPF é remetente ou destinatário (mais recentes primeiro).</summary>
        Task<IReadOnlyList<Transaction>> GetByUserAsync(string cpf);

        /// <summary>Todas as transações suspeitas ou de alto risco (mais recentes primeiro).</summary>
        Task<IReadOnlyList<Transaction>> GetFraudulentAsync();
    }
}
