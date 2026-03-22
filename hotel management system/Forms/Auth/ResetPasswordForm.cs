using hotel_management_system.Services;
using hotel_management_system.Helpers;
using hotel_management_system.UIHelpers;

namespace hotel_management_system.Forms
{
    public partial class ResetPasswordForm : Form
    {
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public ResetPasswordForm()
        {
            InitializeComponent();

            // 🔹 Плавна анімація появи форми (fade-in)
            FormAnimationHelper.AttachFadeInOnVisible(this);

            _userService = new UserService();
            _emailService = new EmailService();

            btnSendCode.Click += async (s, e) => await RunSafeAsync(btnSendCode, SendCodeAsync);
            btnResetPassword.Click += async (s, e) => await RunSafeAsync(btnResetPassword, ResetPasswordAsync);
            lnkBack.LinkClicked += (s, e) => NavigationHelper.SwitchToMain(this, new LoginForm());

            // Опціонально: Enter = підтвердження зміни пароля
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _ = RunSafeAsync(btnResetPassword, ResetPasswordAsync);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        // -------------------- ВІДПРАВКА КОДУ --------------------
        private async Task SendCodeAsync()
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                ToastHelper.ShowToast(this, "Enter your email address.", ToastType.Warning);
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                ToastHelper.ShowToast(this, "Invalid email format.", ToastType.Error);
                return;
            }

            try
            {
                string code = _userService.GenerateResetCode(email);
                if (code == null)
                {
                    ToastHelper.ShowToast(this, "No user found with this email.", ToastType.Error);
                    return;
                }

                bool sent = await _emailService.SendOTPAsync(email, code);
                if (sent)
                    ToastHelper.ShowToast(this, "Reset code sent to your email.", ToastType.Success);
                else
                    ToastHelper.ShowToast(this, "Failed to send email. Check your connection or email settings.", ToastType.Error);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(this, $"Unexpected error: {ex.Message}", ToastType.Error);
            }
        }

        // -------------------- ЗМІНА ПАРОЛЯ --------------------
        private async Task ResetPasswordAsync()
        {
            string email = txtEmail.Text.Trim();
            string code = txtOTP.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(code) || string.IsNullOrEmpty(newPass))
            {
                ToastHelper.ShowToast(this, "Please fill in all fields.", ToastType.Warning);
                return;
            }

            if (!ValidationHelper.IsStrongPassword(newPass))
            {
                ToastHelper.ShowToast(this, "Password must be at least 8 characters and contain letters and numbers.", ToastType.Warning);
                return;
            }

            try
            {
                bool valid = _userService.VerifyResetCode(email, code);
                if (!valid)
                {
                    ToastHelper.ShowToast(this, "Invalid or expired reset code.", ToastType.Error);
                    return;
                }

                bool reset = _userService.ResetPassword(email, newPass);
                if (reset)
                {
                    ToastHelper.ShowToast(this, "Password successfully reset!", ToastType.Success);
                    await Task.Delay(1200);
                    NavigationHelper.SwitchToMain(this, new LoginForm());
                }
                else
                {
                    ToastHelper.ShowToast(this, "Failed to reset password. Try again.", ToastType.Error);
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(this, $"Unexpected error: {ex.Message}", ToastType.Error);
            }
        }

        // -------------------- ДОПОМІЖНИЙ МЕТОД --------------------
        /// <summary>
        /// Безпечно виконує асинхронну дію, блокуючи кнопку (будь-якого типу) і показуючи "Please wait..."
        /// </summary>
        private async Task RunSafeAsync<TButton>(TButton button, Func<Task> asyncAction) where TButton : Control
        {
            if (button == null || asyncAction == null) return;

            string originalText = button.Text;

            button.Enabled = false;
            button.Text = "Please wait...";

            try
            {
                await asyncAction();
            }
            finally
            {
                button.Text = originalText;
                button.Enabled = true;
            }
        }
    }
}
