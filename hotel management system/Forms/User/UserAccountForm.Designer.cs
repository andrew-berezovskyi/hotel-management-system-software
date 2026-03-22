using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class UserAccountForm
    {
        private Label lblTitle;
        private Label lblUserInfo;
        private DataGridView dgvBookings;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel actionPanel;
        private HopeButton btnBack;
        private HopeButton btnRefresh;
        private HopeButton btnCancelBooking;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ===== FORM =====
            this.Text = "My Account";
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // ===== HEADER PANEL =====
            headerPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(25, 28, 38)
            };

            // Заголовок зліва (як у ManageRooms)
            lblTitle = new Label
            {
                Text = "My Account",
                Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Width = 400,
                Padding = new Padding(30, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Інформація про користувача (логін, email, телефон) праворуч
            lblUserInfo = new Label
            {
                Text = "", // буде заповнено в .cs
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.Gainsboro,
                AutoSize = true,
                Location = new Point(450, 26),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // Маленька кнопка Back справа, як у ManageRooms
            btnBack = new HopeButton
            {
                Text = "← Back",
                PrimaryColor = Color.FromArgb(80, 120, 255),
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(120, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.ClientSize.Width - 150, 15)
            };

            // Щоб Anchor правильно відпрацював після створення форми
            headerPanel.SizeChanged += (_, __) =>
            {
                // 20 px відступ справа
                btnBack.Location = new Point(headerPanel.ClientSize.Width - btnBack.Width - 20, 15);
                // лейбл з інфою тримаємо трохи лівіше від кнопки
                lblUserInfo.Location = new Point(btnBack.Left - lblUserInfo.Width - 20, 26);
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblUserInfo);
            headerPanel.Controls.Add(btnBack);

            // ===== MAIN PANEL =====
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

            // ===== ACTION PANEL (кнопки знизу) =====
            actionPanel = new System.Windows.Forms.Panel
            {   
                Dock = DockStyle.Bottom,
                Height = 80,
                Padding = new Padding(30, 10, 30, 10),
                BackColor = Color.FromArgb(25, 28, 38)
            };

            btnCancelBooking = new HopeButton
            {
                Text = "Cancel Booking",
                Size = new Size(170, 40),
                PrimaryColor = Color.FromArgb(200, 60, 60),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(30, 20)
            };

            btnRefresh = new HopeButton
            {
                Text = "Refresh",
                Size = new Size(150, 40),
                PrimaryColor = Color.FromArgb(60, 200, 120),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(220, 20)
            };

            actionPanel.Controls.Add(btnCancelBooking);
            actionPanel.Controls.Add(btnRefresh);

            // ===== ADD TO FORM =====
            this.Controls.Add(mainPanel);
            this.Controls.Add(actionPanel);
            this.Controls.Add(headerPanel);

            this.ResumeLayout(false);
        }
    }
}
