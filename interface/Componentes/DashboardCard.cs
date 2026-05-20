using System.Drawing;
using System.Windows.Forms;

namespace FraudDetection.Interface.Components
{
    public class DashboardCard : Panel
    {
        private Label lblTitle = null!;
        private Label lblValue = null!;

        public DashboardCard(
            string title,
            string value,
            Color accentColor
        )
        {
            InitializeCard(
                title,
                value,
                accentColor
            );
        }

        private void InitializeCard(
            string title,
            string value,
            Color accentColor
        )
        {
            Size = new Size(280, 160);

            BackColor = Color.FromArgb(28, 28, 28);

            lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            Controls.Add(lblTitle);

            lblValue = new Label
            {
                Text = value,
                ForeColor = accentColor,
                Font = new Font("Segoe UI", 34, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 60)
            };

            Controls.Add(lblValue);
        }

        public void UpdateValue(
            string value
        )
        {
            lblValue.Text = value;
        }
    }
}