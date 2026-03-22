using hotel_management_system.Forms;
using hotel_management_system.Helpers;
using hotel_management_system.UIHelpers;


namespace HotelManagementSystem.Forms
{
    public partial class UserDashboardForm : Form
    {
        public UserDashboardForm()
        {
            InitializeComponent();


            FormAnimationHelper.AttachFadeInOnVisible(this);

            // Закриття всієї програми при закритті головного юзер-дешборду
            NavigationHelper.AttachExitOnClose(this);

            btnRooms.Click += (_, __) => NavigationHelper.OpenChild(this, new UserRoomsForm());
            btnBooking.Click += (_, __) => NavigationHelper.OpenChild(this, new UserRoomsForm()); // той самий список

            btnAccount.Click += (_, __) =>
            {
                if (AuthHelper.CurrentUser == null)
                {
                    ToastHelper.ShowToast(this, "Please log in again.", ToastType.Error);
                    NavigationHelper.LogoutToLogin(this);
                    return;
                }

                NavigationHelper.OpenChild(this, new UserAccountForm());
            };

            btnContacts.Click += (_, __) => NavigationHelper.OpenChild(this, new UserContactsForm());
            btnLogout.Click += BtnLogout_Click;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                ToastHelper.ShowToast(this, "Logging out...", ToastType.Info);
                NavigationHelper.LogoutToLogin(this);
            }
            else
            {
                ToastHelper.ShowToast(this, "Logout cancelled.", ToastType.Info);
            }
        }
    }
}
