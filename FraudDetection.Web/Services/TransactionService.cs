using FraudDetection.Web.Data;
using FraudDetection.Web.Models.Entities;
using FraudDetection.Web.Models.Enums;
using FraudDetection.Web.Models.ViewModels;
using FraudDetection.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FraudDetection.Web.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        private readonly IFraudDetectionService _fraudDetection;

        public TransactionService(
            AppDbContext context,
            IFraudDetectionService fraudDetection)
        {
            _context = context;
            _fraudDetection = fraudDetection;
        }

        public async Task<TransactionResultViewModel> CreateAsync(TransactionViewModel model)
        {
            DateTime now = DateTime.Now;

            FraudAnalysisResult analysis = _fraudDetection.Analyze(model.Amount, now);

            Transaction transaction = new()
            {
                Id = Guid.NewGuid(),
                SenderCpf = model.SenderCpf.Trim(),
                ReceiverCpf = model.ReceiverCpf.Trim(),
                Amount = model.Amount,
                Location = model.Location?.Trim() ?? "",
                Description = model.Description?.Trim() ?? "",
                RiskLevel = analysis.RiskLevel,
                CreatedAt = now
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return new TransactionResultViewModel
            {
                RiskLevel = analysis.RiskLevel,
                Alerts = analysis.Alerts,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt
            };
        }

        public async Task<IReadOnlyList<Transaction>> GetByUserAsync(string cpf)
        {
            cpf = cpf.Trim();

            return await _context.Transactions
                .Where(t => t.SenderCpf == cpf || t.ReceiverCpf == cpf)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Transaction>> GetFraudulentAsync()
        {
            return await _context.Transactions
                .Where(t => t.RiskLevel != FraudRiskLevel.Safe)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}
