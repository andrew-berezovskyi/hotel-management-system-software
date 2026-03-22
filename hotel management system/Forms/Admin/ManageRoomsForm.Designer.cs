using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class ManageRoomsForm
    {
        private Label lblTitle;
        private DataGridView dgvRooms;
        private HopeButton btnAddRoom;
        private HopeButton btnEditRoom;
        private HopeButton btnDeleteRoom;
        private HopeButton btnRefresh;
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

            // ---------------- FORM ----------------
            this.Text = "Manage Rooms";
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
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
                Text = "Manage Rooms",
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
                Hint = "Search rooms...",
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

            dgvRooms = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.FromArgb(35, 40, 55),
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowTemplate = { Height = 120 },
                MultiSelect = false,
                EnableHeadersVisualStyles = false
            };

            dgvRooms.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 90, 130);
            dgvRooms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRooms.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgvRooms.DefaultCellStyle.BackColor = Color.FromArgb(35, 40, 55);
            dgvRooms.DefaultCellStyle.ForeColor = Color.White;
            dgvRooms.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 120, 255);
            dgvRooms.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvRooms.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            // ---------- ADDING COLUMNS ----------
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", Name = "Id", Width = 50 });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Number", Name = "Number", Width = 100 });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Type", Name = "Type", Width = 150 });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Price ($)", Name = "Price", Width = 100 });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", Name = "Status", Width = 120 });
            dgvRooms.Columns.Add(new DataGridViewImageColumn
            {
                HeaderText = "Image",
                Name = "Image",
                Width = 160,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            });

            mainPanel.Controls.Add(dgvRooms);

            // ---------------- ACTION PANEL ----------------
            actionPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(25, 28, 38)
            };

            btnAddRoom = new HopeButton
            {
                Text = "Add Room",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(50, 120, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(30, 20)
            };

            btnEditRoom = new HopeButton
            {
                Text = "Edit Room",
                Size = new Size(160, 40),
                PrimaryColor = Color.FromArgb(80, 140, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(220, 20)
            };

            btnDeleteRoom = new HopeButton
            {
                Text = "Delete Room",
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

            actionPanel.Controls.Add(btnAddRoom);
            actionPanel.Controls.Add(btnEditRoom);
            actionPanel.Controls.Add(btnDeleteRoom);
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
