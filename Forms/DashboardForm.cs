using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Core;
using FraudDetection.Interface.Components;
using FraudDetection.Services;

namespace FraudDetection.Interface.Forms
{
    public class DashboardForm : Form
    {
        private DashboardCard cardUsers = null!;
        private DashboardCard cardTransactions = null!;
        private DashboardCard cardFrauds = null!;

        private DashboardService dashboardService =
            new DashboardService();

        public DashboardForm()
        {
            InitializeDashboard();

            LoadDashboardData();

            EventBus.OnDataChanged +=
                RefreshDashboard;
        }

        private void InitializeDashboard()
        {
            BackColor =
                Color.FromArgb(18, 18, 18);

            AutoScroll = true;

            Panel container = new Panel
            {
                Size = new Size(1100, 700),

                BackColor = Color.Transparent,

                Anchor = AnchorStyles.None
            };

            Controls.Add(container);

            container.Location =
                new Point(295, 80);

            Label lblTitle = new Label
            {
                Text =
                    "Visão Geral do Sistema",

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

            container.Controls.Add(cardUsers);

            container.Controls.Add(
                cardTransactions
            );

            container.Controls.Add(cardFrauds);

            Panel chartPanel = new Panel
            {
                Size = new Size(1020, 350),

                Location = new Point(
                    20,
                    320
                ),

                BackColor =
                    Color.FromArgb(
                        28,
                        28,
                        28
                    )
            };

            container.Controls.Add(chartPanel);

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

            chartPanel.Controls.Add(lblChart);

            Label lblInfo = new Label
            {
                Text =
                    "O dashboard é atualizado automaticamente após novas transações.",

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

            chartPanel.Controls.Add(lblInfo);
        }

        private void LoadDashboardData()
        {
            cardUsers.UpdateValue(
                DataStore.Users.Count
                    .ToString()
            );

            cardTransactions.UpdateValue(
                dashboardService
                    .GetTotalTransactions()
                    .ToString()
            );

            cardFrauds.UpdateValue(
                dashboardService
                    .GetTotalFrauds()
                    .ToString()
            );
        }

        private void RefreshDashboard()
        {
            LoadDashboardData();
        }
    }
}