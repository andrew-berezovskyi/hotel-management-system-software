using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class UserRoomsForm
    {
        private Label lblTitle;
        private HopeButton btnBack;

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Panel contentPanel;

        private HopeTextBox txtSearch;
        private HopeComboBox cbType;
        private HopeComboBox cbStatus;
        private HopeButton btnFilter;
        private HopeButton btnClear;

        private FlowLayoutPanel flowRooms;

        /// <summary>
        ///  DO NOT edit in code-behind. This is the designer method.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // === FORM ===
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.ForeColor = Color.White;
            this.Text = "Rooms";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // === HEADER ===
            headerPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(25, 28, 38)
            };

            lblTitle = new Label
            {
                Text = "Rooms",
                Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Width = 400,
                Padding = new Padding(30, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnBack = new HopeButton
            {
                Text = "← Back",
                Size = new Size(120, 40),
                PrimaryColor = Color.FromArgb(80, 120, 255),
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.Width - 180, 15)
            };
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(btnBack);

            // === FILTER BAR ===
            filterPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.FromArgb(30, 33, 45),
                Padding = new Padding(30, 12, 30, 12)
            };

            txtSearch = new HopeTextBox
            {
                Hint = "Search (room number/type/price)…",
                Size = new Size(240, 40),
                Location = new Point(30, 16),
                BorderColorA = Color.FromArgb(80, 120, 255),
                BorderColorB = Color.FromArgb(50, 80, 200),
                Font = new Font("Segoe UI", 10F)
            };

            cbType = new HopeComboBox
            {
                Size = new Size(150, 40),
                Location = new Point(290, 16),
                Font = new Font("Segoe UI", 10F),
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbType.Items.AddRange(new object[] { "All types", "Standard Room", "Superior Room", "Deluxe Room", "Junior Suite", "Presidential Suite" });
            cbType.SelectedIndex = 0;

            cbStatus = new HopeComboBox
            {
                Size = new Size(150, 40),
                Location = new Point(450, 16),
                Font = new Font("Segoe UI", 10F),
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbStatus.Items.AddRange(new object[] { "Any status", "Available", "Booked", "Occupied", "Maintenance" });
            cbStatus.SelectedIndex = 0;

            btnFilter = new HopeButton
            {
                Text = "Filter",
                Size = new Size(110, 40),
                Location = new Point(610, 16),
                PrimaryColor = Color.FromArgb(80, 120, 255),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold)
            };

            btnClear = new HopeButton
            {
                Text = "Clear",
                Size = new Size(110, 40),
                Location = new Point(730, 16),
                PrimaryColor = Color.FromArgb(120, 120, 120),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold)
            };

            filterPanel.Controls.Add(txtSearch);
            filterPanel.Controls.Add(cbType);
            filterPanel.Controls.Add(cbStatus);
            filterPanel.Controls.Add(btnFilter);
            filterPanel.Controls.Add(btnClear);

            // === CONTENT ===
            contentPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 33, 45),
                Padding = new Padding(30)
            };

            flowRooms = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 33, 45),
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.LeftToRight
            };
            contentPanel.Controls.Add(flowRooms);

            // === ADD TO FORM ===
            this.Controls.Add(contentPanel);
            this.Controls.Add(filterPanel);
            this.Controls.Add(headerPanel);

            this.Resize += (_, __) =>
            {
                // keep Back button pinned
                btnBack.Location = new Point(this.ClientSize.Width - btnBack.Width - 30, 15);
            };

            this.ResumeLayout(false);
        }
    }
}
