using System;
using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Models;
using FraudDetection.Services;
using FraudDetection.Session;
using FraudDetection.Interface.Forms;

namespace FraudDetection.Forms
{
    public class LoginForm : Form
    {
        private TextBox txtCpf = null!;
        private TextBox txtPassword = null!;

        private Button btnLogin = null!;
        private Button btnRegister = null!;

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

            Size =
                new Size(
                    1600,
                    900
                );

            StartPosition =
                FormStartPosition.CenterScreen;

            Text =
                "TravaPix — Login";

            Panel container =
                new Panel
                {
                    Size =
                        new Size(
                            700,
                            500
                        ),

                    BackColor =
                        Color.FromArgb(
                            28,
                            28,
                            28
                        ),

                    Location =
                        new Point(
                            430,
                            170
                        )
                };

            Controls.Add(
                container
            );

            Label lblTitle =
                new Label
                {
                    Text =
                        "LOGIN",

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            26,
                            FontStyle.Bold
                        ),

                    AutoSize =
                        true,

                    Location =
                        new Point(
                            280,
                            35
                        )
                };

            container.Controls.Add(
                lblTitle
            );

            txtCpf =
                CreateTextBox(
                    "CPF",
                    140
                );

            txtPassword =
                CreateTextBox(
                    "Senha",
                    240
                );

            container.Controls.Add(
                txtCpf
            );

            container.Controls.Add(
                txtPassword
            );

            btnLogin =
                new Button
                {
                    Text =
                        "Entrar",

                    Size =
                        new Size(
                            420,
                            45
                        ),

                    Location =
                        new Point(
                            140,
                            340
                        ),

                    BackColor =
                        Color.FromArgb(
                            0,
                            120,
                            215
                        ),

                    ForeColor =
                        Color.White,

                    FlatStyle =
                        FlatStyle.Flat,

                    Font =
                        new Font(
                            "Segoe UI",
                            11,
                            FontStyle.Bold
                        )
                };

            btnLogin.Click +=
                BtnLogin_Click;

            container.Controls.Add(
                btnLogin
            );

            btnRegister =
                new Button
                {
                    Text =
                        "Criar Conta",

                    Size =
                        new Size(
                            420,
                            45
                        ),

                    Location =
                        new Point(
                            140,
                            400
                        ),

                    BackColor =
                        Color.FromArgb(
                            45,
                            45,
                            45
                        ),

                    ForeColor =
                        Color.White,

                    FlatStyle =
                        FlatStyle.Flat,

                    Font =
                        new Font(
                            "Segoe UI",
                            10,
                            FontStyle.Bold
                        )
                };

            btnRegister.Click +=
                (
                    s,
                    e
                ) =>
            {
                MainForm.Instance!
                    .OpenForm(
                        new RegisterForm()
                    );
            };

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
                            420,
                            40
                        ),

                    Location =
                        new Point(
                            140,
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
                LoginResponse response =
                    await apiService
                        .Login(
                            request
                        );

                if(
                    !response.Success
                )
                {
                    MessageBox.Show(
                        response.Message,
                        "Erro Login"
                    );

                    return;
                }

                UserSession.Login(
                    new User
                    {
                        Name =
                            response.Name,

                        Cpf =
                            response.Cpf,

                        Email =
                            response.Email,

                        Role =
                            response.Role
                    }
                );

                MessageBox.Show(
                    $"Bem-vindo {response.Name}"
                );

                MainForm.Instance!
                    .UpdateAuthUI();

                MainForm.Instance!
                    .OpenDashboard();
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Erro Login"
                );
            }
        }
    }
}