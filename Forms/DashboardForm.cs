using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Interface.Components;

using FraudDetection.Repositories;
using FraudDetection.Core;

namespace FraudDetection.Interface.Forms
{
    public class DashboardForm : Form
    {
        private DashboardCard cardUsers = null!;
        private DashboardCard cardTransactions = null!;
        private DashboardCard cardFrauds = null!;

        private readonly FakeUserRepository
            userRepository =
                new FakeUserRepository();

        private readonly FakeTransactionRepository
            transactionRepository =
                new FakeTransactionRepository();

        private readonly FakeFraudRepository
            fraudRepository =
                new FakeFraudRepository();

        public DashboardForm()
        {
            InitializeDashboard();
        }

        private void InitializeDashboard()
        {
            BackColor =
                Color.FromArgb(18, 18, 18);

            AutoScroll = true;

            Panel container = new Panel
            {
                Size = new Size(1100, 700),

                BackColor =
                    Color.Transparent,

                Anchor = AnchorStyles.None
            };

            Controls.Add(container);

            container.Location =
                new Point(-250, -200);

            Label lblTitle = new Label
            {
                Text = "Visão Geral do Sistema",

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    24,
                    FontStyle.Bold
                ),

                AutoSize = true,

                Location = new Point(20, 20)
            };

            container.Controls.Add(lblTitle);

            cardUsers =
                new DashboardCard(
                    "Usuários",
                    "0",
                    Color.FromArgb(
                        0,
                        120,
                        215
                    )
                );

            cardUsers.Location =
                new Point(20, 100);

            cardTransactions =
                new DashboardCard(
                    "Transações",
                    "0",
                    Color.FromArgb(
                        40,
                        167,
                        69
                    )
                );

            cardTransactions.Location =
                new Point(390, 100);

            cardFrauds =
                new DashboardCard(
                    "Fraudes",
                    "0",
                    Color.FromArgb(
                        220,
                        53,
                        69
                    )
                );

            cardFrauds.Location =
                new Point(760, 100);

            container.Controls.Add(
                cardUsers
            );

            container.Controls.Add(
                cardTransactions
            );

            container.Controls.Add(
                cardFrauds
            );

            Panel chartPanel = new Panel
            {
                Size = new Size(1020, 350),

                Location =
                    new Point(20, 320),

                BackColor =
                    Color.FromArgb(
                        28,
                        28,
                        28
                    )
            };

            container.Controls.Add(
                chartPanel
            );

            Label lblChart = new Label
            {
                Text =
                    "Monitoramento Geral",

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    16,
                    FontStyle.Bold
                ),

                AutoSize = true,

                Location = new Point(
                    20,
                    20
                )
            };

            chartPanel.Controls.Add(
                lblChart
            );

            Label lblInfo = new Label
            {
                Text =
                    "Atualização automática ativada.",

                ForeColor = Color.Gray,

                Font = new Font(
                    "Segoe UI",
                    12
                ),

                AutoSize = true,

                Location = new Point(
                    20,
                    80
                )
            };

            chartPanel.Controls.Add(
                lblInfo
            );

            LoadDashboardData();

            EventBus.OnDataChanged +=
                LoadDashboardData;
        }

        private void LoadDashboardData()
        {
            cardUsers.UpdateValue(
                userRepository
                    .GetAll()
                    .Count
                    .ToString()
            );

            cardTransactions.UpdateValue(
                transactionRepository
                    .GetAll()
                    .Count
                    .ToString()
            );

            cardFrauds.UpdateValue(
                fraudRepository
                    .GetAll()
                    .Count
                    .ToString()
            );
        }
    }
}