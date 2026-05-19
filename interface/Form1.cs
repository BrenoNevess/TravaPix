using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FraudDetection.Interface
{
    public partial class Form1 : Form
    {
        private RegisterControl registerControl1 = null!;
        private readonly List<UserRegistration> registrations = new();
        private readonly List<TransactionRecord> transactions = new();
        private readonly List<FraudRecord> fraudHistory = new();

        public Form1()
        {
            InitializeComponent();
            InitializeRegisterControl();
            InitializeGridViews();
            LoadSampleData();
            UpdateDashboard();
        }

        private void InitializeRegisterControl()
        {
            registerControl1 = new RegisterControl
            {
                Dock = DockStyle.Fill
            };

            registerControl1.UserRegistered += Reg_UserRegistered;
            panelRegisterHost.Controls.Add(registerControl1);
        }

        private void InitializeGridViews()
        {
            dgvTransactionHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dgvTransactionHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTransactionHistory.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dgvTransactionHistory.DefaultCellStyle.ForeColor = Color.White;
            dgvTransactionHistory.RowHeadersVisible = false;
            dgvTransactionHistory.EnableHeadersVisualStyles = false;

            dgvFraudHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dgvFraudHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvFraudHistory.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dgvFraudHistory.DefaultCellStyle.ForeColor = Color.White;
            dgvFraudHistory.RowHeadersVisible = false;
            dgvFraudHistory.EnableHeadersVisualStyles = false;
        }

        private void LoadSampleData()
        {
            registrations.Add(new UserRegistration
            {
                Name = "Cliente Demo",
                Email = "demo@frauddetector.com",
                Cpf = "12345678900",
                Password = "senha123",
                CardNumber = "4111111111111111",
                CardExpiry = "12/28",
                CardCvv = "123"
            });

            transactions.Add(new TransactionRecord
            {
                PurchaseCode = "TX10001",
                SourceCpf = "00000000000",
                DestinationCpf = "12345678900",
                Amount = 1200.00m,
                Description = "Pagamento de teste",
                Date = DateTime.Now.AddDays(-1)
            });

            fraudHistory.Add(new FraudRecord
            {
                PurchaseCode = "TX10001",
                Cpf = "12345678900",
                Amount = 1200.00m,
                Date = DateTime.Now.AddDays(-1),
                Reason = "Valor acima do padrão de risco"
            });

            PopulateTransactionGrid(transactions);
            PopulateFraudGrid();
        }

        private void UpdateDashboard()
        {
            var latestTransaction = transactions.OrderByDescending(t => t.Date).FirstOrDefault();
            var lastTransactionText = latestTransaction != null
                ? $"Última transação: {latestTransaction.PurchaseCode} ({latestTransaction.Amount:C}) para CPF {latestTransaction.DestinationCpf}"
                : "Nenhuma transação processada ainda.";

            lblDashboardSummary.Text = $"Usuários cadastrados: {registrations.Count}\n" +
                                       $"Transações registradas: {transactions.Count}\n" +
                                       $"Possíveis fraudes: {fraudHistory.Count}\n\n" +
                                       lastTransactionText;
        }

        private void Reg_UserRegistered(object? sender, UserRegisteredEventArgs e)
        {
            registrations.Add(e.Registration);
            MessageBox.Show($"Usuário cadastrado com sucesso:\nNome: {e.Registration.Name}\nCPF: {e.Registration.Cpf}\nEmail: {e.Registration.Email}", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateDashboard();
        }

        private void btnSearchPurchase_Click(object sender, EventArgs e)
        {
            var code = txtSearchPurchaseCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Informe o código da compra para pesquisar.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var matches = transactions.Where(t => t.PurchaseCode.Contains(code, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!matches.Any())
            {
                MessageBox.Show("Nenhuma transação encontrada para o código informado.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvTransactionHistory.DataSource = null;
                return;
            }

            PopulateTransactionGrid(matches);
        }

        private void btnProcessTransaction_Click(object sender, EventArgs e)
        {
            var destinationCpf = txtDestinationCpf.Text.Trim();
            var amountText = txtTransactionAmount.Text.Trim();
            var description = txtTransactionDescription.Text.Trim();

            if (string.IsNullOrEmpty(destinationCpf))
            {
                MessageBox.Show("Informe o CPF do destinatário.", "Transação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!registrations.Any(r => r.Cpf == destinationCpf))
            {
                MessageBox.Show("Destinatário não localizado. Cadastre o usuário antes de enviar a transação.", "Transação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(amountText, out var amount) || amount <= 0)
            {
                MessageBox.Show("Informe um valor de transação válido.", "Transação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var purchaseCode = $"TX{DateTime.Now:yyyyMMddHHmmss}";
            var transaction = new TransactionRecord
            {
                PurchaseCode = purchaseCode,
                SourceCpf = "00000000000",
                DestinationCpf = destinationCpf,
                Amount = amount,
                Description = string.IsNullOrEmpty(description) ? "Transferência via aplicativo" : description,
                Date = DateTime.Now
            };

            transactions.Add(transaction);
            PopulateTransactionGrid(transactions);
            UpdateDashboard();

            var warning = string.Empty;
            if (amount >= 5000)
            {
                var fraud = new FraudRecord
                {
                    PurchaseCode = purchaseCode,
                    Cpf = destinationCpf,
                    Amount = amount,
                    Date = DateTime.Now,
                    Reason = "Valor elevado que exige análise adicional"
                };
                fraudHistory.Add(fraud);
                PopulateFraudGrid();
                warning = "\nA transação foi registrada como possível fraude devido ao valor.";
            }

            lblTransactionResult.Text = $"Transação enviada para CPF {destinationCpf}. Código: {purchaseCode}.{warning}";
            MessageBox.Show($"Transação concluída com sucesso.{warning}", "Transação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PopulateTransactionGrid(IEnumerable<TransactionRecord> list)
        {
            dgvTransactionHistory.DataSource = list.Select(t => new
            {
                t.PurchaseCode,
                t.Date,
                t.SourceCpf,
                t.DestinationCpf,
                Valor = t.Amount.ToString("C"),
                t.Description
            }).ToList();
        }

        private void PopulateFraudGrid()
        {
            dgvFraudHistory.DataSource = fraudHistory.Select(f => new
            {
                f.PurchaseCode,
                f.Date,
                f.Cpf,
                Valor = f.Amount.ToString("C"),
                f.Reason
            }).ToList();
        }

        private sealed class TransactionRecord
        {
            public string PurchaseCode { get; set; } = string.Empty;
            public string SourceCpf { get; set; } = string.Empty;
            public string DestinationCpf { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public string Description { get; set; } = string.Empty;
            public DateTime Date { get; set; }
        }

        private sealed class FraudRecord
        {
            public string PurchaseCode { get; set; } = string.Empty;
            public string Cpf { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
            public string Reason { get; set; } = string.Empty;
        }
    }
}
