using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FraudDetection.Services;
using FraudDetection.Models;
using FraudDetection.Session;
using FraudDetection.Interface.Forms;

namespace FraudDetection.Forms
{
    public class RegisterForm : Form
    {
        private TextBox txtName = null!;
        private TextBox txtCpf = null!;
        private TextBox txtEmail = null!;
        private TextBox txtLocation = null!; // Novo campo adicionado
        private TextBox txtPassword = null!;
        private TextBox txtConfirmPassword = null!;

        private TextBox txtCardNumber = null!;
        private TextBox txtCvv = null!;
        private TextBox txtExpiry = null!;
        private TextBox txtLimit = null!;

        private Label lblPasswordStatus = null!;
        private Label lblEmailStatus = null!;
        private Button btnRegister = null!;

        private readonly ApiService apiService = new();

        public RegisterForm()
        {
            InitializeRegister();
        }

        private void InitializeRegister()
        {
            AutoScroll = true;

            BackColor = Color.FromArgb(18, 18, 18);

            Panel container = new Panel
            {
                Size = new Size(900, 1300), // Aumentado ligeiramente o tamanho para caber o novo campo
                BackColor = Color.FromArgb(28, 28, 28)
            };

            Controls.Add(container);
            container.Location = new Point(400, 40);

            Label lblTitle = new Label
            {
                Text = "CADASTRO",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(325, 40)
            };
            container.Controls.Add(lblTitle);

            Label userSection = new Label
            {
                Text = "DADOS DO USUÁRIO",
                ForeColor = Color.DeepSkyBlue,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(305, 100)
            };
            container.Controls.Add(userSection);

            txtName = CreateTextBox("Nome Completo", 150);
            txtCpf = CreateTextBox("CPF", 230);
            txtCpf.TextChanged += FormatCpf;

            txtEmail = CreateTextBox("Email", 310);
            txtEmail.TextChanged += ValidateEmail;

            // Criando e posicionando o novo campo de Localização
            txtLocation = CreateTextBox("Localização (Cidade/Estado)", 390);

            // Campos empurrados para baixo (+80px na coordenada Y para acomodar o novo campo)
            txtPassword = CreateTextBox("Senha", 470);
            txtConfirmPassword = CreateTextBox("Confirmar Senha", 550);

            txtPassword.TextChanged += ValidatePasswords;
            txtConfirmPassword.TextChanged += ValidatePasswords;

            lblEmailStatus = new Label
            {
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(200, 350)
            };

            lblPasswordStatus = new Label
            {
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(200, 600) // Ajustado Y de 520 para 600
            };

            container.Controls.Add(txtName);
            container.Controls.Add(txtCpf);
            container.Controls.Add(txtEmail);
            container.Controls.Add(lblEmailStatus);
            container.Controls.Add(txtLocation); // Adicionado ao container
            container.Controls.Add(txtPassword);
            container.Controls.Add(txtConfirmPassword);
            container.Controls.Add(lblPasswordStatus);

            Panel divider = new Panel
            {
                Size = new Size(650, 2),
                BackColor = Color.Gray,
                Location = new Point(120, 660) // Ajustado Y de 580 para 660
            };
            container.Controls.Add(divider);

            Label cardSection = new Label
            {
                Text = "DADOS DO CARTÃO",
                ForeColor = Color.DeepSkyBlue,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(305, 700) // Ajustado Y de 620 para 700
            };
            container.Controls.Add(cardSection);

            txtCardNumber = CreateTextBox("Número do Cartão", 760); // Ajustado Y de 680 para 760
            txtCardNumber.TextChanged += FormatCardNumber;

            txtCvv = CreateTextBox("CVV", 840); // Ajustado Y de 760 para 840
            txtExpiry = CreateTextBox("MM/AA", 920); // Ajustado Y de 840 para 920
            txtLimit = CreateTextBox("Limite do Cartão", 1000); // Ajustado Y de 920 para 1000

            container.Controls.Add(txtCardNumber);
            container.Controls.Add(txtCvv);
            container.Controls.Add(txtExpiry);
            container.Controls.Add(txtLimit);

            btnRegister = new Button
            {
                Text = "Cadastrar",
                Size = new Size(500, 45),
                Location = new Point(200, 1110), // Ajustado Y de 1030 para 1110
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnRegister.Click += BtnRegister_Click;
            container.Controls.Add(btnRegister);
        }

        private void FormatCpf(object? sender, EventArgs e)
        {
            string numbers = Regex.Replace(txtCpf.Text, @"\D", "");
            if (numbers.Length > 11) numbers = numbers.Substring(0, 11);

            string formatted = "";
            if (numbers.Length <= 3) formatted = numbers;
            else if (numbers.Length <= 6) formatted = $"{numbers[..3]}.{numbers[3..]}";
            else if (numbers.Length <= 9) formatted = $"{numbers[..3]}.{numbers[3..6]}.{numbers[6..]}";
            else formatted = $"{numbers[..3]}.{numbers[3..6]}.{numbers[6..9]}-{numbers[9..]}";

            txtCpf.TextChanged -= FormatCpf;
            txtCpf.Text = formatted;
            txtCpf.SelectionStart = txtCpf.Text.Length;
            txtCpf.TextChanged += FormatCpf;
        }

        private void FormatCardNumber(object? sender, EventArgs e)
        {
            string numbers = Regex.Replace(txtCardNumber.Text, @"\D", "");
            if (numbers.Length > 16) numbers = numbers.Substring(0, 16);

            string formatted = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                if (i > 0 && i % 4 == 0) formatted += " ";
                formatted += numbers[i];
            }

            txtCardNumber.TextChanged -= FormatCardNumber;
            txtCardNumber.Text = formatted;
            txtCardNumber.SelectionStart = txtCardNumber.Text.Length;
            txtCardNumber.TextChanged += FormatCardNumber;
        }

