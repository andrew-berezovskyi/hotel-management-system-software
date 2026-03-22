using hotel_management_system.Helpers;
using hotel_management_system.Services;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Models;

namespace hotel_management_system.Forms
{
    public partial class AdminSetupForm : Form
    {
        private readonly UserService _userService = new UserService();

        public AdminSetupForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            txtPassword.UseSystemPasswordChar = true;
            txtConfirm.UseSystemPasswordChar = true;

            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;
            btnCreateAdmin.Click += BtnCreateAdmin_Click;

            // при натисканні хрестика — закриття програми, якщо це остання форма
            this.FormClosing += (_, e) =>
            {
                if (Application.OpenForms.Count == 1)
                    Application.Exit();
            };

            // Опціонально: Enter = створити адміна
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnCreateAdmin_Click(btnCreateAdmin, EventArgs.Empty);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPassword.Checked;
            txtPassword.UseSystemPasswordChar = !show;
            txtConfirm.UseSystemPasswordChar = !show;
        }

        private void BtnCreateAdmin_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string passport = txtPassport.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            // ---------------- VALIDATION ----------------
            if (!ValidationHelper.IsNotEmpty(fullName) ||
                !ValidationHelper.IsNotEmpty(phone) ||
                !ValidationHelper.IsNotEmpty(passport) ||
                !ValidationHelper.IsNotEmpty(email) ||
                !ValidationHelper.IsNotEmpty(password) ||
                !ValidationHelper.IsNotEmpty(confirm))
            {
                ToastHelper.ShowToast(this, "Please fill in all fields.", ToastType.Warning);
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                ToastHelper.ShowToast(this, "Invalid email format.", ToastType.Error);
                return;
            }

            if (!ValidationHelper.IsPasswordMatch(password, confirm))
            {
                ToastHelper.ShowToast(this, "Passwords do not match.", ToastType.Error);
                return;
            }

            if (!ValidationHelper.IsValidPhone(phone))
            {
                ToastHelper.ShowToast(this, "Invalid phone number. Must start with 0 and contain 10 digits.", ToastType.Error);
                return;
            }

            if (!ValidationHelper.IsValidPassport(passport))
            {
                ToastHelper.ShowToast(this, "Invalid passport format (9 digits or 2 letters + 6 digits).", ToastType.Error);
                return;
            }

            if (!ValidationHelper.IsStrongPassword(password))
            {
                ToastHelper.ShowToast(this, "Password must contain at least 8 characters, letters, and numbers.", ToastType.Warning);
                return;
            }

            // ---------------- CREATE ADMIN ----------------
            string hashedPassword = PasswordHelper.HashPassword(password);

            var admin = new User
            {
                FullName = fullName,
                Username = fullName,
                Email = email,
                PasswordHash = hashedPassword,
                Phone = phone,
                PassportNumber = passport,
                IsAdmin = true
            };

            if (_userService.AddUser(admin))
            {
                ToastHelper.ShowToast(this, "Administrator created successfully!", ToastType.Success);

                // Переходимо до LoginForm
                NavigationHelper.SwitchToMain(this, new LoginForm());
            }
            else
            {
                ToastHelper.ShowToast(this, "An error occurred while creating the administrator.", ToastType.Error);
            }
        }
    }
}
