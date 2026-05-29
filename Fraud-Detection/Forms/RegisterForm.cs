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

        private TextBox txtCardNumber = null!;
        private TextBox txtCvv = null!;
        private TextBox txtExpiry = null!;
        private TextBox txtLimit = null!;

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
                            1100
                        ),

                    BackColor =
                        Color.FromArgb(
                            28,
                            28,
                            28
                        )
                };

            Controls.Add(
                container
            );

            container.Location =
                new Point(
                    400,
                    40
                );

            /*Titulo Principal*/

            Label lblTitle =
                new Label
                {
                    Text =
                        "CADASTRO",

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

            /*Usuário*/

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

            txtPassword.PasswordChar =
                '*';

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

            /*Linha*/

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
                            470
                        )
                };

            container.Controls.Add(
                divider
            );

            /* Cartão */

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
                            510
                        )
                };

            container.Controls.Add(
                cardSection
            );

            txtCardNumber =
                CreateTextBox(
                    "Número do Cartão",
                    570
                );

            txtCvv =
                CreateTextBox(
                    "CVV",
                    650
                );

            txtExpiry =
                CreateTextBox(
                    "MM/AA",
                    730
                );

            txtLimit =
                CreateTextBox(
                    "Limite do Cartão",
                    810
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

            /*Botão*/

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
                            920
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

                    Text =
                        placeholder,

                    BackColor =
                        Color.FromArgb(
                            40,
                            40,
                            40
                        ),

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            11
                        ),

                    BorderStyle =
                        BorderStyle.FixedSingle
                };

            txt.GotFocus +=
                (s,e)=>
                {
                    if(
                        txt.Text
                        ==
                        placeholder
                    )
                    {
                        txt.Text="";
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
                        txt.Text =
                            placeholder;
                    }
                };

            return txt;
        }

        private async void BtnRegister_Click(
            object? sender,
            EventArgs e
        )
        {
            bool validLimit =
                decimal.TryParse(
                    txtLimit.Text,
                    out decimal limit
                );

            if(!validLimit)
            {
                MessageBox.Show(
                    "Limite inválido.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            var request =
                new
                {
                    Name =
                        txtName.Text,

                    Cpf =
                        txtCpf.Text,

                    Email =
                        txtEmail.Text,

                    Password =
                        txtPassword.Text,

                    CardNumber =
                        txtCardNumber.Text,

                    CardCvv =
                        txtCvv.Text,

                    ExpiryDate =
                        txtExpiry.Text,

                    CreditLimit =
                        limit
                };

            try
            {
                string response =
                    await apiService
                        .Register(
                            request
                        );

                MessageBox.Show(
                    response,
                    "Cadastro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}