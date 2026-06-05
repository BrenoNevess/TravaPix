using System.Globalization;
using System.Text;
using FraudDetection.Web.Helpers;
using FraudDetection.Web.Models.Entities;
using FraudDetection.Web.Models.Enums;
using FraudDetection.Web.Models.ViewModels;
using FraudDetection.Web.Services;
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
            // Pré-preenche o remetente com o CPF do usuário logado.
            return View(new TransactionViewModel { SenderCpf = User.GetCpf() });
        }

        // Etapa 1: valida e leva à revisão/confirmação (ou bloqueia se o
        // destinatário já estiver bloqueado).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BlockedRecipient? activeBlock =
                await _transactionService.GetActiveBlockAsync(model.ReceiverCpf);

            if (activeBlock is not null)
            {
                return AlreadyBlockedView(model.ReceiverCpf, model.Amount, activeBlock.ExpiresAt);
            }

            FraudAnalysisResult analysis = await _transactionService.AnalyzeAsync(
                model.SenderCpf, model.ReceiverCpf, model.Amount, model.Location);

            return View("Confirm", ToConfirmModel(model, analysis));
        }

        // Etapa 2: o usuário confirmou os dados (e assinou, se suspeita).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(TransactionConfirmViewModel model)
        {
            BlockedRecipient? activeBlock =
                await _transactionService.GetActiveBlockAsync(model.ReceiverCpf);

            if (activeBlock is not null)
            {
                return AlreadyBlockedView(model.ReceiverCpf, model.Amount, activeBlock.ExpiresAt);
            }

            FraudAnalysisResult analysis = await _transactionService.AnalyzeAsync(
                model.SenderCpf, model.ReceiverCpf, model.Amount, model.Location);

            if (analysis.Decision == FraudDecision.Block)
            {
                BlockedTransactionViewModel blocked =
                    await _transactionService.BlockAsync(model, analysis);

                return View("Blocked", blocked);
            }

            if (analysis.Decision == FraudDecision.RequireConfirmation)
            {
                string expectedName = User.GetDisplayName();

                if (string.IsNullOrWhiteSpace(model.SignatureName))
                {
                    ModelState.AddModelError(
                        nameof(model.SignatureName),
                        "Assine digitando seu nome completo para confirmar.");
                }
                else if (!NamesMatch(model.SignatureName, expectedName))
                {
                    ModelState.AddModelError(
                        nameof(model.SignatureName),
                        "A assinatura deve ser exatamente o seu nome completo, como no cadastro.");
                }

                if (!model.AcknowledgedRisk)
                {
                    ModelState.AddModelError(
                        nameof(model.AcknowledgedRisk),
                        "É necessário marcar que está ciente do risco.");
                }

                if (!ModelState.IsValid)
                {
                    Apply(analysis, model);
                    return View("Confirm", model);
                }
            }

            TransactionResultViewModel result =
                await _transactionService.CompleteAsync(model, analysis);

            return View("Result", result);
        }

        private IActionResult AlreadyBlockedView(string receiverCpf, decimal amount, DateTime expiresAt)
        {
            return View("Blocked", new BlockedTransactionViewModel
            {
                ReceiverCpf = receiverCpf,
                Amount = amount,
                BlockReason = "Destinatário bloqueado por atividade de risco recente.",
                BlockedUntil = expiresAt
            });
        }

        private static TransactionConfirmViewModel ToConfirmModel(
            TransactionViewModel model,
            FraudAnalysisResult analysis)
        {
            TransactionConfirmViewModel confirm = new()
            {
                SenderCpf = model.SenderCpf,
                ReceiverCpf = model.ReceiverCpf,
                Amount = model.Amount,
                Location = model.Location,
                Description = model.Description
            };

            Apply(analysis, confirm);
            return confirm;
        }

        private static void Apply(FraudAnalysisResult analysis, TransactionConfirmViewModel model)
        {
            model.RiskLevel = analysis.RiskLevel;
            model.Decision = analysis.Decision;
            model.Alerts = analysis.Alerts;
            model.BlockReason = analysis.BlockReason;
        }

        // A assinatura precisa ser o nome completo cadastrado (ignora caixa,
        // espaços extras e acentos), impedindo qualquer texto diferente.
        private static bool NamesMatch(string typed, string registered)
            => !string.IsNullOrWhiteSpace(registered)
               && NormalizeName(typed) == NormalizeName(registered);

        private static string NormalizeName(string value)
        {
            string collapsed = string.Join(
                ' ',
                value.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            string formD = collapsed.ToLowerInvariant().Normalize(NormalizationForm.FormD);

            StringBuilder builder = new();
            foreach (char c in formD)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
