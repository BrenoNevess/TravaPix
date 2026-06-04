using FraudDetection.Web.Helpers;
using FraudDetection.Web.Models.ViewModels;
using FraudDetection.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetection.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Pre-preenche o remetente com o CPF do usuario logado.
            return View(new TransactionViewModel { SenderCpf = User.GetCpf() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Result = await _transactionService.CreateAsync(model);

            return View(model);
        }
    }
}
