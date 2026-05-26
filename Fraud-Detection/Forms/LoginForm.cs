using System;
using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Services;

namespace FraudDetection.Forms
{
    public class LoginForm : Form
    {
        private TextBox txtCpf = null!;
        private TextBox txtPassword = null!;

        private Button btnLogin = null!;

        private readonly ApiService
            apiService = new();

        public LoginForm()
        {
            InitializeLogin();
        }

        private void InitializeLogin()
        {
            BackColor =
                Color.FromArgb(
                    18,
                    18,
                    18
                );

            txtCpf =
                CreateTextBox(
                    "CPF",
                    160
                );

            txtPassword =
                CreateTextBox(
                    "Senha",
                    250
                );

            txtPassword.PasswordChar =
                '*';

            btnLogin =
                new Button
                {
                    Text =
                        "Entrar",

                    Size =
                        new Size(
                            300,
                            45
                        ),

                    Location =
                        new Point(
                            250,
                            360
                        ),

                    BackColor =
                        Color.FromArgb(
                            0,
                            120,
                            215
                        ),

                    ForeColor =
                        Color.White
                };

            btnLogin.Click +=
                BtnLogin_Click;

            Controls.Add(txtCpf);

            Controls.Add(txtPassword);

            Controls.Add(btnLogin);
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
                            300,
                            40
                        ),

                    Location =
                        new Point(
                            250,
                            y
                        ),

                    Text =
                        placeholder
                };

            return txt;
        }

        private async void BtnLogin_Click(
            object? sender,
            EventArgs e
        )
        {
            var request =
                new
                {
                    Cpf =
                        txtCpf.Text,

                    Password =
                        txtPassword.Text
                };

            try
            {
                string response =
                    await apiService
                        .Login(
                            request
                        );

                MessageBox.Show(
                    response,
                    "Login"
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