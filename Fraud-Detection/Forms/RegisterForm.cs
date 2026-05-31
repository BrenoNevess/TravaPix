using System;
using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Services;

namespace FraudDetection.Forms
{
    public class RegisterForm : Form
    {
        private TextBox txtName = null!;
        private TextBox txtCpf = null!;
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;
        private TextBox txtConfirmPassword = null!;

        private TextBox txtCardNumber = null!;
        private TextBox txtCvv = null!;
        private TextBox txtExpiry = null!;
        private TextBox txtLimit = null!;

        private Label lblPasswordStatus = null!;

        private Button btnRegister = null!;

        private readonly ApiService
            apiService = new();

        public RegisterForm()
        {
            InitializeRegister();
        }

        private void InitializeRegister()
        {
            AutoScroll = true;

            BackColor =
                Color.FromArgb(
                    18,
                    18,
                    18
                );

            Panel container =
                new Panel
                {
                    Size =
                        new Size(
                            900,
                            1200
                        ),

                    BackColor =
                        Color.FromArgb(
                            28,
                            28,
                            28
                        )
                };

            Controls.Add(container);

            container.Location =
                new Point(400, 40);

            Label lblTitle =
                new Label
                {
                    Text = "CADASTRO",

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
                            325,
                            40
                        )
                };

            container.Controls.Add(
                lblTitle
            );

            Label userSection =
                new Label
                {
                    Text =
                        "DADOS DO USUÁRIO",

                    ForeColor =
                        Color.DeepSkyBlue,

                    Font =
                        new Font(
                            "Segoe UI",
                            16,
                            FontStyle.Bold
                        ),

                    AutoSize = true,

                    Location =
                        new Point(
                            305,
                            100
                        )
                };

            container.Controls.Add(
                userSection
            );

            txtName =
                CreateTextBox(
                    "Nome Completo",
                    150
                );

            txtCpf =
                CreateTextBox(
                    "CPF",
                    230
                );

            txtEmail =
                CreateTextBox(
                    "Email",
                    310
                );

            txtPassword =
                CreateTextBox(
                    "Senha",
                    390
                );

            txtConfirmPassword =
                CreateTextBox(
                    "Confirmar Senha",
                    470
                );

            txtPassword.TextChanged +=
                ValidatePasswords;

            txtConfirmPassword.TextChanged +=
                ValidatePasswords;

            lblPasswordStatus =
                new Label
                {
                    AutoSize = true,

                    ForeColor =
                        Color.Gray,

                    Font =
                        new Font(
                            "Segoe UI",
                            10,
                            FontStyle.Bold
                        ),

                    Location =
                        new Point(
                            200,
                            520
                        )
                };

            container.Controls.Add(
                txtName
            );

            container.Controls.Add(
                txtCpf
            );

            container.Controls.Add(
                txtEmail
            );

            container.Controls.Add(
                txtPassword
            );

            container.Controls.Add(
                txtConfirmPassword
            );

            container.Controls.Add(
                lblPasswordStatus
            );

            Panel divider =
                new Panel
                {
                    Size =
                        new Size(
                            650,
                            2
                        ),

                    BackColor =
                        Color.Gray,

                    Location =
                        new Point(
                            120,
                            580
                        )
                };

            container.Controls.Add(
                divider
            );

            Label cardSection =
                new Label
                {
                    Text =
                        "DADOS DO CARTÃO",

                    ForeColor =
                        Color.DeepSkyBlue,

                    Font =
                        new Font(
                            "Segoe UI",
                            16,
                            FontStyle.Bold
                        ),

                    AutoSize = true,

                    Location =
                        new Point(
                            305,
                            620
                        )
                };

            container.Controls.Add(
                cardSection
            );

            txtCardNumber =
                CreateTextBox(
                    "Número do Cartão",
                    680
                );

            txtCvv =
                CreateTextBox(
                    "CVV",
                    760
                );

            txtExpiry =
                CreateTextBox(
                    "MM/AA",
                    840
                );

