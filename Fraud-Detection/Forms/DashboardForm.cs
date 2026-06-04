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
        private DashboardCard cardBalance = null!;

        private readonly ApiService
            apiService = new();

        public DashboardForm()
        {
            InitializeDashboard();
        }

        private async void InitializeDashboard()
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
                        Color.Transparent
                };

            Controls.Add(
                container
            );

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

                    UserSession.CurrentUser?
                        .Name
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

            cardBalance =
                new DashboardCard(
                    "Saldo Atual",
                    "R$ 0,00",

                    Color.FromArgb(
                        255,
                        193,
                        7
                    )
                );

            cardBalance.Location =
                new Point(760,100);

            container.Controls.Add(
                cardUser
            );

            container.Controls.Add(
                cardTransactions
            );

            container.Controls.Add(
                cardBalance
            );

            await LoadDashboardData();
        }

        private async Task LoadDashboardData()
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
                        JsonSerializer.Deserialize
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
                    transactions?.Count
                    ??
                    0;

                decimal balance =
                    UserSession
                        .CurrentUser!
                        .CreditLimit;

                if(
                    transactions != null
                )
                {
                    foreach(
                        var t
                        in transactions
                    )
                    {
                        if(
                            t.SenderCpf ==
                            cpf
                        )
                        {
                            balance -=
                                t.Amount;
                        }

                        if(
                            t.ReceiverCpf ==
                            cpf
                        )
                        {
                            balance +=
                                t.Amount;
                        }
                    }
                }

                cardTransactions
                    .UpdateValue(
                        total.ToString()
                    );

                cardBalance
                    .UpdateValue(
                        $"R$ {balance:N2}"
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