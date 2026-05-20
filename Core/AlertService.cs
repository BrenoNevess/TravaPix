using System.Windows.Forms;

using FraudDetection.Models;

namespace FraudDetection.Core
{
    public static class AlertService
    {
        public static void SendAlert(
            TransactionRecord transaction,
            string riskLevel
        )
        {
            MessageBox.Show(
                $"ALERTA DE FRAUDE\n\n" +
                $"CPF: {transaction.SenderCpf}\n" +
                $"Valor: R$ {transaction.Amount}\n" +
                $"Nível: {riskLevel}",

                "Sistema de Fraude",

                MessageBoxButtons.OK,

                MessageBoxIcon.Warning
            );
        }
    }
}