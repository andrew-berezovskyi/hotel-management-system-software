using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class ResetPasswordForm
    {
        private HopeTextBox txtEmail;
        private HopeTextBox txtOTP;
        private HopeTextBox txtNewPassword;
        private HopeButton btnSendCode;
        private HopeButton btnResetPassword;
        private Label lblTitle;
        private LinkLabel lnkBack;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtEmail = new HopeTextBox();
            txtOTP = new HopeTextBox();
            txtNewPassword = new HopeTextBox();
            btnSendCode = new HopeButton();
            btnResetPassword = new HopeButton();
            lnkBack = new LinkLabel();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(130, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(205, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Reset Password";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BaseColor = Color.FromArgb(44, 55, 66);
            txtEmail.BorderColorA = Color.FromArgb(64, 158, 255);
            txtEmail.BorderColorB = Color.FromArgb(220, 223, 230);
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.ForeColor = Color.FromArgb(48, 49, 51);
            txtEmail.Hint = "Enter your email";
            txtEmail.Location = new Point(100, 100);
            txtEmail.MaxLength = 32767;
            txtEmail.Multiline = false;
            txtEmail.Name = "txtEmail";
            txtEmail.PasswordChar = '\0';
            txtEmail.ScrollBars = ScrollBars.None;
            txtEmail.SelectedText = "";
            txtEmail.SelectionLength = 0;
            txtEmail.SelectionStart = 0;
            txtEmail.Size = new Size(250, 39);
            txtEmail.TabIndex = 1;
            txtEmail.TabStop = false;
            txtEmail.UseSystemPasswordChar = false;
            // 
            // txtOTP
            // 
            txtOTP.BackColor = Color.White;
            txtOTP.BaseColor = Color.FromArgb(44, 55, 66);
            txtOTP.BorderColorA = Color.FromArgb(64, 158, 255);
            txtOTP.BorderColorB = Color.FromArgb(220, 223, 230);
            txtOTP.Font = new Font("Segoe UI", 10F);
            txtOTP.ForeColor = Color.FromArgb(48, 49, 51);
            txtOTP.Hint = "Enter OTP code";
            txtOTP.Location = new Point(100, 160);
            txtOTP.MaxLength = 32767;
            txtOTP.Multiline = false;
            txtOTP.Name = "txtOTP";
            txtOTP.PasswordChar = '\0';
            txtOTP.ScrollBars = ScrollBars.None;
            txtOTP.SelectedText = "";
            txtOTP.SelectionLength = 0;
            txtOTP.SelectionStart = 0;
            txtOTP.Size = new Size(250, 39);
            txtOTP.TabIndex = 2;
            txtOTP.TabStop = false;
            txtOTP.UseSystemPasswordChar = false;
            // 
            // txtNewPassword
            // 
            txtNewPassword.BackColor = Color.White;
            txtNewPassword.BaseColor = Color.FromArgb(44, 55, 66);
            txtNewPassword.BorderColorA = Color.FromArgb(64, 158, 255);
            txtNewPassword.BorderColorB = Color.FromArgb(220, 223, 230);
            txtNewPassword.Font = new Font("Segoe UI", 10F);
            txtNewPassword.ForeColor = Color.FromArgb(48, 49, 51);
            txtNewPassword.Hint = "Enter new password";
            txtNewPassword.Location = new Point(100, 220);
            txtNewPassword.MaxLength = 32767;
            txtNewPassword.Multiline = false;
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '\0';
            txtNewPassword.ScrollBars = ScrollBars.None;
            txtNewPassword.SelectedText = "";
            txtNewPassword.SelectionLength = 0;
            txtNewPassword.SelectionStart = 0;
            txtNewPassword.Size = new Size(250, 39);
            txtNewPassword.TabIndex = 3;
            txtNewPassword.TabStop = false;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // btnSendCode
            // 
            btnSendCode.BorderColor = Color.FromArgb(220, 223, 230);
            btnSendCode.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnSendCode.DangerColor = Color.FromArgb(245, 108, 108);
            btnSendCode.DefaultColor = Color.FromArgb(255, 255, 255);
            btnSendCode.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnSendCode.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnSendCode.InfoColor = Color.FromArgb(144, 147, 153);
            btnSendCode.Location = new Point(100, 280);
            btnSendCode.Name = "btnSendCode";
            btnSendCode.PrimaryColor = Color.RoyalBlue;
            btnSendCode.Size = new Size(250, 40);
            btnSendCode.SuccessColor = Color.FromArgb(103, 194, 58);
            btnSendCode.TabIndex = 4;
            btnSendCode.Text = "Send Code";
            btnSendCode.TextColor = Color.White;
            btnSendCode.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // btnResetPassword
            // 
            btnResetPassword.BorderColor = Color.FromArgb(220, 223, 230);
            btnResetPassword.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnResetPassword.DangerColor = Color.FromArgb(245, 108, 108);
            btnResetPassword.DefaultColor = Color.FromArgb(255, 255, 255);
            btnResetPassword.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnResetPassword.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnResetPassword.InfoColor = Color.FromArgb(144, 147, 153);
            btnResetPassword.Location = new Point(100, 330);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.PrimaryColor = Color.MediumSeaGreen;
            btnResetPassword.Size = new Size(250, 40);
            btnResetPassword.SuccessColor = Color.FromArgb(103, 194, 58);
            btnResetPassword.TabIndex = 5;
            btnResetPassword.Text = "Reset Password";
            btnResetPassword.TextColor = Color.White;
            btnResetPassword.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // lnkBack
            // 
            lnkBack.AutoSize = true;
            lnkBack.Font = new Font("Segoe UI", 9F);
            lnkBack.LinkColor = Color.LightGray;
            lnkBack.Location = new Point(172, 385);
            lnkBack.Name = "lnkBack";
            lnkBack.Size = new Size(99, 20);
            lnkBack.TabIndex = 6;
            lnkBack.TabStop = true;
            lnkBack.Text = "Back to Login";
            // 
            // ResetPasswordForm
            // 
            BackColor = Color.FromArgb(30, 33, 45);
            ClientSize = new Size(460, 450);
            Controls.Add(lblTitle);
            Controls.Add(txtEmail);
            Controls.Add(txtOTP);
            Controls.Add(txtNewPassword);
            Controls.Add(btnSendCode);
            Controls.Add(btnResetPassword);
            Controls.Add(lnkBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ResetPasswordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reset Password";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
