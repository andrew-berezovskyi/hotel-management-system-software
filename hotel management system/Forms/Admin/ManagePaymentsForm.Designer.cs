using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class ManagePaymentsForm
    {
        private Label lblTitle;
        private DataGridView dgvPayments;
        private HopeButton btnAddPayment;
        private HopeButton btnEditPayment;
        private HopeButton btnDeletePayment;
        private HopeButton btnRefresh;
        private HopeButton btnExport;
        private HopeButton btnBack;
        private HopeTextBox txtSearch;
        private HopeButton btnSearch;
        private HopeButton btnClear;

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Panel actionPanel;
        private System.Windows.Forms.Panel mainPanel;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // === FORM ===
            this.Text = "Manage Payments";
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // === HEADER PANEL ===
            headerPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(25, 28, 38)
            };

            lblTitle = new Label
            {
                Text = "Manage Payments",
                Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Width = 500,
                Padding = new Padding(30, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnBack = new HopeButton
            {
                Text = "← Back",
                PrimaryColor = Color.FromArgb(80, 120, 255),
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(120, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            //btnBack.Click += new EventHandler(this.btnBack_Click);

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(btnBack);

            // === SEARCH PANEL ===
            searchPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(30, 33, 45)
            };

            txtSearch = new HopeTextBox
            {
                Hint = "Search payments...",
                Font = new Font("Segoe UI", 10F),
                Size = new Size(250, 40),
                BorderColorA = Color.FromArgb(80, 120, 255),
                BorderColorB = Color.FromArgb(50, 80, 200),
                Location = new Point(0, 15)
            };

            btnSearch = new HopeButton
            {
                Text = "Search",
                Size = new Size(100, 38),
                Location = new Point(270, 15),
                PrimaryColor = Color.FromArgb(80, 120, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold)
            };

            btnClear = new HopeButton
            {
                Text = "Clear",
                Size = new Size(100, 38),
                Location = new Point(380, 15),
                PrimaryColor = Color.FromArgb(120, 120, 120),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold)
            };

            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(btnSearch);
            searchPanel.Controls.Add(btnClear);

            // === MAIN PANEL ===
            mainPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 33, 45)
            };

            dgvPayments = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.FromArgb(35, 40, 55),
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowTemplate = { Height = 40 },
                MultiSelect = false,
                EnableHeadersVisualStyles = false
            };

            dgvPayments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 90, 130);
            dgvPayments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPayments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgvPayments.DefaultCellStyle.BackColor = Color.FromArgb(35, 40, 55);
            dgvPayments.DefaultCellStyle.ForeColor = Color.White;
            dgvPayments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 120, 255);
            dgvPayments.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPayments.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            mainPanel.Controls.Add(dgvPayments);

            // === ACTION PANEL ===
            actionPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(25, 28, 38)
            };

            btnAddPayment = new HopeButton
            {
                Text = "Add Payment",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(50, 120, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(30, 20)
            };

            btnEditPayment = new HopeButton
            {
                Text = "Edit Payment",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(80, 140, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(220, 20)
            };

            btnDeletePayment = new HopeButton
            {
                Text = "Delete Payment",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(200, 60, 60),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(410, 20)
            };

            btnRefresh = new HopeButton
            {
                Text = "Refresh",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(60, 200, 120),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(600, 20)
            };

            // === EXPORT BUTTON ===
            btnExport = new HopeButton
            {
                Text = "Export to Excel",
                Size = new Size(180, 40),
                PrimaryColor = Color.FromArgb(255, 180, 60),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            actionPanel.Controls.Add(btnAddPayment);
            actionPanel.Controls.Add(btnEditPayment);
            actionPanel.Controls.Add(btnDeletePayment);
            actionPanel.Controls.Add(btnRefresh);
            actionPanel.Controls.Add(btnExport);

            // === ADD CONTROLS ===
            this.Controls.Add(mainPanel);
            this.Controls.Add(actionPanel);
            this.Controls.Add(searchPanel);
            this.Controls.Add(headerPanel);

            // === EXPORT BUTTON POSITIONING ===
            this.Load += (s, e) =>
            {
                btnBack.Location = new Point(
                    headerPanel.ClientSize.Width - btnBack.Width - 30,
                    (headerPanel.Height - btnBack.Height) / 2
                );

                btnExport.Location = new Point(
                    actionPanel.ClientSize.Width - btnExport.Width - 30,
                    actionPanel.ClientSize.Height - btnExport.Height - 20
                );
            };

            this.Resize += (s, e) =>
            {
                btnBack.Location = new Point(
                    headerPanel.ClientSize.Width - btnBack.Width - 30,
                    (headerPanel.Height - btnBack.Height) / 2
                );

                btnExport.Location = new Point(
                    actionPanel.ClientSize.Width - btnExport.Width - 30,
                    actionPanel.ClientSize.Height - btnExport.Height - 20
                );
            };

            this.ResumeLayout(false);
        }
    }
}
