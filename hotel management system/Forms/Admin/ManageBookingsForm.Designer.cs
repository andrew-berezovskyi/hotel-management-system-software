using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class ManageBookingsForm
    {
        private Label lblTitle;
        private DataGridView dgvBookings;
        private HopeButton btnAdd;
        private HopeButton btnEdit;
        private HopeButton btnDelete;
        private HopeButton btnRefresh;
        private HopeButton btnBack;
        private HopeTextBox txtSearch;
        private HopeButton btnSearch;
        private HopeButton btnClear;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel actionPanel;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // === Form ===
            this.Text = "Manage Bookings";
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // === Header ===
            headerPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(25, 28, 38)
            };

            lblTitle = new Label
            {
                Text = "Manage Bookings",
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

            // === Search Panel ===
            searchPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(30, 33, 45)
            };

            txtSearch = new HopeTextBox
            {
                Hint = "Search by guest or room...",
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

            // === Main Panel ===
            mainPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 33, 45)
            };

            dgvBookings = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.FromArgb(35, 40, 55),
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                EnableHeadersVisualStyles = false
            };

            dgvBookings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 90, 130);
            dgvBookings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBookings.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgvBookings.DefaultCellStyle.BackColor = Color.FromArgb(35, 40, 55);
            dgvBookings.DefaultCellStyle.ForeColor = Color.White;
            dgvBookings.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 120, 255);
            dgvBookings.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvBookings.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            mainPanel.Controls.Add(dgvBookings);

            // === Action Panel ===
            actionPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(25, 28, 38)
            };

            btnAdd = new HopeButton { Text = "Add Booking", Size = new Size(160, 40), PrimaryColor = Color.FromArgb(50, 120, 255), Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold), Location = new Point(30, 20) };
            btnEdit = new HopeButton { Text = "Edit Booking", Size = new Size(160, 40), PrimaryColor = Color.FromArgb(80, 140, 255), Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold), Location = new Point(220, 20) };
            btnDelete = new HopeButton { Text = "Cancel Booking", Size = new Size(160, 40), PrimaryColor = Color.FromArgb(200, 60, 60), Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold), Location = new Point(410, 20) };
            btnRefresh = new HopeButton { Text = "Refresh", Size = new Size(160, 40), PrimaryColor = Color.FromArgb(60, 200, 120), Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold), Location = new Point(600, 20) };

            actionPanel.Controls.Add(btnAdd);
            actionPanel.Controls.Add(btnEdit);
            actionPanel.Controls.Add(btnDelete);
            actionPanel.Controls.Add(btnRefresh);

            // === Add Panels ===
            this.Controls.Add(mainPanel);
            this.Controls.Add(actionPanel);
            this.Controls.Add(searchPanel);
            this.Controls.Add(headerPanel);

            this.ResumeLayout(false);
        }
    }
}
