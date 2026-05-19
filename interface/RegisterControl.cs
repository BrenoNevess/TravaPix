using System;
using System.Drawing;
using System.Windows.Forms;

namespace FraudDetection.Interface
{
    public class UserRegistration
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string CardExpiry { get; set; } = string.Empty;
        public string CardCvv { get; set; } = string.Empty;
    }

    public class UserRegisteredEventArgs : EventArgs
    {
        public UserRegistration Registration { get; }
        public UserRegisteredEventArgs(UserRegistration registration) => Registration = registration;
    }

    public class RegisterControl : UserControl
    {
        private Label lblName = null!;
        private Label lblEmail = null!;
        private Label lblCpf = null!;
        private Label lblPassword = null!;
        private Label lblCardNumber = null!;
        private Label lblCardExpiry = null!;
        private Label lblCardCvv = null!;
        private TextBox txtName = null!;
        private TextBox txtEmail = null!;
        private TextBox txtCpf = null!;
        private TextBox txtPassword = null!;
        private TextBox txtCardNumber = null!;
        private TextBox txtCardExpiry = null!;
        private TextBox txtCardCvv = null!;
        private Button btnSubmit = null!;

        public event EventHandler<UserRegisteredEventArgs>? UserRegistered;

        public RegisterControl()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.BackColor = Color.FromArgb(40, 40, 40);
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(24);

            lblName = new Label()
            {
                Text = "Nome:",
                ForeColor = Color.White,
                Location = new Point(24, 24),
                AutoSize = true
            };
            txtName = new TextBox()
            {
                Location = new Point(24, 56),
                Width = 420,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblEmail = new Label()
            {
                Text = "Email:",
                ForeColor = Color.White,
                Location = new Point(24, 100),
                AutoSize = true
            };
            txtEmail = new TextBox()
            {
                Location = new Point(24, 132),
                Width = 420,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblCpf = new Label()
            {
                Text = "CPF:",
                ForeColor = Color.White,
                Location = new Point(24, 176),
                AutoSize = true
            };
            txtCpf = new TextBox()
            {
                Location = new Point(24, 208),
                Width = 220,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 11
            };

            lblPassword = new Label()
            {
                Text = "Senha:",
                ForeColor = Color.White,
                Location = new Point(280, 176),
                AutoSize = true
            };
            txtPassword = new TextBox()
            {
                Location = new Point(280, 208),
                Width = 220,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '*'
            };

            lblCardNumber = new Label()
            {
                Text = "Número do Cartão:",
                ForeColor = Color.White,
                Location = new Point(24, 256),
                AutoSize = true
            };
            txtCardNumber = new TextBox()
            {
                Location = new Point(24, 288),
                Width = 420,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 19
            };

            lblCardExpiry = new Label()
            {
                Text = "Validade (MM/AA):",
                ForeColor = Color.White,
                Location = new Point(24, 336),
                AutoSize = true
            };
            txtCardExpiry = new TextBox()
            {
                Location = new Point(24, 368),
                Width = 160,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 5
            };

            lblCardCvv = new Label()
            {
                Text = "CVV:",
                ForeColor = Color.White,
                Location = new Point(212, 336),
                AutoSize = true
            };
            txtCardCvv = new TextBox()
            {
                Location = new Point(212, 368),
                Width = 120,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 4,
                PasswordChar = '*'
            };

            btnSubmit = new Button()
            {
                Text = "Cadastrar Usuário",
                Location = new Point(24, 420),
                Width = 180,
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.Click += BtnSubmit_Click;

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblCpf);
            this.Controls.Add(txtCpf);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblCardNumber);
            this.Controls.Add(txtCardNumber);
            this.Controls.Add(lblCardExpiry);
            this.Controls.Add(txtCardExpiry);
            this.Controls.Add(lblCardCvv);
            this.Controls.Add(txtCardCvv);
            this.Controls.Add(btnSubmit);
        }

        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var email = txtEmail.Text.Trim();
            var cpf = txtCpf.Text.Trim();
            var password = txtPassword.Text;
            var cardNumber = txtCardNumber.Text.Trim();
            var cardExpiry = txtCardExpiry.Text.Trim();
            var cardCvv = txtCardCvv.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios: nome, email, CPF e senha.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                MessageBox.Show("CPF inválido. Use apenas números, sem pontos ou traços.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cardNumber.Length < 13 || cardNumber.Length > 19 || !cardNumber.All(char.IsDigit))
            {
                MessageBox.Show("Número de cartão inválido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(cardExpiry) || cardExpiry.Length != 5 || cardExpiry[2] != '/')
            {
                MessageBox.Show("Validade do cartão inválida. Use formato MM/AA.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(cardCvv) || cardCvv.Length < 3 || cardCvv.Length > 4 || !cardCvv.All(char.IsDigit))
            {
                MessageBox.Show("CVV inválido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var registration = new UserRegistration
            {
                Name = name,
                Email = email,
                Cpf = cpf,
                Password = password,
                CardNumber = cardNumber,
                CardExpiry = cardExpiry,
                CardCvv = cardCvv
            };

            UserRegistered?.Invoke(this, new UserRegisteredEventArgs(registration));
        }
    }
}
