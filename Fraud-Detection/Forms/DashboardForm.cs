using System.Text.Json;
using System.Drawing;
using System.Windows.Forms;

using FraudDetection.Interface.Components;
using FraudDetection.Services;
using FraudDetection.Session;
using FraudDetection.Models;

namespace FraudDetection.Interface.Forms
{
    public class DashboardForm : Form
    {
        private DashboardCard cardUser = null!;
        private DashboardCard cardTransactions = null!;
        private DashboardCard cardFrauds = null!;

        private readonly ApiService
            apiService =
                new();

        public DashboardForm()
        {
            InitializeDashboard();
        }

        private async void
            InitializeDashboard()
        {
            BackColor =
                Color.FromArgb(18,18,18);

            AutoScroll = true;

            Panel container =
                new Panel
                {
                    Size =
                        new Size(
                            1100,
                            700
                        ),

                    BackColor =
                        Color.Transparent,

                    Anchor =
                        AnchorStyles.None
                };

            Controls.Add(container);

            container.Location =
                new Point(-300,-200);

            Label lblTitle =
                new Label
                {
                    Text =
                        "Visão Geral",

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            24,
                            FontStyle.Bold
                        ),

                    AutoSize = true,

                    Location =
                        new Point(
                            20,
                            20
                        )
                };

            container.Controls.Add(
                lblTitle
            );

            cardUser =
                new DashboardCard(
                    "Usuário",

                    UserSession.CurrentUser?.Name
                    .Split(' ')[0]
                    ??
                    "Desconhecido",

                    Color.FromArgb(
                        0,
                        120,
                        215
                    )
                );

            cardUser.Location =
                new Point(20,100);

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
                new Point(390,100);

            cardFrauds =
                new DashboardCard(
                    "Suspeitas",
                    "0",

                    Color.FromArgb(
                        220,
                        53,
                        69
                    )
                );

            cardFrauds.Location =
                new Point(760,100);

            container.Controls.Add(
                cardUser
            );

            container.Controls.Add(
                cardTransactions
            );

            container.Controls.Add(
                cardFrauds
            );

            Panel chartPanel =
                new Panel
                {
                    Size =
                        new Size(
                            1020,
                            350
                        ),

                    Location =
                        new Point(
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

            container.Controls.Add(
                chartPanel
            );

            Label lblChart =
                new Label
                {
                    Text =
                        "Monitoramento Pessoal",

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            16,
                            FontStyle.Bold
                        ),

                    AutoSize =
                        true,

                    Location =
                        new Point(
                            20,
                            20
                        )
                };

            chartPanel.Controls.Add(
                lblChart
            );

            Label lblInfo =
                new Label
                {
                    Text =
                        "Dados sincronizados com a API.",

                    ForeColor =
                        Color.Gray,

                    Font =
                        new Font(
                            "Segoe UI",
                            12
                        ),

                    AutoSize =
                        true,

                    Location =
                        new Point(
                            20,
                            80
                        )
                };

            chartPanel.Controls.Add(
                lblInfo
            );

            await LoadDashboardData();
        }

        private async Task
            LoadDashboardData()
        {
            try
            {
                string cpf =
                    UserSession
                        .CurrentUser!
                        .Cpf;

                string json =
                    await apiService
                        .GetUserTransactions(
                            cpf
                        );

                List<TransactionResponse>?
                    transactions =
                        JsonSerializer
                            .Deserialize
                            <
                                List<TransactionResponse>
                            >(
                                json,
                                new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive =
                                        true
                                }
                            );

                int total =
                    transactions?
                        .Count
                    ??
                    0;

                int suspicious =
                    transactions?
                        .Count(
                            t =>
                                t.RiskLevel
                                    .ToString()
                                !=
                                "Safe"
                        )
                    ??
                    0;

                cardTransactions
                    .UpdateValue(
                        total
                            .ToString()
                    );

                cardFrauds
                    .UpdateValue(
                        suspicious
                            .ToString()
                    );
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Erro Dashboard"
                );
            }
        }
    }
}