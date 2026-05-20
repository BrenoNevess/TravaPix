using System;
using System.Drawing;
using System.Windows.Forms;

namespace FraudDetection.Interface.Forms
{
    public class MainForm : Form
    {
        private Panel sidebar = null!;
        private Panel topbar = null!;
        private Panel contentPanel = null!;

        private Button btnDashboard = null!;
        private Button btnTransaction = null!;
        private Button btnHistory = null!;
        private Button btnFraud = null!;

        private Label lblTitle = null!;

        public MainForm()
        {
            InitializeForm();

            InitializeSidebar();

            InitializeTopbar();

            InitializeContent();

            OpenForm(new DashboardForm());
        }

        private void InitializeForm()
        {
            Text = "Fraud Detection System";

            Size = new Size(1500, 900);

            StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(18, 18, 18);

            FormBorderStyle = FormBorderStyle.FixedSingle;

            MaximizeBox = false;
        }

        private void InitializeSidebar()
        {
            sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.FromArgb(25, 25, 25)
            };

            Controls.Add(sidebar);

            Label logo = new Label
            {
                Text = "FRAUD\nSYSTEM",
                ForeColor = Color.FromArgb(0, 120, 215),
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 40)
            };

            sidebar.Controls.Add(logo);

            btnDashboard =
                CreateMenuButton(
                    "Dashboard",
                    180
                );

            btnTransaction =
                CreateMenuButton(
                    "Nova Transação",
                    240
                );

            btnHistory =
                CreateMenuButton(
                    "Histórico",
                    300
                );

            btnFraud =
                CreateMenuButton(
                    "Fraudes",
                    360
                );

            btnDashboard.Click += (s, e) =>
            {
                lblTitle.Text = "Dashboard";

                OpenForm(new DashboardForm());
            };

            btnTransaction.Click += (s, e) =>
            {
                lblTitle.Text = "Nova Transação";

                OpenForm(new TransactionForm());
            };

            btnHistory.Click += (s, e) =>
            {
                lblTitle.Text = "Histórico";

                OpenForm(new HistoryForm());
            };

            btnFraud.Click += (s, e) =>
            {
                lblTitle.Text = "Fraudes Detectadas";

                OpenForm(new FraudForm());
            };

            sidebar.Controls.Add(btnDashboard);

            sidebar.Controls.Add(btnTransaction);

            sidebar.Controls.Add(btnHistory);

            sidebar.Controls.Add(btnFraud);
        }

        private void InitializeTopbar()
        {
            topbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(20, 20, 20)
            };

            Controls.Add(topbar);

            lblTitle = new Label
            {
                Text = "Dashboard",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 20)
            };

            topbar.Controls.Add(lblTitle);
        }

        private void InitializeContent()
        {
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(18, 18, 18)
            };

            Controls.Add(contentPanel);
        }

        private Button CreateMenuButton(
            string text,
            int y
        )
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(250, 50),
                Location = new Point(0, y),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(25, 25, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor =
                    Color.FromArgb(35, 35, 35);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor =
                    Color.FromArgb(25, 25, 25);
            };

            return btn;
        }

        private void OpenForm(Form form)
        {
            contentPanel.Controls.Clear();

            form.TopLevel = false;

            form.FormBorderStyle =
                FormBorderStyle.None;

            form.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(form);

            form.Show();
        }
    }
}