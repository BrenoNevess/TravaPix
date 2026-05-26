using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.Data;
using FraudDetection.API.DTOs;
using FraudDetection.API.Models;
using FraudDetection.API.Services;

namespace FraudDetection.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class TransactionController
        : ControllerBase
    {
        private readonly AppDbContext
            _context;

        public TransactionController(
            AppDbContext context
        )
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(
                _context.Transactions
                    .ToList()
            );
        }

        [HttpPost]
        public IActionResult Create(
            TransactionRequest request
        )
        {
            if (request.Amount <= 0)
            {
                throw new Exception(
                    "Valor inválido."
                );
            }

            FraudDetectionService service =
                new FraudDetectionService();

            FraudAnalysisResult analysis =
                service
                    .AnalyzeTransaction(
                        request.Amount,
                        DateTime.Now
                    );

            Transaction transaction =
                new Transaction
                {
                    Id =
                        Guid.NewGuid(),

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
                        Enum.Parse<
                            FraudRiskLevel
                        >(
                            analysis.RiskLevel
                        ),

                    CreatedAt =
                        DateTime.Now
                };

            _context.Transactions
                .Add(
                    transaction
                );

            _context.SaveChanges();

            return Ok(
                new
                {
                    message =
                        "Transação criada.",

                    risk =
                        analysis.RiskLevel,

                    alerts =
                        analysis.Alerts,

                    transaction
                }
            );
        }
    }
}