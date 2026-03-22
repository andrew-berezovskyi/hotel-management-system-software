using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class AdminDashboardForm
    {
        private Label lblTitle;
        private HopeButton btnManageRooms;
        private HopeButton btnManageUsers;
        private HopeButton btnManageBookings;
        private HopeButton btnPayments;
        private HopeButton btnManageStaff;
        private HopeButton btnHotelInfo;
        private HopeButton btnReports;
        private HopeButton btnSettings;
        private HopeButton btnLogout;
        private HopeButton btnVoiceToggle;   // 🔥 ДОДАНО

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.btnManageRooms = new HopeButton();
            this.btnManageUsers = new HopeButton();
            this.btnManageBookings = new HopeButton();
            this.btnPayments = new HopeButton();
            this.btnManageStaff = new HopeButton();
            this.btnHotelInfo = new HopeButton();
            this.btnReports = new HopeButton();
            this.btnSettings = new HopeButton();
            this.btnLogout = new HopeButton();
            this.btnVoiceToggle = new HopeButton();   // 🔥 ДОДАНО

            this.SuspendLayout();

            // ---------------- TITLE ----------------
            this.lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Text = "Hotel Admin Dashboard";
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 70;

            // ----------- COMMON BUTTON STYLE ----------
            Color mainColor = Color.FromArgb(80, 120, 255);
            Color hoverColor = Color.FromArgb(100, 140, 255);
            Size btnSize = new Size(280, 60);
            Font btnFont = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);

            // ---------------- BUTTONS ----------------
            this.btnManageRooms = CreateHopeButton("Manage Rooms", new Point(60, 100), btnSize, btnFont, mainColor, hoverColor);
            this.btnManageRooms.Click += new EventHandler(this.btnManageRooms_Click);

            this.btnManageUsers = CreateHopeButton("Manage Users", new Point(400, 100), btnSize, btnFont, mainColor, hoverColor);
            this.btnManageUsers.Click += new EventHandler(this.btnManageUsers_Click);

            this.btnManageBookings = CreateHopeButton("Manage Bookings", new Point(60, 190), btnSize, btnFont, mainColor, hoverColor);
            this.btnManageBookings.Click += new EventHandler(this.btnManageBookings_Click);

            this.btnPayments = CreateHopeButton("Manage Payments", new Point(400, 190), btnSize, btnFont, mainColor, hoverColor);
            this.btnPayments.Click += new EventHandler(this.btnPayments_Click);

            this.btnManageStaff = CreateHopeButton("Manage Staff", new Point(60, 280), btnSize, btnFont, mainColor, hoverColor);
            this.btnManageStaff.Click += new EventHandler(this.btnManageStaff_Click);

            this.btnHotelInfo = CreateHopeButton("Hotel Info", new Point(400, 280), btnSize, btnFont, mainColor, hoverColor);
            this.btnHotelInfo.Click += new EventHandler(this.btnHotelInfo_Click);

            this.btnReports = CreateHopeButton("Manage Reports", new Point(60, 370), btnSize, btnFont, mainColor, hoverColor);
            this.btnReports.Click += new EventHandler(this.btnReports_Click);

            this.btnSettings = CreateHopeButton("System Settings", new Point(400, 370), btnSize, btnFont, mainColor, hoverColor);
            this.btnSettings.Click += new EventHandler(this.btnSettings_Click);

            // ---------------- Voice Toggle Button ----------------
            this.btnVoiceToggle = CreateHopeButton(
                "Voice: OFF",
                new Point(440, 470),
                new Size(140, 40),
                new Font("Segoe UI", 10F, FontStyle.Bold),
                Color.FromArgb(120, 120, 120),        // сірий – вимкнено
                Color.FromArgb(150, 150, 150)
            );
            this.btnVoiceToggle.Click += new EventHandler(this.btnVoiceToggle_Click);

            // ---------------- LOGOUT button ----------------
            this.btnLogout = CreateHopeButton(
                "Logout",
                new Point(600, 470),
                new Size(120, 40),
                new Font("Segoe UI", 10F, FontStyle.Bold),
                Color.FromArgb(220, 70, 70),
                Color.FromArgb(240, 90, 90)
            );
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);

            // ---------------- FORM ----------------
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.ClientSize = new Size(784, 561);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnManageRooms);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.btnManageBookings);
            this.Controls.Add(this.btnPayments);
            this.Controls.Add(this.btnManageStaff);
            this.Controls.Add(this.btnHotelInfo);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnVoiceToggle);  // 🔥 ВАЖЛИВО
            this.Controls.Add(this.btnLogout);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard";

            this.ResumeLayout(false);
        }

        // ---------------- BUTTON CREATOR ----------------
        private HopeButton CreateHopeButton(string text, Point location, Size size, Font font, Color color, Color hoverColor)
        {
            var btn = new HopeButton
            {
                Text = text,
                Location = location,
                Size = size,
                Font = font,
                PrimaryColor = color,
                BorderColor = color,
                Cursor = Cursors.Hand,
                ForeColor = Color.White
            };

            btn.MouseEnter += (s, e) => btn.PrimaryColor = hoverColor;
            btn.MouseLeave += (s, e) => btn.PrimaryColor = color;

            return btn;
        }
    }
}
