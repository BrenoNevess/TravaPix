using FraudDetection.Web.Models.Entities;

namespace FraudDetection.Web.Models.ViewModels
{
    /// <summary>
    /// Dados agregados exibidos na visão geral (dashboard).
    /// </summary>
    public class DashboardViewModel
    {
        public string FirstName { get; set; } = "";

        public int TransactionCount { get; set; }

        public decimal Balance { get; set; }

        public decimal CreditLimit { get; set; }

        public int SafeCount { get; set; }

        public int SuspiciousCount { get; set; }

        public int HighRiskCount { get; set; }

        /// <summary>True quando o usuário logado é administrador.</summary>
        public bool IsAdmin { get; set; }

        /// <summary>Soma dos saldos de todas as contas (preenchido apenas para o admin).</summary>
        public decimal SystemTotalBalance { get; set; }

        public IReadOnlyList<Transaction> RecentTransactions { get; set; }
            = new List<Transaction>();
    }
}
