using System.Text.Json;
using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Services;
using FraudDetection.Session;
using FraudDetection.Models;
using FraudDetection.Interface.Components;

namespace FraudDetection.Forms
{
    public class DashboardForm
        : Form
    {
        private DashboardCard
            cardUsers = null!;

        private DashboardCard
            cardTransactions = null!;

        private DashboardCard
            cardUser = null!;

        private Button
            btnLogout = null!;

        private readonly ApiService
            apiService = new();

        public DashboardForm()
        {
            InitializeDashboard();
        }

        private async void
            InitializeDashboard()
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

            Panel container =
                new Panel
                {
                    Dock = DockStyle.Fill
                };

            Controls.Add(
                container
            );

            Label title =
                new Label
                {
                    Text =
                        "Dashboard",

                    Font =
                        new Font(
                            "Segoe UI",
                            24,
                            FontStyle.Bold
                        ),

                    ForeColor =
                        Color.White,

                    AutoSize =
                        true,

                    Location =
                        new Point(
                            40,
                            30
                        )
                };

            container.Controls.Add(
                title
            );

            cardUsers =
                new DashboardCard(
                    "Usuários",
                    "...",
                    Color.Blue
                );

            cardUsers.Location =
                new Point(
                    40,
                    120
                );

            container.Controls.Add(
                cardUsers
            );

            cardTransactions =
                new DashboardCard(
                    "Transações",
                    "...",
                    Color.Green
                );

            cardTransactions.Location =
                new Point(
                    420,
                    120
                );

            container.Controls.Add(
                cardTransactions
            );

            cardUser =
                new DashboardCard(
                    "Usuário Atual",
                    UserSession
                        .CurrentUser?
                        .Name
                        ??
                        "Desconhecido",
                    Color.DarkOrange
                );

            cardUser.Location =
                new Point(
                    800,
                    120
                );

            container.Controls.Add(
                cardUser
            );

            btnLogout =
                new Button
                {
                    Text =
                        "Logout",

                    Size =
                        new Size(
                            180,
                            45
                        ),

                    Location =
                        new Point(
                            1250,
                            35
                        ),

                    BackColor =
                        Color.DarkRed,

                    ForeColor =
                        Color.White
                };

            btnLogout.Click +=
                BtnLogout_Click;

            container.Controls.Add(
                btnLogout
            );

            await LoadDashboard();
        }

        private async Task
            LoadDashboard()
        {
            try
            {
                string usersJson =
                    await apiService
                        .GetUsers();

                List<User>?
                    users =
                        JsonSerializer
                            .Deserialize
                            <
                                List<User>
                            >(
                                usersJson,
                                new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive =
                                        true
                                }
                            );

                string transactionsJson =
                    await apiService
                        .GetTransactions();

                List<TransactionRecord>?
                    transactions =
                        JsonSerializer
                            .Deserialize
                            <
                                List<TransactionRecord>
                            >(
                                transactionsJson,
                                new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive =
                                        true
                                }
                            );

                cardUsers.UpdateValue(
                    users?.Count
                        .ToString()
                    ??
                    "0"
                );

                cardTransactions
                    .UpdateValue(
                        transactions
                            ?.Count
                            .ToString()
                        ??
                        "0"
                    );
            }
            catch
            {
                MessageBox.Show(
                    "Erro carregando dashboard."
                );
            }
        }

        private void BtnLogout_Click(
            object? sender,
            EventArgs e
        )
        {
            UserSession.Logout();

            Hide();

            new LoginForm()
                .Show();
        }
    }
}