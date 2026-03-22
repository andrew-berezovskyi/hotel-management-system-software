using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Forms;

namespace hotel_management_system.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserService _userService = new UserService();
        private bool _isNavigating = false; // щоб уникнути повторних відкриттів

        public LoginForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            btnLogin.Click += btnLogin_Click;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            lnkSignUp.LinkClicked += lnkSignUp_LinkClicked;
            lnkForgotPassword.LinkClicked += lnkForgotPassword_LinkClicked;

            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnLogin_Click(btnLogin, EventArgs.Empty);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string input = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!ValidationHelper.IsNotEmpty(input) || !ValidationHelper.IsNotEmpty(password))
            {
                ToastHelper.ShowToast(this, "Please fill in all fields.", ToastType.Warning);
                return;
            }

            if (input.Contains("@") && !ValidationHelper.IsValidEmail(input))
            {
                ToastHelper.ShowToast(this, "Invalid email format.", ToastType.Error);
                return;
            }

            if (input.StartsWith("0") && !ValidationHelper.IsValidPhone(input))
            {
                ToastHelper.ShowToast(this, "Invalid phone number format.", ToastType.Error);
                return;
            }

            var user = _userService.ValidateLogin(input, password);

            if (user == null)
            {
                ToastHelper.ShowToast(this, "Invalid credentials. Please try again.", ToastType.Error);
                return;
            }

            // ЗБЕРІГАЄМО КОРИСТУВАЧА В СЕСІЮ
            AuthHelper.SetUser(user);
            AuthHelper.RefreshActivity();

            ToastHelper.ShowToast(this, "Login successful!", ToastType.Success);

            // ВІДКРИВАЄМО ВІДПОВІДНИЙ ДАШБОРД ЧЕРЕЗ NavigationHelper
            if (AuthHelper.IsAdmin())
            {
                NavigationHelper.SwitchToMain(this, new AdminDashboardForm());
            }
            else
            {
                NavigationHelper.SwitchToMain(this, new UserDashboardForm());
            }
        }

        private void lnkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_isNavigating) return; // запобігає дублюванню
            _isNavigating = true;

            var registerForm = new RegistrationForm();
            registerForm.FormClosed += (s, args) =>
            {
                _isNavigating = false;
                this.Show(); // коли повертаємося — VisibleChanged спрацює і fade-in зробить хелпер
            };
            this.Hide();
            registerForm.Show();
        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_isNavigating) return;
            _isNavigating = true;

            var resetForm = new ResetPasswordForm();
            resetForm.FormClosed += (s, args) =>
            {
                _isNavigating = false;
                this.Show(); // знову fade-in автоматично через AttachFadeInOnVisible
            };
            this.Hide();
            resetForm.Show();
        }
    }
}
