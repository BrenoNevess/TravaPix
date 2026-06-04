using FraudDetection.Web.Models.Entities;

namespace FraudDetection.Web.Models.ViewModels
{
    /// <summary>
    /// Histórico de transações do usuário logado.
    /// </summary>
    public class HistoryViewModel
    {
        public IReadOnlyList<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