        private void ValidatePasswords(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                lblPasswordStatus.Text = "";
                return;
            }

            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                lblPasswordStatus.Text = "✓ As senhas coincidem!";
                lblPasswordStatus.ForeColor = Color.LimeGreen;
            }
            else
            {
                lblPasswordStatus.Text = "✗ As senhas precisam ser iguais!";
                lblPasswordStatus.ForeColor = Color.Red;
            }
        }

        private void ValidateEmail(object? sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email) || email == "Email")
            {
                lblEmailStatus.Text = "";
                return;
            }

            bool valid = email.Contains("@") && email.Contains(".");
            if (valid)
            {
                lblEmailStatus.Text = "✓ Email válido!";
                lblEmailStatus.ForeColor = Color.LimeGreen;
            }
            else
            {
                lblEmailStatus.Text = "✗ O email precisa ser válido!";
                lblEmailStatus.ForeColor = Color.Red;
            }
        }

        private TextBox CreateTextBox(string placeholder, int y)
        {
            TextBox txt = new TextBox
            {
                Size = new Size(500, 40),
                Location = new Point(200, y),
                Text = placeholder,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };

            bool isPasswordField = placeholder.Contains("Senha");

            txt.GotFocus += (s, e) =>
            {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.White;
                    if (isPasswordField) txt.PasswordChar = '*';
                }
            };

            txt.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.PasswordChar = '\0';
                    txt.Text = placeholder;
                    txt.ForeColor = Color.Gray;
                }
            };

            return txt;
        }

        private async void BtnRegister_Click(object? sender, EventArgs e)
        {
            // Validação para evitar enviar o texto do placeholder caso o usuário não digite nada
            string locationValue = txtLocation.Text == "Localização (Cidade/Estado)" ? "" : txtLocation.Text.Trim();

            bool validLimit = decimal.TryParse(txtLimit.Text, out decimal limit);
            if (!validLimit)
            {
                MessageBox.Show("Limite inválido.");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("As senhas precisam ser iguais!");
                return;
            }

            // Mapeando o novo campo no objeto anônimo enviado para a API
            var request = new
            {
                Name = txtName.Text.Trim(),
                Cpf = txtCpf.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Location = locationValue, // Enviando a propriedade para a API
                Password = txtPassword.Text,
                ConfirmPassword = txtConfirmPassword.Text,
                CardNumber = txtCardNumber.Text.Trim(),
                CardCvv = txtCvv.Text.Trim(),
                ExpiryDate = txtExpiry.Text.Trim(),
                CreditLimit = limit
            };

            try
            {
                // Cadastro
                await apiService.Register(request);

                // Login automatico
                LoginResponse loginResponse = await apiService.Login(new
                {
                    Cpf = txtCpf.Text.Trim(),
                    Password = txtPassword.Text.Trim()
                });

                if (!loginResponse.Success)
                {
                    MessageBox.Show(loginResponse.Message, "Erro Login");
                    return;
                }

                // Cria sessão
                UserSession.Login(
                new User
                {
                    Name =
                        loginResponse.Name,

                    Cpf =
                        loginResponse.Cpf,

                    Email =
                        loginResponse.Email,

                    Role =
                        loginResponse.Role,

                    CreditLimit =
                        loginResponse.CreditLimit
                }
            );

                MessageBox.Show("Cadastro realizado com sucesso!");
                Hide();

                MainForm main = new MainForm();
                main.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}