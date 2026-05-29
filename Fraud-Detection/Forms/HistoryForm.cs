using System;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;

using FraudDetection.Models;
using FraudDetection.Services;

namespace FraudDetection.Forms
{
    public class HistoryForm : Form
    {
        private DataGridView
            dgvHistory = null!;

        private readonly ApiService
            apiService = new();

        public HistoryForm()
        {
            InitializeHistory();
        }

        private void InitializeHistory()
        {
            BackColor =
                Color.FromArgb(
                    18,
                    18,
                    18
                );

            Size =
                new Size(
                    1400,
                    850
                );

            AutoScroll = true;

            Panel container =
                new Panel
                {
                    Size =
                        new Size(
                            1250,
                            700
                        ),

                    BackColor =
                        Color.FromArgb(
                            28,
                            28,
                            28
                        ),

                    Location =
                        new Point(
                            40,
                            40
                        )
                };

            Controls.Add(
                container
            );

            Label lblTitle =
                new Label
                {
                    Text =
                        "Histórico de Transações",

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            24,
                            FontStyle.Bold
                        ),

                    AutoSize = true,

                    Location =
                        new Point(
                            30,
                            25
                        )
                };

            container.Controls.Add(
                lblTitle
            );

            dgvHistory =
                new DataGridView
                {
                    Size =
                        new Size(
                            1180,
                            540
                        ),

                    Location =
                        new Point(
                            30,
                            100
                        ),

                    BackgroundColor =
                        Color.FromArgb(
                            18,
                            18,
                            18
                        ),

                    BorderStyle =
                        BorderStyle.None,

                    RowHeadersVisible =
                        false,

                    ReadOnly = true,

                    AllowUserToAddRows =
                        false,

                    AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode.Fill
                };

            dgvHistory.Columns.Add(
                "Sender",
                "Remetente"
            );

            dgvHistory.Columns.Add(
                "Receiver",
                "Destinatário"
            );

            dgvHistory.Columns.Add(
                "Amount",
                "Valor"
            );

            dgvHistory.Columns.Add(
                "Location",
                "Local"
            );

            dgvHistory.Columns.Add(
                "Description",
                "Descrição"
            );

            dgvHistory.Columns.Add(
                "Risk",
                "Risco"
            );

            dgvHistory.Columns.Add(
                "Date",
                "Data"
            );

            container.Controls.Add(
                dgvHistory
            );

            LoadTransactions();
        }

        private async void LoadTransactions()
        {
            try
            {
                dgvHistory.Rows.Clear();

                string json =
                    await apiService
                        .GetTransactions();

                List<TransactionResponse>?
                    transactions =
                        JsonSerializer
                            .Deserialize<
                                List<
                                    TransactionResponse
                                >
                            >(
                                json,
                                new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive =
                                        true
                                }
                            );

                if(
                    transactions == null
                )
                {
                    return;
                }

                foreach(
                    TransactionResponse
                        transaction
                    in transactions
                )
                {
                    dgvHistory.Rows.Add(
                        transaction.SenderCpf,

                        transaction.ReceiverCpf,

                        $"R$ {transaction.Amount}",

                        transaction.Location,

                        transaction.Description,

                        transaction.RiskLevel,

                        transaction.CreatedAt
                            .ToString(
                                "dd/MM/yyyy HH:mm"
                            )
                    );
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Erro Histórico"
                );
            }
        }
    }
}