            txtLimit =
                CreateTextBox(
                    "Limite do Cartão",
                    920
                );

            container.Controls.Add(
                txtCardNumber
            );

            container.Controls.Add(
                txtCvv
            );

            container.Controls.Add(
                txtExpiry
            );

            container.Controls.Add(
                txtLimit
            );

            btnRegister =
                new Button
                {
                    Text =
                        "Cadastrar",

                    Size =
                        new Size(
                            500,
                            45
                        ),

                    Location =
                        new Point(
                            200,
                            1030
                        ),

                    FlatStyle =
                        FlatStyle.Flat,

                    BackColor =
                        Color.FromArgb(
                            0,
                            120,
                            215
                        ),

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            11,
                            FontStyle.Bold
                        )
                };

            btnRegister.Click +=
                BtnRegister_Click;

            container.Controls.Add(
                btnRegister
            );
        }

        private void ValidatePasswords(
            object? sender,
            EventArgs e
        )
        {
            if(
                string.IsNullOrWhiteSpace(
                    txtPassword.Text
                )
                ||
                string.IsNullOrWhiteSpace(
                    txtConfirmPassword.Text
                )
            )
            {
                lblPasswordStatus.Text = "";
                return;
            }

            if(
                txtPassword.Text ==
                txtConfirmPassword.Text
            )
            {
                lblPasswordStatus.Text =
                    "✓ As senhas coincidem!";

                lblPasswordStatus.ForeColor =
                    Color.LimeGreen;
            }
            else
            {
                lblPasswordStatus.Text =
                    "✗ As senhas precisam ser iguais!";

                lblPasswordStatus.ForeColor =
                    Color.Red;
            }
        }

        private TextBox CreateTextBox(
            string placeholder,
            int y
        )
        {
            TextBox txt =
                new TextBox
                {
                    Size =
                        new Size(
                            500,
                            40
                        ),

                    Location =
                        new Point(
                            200,
                            y
                        ),

                    Text = placeholder,

                    BackColor =
                        Color.FromArgb(
                            40,
                            40,
                            40
                        ),

                    ForeColor =
                        Color.Gray,

                    Font =
                        new Font(
                            "Segoe UI",
                            11
                        ),

                    BorderStyle =
                        BorderStyle.FixedSingle
                };

            bool isPasswordField =
                placeholder.Contains(
                    "Senha"
                );

            txt.GotFocus +=
                (s,e)=>
                {
                    if(
                        txt.Text ==
                        placeholder
                    )
                    {
                        txt.Text = "";

                        txt.ForeColor =
                            Color.White;

                        if(isPasswordField)
                        {
                            txt.PasswordChar =
                                '*';
                        }
                    }
                };

            txt.LostFocus +=
                (s,e)=>
                {
                    if(
                        string.IsNullOrWhiteSpace(
                            txt.Text
                        )
                    )
                    {
                        txt.PasswordChar =
                            '\0';

                        txt.Text =
                            placeholder;

                        txt.ForeColor =
                            Color.Gray;
                    }
                };

            return txt;
        }

        private async void BtnRegister_Click(
            object? sender,
            EventArgs e
        )
        {
            if(
                txtPassword.Text !=
                txtConfirmPassword.Text
            )
            {
                MessageBox.Show(
                    "As senhas não coincidem.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            bool validLimit =
                decimal.TryParse(
                    txtLimit.Text,
                    out decimal limit
                );

            if(!validLimit)
            {
                MessageBox.Show(
                    "Limite inválido."
                );

                return;
            }

            var request =
                new
                {
                    Name = txtName.Text,
                    Cpf = txtCpf.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    CardNumber = txtCardNumber.Text,
                    CardCvv = txtCvv.Text,
                    ExpiryDate = txtExpiry.Text,
                    CreditLimit = limit
                };

            try
            {
                string response =
                    await apiService
                        .Register(
                            request
                        );

                MessageBox.Show(
                    response
                );
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.Message
                );
            }
        }
    }
}