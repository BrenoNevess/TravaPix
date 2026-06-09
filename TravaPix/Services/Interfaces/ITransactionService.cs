using FraudDetection.Web.Models.Entities;
using FraudDetection.Web.Models.ViewModels;

namespace FraudDetection.Web.Services.Interfaces
{
    /// <summary>
    /// Criação, análise e consulta de transações, incluindo a lista de
    /// destinatários bloqueados temporariamente.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>Analisa o risco da transação (busca a localização do remetente).</summary>
        Task<FraudAnalysisResult> AnalyzeAsync(
            string senderCpf,
            string receiverCpf,
            decimal amount,
            string? location);

        /// <summary>Bloqueio ativo do destinatário (não expirado) ou null.</summary>
        Task<BlockedRecipient?> GetActiveBlockAsync(string receiverCpf);

        /// <summary>Persiste uma transação aprovada (registra a assinatura, se houver).</summary>
        Task<TransactionResultViewModel> CompleteAsync(
            TransactionConfirmViewModel model,
            FraudAnalysisResult analysis);

        /// <summary>Persiste a transação como bloqueada e bloqueia o destinatário por 8h.</summary>
        Task<BlockedTransactionViewModel> BlockAsync(
            TransactionConfirmViewModel model,
            FraudAnalysisResult analysis);

        /// <summary>Transações em que o CPF é remetente ou destinatário (mais recentes primeiro).</summary>
        Task<IReadOnlyList<Transaction>> GetByUserAsync(string cpf);

        /// <summary>Todas as transações suspeitas, de alto risco ou bloqueadas.</summary>
        Task<IReadOnlyList<Transaction>> GetFraudulentAsync();

        /// <summary>Destinatários atualmente bloqueados (não expirados).</summary>
        Task<IReadOnlyList<BlockedRecipient>> GetActiveBlocksAsync();

        /// <summary>Remove um bloqueio (ação do administrador).</summary>
        Task RemoveBlockAsync(Guid id);
    }
}
