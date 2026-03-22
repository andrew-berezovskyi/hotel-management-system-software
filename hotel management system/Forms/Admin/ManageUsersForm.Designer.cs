using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class ManageUsersForm
    {
        private Label lblTitle;
        private DataGridView dgvUsers;
        private HopeButton btnAddUser;
        private HopeButton btnEditUser;
        private HopeButton btnDeleteUser;
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

            // ---------------- FORM ----------------
            this.Text = "Manage Users";
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // ← було None, тепер як у ManageRoomsForm
            this.StartPosition = FormStartPosition.CenterScreen;


            // ---------------- HEADER PANEL ----------------
            headerPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(25, 28, 38)
            };

            lblTitle = new Label
            {
                Text = "Manage Users",
                Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Left,
                Padding = new Padding(30, 0, 0, 0),
                Width = 500
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

            // ---------------- SEARCH PANEL ----------------
            searchPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(30, 33, 45)
            };

            txtSearch = new HopeTextBox
            {
                Hint = "Search users...",
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

            // ---------------- MAIN PANEL ----------------
            mainPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 33, 45)
            };

            dgvUsers = new DataGridView
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

            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 90, 130);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgvUsers.DefaultCellStyle.BackColor = Color.FromArgb(35, 40, 55);
            dgvUsers.DefaultCellStyle.ForeColor = Color.White;
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 120, 255);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            mainPanel.Controls.Add(dgvUsers);

            // ---------------- ACTION PANEL ----------------
            actionPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(25, 28, 38)
            };

            btnAddUser = new HopeButton
            {
                Text = "Add User",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(50, 120, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Margin = new Padding(10),
                Location = new Point(30, 20)
            };

            btnEditUser = new HopeButton
            {
                Text = "Edit User",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(80, 140, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Margin = new Padding(10),
                Location = new Point(220, 20)
            };

            btnDeleteUser = new HopeButton
            {
                Text = "Delete User",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(200, 60, 60),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Margin = new Padding(10),
                Location = new Point(410, 20)
            };

            btnRefresh = new HopeButton
            {
                Text = "Refresh",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(60, 200, 120),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Margin = new Padding(10),
                Location = new Point(600, 20)
            };

            actionPanel.Controls.Add(btnAddUser);
            actionPanel.Controls.Add(btnEditUser);
            actionPanel.Controls.Add(btnDeleteUser);
            actionPanel.Controls.Add(btnRefresh);

            // ---------------- ADD CONTROLS ----------------
            this.Controls.Add(mainPanel);
            this.Controls.Add(actionPanel);
            this.Controls.Add(searchPanel);
            this.Controls.Add(headerPanel);

            this.ResumeLayout(false);
        }
    }
}




