using FraudDetection.Web.Models.Entities;
using FraudDetection.Web.Models.Enums;
using FraudDetection.Web.Models.ViewModels;
using FraudDetection.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetection.Web.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class FraudController : Controller
    {
        private readonly ITransactionService _transactionService;

        public FraudController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            IReadOnlyList<Transaction> frauds = await _transactionService.GetFraudulentAsync();

            FraudViewModel model = new()
            {
                Frauds = frauds,
                SuspiciousCount = frauds.Count(f => f.RiskLevel == FraudRiskLevel.Suspicious),
                HighRiskCount = frauds.Count(f => f.RiskLevel == FraudRiskLevel.HighRisk)
            };

            return View(model);
        }
    }
}
