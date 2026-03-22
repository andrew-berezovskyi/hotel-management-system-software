using HotelManagementSystem.Models;
using hotel_management_system.Services;
using hotel_management_system.Helpers;
using hotel_management_system.UIHelpers;

namespace hotel_management_system.Forms
{
    public partial class RegistrationForm : Form
    {
        private readonly UserService _userService = new UserService();

        public RegistrationForm()
        {
            InitializeComponent();

            // 🔹 Додаємо плавну анімацію появи форми
            FormAnimationHelper.AttachFadeInOnVisible(this);

            // Початкові налаштування
            txtPasswordRegister.UseSystemPasswordChar = true;
            txtConfirm.UseSystemPasswordChar = true;

            // Підключення подій
            chkShowPasswordRegister.CheckedChanged += ChkShowPasswordRegister_CheckedChanged;
            btnRegister.Click += BtnRegister_Click;
            lnkLogin.LinkClicked += LnkLogin_LinkClicked;

            // Enter = натискання кнопки реєстрації
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnRegister_Click(btnRegister, EventArgs.Empty);
                    e.Handled = true;
                }
            };
        }

        private void ChkShowPasswordRegister_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPasswordRegister.Checked;
            txtPasswordRegister.UseSystemPasswordChar = !show;
            txtConfirm.UseSystemPasswordChar = !show;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhoneNumber.Text.Trim();
            string passport = txtPassportNumber.Text.Trim();
            string password = txtPasswordRegister.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            // Перевірка введених даних
            if (!ValidateInputs(fullName, email, phone, passport, password, confirm))
                return;

            // Перевірка, чи користувач уже існує
            if (_userService.UserExists(fullName, email, passport, phone))
            {
                ToastHelper.ShowToast(this, "A user with these details already exists.", ToastType.Warning);
                return;
            }

            // Хешування пароля
            string hashedPassword = PasswordHelper.HashPassword(password);

            var newUser = new User
            {
                FullName = fullName,
                Username = fullName,
                Email = email,
                Phone = phone,
                PassportNumber = passport,
                PasswordHash = hashedPassword,
                IsAdmin = false
            };

            // Додавання користувача
            if (_userService.AddUser(newUser))
            {
                ToastHelper.ShowToast(this, "Registration successful! Redirecting to login...", ToastType.Success);
                NavigationHelper.SwitchToMain(this, new LoginForm());
            }
            else
            {
                ToastHelper.ShowToast(this, "An error occurred during registration.", ToastType.Error);
            }
        }

        private void LnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NavigationHelper.SwitchToMain(this, new LoginForm());
        }

        private bool ValidateInputs(string fullName, string email, string phone, string passport, string password, string confirm)
        {
            if (!ValidationHelper.IsNotEmpty(fullName) ||
                !ValidationHelper.IsNotEmpty(email) ||
                !ValidationHelper.IsNotEmpty(phone) ||
                !ValidationHelper.IsNotEmpty(passport) ||
                !ValidationHelper.IsNotEmpty(password) ||
                !ValidationHelper.IsNotEmpty(confirm))
            {
                ToastHelper.ShowToast(this, "Please fill in all fields.", ToastType.Warning);
                return false;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                ToastHelper.ShowToast(this, "Invalid email format.", ToastType.Error);
                return false;
            }

            if (!ValidationHelper.IsValidPasswordMatch(password, confirm))
            {
                ToastHelper.ShowToast(this, "Passwords do not match.", ToastType.Error);
                return false;
            }

            if (!ValidationHelper.IsValidPassport(passport))
            {
                ToastHelper.ShowToast(this, "Invalid passport format (must be 9 digits or 2 letters + 6 digits).", ToastType.Error);
                return false;
            }

            if (!ValidationHelper.IsValidPhone(phone))
            {
                ToastHelper.ShowToast(this, "Invalid phone number (must be 10 digits starting with 0).", ToastType.Error);
                return false;
            }

            return true;
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
    }
}
