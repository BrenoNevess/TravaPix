using System.Linq;

using FraudDetection.Core;
using FraudDetection.Models;

namespace FraudDetection.Services
{
    public static class FraudDetectionService
    {
        public static FraudAnalysisResult
            AnalyzeTransaction(
                TransactionRecord transaction
            )
        {
            int score = 0;

            string reason = "";

            /*
             VALOR ALTO
            */

            if (
                transaction.Amount >= 10000
            )
            {
                score += 70;

                reason +=
                    "Valor elevado. ";
            }

            /*
             HORÁRIO SUSPEITO
            */

            int hour =
                transaction.Date.Hour;

            if (
                hour <= 5 ||
                hour >= 23
            )
            {
                score += 20;

                reason +=
                    "Horário suspeito. ";
            }

            /*
             MÚLTIPLAS TRANSAÇÕES
            */

            int recentTransactions =
                DataStore.Transactions
                    .Count(
                        t =>
                            t.SenderCpf ==
                                transaction
                                    .SenderCpf
                            &&
                            (
                                transaction.Date
                                -
                                t.Date
                            ).TotalMinutes
                            <= 2
                    );

            if (recentTransactions >= 3)
            {
                score += 40;

                reason +=
                    "Múltiplas transações consecutivas.";
            }

            FraudRiskLevel level =
                FraudRiskLevel.Safe;

            if (score >= 80)
            {
                level =
                    FraudRiskLevel.HighRisk;
            }
            else if (score >= 40)
            {
                level =
                    FraudRiskLevel.Suspicious;
            }

            return new FraudAnalysisResult
            {
                RiskScore =
                    score,

                RiskLevel =
                    level,

                IsFraud =
                    level !=
                        FraudRiskLevel
                            .Safe,

                Reason =
                    string.IsNullOrWhiteSpace(
                        reason
                    )
                    ?

                    "Transação segura."

                    :

                    reason
            };
        }
    }
}