using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class RegistrationForm
    {
        private HopeTextBox txtUsername;
        private HopeTextBox txtEmail;
        private HopeTextBox txtPhoneNumber;
        private HopeTextBox txtPassportNumber;
        private HopeTextBox txtPasswordRegister;
        private HopeTextBox txtConfirm;
        private HopeButton btnRegister;
        private HopeCheckBox chkShowPasswordRegister;
        private LinkLabel lnkLogin;
        private Label lblInfo;
        private Label lblTitle;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtUsername = new HopeTextBox();
            txtEmail = new HopeTextBox();
            txtPhoneNumber = new HopeTextBox();
            txtPassportNumber = new HopeTextBox();
            txtPasswordRegister = new HopeTextBox();
            txtConfirm = new HopeTextBox();
            btnRegister = new HopeButton();
            chkShowPasswordRegister = new HopeCheckBox();
            lnkLogin = new LinkLabel();
            lblInfo = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(120, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(242, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Create an Account";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BaseColor = Color.FromArgb(44, 55, 66);
            txtUsername.BorderColorA = Color.FromArgb(80, 120, 255);
            txtUsername.BorderColorB = Color.FromArgb(50, 80, 200);
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.ForeColor = Color.FromArgb(48, 49, 51);
            txtUsername.Hint = "Name, surname";
            txtUsername.Location = new Point(115, 80);
            txtUsername.MaxLength = 32767;
            txtUsername.Multiline = false;
            txtUsername.Name = "txtUsername";
            txtUsername.PasswordChar = '\0';
            txtUsername.ScrollBars = ScrollBars.None;
            txtUsername.SelectedText = "";
            txtUsername.SelectionLength = 0;
            txtUsername.SelectionStart = 0;
            txtUsername.Size = new Size(250, 39);
            txtUsername.TabIndex = 1;
            txtUsername.TabStop = false;
            txtUsername.UseSystemPasswordChar = false;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BaseColor = Color.FromArgb(44, 55, 66);
            txtEmail.BorderColorA = Color.FromArgb(80, 120, 255);
            txtEmail.BorderColorB = Color.FromArgb(50, 80, 200);
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.ForeColor = Color.FromArgb(48, 49, 51);
            txtEmail.Hint = "Email address";
            txtEmail.Location = new Point(115, 130);
            txtEmail.MaxLength = 32767;
            txtEmail.Multiline = false;
            txtEmail.Name = "txtEmail";
            txtEmail.PasswordChar = '\0';
            txtEmail.ScrollBars = ScrollBars.None;
            txtEmail.SelectedText = "";
            txtEmail.SelectionLength = 0;
            txtEmail.SelectionStart = 0;
            txtEmail.Size = new Size(250, 39);
            txtEmail.TabIndex = 2;
            txtEmail.TabStop = false;
            txtEmail.UseSystemPasswordChar = false;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.BackColor = Color.White;
            txtPhoneNumber.BaseColor = Color.FromArgb(44, 55, 66);
            txtPhoneNumber.BorderColorA = Color.FromArgb(80, 120, 255);
            txtPhoneNumber.BorderColorB = Color.FromArgb(50, 80, 200);
            txtPhoneNumber.Font = new Font("Segoe UI", 10F);
            txtPhoneNumber.ForeColor = Color.FromArgb(48, 49, 51);
            txtPhoneNumber.Hint = "Phone number";
            txtPhoneNumber.Location = new Point(115, 180);
            txtPhoneNumber.MaxLength = 32767;
            txtPhoneNumber.Multiline = false;
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.PasswordChar = '\0';
            txtPhoneNumber.ScrollBars = ScrollBars.None;
            txtPhoneNumber.SelectedText = "";
            txtPhoneNumber.SelectionLength = 0;
            txtPhoneNumber.SelectionStart = 0;
            txtPhoneNumber.Size = new Size(250, 39);
            txtPhoneNumber.TabIndex = 3;
            txtPhoneNumber.TabStop = false;
            txtPhoneNumber.UseSystemPasswordChar = false;
            // 
            // txtPassportNumber
            // 
            txtPassportNumber.BackColor = Color.White;
            txtPassportNumber.BaseColor = Color.FromArgb(44, 55, 66);
            txtPassportNumber.BorderColorA = Color.FromArgb(80, 120, 255);
            txtPassportNumber.BorderColorB = Color.FromArgb(50, 80, 200);
            txtPassportNumber.Font = new Font("Segoe UI", 10F);
            txtPassportNumber.ForeColor = Color.FromArgb(48, 49, 51);
            txtPassportNumber.Hint = "ID card number";
            txtPassportNumber.Location = new Point(115, 230);
            txtPassportNumber.MaxLength = 32767;
            txtPassportNumber.Multiline = false;
            txtPassportNumber.Name = "txtPassportNumber";
            txtPassportNumber.PasswordChar = '\0';
            txtPassportNumber.ScrollBars = ScrollBars.None;
            txtPassportNumber.SelectedText = "";
            txtPassportNumber.SelectionLength = 0;
            txtPassportNumber.SelectionStart = 0;
            txtPassportNumber.Size = new Size(250, 39);
            txtPassportNumber.TabIndex = 4;
            txtPassportNumber.TabStop = false;
            txtPassportNumber.UseSystemPasswordChar = false;
            // 
            // txtPasswordRegister
            // 
            txtPasswordRegister.BackColor = Color.White;
            txtPasswordRegister.BaseColor = Color.FromArgb(44, 55, 66);
            txtPasswordRegister.BorderColorA = Color.FromArgb(80, 120, 255);
            txtPasswordRegister.BorderColorB = Color.FromArgb(50, 80, 200);
            txtPasswordRegister.Font = new Font("Segoe UI", 10F);
            txtPasswordRegister.ForeColor = Color.FromArgb(48, 49, 51);
            txtPasswordRegister.Hint = "Enter password";
            txtPasswordRegister.Location = new Point(115, 280);
            txtPasswordRegister.MaxLength = 32767;
            txtPasswordRegister.Multiline = false;
            txtPasswordRegister.Name = "txtPasswordRegister";
            txtPasswordRegister.PasswordChar = '\0';
            txtPasswordRegister.ScrollBars = ScrollBars.None;
            txtPasswordRegister.SelectedText = "";
            txtPasswordRegister.SelectionLength = 0;
            txtPasswordRegister.SelectionStart = 0;
            txtPasswordRegister.Size = new Size(250, 39);
            txtPasswordRegister.TabIndex = 5;
            txtPasswordRegister.TabStop = false;
            txtPasswordRegister.UseSystemPasswordChar = true;
            // 
            // txtConfirm
            // 
            txtConfirm.BackColor = Color.White;
            txtConfirm.BaseColor = Color.FromArgb(44, 55, 66);
            txtConfirm.BorderColorA = Color.FromArgb(80, 120, 255);
            txtConfirm.BorderColorB = Color.FromArgb(50, 80, 200);
            txtConfirm.Font = new Font("Segoe UI", 10F);
            txtConfirm.ForeColor = Color.FromArgb(48, 49, 51);
            txtConfirm.Hint = "Confirm password";
            txtConfirm.Location = new Point(115, 330);
            txtConfirm.MaxLength = 32767;
            txtConfirm.Multiline = false;
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PasswordChar = '\0';
            txtConfirm.ScrollBars = ScrollBars.None;
            txtConfirm.SelectedText = "";
            txtConfirm.SelectionLength = 0;
            txtConfirm.SelectionStart = 0;
            txtConfirm.Size = new Size(250, 39);
            txtConfirm.TabIndex = 6;
            txtConfirm.TabStop = false;
            txtConfirm.UseSystemPasswordChar = true;
            // 
            // btnRegister
            // 
            btnRegister.BorderColor = Color.FromArgb(220, 223, 230);
            btnRegister.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnRegister.DangerColor = Color.FromArgb(245, 108, 108);
            btnRegister.DefaultColor = Color.FromArgb(255, 255, 255);
            btnRegister.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnRegister.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnRegister.InfoColor = Color.FromArgb(144, 147, 153);
            btnRegister.Location = new Point(115, 420);
            btnRegister.Name = "btnRegister";
            btnRegister.PrimaryColor = Color.RoyalBlue;
            btnRegister.Size = new Size(250, 40);
            btnRegister.SuccessColor = Color.FromArgb(103, 194, 58);
            btnRegister.TabIndex = 8;
            btnRegister.Text = "Sign up";
            btnRegister.TextColor = Color.White;
            btnRegister.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // chkShowPasswordRegister
            // 
            chkShowPasswordRegister.CheckedColor = Color.FromArgb(64, 158, 255);
            chkShowPasswordRegister.DisabledColor = Color.FromArgb(196, 198, 202);
            chkShowPasswordRegister.DisabledStringColor = Color.FromArgb(186, 187, 189);
            chkShowPasswordRegister.Enable = true;
            chkShowPasswordRegister.EnabledCheckedColor = Color.FromArgb(64, 158, 255);
            chkShowPasswordRegister.EnabledStringColor = Color.FromArgb(153, 153, 153);
            chkShowPasswordRegister.EnabledUncheckedColor = Color.FromArgb(156, 158, 161);
            chkShowPasswordRegister.Font = new Font("Segoe UI", 10F);
            chkShowPasswordRegister.ForeColor = Color.LightGray;
            chkShowPasswordRegister.Location = new Point(115, 380);
            chkShowPasswordRegister.Name = "chkShowPasswordRegister";
            chkShowPasswordRegister.Size = new Size(152, 20);
            chkShowPasswordRegister.TabIndex = 7;
            chkShowPasswordRegister.Text = "Show password";
            // 
            // lnkLogin
            // 
            lnkLogin.AutoSize = true;
            lnkLogin.Font = new Font("Segoe UI", 9F);
            lnkLogin.LinkColor = Color.LightGray;
            lnkLogin.Location = new Point(299, 470);
            lnkLogin.Name = "lnkLogin";
            lnkLogin.Size = new Size(50, 20);
            lnkLogin.TabIndex = 10;
            lnkLogin.TabStop = true;
            lnkLogin.Text = "Log in";
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Segoe UI", 9F);
            lblInfo.ForeColor = Color.LightGray;
            lblInfo.Location = new Point(115, 470);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(178, 20);
            lblInfo.TabIndex = 9;
            lblInfo.Text = "Already have an account?";
            // 
            // RegistrationForm
            // 
            BackColor = Color.FromArgb(30, 33, 45);
            ClientSize = new Size(480, 520);
            Controls.Add(lblTitle);
            Controls.Add(txtUsername);
            Controls.Add(txtEmail);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtPassportNumber);
            Controls.Add(txtPasswordRegister);
            Controls.Add(txtConfirm);
            Controls.Add(chkShowPasswordRegister);
            Controls.Add(btnRegister);
            Controls.Add(lblInfo);
            Controls.Add(lnkLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "RegistrationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sign up";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
