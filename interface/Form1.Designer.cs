using System;
using System.Drawing;
using System.Windows.Forms;

namespace FraudDetection.Interface
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl mainTabs;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.TabPage tabCadastro;
        private System.Windows.Forms.TabPage tabTransactionHistory;
        private System.Windows.Forms.TabPage tabProcessTransaction;
        private System.Windows.Forms.TabPage tabFraudHistory;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.Label lblDashboardSummary;
        private System.Windows.Forms.Panel panelRegisterHost;
        private System.Windows.Forms.Label lblSearchPurchaseCode;
        private System.Windows.Forms.TextBox txtSearchPurchaseCode;
        private System.Windows.Forms.Button btnSearchPurchase;
        private System.Windows.Forms.DataGridView dgvTransactionHistory;
        private System.Windows.Forms.Label lblDestinationCpf;
        private System.Windows.Forms.TextBox txtDestinationCpf;
        private System.Windows.Forms.Label lblTransactionAmount;
        private System.Windows.Forms.TextBox txtTransactionAmount;
        private System.Windows.Forms.Label lblTransactionDescription;
        private System.Windows.Forms.TextBox txtTransactionDescription;
        private System.Windows.Forms.Button btnProcessTransaction;
        private System.Windows.Forms.Label lblTransactionResult;
        private System.Windows.Forms.DataGridView dgvFraudHistory;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.lblDashboardSummary = new System.Windows.Forms.Label();
            this.lblDashboardTitle = new System.Windows.Forms.Label();
            this.tabCadastro = new System.Windows.Forms.TabPage();
            this.panelRegisterHost = new System.Windows.Forms.Panel();
            this.tabTransactionHistory = new System.Windows.Forms.TabPage();
            this.dgvTransactionHistory = new System.Windows.Forms.DataGridView();
            this.btnSearchPurchase = new System.Windows.Forms.Button();
            this.txtSearchPurchaseCode = new System.Windows.Forms.TextBox();
            this.lblSearchPurchaseCode = new System.Windows.Forms.Label();
            this.tabProcessTransaction = new System.Windows.Forms.TabPage();
            this.lblTransactionResult = new System.Windows.Forms.Label();
            this.btnProcessTransaction = new System.Windows.Forms.Button();
            this.txtTransactionDescription = new System.Windows.Forms.TextBox();
            this.lblTransactionDescription = new System.Windows.Forms.Label();
            this.txtTransactionAmount = new System.Windows.Forms.TextBox();
            this.lblTransactionAmount = new System.Windows.Forms.Label();
            this.txtDestinationCpf = new System.Windows.Forms.TextBox();
            this.lblDestinationCpf = new System.Windows.Forms.Label();
            this.tabFraudHistory = new System.Windows.Forms.TabPage();
            this.dgvFraudHistory = new System.Windows.Forms.DataGridView();
            this.mainTabs.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.tabCadastro.SuspendLayout();
            this.tabTransactionHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactionHistory)).BeginInit();
            this.tabProcessTransaction.SuspendLayout();
            this.tabFraudHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFraudHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabs
            // 
            this.mainTabs.Controls.Add(this.tabDashboard);
            this.mainTabs.Controls.Add(this.tabCadastro);
            this.mainTabs.Controls.Add(this.tabTransactionHistory);
            this.mainTabs.Controls.Add(this.tabProcessTransaction);
            this.mainTabs.Controls.Add(this.tabFraudHistory);
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mainTabs.Location = new System.Drawing.Point(0, 0);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(1000, 600);
            this.mainTabs.TabIndex = 0;
            // 
            // tabDashboard
            // 
            this.tabDashboard.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.tabDashboard.Controls.Add(this.lblDashboardSummary);
            this.tabDashboard.Controls.Add(this.lblDashboardTitle);
            this.tabDashboard.Location = new System.Drawing.Point(4, 30);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabDashboard.Size = new System.Drawing.Size(992, 566);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "Dashboard";
            // 
            // lblDashboardSummary
            // 
            this.lblDashboardSummary.ForeColor = System.Drawing.Color.White;
            this.lblDashboardSummary.Location = new System.Drawing.Point(24, 88);
            this.lblDashboardSummary.Name = "lblDashboardSummary";
            this.lblDashboardSummary.Size = new System.Drawing.Size(940, 120);
            this.lblDashboardSummary.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDashboardSummary.Text = "Bem-vindo ao painel de controle. Aqui você verá o estado dos usuários, transações e fraudes detectadas.";
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.White;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDashboardTitle.Location = new System.Drawing.Point(24, 24);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(400, 40);
            this.lblDashboardTitle.Text = "Painel de Controle";
            // 
            // tabCadastro
            // 
            this.tabCadastro.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.tabCadastro.Controls.Add(this.panelRegisterHost);
            this.tabCadastro.Location = new System.Drawing.Point(4, 30);
            this.tabCadastro.Name = "tabCadastro";
            this.tabCadastro.Padding = new System.Windows.Forms.Padding(3);
            this.tabCadastro.Size = new System.Drawing.Size(992, 566);
            this.tabCadastro.TabIndex = 1;
            this.tabCadastro.Text = "Cadastro";
            // 
            // panelRegisterHost
            // 
            this.panelRegisterHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRegisterHost.Location = new System.Drawing.Point(3, 3);
            this.panelRegisterHost.Name = "panelRegisterHost";
            this.panelRegisterHost.Size = new System.Drawing.Size(986, 560);
            this.panelRegisterHost.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            // 
            // tabTransactionHistory
            // 
            this.tabTransactionHistory.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.tabTransactionHistory.Controls.Add(this.dgvTransactionHistory);
            this.tabTransactionHistory.Controls.Add(this.btnSearchPurchase);
            this.tabTransactionHistory.Controls.Add(this.txtSearchPurchaseCode);
            this.tabTransactionHistory.Controls.Add(this.lblSearchPurchaseCode);
            this.tabTransactionHistory.Location = new System.Drawing.Point(4, 30);
            this.tabTransactionHistory.Name = "tabTransactionHistory";
            this.tabTransactionHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactionHistory.Size = new System.Drawing.Size(992, 566);
            this.tabTransactionHistory.TabIndex = 2;
            this.tabTransactionHistory.Text = "Histórico de Transações";
            // 
            // dgvTransactionHistory
            // 
            this.dgvTransactionHistory.AllowUserToAddRows = false;
            this.dgvTransactionHistory.AllowUserToDeleteRows = false;
            this.dgvTransactionHistory.BackgroundColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.dgvTransactionHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTransactionHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactionHistory.Location = new System.Drawing.Point(24, 88);
            this.dgvTransactionHistory.Name = "dgvTransactionHistory";
            this.dgvTransactionHistory.ReadOnly = true;
            this.dgvTransactionHistory.RowTemplate.Height = 29;
            this.dgvTransactionHistory.Size = new System.Drawing.Size(940, 460);
            this.dgvTransactionHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransactionHistory.GridColor = System.Drawing.Color.FromArgb(60, 60, 60);
            // 
            // btnSearchPurchase
            // 
            this.btnSearchPurchase.Text = "Pesquisar";
            this.btnSearchPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPurchase.ForeColor = System.Drawing.Color.White;
            this.btnSearchPurchase.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnSearchPurchase.Location = new System.Drawing.Point(550, 30);
            this.btnSearchPurchase.Size = new System.Drawing.Size(120, 36);
            this.btnSearchPurchase.Name = "btnSearchPurchase";
            this.btnSearchPurchase.Click += new System.EventHandler(this.btnSearchPurchase_Click);
            // 
            // txtSearchPurchaseCode
            // 
            this.txtSearchPurchaseCode.Location = new System.Drawing.Point(220, 36);
            this.txtSearchPurchaseCode.Size = new System.Drawing.Size(300, 30);
            this.txtSearchPurchaseCode.Name = "txtSearchPurchaseCode";
            this.txtSearchPurchaseCode.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.txtSearchPurchaseCode.ForeColor = System.Drawing.Color.White;
            this.txtSearchPurchaseCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblSearchPurchaseCode
            // 
            this.lblSearchPurchaseCode.ForeColor = System.Drawing.Color.White;
            this.lblSearchPurchaseCode.Location = new System.Drawing.Point(24, 36);
            this.lblSearchPurchaseCode.Size = new System.Drawing.Size(180, 30);
            this.lblSearchPurchaseCode.Name = "lblSearchPurchaseCode";
            this.lblSearchPurchaseCode.Text = "Código da Compra:";
            // 
            // tabProcessTransaction
            // 
            this.tabProcessTransaction.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.tabProcessTransaction.Controls.Add(this.lblTransactionResult);
            this.tabProcessTransaction.Controls.Add(this.btnProcessTransaction);
            this.tabProcessTransaction.Controls.Add(this.txtTransactionDescription);
            this.tabProcessTransaction.Controls.Add(this.lblTransactionDescription);
            this.tabProcessTransaction.Controls.Add(this.txtTransactionAmount);
            this.tabProcessTransaction.Controls.Add(this.lblTransactionAmount);
            this.tabProcessTransaction.Controls.Add(this.txtDestinationCpf);
            this.tabProcessTransaction.Controls.Add(this.lblDestinationCpf);
            this.tabProcessTransaction.Location = new System.Drawing.Point(4, 30);
            this.tabProcessTransaction.Name = "tabProcessTransaction";
            this.tabProcessTransaction.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcessTransaction.Size = new System.Drawing.Size(992, 566);
            this.tabProcessTransaction.TabIndex = 3;
            this.tabProcessTransaction.Text = "Realizar Transação";
            // 
            // lblDestinationCpf
            // 
            this.lblDestinationCpf.ForeColor = System.Drawing.Color.White;
            this.lblDestinationCpf.Location = new System.Drawing.Point(24, 32);
            this.lblDestinationCpf.Size = new System.Drawing.Size(220, 30);
            this.lblDestinationCpf.Name = "lblDestinationCpf";
            this.lblDestinationCpf.Text = "CPF do Destinatário:";
            // 
            // txtDestinationCpf
            // 
            this.txtDestinationCpf.Location = new System.Drawing.Point(24, 64);
            this.txtDestinationCpf.Size = new System.Drawing.Size(320, 30);
            this.txtDestinationCpf.Name = "txtDestinationCpf";
            this.txtDestinationCpf.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.txtDestinationCpf.ForeColor = System.Drawing.Color.White;
            this.txtDestinationCpf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblTransactionAmount
            // 
            this.lblTransactionAmount.ForeColor = System.Drawing.Color.White;
            this.lblTransactionAmount.Location = new System.Drawing.Point(24, 112);
            this.lblTransactionAmount.Size = new System.Drawing.Size(220, 30);
            this.lblTransactionAmount.Name = "lblTransactionAmount";
            this.lblTransactionAmount.Text = "Valor da Transação:";
            // 
            // txtTransactionAmount
            // 
            this.txtTransactionAmount.Location = new System.Drawing.Point(24, 144);
            this.txtTransactionAmount.Size = new System.Drawing.Size(200, 30);
            this.txtTransactionAmount.Name = "txtTransactionAmount";
            this.txtTransactionAmount.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.txtTransactionAmount.ForeColor = System.Drawing.Color.White;
            this.txtTransactionAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblTransactionDescription
            // 
            this.lblTransactionDescription.ForeColor = System.Drawing.Color.White;
            this.lblTransactionDescription.Location = new System.Drawing.Point(24, 192);
            this.lblTransactionDescription.Size = new System.Drawing.Size(220, 30);
            this.lblTransactionDescription.Name = "lblTransactionDescription";
            this.lblTransactionDescription.Text = "Descrição da Transação:";
            // 
            // txtTransactionDescription
            // 
            this.txtTransactionDescription.Location = new System.Drawing.Point(24, 224);
            this.txtTransactionDescription.Size = new System.Drawing.Size(560, 30);
            this.txtTransactionDescription.Name = "txtTransactionDescription";
            this.txtTransactionDescription.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.txtTransactionDescription.ForeColor = System.Drawing.Color.White;
            this.txtTransactionDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // btnProcessTransaction
            // 
            this.btnProcessTransaction.Text = "Enviar Pagamento";
            this.btnProcessTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessTransaction.ForeColor = System.Drawing.Color.White;
            this.btnProcessTransaction.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnProcessTransaction.Location = new System.Drawing.Point(24, 280);
            this.btnProcessTransaction.Size = new System.Drawing.Size(160, 40);
            this.btnProcessTransaction.Name = "btnProcessTransaction";
            this.btnProcessTransaction.Click += new System.EventHandler(this.btnProcessTransaction_Click);
            // 
            // lblTransactionResult
            // 
            this.lblTransactionResult.ForeColor = System.Drawing.Color.White;
            this.lblTransactionResult.Location = new System.Drawing.Point(24, 340);
            this.lblTransactionResult.Size = new System.Drawing.Size(760, 120);
            this.lblTransactionResult.Name = "lblTransactionResult";
            this.lblTransactionResult.Text = "Informe o CPF destinatário, valor e descrição, depois envie o pagamento.";
            // 
            // tabFraudHistory
            // 
            this.tabFraudHistory.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.tabFraudHistory.Controls.Add(this.dgvFraudHistory);
            this.tabFraudHistory.Location = new System.Drawing.Point(4, 30);
            this.tabFraudHistory.Name = "tabFraudHistory";
            this.tabFraudHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabFraudHistory.Size = new System.Drawing.Size(992, 566);
            this.tabFraudHistory.TabIndex = 4;
            this.tabFraudHistory.Text = "Fraudes Detectadas";
            // 
            // dgvFraudHistory
            // 
            this.dgvFraudHistory.AllowUserToAddRows = false;
            this.dgvFraudHistory.AllowUserToDeleteRows = false;
            this.dgvFraudHistory.BackgroundColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.dgvFraudHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFraudHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFraudHistory.Location = new System.Drawing.Point(24, 24);
            this.dgvFraudHistory.Name = "dgvFraudHistory";
            this.dgvFraudHistory.ReadOnly = true;
            this.dgvFraudHistory.RowTemplate.Height = 29;
            this.dgvFraudHistory.Size = new System.Drawing.Size(940, 520);
            this.dgvFraudHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFraudHistory.GridColor = System.Drawing.Color.FromArgb(60, 60, 60);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.mainTabs);
            this.Text = "Fraud Detection - Interface";
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.mainTabs.ResumeLayout(false);
            this.tabDashboard.ResumeLayout(false);
            this.tabCadastro.ResumeLayout(false);
            this.tabTransactionHistory.ResumeLayout(false);
            this.tabTransactionHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactionHistory)).EndInit();
            this.tabProcessTransaction.ResumeLayout(false);
            this.tabProcessTransaction.PerformLayout();
            this.tabFraudHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFraudHistory)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
