using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.DTOs;
using FraudDetection.API.Models;
using FraudDetection.API.Repositories;

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
            if (
                string.IsNullOrWhiteSpace(
                    request.SenderCpf
                )
                ||
                string.IsNullOrWhiteSpace(
                    request.ReceiverCpf
                )
            )
            {
                return BadRequest(
                    new
                    {
                        message =
                            "CPF obrigatório."
                    }
                );
            }

            if (
                request.Amount <= 0
            )
            {
                return BadRequest(
                    new
                    {
                        message =
                            "Valor inválido."
                    }
                );
            }

            FraudRiskLevel riskLevel;

            if (request.Amount > 10000)
            {
                riskLevel =
                    FraudRiskLevel
                        .HighRisk;
            }
            else if (
                request.Amount > 5000
            )
            {
                riskLevel =
                    FraudRiskLevel
                        .Suspicious;
            }
            else
            {
                riskLevel =
                    FraudRiskLevel
                        .Safe;
            }

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
                        riskLevel,

                    CreatedAt =
                        DateTime.Now
                };

            TransactionRepository
                .Transactions
                .Add(
                    transaction
                );

            return Ok(
                new
                {
                    message =
                        "Transação criada.",

                    classification =
                        riskLevel
                            .ToString(),

                    transaction
                }
            );
        }
    }
}