using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace HotelManagementSystem.Forms
{
    partial class UserDashboardForm
    {
        private Label lblHotelName;
        private HopeButton btnRooms;
        private HopeButton btnBooking;
        private HopeButton btnAccount;
        private HopeButton btnContacts;
        private HopeButton btnLogout;

        private void InitializeComponent()
        {
            this.lblHotelName = new Label();
            this.btnRooms = new HopeButton();
            this.btnBooking = new HopeButton();
            this.btnAccount = new HopeButton();
            this.btnContacts = new HopeButton();
            this.btnLogout = new HopeButton();

            this.SuspendLayout();

            // ======== Заголовок ========
            this.lblHotelName.Font = new Font("Georgia", 32F, FontStyle.Bold);
            this.lblHotelName.ForeColor = Color.White;
            this.lblHotelName.BackColor = Color.Transparent;
            this.lblHotelName.Location = new Point(0, 40);
            this.lblHotelName.Size = new Size(1000, 70);
            this.lblHotelName.Text = "The Hotel Kyiv";
            this.lblHotelName.TextAlign = ContentAlignment.MiddleCenter;

            // ======== КНОПКИ (НОВИЙ ПОРЯДОК) ========
            // My Account – на місці старого Rooms (y = 160)
            HopeButtonStyle(this.btnAccount, "My Account", 160);
            // Booking – як було
            HopeButtonStyle(this.btnBooking, "Booking", 250);
            // Rooms – переносимо на місце Services (340)
            HopeButtonStyle(this.btnRooms, "Rooms", 340);
            // Contacts – залишається внизу
            HopeButtonStyle(this.btnContacts, "Contacts", 430);

            // ======== Logout ========
            Color logoutColor = Color.FromArgb(160, 40, 50);
            Color logoutHover = Color.FromArgb(190, 60, 70);

            this.btnLogout.Text = "Logout";
            this.btnLogout.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            this.btnLogout.PrimaryColor = logoutColor;
            this.btnLogout.Location = new Point(830, 600);
            this.btnLogout.Size = new Size(120, 40);
            this.btnLogout.ForeColor = Color.White;

            this.btnLogout.MouseEnter += (s, e) => this.btnLogout.PrimaryColor = logoutHover;
            this.btnLogout.MouseLeave += (s, e) => this.btnLogout.PrimaryColor = logoutColor;

            // ======== FORM ========
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.ClientSize = new Size(1000, 700);
            this.Controls.Add(this.lblHotelName);
            this.Controls.Add(this.btnRooms);
            this.Controls.Add(this.btnBooking);
            this.Controls.Add(this.btnAccount);
            this.Controls.Add(this.btnContacts);
            this.Controls.Add(this.btnLogout);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "User Dashboard - The Hotel Kyiv";

            this.ResumeLayout(false);
        }

        private void HopeButtonStyle(HopeButton button, string text, int y)
        {
            Color mainColor = Color.FromArgb(80, 120, 255);
            Color hoverColor = Color.FromArgb(100, 140, 255);

            button.Text = text;
            button.Size = new Size(280, 60);
            button.Location = new Point(360, y);
            button.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            button.PrimaryColor = mainColor;
            button.ForeColor = Color.White;

            button.MouseEnter += (s, e) => button.PrimaryColor = hoverColor;
            button.MouseLeave += (s, e) => button.PrimaryColor = mainColor;
        }
    }
}
