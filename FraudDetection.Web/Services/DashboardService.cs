using FraudDetection.Web.Data;
using FraudDetection.Web.Models.Entities;
using FraudDetection.Web.Models.Enums;
using FraudDetection.Web.Models.ViewModels;
using FraudDetection.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FraudDetection.Web.Services
{
    public class DashboardService : IDashboardService
    {
        private const int RecentCount = 5;
        private const string AdminRole = "ADMIN";

        private readonly AppDbContext _context;
        private readonly ITransactionService _transactions;

        public DashboardService(
            AppDbContext context,
            ITransactionService transactions)
        {
            _context = context;
            _transactions = transactions;
        }

        public async Task<DashboardViewModel> GetDashboardAsync(string cpf, string firstName, bool isAdmin)
        {
            cpf = cpf.Trim();

            decimal creditLimit = await _context.Cards
                .Where(c => c.User.Cpf == cpf)
                .Select(c => c.CreditLimit)
                .FirstOrDefaultAsync();

            IReadOnlyList<Transaction> transactions =
                await _transactions.GetByUserAsync(cpf);

            // Transações bloqueadas não movimentaram dinheiro: ignoradas no saldo.
            decimal sent = transactions
                .Where(t => t.SenderCpf == cpf && !t.IsBlocked)
                .Sum(t => t.Amount);

            decimal received = transactions
                .Where(t => t.ReceiverCpf == cpf && !t.IsBlocked)
                .Sum(t => t.Amount);

            DashboardViewModel model = new()
            {
                FirstName = firstName,
                CreditLimit = creditLimit,
                Balance = creditLimit - sent + received,
                TransactionCount = transactions.Count,
                SafeCount = transactions.Count(t => t.RiskLevel == FraudRiskLevel.Safe),
                SuspiciousCount = transactions.Count(t => t.RiskLevel == FraudRiskLevel.Suspicious),
                HighRiskCount = transactions.Count(t => t.RiskLevel == FraudRiskLevel.HighRisk),
                RecentTransactions = transactions.Take(RecentCount).ToList(),
                IsAdmin = isAdmin
            };

            // O saldo total do sistema é calculado apenas para o administrador.
            if (isAdmin)
            {
                model.SystemTotalBalance = await ComputeSystemTotalBalanceAsync();
            }

            return model;
        }

        /// <summary>
        /// Soma dos saldos das contas de usuários comuns (contas de administrador
        /// são desconsideradas): total de limites concedidos, menos o que enviaram,
        /// mais o que receberam (ignorando transações bloqueadas).
        /// </summary>
        private async Task<decimal> ComputeSystemTotalBalanceAsync()
        {
            decimal totalLimit = await _context.Cards
                .Where(c => c.User.Role != AdminRole)
                .SumAsync(c => (decimal?)c.CreditLimit) ?? 0m;

            List<string> userCpfs = await _context.Users
                .Where(u => u.Role != AdminRole)
                .Select(u => u.Cpf)
                .ToListAsync();

            decimal sent = await _context.Transactions
                .Where(t => !t.IsBlocked && userCpfs.Contains(t.SenderCpf))
                .SumAsync(t => (decimal?)t.Amount) ?? 0m;

            decimal received = await _context.Transactions
                .Where(t => !t.IsBlocked && userCpfs.Contains(t.ReceiverCpf))
                .SumAsync(t => (decimal?)t.Amount) ?? 0m;

            return totalLimit - sent + received;
        }
    }
}
