using hotel_management_system.Helpers;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Voice;

namespace hotel_management_system.Forms
{
    public partial class AdminDashboardForm : Form
    {
        private VoiceCommandService _voiceService;   // сервіс для голосу

        public AdminDashboardForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            // при закритті програми (через ❌)
            NavigationHelper.AttachExitOnClose(this);

            // ініціалізуємо голосове керування
            InitVoice();
        }

        private void btnVoiceToggle_Click(object sender, EventArgs e)
        {
            if (_voiceService == null)
                return;

            if (_voiceService.IsRunning)
            {
                // Вимикаємо
                _voiceService.Stop();
                btnVoiceToggle.Text = "Voice: OFF";
                btnVoiceToggle.PrimaryColor = Color.FromArgb(120, 120, 120); // сірий
                ToastHelper.ShowToast(this, "Voice control disabled", ToastType.Info);
            }
            else
            {
                // Вмикаємо
                _voiceService.Start();
                btnVoiceToggle.Text = "Voice: ON";
                btnVoiceToggle.PrimaryColor = Color.FromArgb(60, 180, 120); // зелений
                ToastHelper.ShowToast(this, "Voice control enabled", ToastType.Success);
            }
        }

        private void InitVoice()
        {
            try
            {
                _voiceService = new VoiceCommandService();
                _voiceService.CommandRecognized += VoiceService_CommandRecognized;
                _voiceService.StatusChanged += VoiceService_StatusChanged;
                _voiceService.ErrorOccurred += VoiceService_ErrorOccurred;

                // НЕ стартуємо тут, чекаємо натискання кнопки
                // _voiceService.Start();

                // Початковий текст на кнопці – мікрофон вимкнений
                btnVoiceToggle.Text = "Voice: OFF";
                btnVoiceToggle.PrimaryColor = Color.FromArgb(120, 120, 120);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Voice control could not be initialized:\n" + ex.Message,
                    "Voice control",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void VoiceService_StatusChanged(object sender, string status)
        {
            // Можеш тут потім виводити в статус-бар, поки просто ігноруємо або лог:
            // Console.WriteLine(status);
        }

        private void VoiceService_ErrorOccurred(object sender, string error)
        {
            // Аналогічно, поки можна логувати:
            // Console.WriteLine("VOICE ERROR: " + error);
        }

        private void VoiceService_CommandRecognized(object sender, VoiceCommandEventArgs e)
        {
            // Переходимо на UI-потік, щоб безпечно клікати кнопки
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => VoiceService_CommandRecognized(sender, e)));
                return;
            }

            switch (e.Command)
            {
                case VoiceCommandType.OpenRooms:
                    btnManageRooms_Click(btnManageRooms, EventArgs.Empty);
                    break;

                case VoiceCommandType.OpenUsers:
                    btnManageUsers_Click(btnManageUsers, EventArgs.Empty);
                    break;

                case VoiceCommandType.OpenBookings:
                    btnManageBookings_Click(btnManageBookings, EventArgs.Empty);
                    break;

                case VoiceCommandType.OpenPayments:
                    btnPayments_Click(btnPayments, EventArgs.Empty);
                    break;

                case VoiceCommandType.OpenStaff:
                    btnManageStaff_Click(btnManageStaff, EventArgs.Empty);
                    break;

                case VoiceCommandType.OpenHotelInfo:
                    btnHotelInfo_Click(btnHotelInfo, EventArgs.Empty);
                    break;

                case VoiceCommandType.OpenReports:
                    btnReports_Click(btnReports, EventArgs.Empty);
                    break;

                case VoiceCommandType.OpenSettings:
                    btnSettings_Click(btnSettings, EventArgs.Empty);
                    break;

                case VoiceCommandType.Logout:
                    btnLogout_Click(btnLogout, EventArgs.Empty);
                    break;

                case VoiceCommandType.None:
                default:
                    break;
            }
        }

       /* protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // акуратно відписуємося і гасимо голосовий сервіс
            if (_voiceService != null)
            {
                _voiceService.CommandRecognized -= VoiceService_CommandRecognized;
                _voiceService.StatusChanged -= VoiceService_StatusChanged;
                _voiceService.ErrorOccurred -= VoiceService_ErrorOccurred;
                _voiceService.Dispose();
                _voiceService = null;
            }

            base.OnFormClosing(e);
        }
       */

        private void btnManageRooms_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenChild(this, new ManageRoomsForm());
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenChild(this, new ManageUsersForm());
        }

        private void btnManageBookings_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenChild(this, new ManageBookingsForm());
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenChild(this, new ManagePaymentsForm());
        }

        private void btnManageStaff_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenChild(this, new ManageStaffForm());
        }

        private void btnHotelInfo_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenChild(this, new HotelInfoForm());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenChild(this, new ManageReportsForm());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenChild(this, new SystemSettingsForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ToastHelper.ShowToast(this, "Logging out...", ToastType.Info);
            NavigationHelper.LogoutToLogin(this);
        }
    }
}
