using System.Drawing;
using System.Windows.Forms;

namespace FraudDetection.Interface.Forms
{
    public class FraudForm : Form
    {
        private DataGridView dgvFrauds = null!;

        public FraudForm()
        {
            InitializeFraudForm();
        }

        private void InitializeFraudForm()
        {
            BackColor = Color.FromArgb(18, 18, 18);

            Panel container = new Panel
            {
                Size = new Size(1150, 700),

                BackColor =
                    Color.FromArgb(28, 28, 28)
            };

            Controls.Add(container);

            container.Location = new Point(295, 100);

            Label lblTitle = new Label
            {
                Text = "FRAUDES DETECTADAS",

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    22,
                    FontStyle.Bold
                ),

                AutoSize = true,

                Location = new Point(30, 25)
            };

            container.Controls.Add(lblTitle);

            dgvFrauds = new DataGridView
            {
                Size = new Size(1080, 540),

                Location = new Point(30, 100),

                BackgroundColor =
                    Color.FromArgb(18, 18, 18),

                BorderStyle = BorderStyle.None,

                RowHeadersVisible = false,

                AllowUserToAddRows = false,

                ReadOnly = true,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvFrauds.EnableHeadersVisualStyles = false;

            dgvFrauds.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(45, 45, 45);

            dgvFrauds.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvFrauds.DefaultCellStyle.BackColor =
                Color.FromArgb(30, 30, 30);

            dgvFrauds.DefaultCellStyle.ForeColor =
                Color.White;

            dgvFrauds.Columns.Add(
                "Cpf",
                "CPF"
            );

            dgvFrauds.Columns.Add(
                "Amount",
                "Valor"
            );

            dgvFrauds.Columns.Add(
                "Location",
                "Localização"
            );

            dgvFrauds.Columns.Add(
                "Reason",
                "Motivo"
            );

            dgvFrauds.Columns.Add(
                "Date",
                "Data"
            );

            container.Controls.Add(dgvFrauds);

            dgvFrauds.Rows.Add(
                "123.456.789-00",
                "R$ 15.000",
                "São Paulo",
                "Valor elevado",
                "17/05/2026"
            );
        }
    }
}