using FraudDetection.Web.Helpers;
using FraudDetection.Web.Models.ViewModels;
using FraudDetection.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetection.Web.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly ITransactionService _transactionService;

        public HistoryController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            HistoryViewModel model = new()
            {
                Transactions = await _transactionService.GetByUserAsync(User.GetCpf())
            };

            return View(model);
        }
    }
}
