using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class AdminSetupForm
    {
        private HopeTextBox txtFullName;
        private HopeTextBox txtPhone;
        private HopeTextBox txtPassport;
        private HopeTextBox txtEmail;
        private HopeTextBox txtPassword;
        private HopeTextBox txtConfirm;
        private HopeCheckBox chkShowPassword;
        private HopeButton btnCreateAdmin;
        private Label lblTitle;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtFullName = new HopeTextBox();
            txtPhone = new HopeTextBox();
            txtPassport = new HopeTextBox();
            txtEmail = new HopeTextBox();
            txtPassword = new HopeTextBox();
            txtConfirm = new HopeTextBox();
            chkShowPassword = new HopeCheckBox();
            btnCreateAdmin = new HopeButton();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(103, 45);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(273, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Create Administrator";
            // 
            // txtFullName
            // 
            txtFullName.BackColor = Color.White;
            txtFullName.BaseColor = Color.FromArgb(44, 55, 66);
            txtFullName.BorderColorA = Color.FromArgb(80, 120, 255);
            txtFullName.BorderColorB = Color.FromArgb(50, 80, 200);
            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.ForeColor = Color.FromArgb(48, 49, 51);
            txtFullName.Hint = "Full name";
            txtFullName.Location = new Point(115, 100);
            txtFullName.MaxLength = 32767;
            txtFullName.Multiline = false;
            txtFullName.Name = "txtFullName";
            txtFullName.PasswordChar = '\0';
            txtFullName.ScrollBars = ScrollBars.None;
            txtFullName.SelectedText = "";
            txtFullName.SelectionLength = 0;
            txtFullName.SelectionStart = 0;
            txtFullName.Size = new Size(250, 39);
            txtFullName.TabIndex = 1;
            txtFullName.TabStop = false;
            txtFullName.UseSystemPasswordChar = false;
            // 
            // txtPhone
            // 
            txtPhone.BackColor = Color.White;
            txtPhone.BaseColor = Color.FromArgb(44, 55, 66);
            txtPhone.BorderColorA = Color.FromArgb(80, 120, 255);
            txtPhone.BorderColorB = Color.FromArgb(50, 80, 200);
            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.ForeColor = Color.FromArgb(48, 49, 51);
            txtPhone.Hint = "Phone number";
            txtPhone.Location = new Point(115, 150);
            txtPhone.MaxLength = 32767;
            txtPhone.Multiline = false;
            txtPhone.Name = "txtPhone";
            txtPhone.PasswordChar = '\0';
            txtPhone.ScrollBars = ScrollBars.None;
            txtPhone.SelectedText = "";
            txtPhone.SelectionLength = 0;
            txtPhone.SelectionStart = 0;
            txtPhone.Size = new Size(250, 39);
            txtPhone.TabIndex = 2;
            txtPhone.TabStop = false;
            txtPhone.UseSystemPasswordChar = false;
            // 
            // txtPassport
            // 
            txtPassport.BackColor = Color.White;
            txtPassport.BaseColor = Color.FromArgb(44, 55, 66);
            txtPassport.BorderColorA = Color.FromArgb(80, 120, 255);
            txtPassport.BorderColorB = Color.FromArgb(50, 80, 200);
            txtPassport.Font = new Font("Segoe UI", 10F);
            txtPassport.ForeColor = Color.FromArgb(48, 49, 51);
            txtPassport.Hint = "ID card number";
            txtPassport.Location = new Point(115, 200);
            txtPassport.MaxLength = 32767;
            txtPassport.Multiline = false;
            txtPassport.Name = "txtPassport";
            txtPassport.PasswordChar = '\0';
            txtPassport.ScrollBars = ScrollBars.None;
            txtPassport.SelectedText = "";
            txtPassport.SelectionLength = 0;
            txtPassport.SelectionStart = 0;
            txtPassport.Size = new Size(250, 39);
            txtPassport.TabIndex = 3;
            txtPassport.TabStop = false;
            txtPassport.UseSystemPasswordChar = false;
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
            txtEmail.Location = new Point(115, 250);
            txtEmail.MaxLength = 32767;
            txtEmail.Multiline = false;
            txtEmail.Name = "txtEmail";
            txtEmail.PasswordChar = '\0';
            txtEmail.ScrollBars = ScrollBars.None;
            txtEmail.SelectedText = "";
            txtEmail.SelectionLength = 0;
            txtEmail.SelectionStart = 0;
            txtEmail.Size = new Size(250, 39);
            txtEmail.TabIndex = 4;
            txtEmail.TabStop = false;
            txtEmail.UseSystemPasswordChar = false;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BaseColor = Color.FromArgb(44, 55, 66);
            txtPassword.BorderColorA = Color.FromArgb(80, 120, 255);
            txtPassword.BorderColorB = Color.FromArgb(50, 80, 200);
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.ForeColor = Color.FromArgb(48, 49, 51);
            txtPassword.Hint = "Enter password";
            txtPassword.Location = new Point(115, 300);
            txtPassword.MaxLength = 32767;
            txtPassword.Multiline = false;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '\0';
            txtPassword.ScrollBars = ScrollBars.None;
            txtPassword.SelectedText = "";
            txtPassword.SelectionLength = 0;
            txtPassword.SelectionStart = 0;
            txtPassword.Size = new Size(250, 39);
            txtPassword.TabIndex = 5;
            txtPassword.TabStop = false;
            txtPassword.UseSystemPasswordChar = true;
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
            txtConfirm.Location = new Point(115, 350);
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
            // chkShowPassword
            // 
            chkShowPassword.CheckedColor = Color.FromArgb(64, 158, 255);
            chkShowPassword.DisabledColor = Color.FromArgb(196, 198, 202);
            chkShowPassword.DisabledStringColor = Color.FromArgb(186, 187, 189);
            chkShowPassword.Enable = true;
            chkShowPassword.EnabledCheckedColor = Color.FromArgb(64, 158, 255);
            chkShowPassword.EnabledStringColor = Color.FromArgb(153, 153, 153);
            chkShowPassword.EnabledUncheckedColor = Color.FromArgb(156, 158, 161);
            chkShowPassword.Font = new Font("Segoe UI", 10F);
            chkShowPassword.ForeColor = Color.LightGray;
            chkShowPassword.Location = new Point(115, 395);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(152, 20);
            chkShowPassword.TabIndex = 7;
            chkShowPassword.Text = "Show password";
            // 
            // btnCreateAdmin
            // 
            btnCreateAdmin.BorderColor = Color.FromArgb(220, 223, 230);
            btnCreateAdmin.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnCreateAdmin.DangerColor = Color.FromArgb(245, 108, 108);
            btnCreateAdmin.DefaultColor = Color.FromArgb(255, 255, 255);
            btnCreateAdmin.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnCreateAdmin.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnCreateAdmin.InfoColor = Color.FromArgb(144, 147, 153);
            btnCreateAdmin.Location = new Point(115, 430);
            btnCreateAdmin.Name = "btnCreateAdmin";
            btnCreateAdmin.PrimaryColor = Color.RoyalBlue;
            btnCreateAdmin.Size = new Size(250, 40);
            btnCreateAdmin.SuccessColor = Color.FromArgb(103, 194, 58);
            btnCreateAdmin.TabIndex = 8;
            btnCreateAdmin.Text = "Create Admin";
            btnCreateAdmin.TextColor = Color.White;
            btnCreateAdmin.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // AdminSetupForm
            // 
            BackColor = Color.FromArgb(30, 33, 45);
            ClientSize = new Size(480, 517);
            Controls.Add(lblTitle);
            Controls.Add(txtFullName);
            Controls.Add(txtPhone);
            Controls.Add(txtPassport);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtConfirm);
            Controls.Add(chkShowPassword);
            Controls.Add(btnCreateAdmin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AdminSetupForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Administrator Setup";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

