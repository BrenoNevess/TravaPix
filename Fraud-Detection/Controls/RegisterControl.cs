using System;
using System.Drawing;
using System.Windows.Forms;

namespace FraudDetection.Interface.Controls
{
    public class RegisterControl : UserControl
    {
        private TextBox txtName = null!;
        private TextBox txtCpf = null!;
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;

        private Button btnRegister = null!;

        public RegisterControl()
        {
            InitializeControl();
        }

        private void InitializeControl()
        {
            BackColor = Color.FromArgb(28, 28, 28);

            Label lblTitle = new Label
            {
                Text = "NOVO USUÁRIO",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(140, 30)
            };

            Controls.Add(lblTitle);

            txtName = CreateTextBox(
                "Nome Completo",
                120
            );

            txtCpf = CreateTextBox(
                "CPF",
                200
            );

            txtEmail = CreateTextBox(
                "Email",
                280
            );

            txtPassword = CreateTextBox(
                "Senha",
                360
            );

            txtPassword.PasswordChar = '●';

            Controls.Add(txtName);

            Controls.Add(txtCpf);

            Controls.Add(txtEmail);

            Controls.Add(txtPassword);

            btnRegister = new Button
            {
                Text = "Cadastrar Usuário",
                Size = new Size(400, 45),
                Location = new Point(60, 460),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnRegister.FlatAppearance.BorderSize = 0;

            btnRegister.Click += BtnRegister_Click;

            Controls.Add(btnRegister);
        }

        private TextBox CreateTextBox(
            string placeholder,
            int y
        )
        {
            TextBox txt = new TextBox
            {
                Size = new Size(400, 40),
                Location = new Point(60, y),
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11),
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
                if (string.IsNullOrWhiteSpace(txt.Text))
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
                "Controle de cadastro pronto para integração com banco.",
                "Cadastro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}