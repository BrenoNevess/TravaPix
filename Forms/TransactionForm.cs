using System;
using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Models;
using FraudDetection.Core;
using FraudDetection.Services;
using FraudDetection.Repositories;

namespace FraudDetection.Interface.Forms
{
    public class TransactionForm : Form
    {
        private TextBox txtSenderCpf = null!;
        private TextBox txtReceiverCpf = null!;
        private TextBox txtAmount = null!;
        private TextBox txtLocation = null!;
        private TextBox txtDescription = null!;

        private Button btnProcess = null!;

        private readonly ITransactionRepository
            transactionRepository =
                new FakeTransactionRepository();

        private readonly IFraudRepository
            fraudRepository =
                new FakeFraudRepository();

        public TransactionForm()
        {
            InitializeTransaction();
        }

        private void InitializeTransaction()
        {
            AutoScroll = true;

            BackColor = Color.FromArgb(18, 18, 18);

            Panel container = new Panel
            {
                Size = new Size(850, 700),

                BackColor =
                    Color.FromArgb(28, 28, 28)
            };

            Controls.Add(container);

            container.Location =
                new Point(400, 100);

            Label lblTitle = new Label
            {
                Text = "NOVA TRANSAÇÃO",

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    24,
                    FontStyle.Bold
                ),

                AutoSize = true,

                Location = new Point(230, 30)
            };

            container.Controls.Add(lblTitle);

            txtSenderCpf =
                CreateTextBox(
                    "CPF Remetente",
                    120
                );

            txtReceiverCpf =
                CreateTextBox(
                    "CPF Destinatário",
                    210
                );

            txtAmount =
                CreateTextBox(
                    "Valor da Transação",
                    300
                );

            txtLocation =
                CreateTextBox(
                    "Localização",
                    390
                );

            txtDescription =
                CreateTextBox(
                    "Descrição",
                    480
                );

            container.Controls.Add(txtSenderCpf);

            container.Controls.Add(txtReceiverCpf);

            container.Controls.Add(txtAmount);

            container.Controls.Add(txtLocation);

            container.Controls.Add(txtDescription);

            btnProcess = new Button
            {
                Text = "Processar Transação",

                Size = new Size(500, 45),

                Location = new Point(170, 600),

                FlatStyle = FlatStyle.Flat,

                BackColor =
                    Color.FromArgb(0, 120, 215),

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnProcess.FlatAppearance.BorderSize = 0;

            btnProcess.Click += BtnProcess_Click;

            container.Controls.Add(btnProcess);
        }

        private TextBox CreateTextBox(
            string placeholder,
            int y
        )
        {
            TextBox txt = new TextBox
            {
                Size = new Size(500, 40),

                Location = new Point(170, y),

                BackColor =
                    Color.FromArgb(40, 40, 40),

                ForeColor = Color.White,

                BorderStyle =
                    BorderStyle.FixedSingle,

                Font = new Font(
                    "Segoe UI",
                    11
                ),

                Text = placeholder
            };

            txt.GotFocus += (s, e) =>
            {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                }
            };

            txt.LostFocus += (s, e) =>
            {
                if (
                    string.IsNullOrWhiteSpace(
                        txt.Text
                    )
                )
                {
                    txt.Text = placeholder;
                }
            };

            return txt;
        }

        private void BtnProcess_Click(
            object? sender,
            EventArgs e
        )
        {
            if (
                txtSenderCpf.Text ==
                    "CPF Remetente" ||

                txtReceiverCpf.Text ==
                    "CPF Destinatário" ||

                txtAmount.Text ==
                    "Valor da Transação"
            )
            {
                MessageBox.Show(
                    "Preencha os campos obrigatórios.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            bool validAmount =
                decimal.TryParse(
                    txtAmount.Text,
                    out decimal amount
                );

            if (!validAmount)
            {
                MessageBox.Show(
                    "Valor inválido.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            TransactionRecord transaction =
                new TransactionRecord
                {
                    SenderCpf =
                        txtSenderCpf.Text,

                    ReceiverCpf =
                        txtReceiverCpf.Text,

                    Amount = amount,

                    Location =
                        txtLocation.Text,

                    Description =
                        txtDescription.Text,

                    Date =
                        DateTime.Now
                };

            transactionRepository.Add(
                transaction
            );

            bool suspicious =
                FraudDetectionService
                    .AnalyzeTransaction(
                        transaction
                    );

            if (suspicious)
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

                        Reason =
                            "Transação suspeita detectada automaticamente."
                    };

                fraudRepository.Add(
                    fraud
                );
            }

            EventBus.NotifyDataChanged();

            MessageBox.Show(
                suspicious
                    ? "⚠ POSSÍVEL FRAUDE DETECTADA"
                    : "Transação processada com sucesso.",

                "Resultado",

                MessageBoxButtons.OK,

                suspicious
                    ? MessageBoxIcon.Warning
                    : MessageBoxIcon.Information
            );

            ClearFields();
        }

        private void ClearFields()
        {
            txtSenderCpf.Text =
                "CPF Remetente";

            txtReceiverCpf.Text =
                "CPF Destinatário";

            txtAmount.Text =
                "Valor da Transação";

            txtLocation.Text =
                "Localização";

            txtDescription.Text =
                "Descrição";
        }
    }
}