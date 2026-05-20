using System;
using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Repositories;
using FraudDetection.Core;
using FraudDetection.Models;

namespace FraudDetection.Interface.Forms
{
    public class HistoryForm : Form
    {
        private DataGridView dgvHistory = null!;

        private readonly FakeTransactionRepository
            transactionRepository =
                new FakeTransactionRepository();

        public HistoryForm()
        {
            InitializeHistory();
        }

        private void InitializeHistory()
        {
            BackColor =
                Color.FromArgb(18, 18, 18);

            AutoScroll = true;

            Panel container = new Panel
            {
                Size = new Size(1150, 700),

                BackColor =
                    Color.FromArgb(28, 28, 28),

                Anchor =
                    AnchorStyles.None
            };

            Controls.Add(container);

            container.Location =
                new Point(-300, -200);

            Label lblTitle = new Label
            {
                Text =
                    "Histórico de Transações",

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    24,
                    FontStyle.Bold
                ),

                AutoSize = true,

                Location = new Point(
                    30,
                    25
                )
            };

            container.Controls.Add(
                lblTitle
            );

            dgvHistory = new DataGridView
            {
                Size = new Size(1080, 540),

                Location =
                    new Point(30, 100),

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

                AllowUserToAddRows =
                    false,

                ReadOnly = true,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvHistory.EnableHeadersVisualStyles =
                false;

            dgvHistory.ColumnHeadersDefaultCellStyle
                .BackColor =
                    Color.FromArgb(
                        45,
                        45,
                        45
                    );

            dgvHistory.ColumnHeadersDefaultCellStyle
                .ForeColor =
                    Color.White;

            dgvHistory.ColumnHeadersDefaultCellStyle
                .Font =
                    new Font(
                        "Segoe UI",
                        10,
                        FontStyle.Bold
                    );

            dgvHistory.DefaultCellStyle
                .BackColor =
                    Color.FromArgb(
                        30,
                        30,
                        30
                    );

            dgvHistory.DefaultCellStyle
                .ForeColor =
                    Color.White;

            dgvHistory.DefaultCellStyle
                .SelectionBackColor =
                    Color.FromArgb(
                        0,
                        120,
                        215
                    );

            dgvHistory.DefaultCellStyle
                .SelectionForeColor =
                    Color.White;

            dgvHistory.Columns.Add(
                "Code",
                "Código"
            );

            dgvHistory.Columns.Add(
                "Cpf",
                "CPF"
            );

            dgvHistory.Columns.Add(
                "Amount",
                "Valor"
            );

            dgvHistory.Columns.Add(
                "Date",
                "Data"
            );

            dgvHistory.Columns.Add(
                "Status",
                "Status"
            );

            container.Controls.Add(
                dgvHistory
            );

            LoadTransactions();

            EventBus.OnDataChanged +=
                LoadTransactions;
        }

        private void LoadTransactions()
        {
            dgvHistory.Rows.Clear();

            foreach (
                TransactionRecord transaction
                in transactionRepository.GetAll()
            )
            {
                dgvHistory.Rows.Add(
                    Guid.NewGuid()
                        .ToString()
                        .Substring(0, 8),

                    transaction.SenderCpf,

                    $"R$ {transaction.Amount}",

                    transaction.Date
                        .ToString(
                            "dd/MM/yyyy HH:mm"
                        ),

                    transaction.Amount >= 5000
                        ? "Suspeita"
                        : "Segura"
                );
            }
        }
    }
}