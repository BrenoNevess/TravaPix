using System;
using System.Drawing;
using System.Windows.Forms;

namespace FraudDetection.Forms
{
    public class RegisterForm : Form
    {
        private TextBox txtName = null!;
        private TextBox txtCpf = null!;
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;

        private TextBox txtCardNumber = null!;
        private TextBox txtCardCvv = null!;
        private TextBox txtCardExpiry = null!;
        private TextBox txtCardLimit = null!;

        private Button btnRegister = null!;

        public RegisterForm()
        {
            InitializeRegister();
        }

        private void InitializeRegister()
        {
            AutoScroll = true;

            BackColor =
                Color.FromArgb(18, 18, 18);

            Panel container = new Panel
            {
                Size = new Size(900, 850),

                BackColor =
                    Color.FromArgb(28, 28, 28)
            };

            Controls.Add(container);

            container.Location =
                new Point(350, 40);

            Label lblTitle = new Label
            {
                Text = "CADASTRO",

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    24,
                    FontStyle.Bold
                ),

                AutoSize = true,

                Location = new Point(320, 30)
            };

            container.Controls.Add(lblTitle);

            txtName =
                CreateTextBox(
                    "Nome Completo",
                    120
                );

            txtCpf =
                CreateTextBox(
                    "CPF",
                    200
                );

            txtEmail =
                CreateTextBox(
                    "Email",
                    280
                );

            txtPassword =
                CreateTextBox(
                    "Senha",
                    360
                );

            txtCardNumber =
                CreateTextBox(
                    "Número do Cartão",
                    470
                );

            txtCardCvv =
                CreateTextBox(
                    "CVV",
                    550
                );

            txtCardExpiry =
                CreateTextBox(
                    "Validade",
                    630
                );

            txtCardLimit =
                CreateTextBox(
                    "Limite do Cartão",
                    710
                );

            txtPassword.PasswordChar = '*';

            container.Controls.Add(txtName);

            container.Controls.Add(txtCpf);

            container.Controls.Add(txtEmail);

            container.Controls.Add(txtPassword);

            container.Controls.Add(txtCardNumber);

            container.Controls.Add(txtCardCvv);

            container.Controls.Add(txtCardExpiry);

            container.Controls.Add(txtCardLimit);

            btnRegister = new Button
            {
                Text = "Cadastrar",

                Size = new Size(500, 45),

                Location = new Point(200, 780),

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

            btnRegister.FlatAppearance.BorderSize = 0;

            btnRegister.Click += BtnRegister_Click;

            container.Controls.Add(btnRegister);
        }

        private TextBox CreateTextBox(
            string placeholder,
            int y
        )
        {
            TextBox txt = new TextBox
            {
                Size = new Size(500, 40),

                Location = new Point(200, y),

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

        private void BtnRegister_Click(
            object? sender,
            EventArgs e
        )
        {
            MessageBox.Show(
                "Usuário cadastrado com sucesso.",
                "Cadastro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}