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

        private readonly AppDbContext _context;
        private readonly ITransactionService _transactions;

        public DashboardService(
            AppDbContext context,
            ITransactionService transactions)
        {
            _context = context;
            _transactions = transactions;
        }

        public async Task<DashboardViewModel> GetDashboardAsync(string cpf, string firstName)
        {
            cpf = cpf.Trim();

            decimal creditLimit = await _context.Cards
                .Where(c => c.User.Cpf == cpf)
                .Select(c => c.CreditLimit)
                .FirstOrDefaultAsync();

            IReadOnlyList<Transaction> transactions =
                await _transactions.GetByUserAsync(cpf);

            decimal sent = transactions
                .Where(t => t.SenderCpf == cpf)
                .Sum(t => t.Amount);

            decimal received = transactions
                .Where(t => t.ReceiverCpf == cpf)
                .Sum(t => t.Amount);

            return new DashboardViewModel
            {
                FirstName = firstName,
                CreditLimit = creditLimit,
                Balance = creditLimit - sent + received,
                TransactionCount = transactions.Count,
                SafeCount = transactions.Count(t => t.RiskLevel == FraudRiskLevel.Safe),
                SuspiciousCount = transactions.Count(t => t.RiskLevel == FraudRiskLevel.Suspicious),
                HighRiskCount = transactions.Count(t => t.RiskLevel == FraudRiskLevel.HighRisk),
                RecentTransactions = transactions.Take(RecentCount).ToList()
            };
        }
    }
}
