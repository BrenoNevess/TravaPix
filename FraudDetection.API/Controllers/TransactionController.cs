using Microsoft.AspNetCore.Mvc;

using FraudDetection.API.Models;

namespace FraudDetection.API.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController
        : ControllerBase
    {
        private static List<TransactionRecord>
            transactions =
                new();

        private static List<FraudRecord>
            frauds =
                new();

        [HttpGet]
        public IActionResult GetTransactions()
        {
            return Ok(transactions);
        }

        [HttpGet("frauds")]
        public IActionResult GetFrauds()
        {
            return Ok(frauds);
        }

        [HttpPost]
        public IActionResult CreateTransaction(
            [FromBody]
            TransactionRecord transaction
        )
        {
            transaction.Date =
                DateTime.Now;

            AnalyzeTransaction(
                transaction
            );

            transactions.Add(
                transaction
            );

            if (transaction.IsFraud)
            {
                FraudRecord fraud =
                    new FraudRecord
                    {
                        SenderCpf =
                            transaction.SenderCpf,

                        ReceiverCpf =
                            transaction.ReceiverCpf,

                        Amount =
                            transaction.Amount,

                        Location =
                            transaction.Location,

                        Date =
                            transaction.Date,

                        RiskLevel =
                            transaction.RiskLevel,

                        Reason =
                            transaction.FraudReason
                    };

                frauds.Add(fraud);
            }

            return Ok(
                new
                {
                    message =
                        "Transação processada.",

                    fraud =
                        transaction.IsFraud,

                    riskLevel =
                        transaction.RiskLevel,

                    reason =
                        transaction.FraudReason
                }
            );
        }

        private void AnalyzeTransaction(
            TransactionRecord transaction
        )
        {
            int riskScore = 0;

            List<string> reasons =
                new();

            if (
                transaction.Amount >= 10000
            )
            {
                riskScore += 50;

                reasons.Add(
                    "Valor elevado"
                );
            }

            int hour =
                transaction.Date.Hour;

            if (
                hour >= 0 &&
                hour <= 5
            )
            {
                riskScore += 30;

                reasons.Add(
                    "Horário suspeito"
                );
            }

            int recentTransactions =
                transactions.Count(
                    t =>
                        t.SenderCpf ==
                            transaction
                                .SenderCpf &&

                        (
                            transaction.Date -
                            t.Date
                        ).TotalMinutes <= 2
                );

            if (
                recentTransactions >= 3
            )
            {
                riskScore += 40;

                reasons.Add(
                    "Múltiplas transações consecutivas"
                );
            }

            if (riskScore >= 70)
            {
                transaction.IsFraud =
                    true;

                transaction.RiskLevel =
                    "HIGH RISK";
            }
            else if (riskScore >= 40)
            {
                transaction.IsFraud =
                    true;

                transaction.RiskLevel =
                    "SUSPICIOUS";
            }
            else
            {
                transaction.IsFraud =
                    false;

                transaction.RiskLevel =
                    "SAFE";
            }

            transaction.FraudReason =
                string.Join(
                    ", ",
                    reasons
                );
        }
    }
}