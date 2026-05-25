using System;
using System.Linq;

using FraudDetection.Core;
using FraudDetection.Models;

namespace FraudDetection.Services
{
    public static class FraudDetectionService
    {
        public static FraudRiskLevel AnalyzeRisk(
            TransactionRecord transaction
        )
        {
            if (
                transaction.Amount >=
                10000
            )
            {
                return FraudRiskLevel
                    .HighRisk;
            }

            int suspiciousHour =
                transaction.Date.Hour;

            if (
                suspiciousHour <= 5
                ||
                suspiciousHour >= 23
            )
            {
                return FraudRiskLevel
                    .Suspicious;
            }

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
                return FraudRiskLevel
                    .HighRisk;
            }

            if (
                transaction.Amount >=
                5000
            )
            {
                return FraudRiskLevel
                    .Suspicious;
            }

            return FraudRiskLevel
                .Safe;
        }

        public static bool AnalyzeTransaction(
            TransactionRecord transaction
        )
        {
            FraudRiskLevel risk =
                AnalyzeRisk(
                    transaction
                );

            return
                risk ==
                    FraudRiskLevel
                        .Suspicious
                ||
                risk ==
                    FraudRiskLevel
                        .HighRisk;
        }
    }
}