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

        private readonly ApiService apiService = new();

        public LoginForm()
        {
            InitializeLogin();
        }

        private void InitializeLogin()
        {
            BackColor = Color.FromArgb(18, 18, 18);
            AutoScroll = true;

            Panel container = new Panel
            {
                Size = new Size(900, 500),
                BackColor = Color.FromArgb(28, 28, 28)
            };

            container.Location = new Point(400, 160);
            Controls.Add(container);

            Label title = new Label
            {
                Text = "LOGIN",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(380, 40)
            };

            container.Controls.Add(title);

            txtCpf = CreateTextBox("CPF", 140, 500);
            txtPassword = CreateTextBox("Senha", 230, 500);
            txtPassword.PasswordChar = '*';

            container.Controls.Add(txtCpf);
            container.Controls.Add(txtPassword);

            btnLogin = new Button
            {
                Text = "Entrar",
                Size = new Size(500, 45),
                Location = new Point(200, 330),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            btnLogin.Click += BtnLogin_Click;
            container.Controls.Add(btnLogin);
        }

        private TextBox CreateTextBox(string placeholder, int y, int width)
        {
            TextBox txt = new TextBox
            {
                Size = new Size(width, 40),
                Location = new Point(200, y),
                Text = placeholder,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };

            txt.GotFocus += (s, e) =>
            {
                if (txt.Text == placeholder)
                    txt.Text = "";
            };

            txt.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                    txt.Text = placeholder;
            };

            return txt;
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            var request = new
            {
                Cpf = txtCpf.Text,
                Password = txtPassword.Text
            };

            try
            {
                string response = await apiService.Login(request);

                MessageBox.Show(response, "Login");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }
    }
}