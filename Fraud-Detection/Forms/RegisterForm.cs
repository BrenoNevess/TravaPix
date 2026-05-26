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
                Color.FromArgb(18,18,18);

            Panel container =
                new Panel
                {
                    Size =
                        new Size(
                            900,
                            700
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
                new Point(
                    350,
                    80
                );

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
                            320,
                            30
                        )
                };

            container.Controls.Add(
                lblTitle
            );

            txtName =
                CreateTextBox(
                    "Nome Completo",
                    140
                );

            txtCpf =
                CreateTextBox(
                    "CPF",
                    230
                );

            txtEmail =
                CreateTextBox(
                    "Email",
                    320
                );

            txtPassword =
                CreateTextBox(
                    "Senha",
                    410
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
                            540
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

                    Text = placeholder,

                    BackColor =
                        Color.FromArgb(
                            40,
                            40,
                            40
                        ),

                    ForeColor =
                        Color.White
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
                        txt.Text=
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
                        txtPassword.Text
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
                    "Cadastro"
                );
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Erro"
                );
            }
        }
    }
}