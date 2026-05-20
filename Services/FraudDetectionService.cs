using System;
using System.Linq;

using FraudDetection.Models;
using FraudDetection.Core;

namespace FraudDetection.Services
{
    public static class FraudDetectionService
    {
        public static FraudAnalysisResult AnalyzeTransaction(
            TransactionRecord transaction
        )
        {
            int riskScore = 0;

            string reason = "";

            /*Valor elevado*/

            if (transaction.Amount >= 5000)
            {
                riskScore += 40;

                reason +=
                    "Valor elevado detectado. ";
            }

            /*localização suspeita*/

            string location =
                transaction.Location
                    .ToLower();

            if (
                location.Contains("russia") ||

                location.Contains("china") ||

                location.Contains("unknown")
            )
            {
                riskScore += 30;

                reason +=
                    "Localização suspeita. ";
            }

            /*Horario suspeito*/

            int hour =
                transaction.Date.Hour;

            if (
                hour >= 0 &&
                hour <= 5
            )
            {
                riskScore += 20;

                reason +=
                    "Horário suspeito. ";
            }

            /*Múltiplas transações consecutivas*/

            int recentTransactions =
                DataStore.Transactions.Count(
                    t =>
                        t.SenderCpf ==
                            transaction.SenderCpf &&

                        (
                            transaction.Date -
                            t.Date
                        ).TotalMinutes <= 2
                );

            if (recentTransactions >= 3)
            {
                riskScore += 30;

                reason +=
                    "Múltiplas transações consecutivas. ";
            }

            /*Classificação*/

            string riskLevel =
                "SEGURO";

            bool isFraud = false;

            if (riskScore >= 70)
            {
                riskLevel =
                    "ALTO RISCO";

                isFraud = true;
            }
            else if (riskScore >= 40)
            {
                riskLevel =
                    "SUSPEITO";

                isFraud = true;
            }

            /* ALERTA */

            if (isFraud)
            {
                AlertService.SendAlert(
                    transaction,
                    riskLevel
                );
            }

            return new FraudAnalysisResult
            {
                IsFraud = isFraud,

                RiskScore = riskScore,

                RiskLevel = riskLevel,

                Reason = reason
            };
        }
    }
}