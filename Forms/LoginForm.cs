using System;
using System.Drawing;
using System.Windows.Forms;

namespace FraudDetection.Forms
{
    public class LoginForm : Form
    {
        private TextBox txtCpf = null!;
        private TextBox txtPassword = null!;

        private Button btnLogin = null!;

        public LoginForm()
        {
            InitializeLogin();
        }

        private void InitializeLogin()
        {
            BackColor =
                Color.FromArgb(18, 18, 18);

            AutoScroll = true;

            Panel container = new Panel
            {
                Size = new Size(700, 500),

                BackColor =
                    Color.FromArgb(28, 28, 28)
            };

            Controls.Add(container);

            container.Location =
                new Point(470, 140);

            Label lblTitle = new Label
            {
                Text = "LOGIN",

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    24,
                    FontStyle.Bold
                ),

                AutoSize = true,

                Location = new Point(280, 40)
            };

            container.Controls.Add(lblTitle);

            txtCpf =
                CreateTextBox(
                    "CPF",
                    150
                );

            txtPassword =
                CreateTextBox(
                    "Senha",
                    250
                );

            txtPassword.PasswordChar = '*';

            container.Controls.Add(txtCpf);

            container.Controls.Add(txtPassword);

            btnLogin = new Button
            {
                Text = "Entrar",

                Size = new Size(400, 45),

                Location = new Point(150, 360),

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

            btnLogin.FlatAppearance.BorderSize = 0;

            btnLogin.Click += BtnLogin_Click;

            container.Controls.Add(btnLogin);
        }

        private TextBox CreateTextBox(
            string placeholder,
            int y
        )
        {
            TextBox txt = new TextBox
            {
                Size = new Size(400, 40),

                Location = new Point(150, y),

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

        private void BtnLogin_Click(
            object? sender,
            EventArgs e
        )
        {
            if (
                txtCpf.Text == "CPF" ||

                txtPassword.Text == "Senha"
            )
            {
                MessageBox.Show(
                    "Preencha todos os campos.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            MessageBox.Show(
                "Login realizado com sucesso.",
                "Login",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}