using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Core;
using FraudDetection.Models;

namespace FraudDetection.Interface.Forms
{
    public class HistoryForm : Form
    {
        private DataGridView dgvHistory = null!;

        public HistoryForm()
        {
            InitializeHistory();

            LoadTransactions();

            EventBus.OnDataChanged +=
                RefreshHistory;
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
                    Color.FromArgb(
                        28,
                        28,
                        28
                    ),

                Anchor = AnchorStyles.None
            };

            Controls.Add(container);

            container.Location =
                new Point(295, 100);

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

            container.Controls.Add(lblTitle);

            dgvHistory = new DataGridView
            {
                Size = new Size(
                    1080,
                    540
                ),

                Location = new Point(
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

                AllowUserToAddRows =
                    false,

                ReadOnly = true,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvHistory.EnableHeadersVisualStyles =
                false;

            dgvHistory
                .ColumnHeadersDefaultCellStyle
                .BackColor =
                Color.FromArgb(
                    45,
                    45,
                    45
                );

            dgvHistory
                .ColumnHeadersDefaultCellStyle
                .ForeColor =
                Color.White;

            dgvHistory
                .ColumnHeadersDefaultCellStyle
                .Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold
                );

            dgvHistory
                .DefaultCellStyle
                .BackColor =
                Color.FromArgb(
                    30,
                    30,
                    30
                );

            dgvHistory
                .DefaultCellStyle
                .ForeColor =
                Color.White;

            dgvHistory
                .DefaultCellStyle
                .SelectionBackColor =
                Color.FromArgb(
                    0,
                    120,
                    215
                );

            dgvHistory
                .DefaultCellStyle
                .SelectionForeColor =
                Color.White;

            dgvHistory.Columns.Add(
                "SenderCpf",
                "CPF Remetente"
            );

            dgvHistory.Columns.Add(
                "ReceiverCpf",
                "CPF Destinatário"
            );

            dgvHistory.Columns.Add(
                "Amount",
                "Valor"
            );

            dgvHistory.Columns.Add(
                "Location",
                "Localização"
            );

            dgvHistory.Columns.Add(
                "Description",
                "Descrição"
            );

            dgvHistory.Columns.Add(
                "Date",
                "Data"
            );

            container.Controls.Add(
                dgvHistory
            );

            Label lblInfo = new Label
            {
                Text =
                    "As transações são atualizadas automaticamente.",

                ForeColor = Color.Gray,

                Font = new Font(
                    "Segoe UI",
                    11
                ),

                AutoSize = true,

                Location = new Point(
                    30,
                    655
                )
            };

            container.Controls.Add(lblInfo);
        }

        private void LoadTransactions()
        {
            dgvHistory.Rows.Clear();

            foreach (
                TransactionRecord transaction
                in DataStore.Transactions
            )
            {
                dgvHistory.Rows.Add(
                    transaction.SenderCpf,
                    transaction.ReceiverCpf,
                    $"R$ {transaction.Amount:N2}",
                    transaction.Location,
                    transaction.Description,
                    transaction.Date
                        .ToString(
                            "dd/MM/yyyy HH:mm"
                        )
                );
            }
        }

        private void RefreshHistory()
        {
            LoadTransactions();
        }
    }
}