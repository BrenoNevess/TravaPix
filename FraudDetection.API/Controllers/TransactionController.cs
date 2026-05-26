using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.DTOs;
using FraudDetection.API.Models;
using FraudDetection.API.Repositories;
using FraudDetection.API.Services;

namespace FraudDetection.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class TransactionController
        : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(
                TransactionRepository
                    .Transactions
            );
        }

        [HttpPost]
        public IActionResult Create(
            TransactionRequest request
        )
        {
            if (request.Amount <= 0)
            {
                throw new ArgumentException(
                    "Valor inválido."
                );
            }

            FraudDetectionService service =
                new FraudDetectionService();

            FraudAnalysisResult analysis =
                service.AnalyzeTransaction(
                    request.Amount,
                    DateTime.Now
                );

            Enum.TryParse<FraudRiskLevel>(
                analysis.RiskLevel,
                true,
                out FraudRiskLevel parsedRisk
            );

            Transaction transaction =
                new Transaction
                {
                    Id = Guid.NewGuid(),

                    SenderCpf =
                        request.SenderCpf,

                    ReceiverCpf =
                        request.ReceiverCpf,

                    Amount =
                        request.Amount,

                    Location =
                        request.Location,

                    Description =
                        request.Description,

                    RiskLevel =
                        parsedRisk,

                    CreatedAt =
                        DateTime.Now
                };

            TransactionRepository
                .Transactions
                .Add(transaction);

            return Ok(
                new
                {
                    message =
                        "Transação criada.",

                    riskLevel =
                        analysis.RiskLevel,

                    transaction
                }
            );
        }
    }
}