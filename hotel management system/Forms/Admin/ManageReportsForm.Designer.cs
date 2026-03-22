using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class ManageReportsForm
    {
        private Label lblTitle;
        private DataGridView dgvReport;
        private HopeButton btnGenerate;
        private HopeButton btnExport;
        private HopeButton btnBack;
        private DateTimePicker dtStart;
        private DateTimePicker dtEnd;
        private Label lblFrom;
        private Label lblTo;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel actionPanel;
        private System.Windows.Forms.Panel mainPanel;

        private void InitializeComponent()
        {
            SuspendLayout();

            // === FORM ===
            this.Text = "Reports";
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
                Text = "Hotel Reports",
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
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(Width - 300, 15)
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(btnBack);

            // === ACTION PANEL ===
            actionPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(30, 33, 45)
            };

            lblFrom = new Label
            {
                Text = "From:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(40, 25),
                AutoSize = true
            };

            dtStart = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(90, 20),
                Size = new Size(130, 30)
            };

            lblTo = new Label
            {
                Text = "To:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(240, 25),
                AutoSize = true
            };

            dtEnd = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(270, 20),
                Size = new Size(130, 30)
            };

            btnGenerate = new HopeButton
            {
                Text = "Generate Report",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(60, 200, 120),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(430, 18)
            };

            btnExport = new HopeButton
            {
                Text = "Export to Excel",
                Size = new Size(180, 40),
                PrimaryColor = Color.FromArgb(255, 180, 60),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            actionPanel.Controls.AddRange(new Control[] { lblFrom, dtStart, lblTo, dtEnd, btnGenerate, btnExport });

            // === MAIN PANEL ===
            mainPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 33, 45)
            };

            dgvReport = new DataGridView
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

            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 90, 130);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgvReport.DefaultCellStyle.BackColor = Color.FromArgb(35, 40, 55);
            dgvReport.DefaultCellStyle.ForeColor = Color.White;
            dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 120, 255);
            dgvReport.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            mainPanel.Controls.Add(dgvReport);

            // === ADD CONTROLS ===
            Controls.Add(mainPanel);
            Controls.Add(actionPanel);
            Controls.Add(headerPanel);

            ResumeLayout(false);
        }
    }
}
