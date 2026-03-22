using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class LoginForm
    {
        private HopeTextBox txtUsername;
        private HopeTextBox txtPassword;
        private HopeButton btnLogin;
        private HopeCheckBox chkShowPassword;
        private LinkLabel lnkSignUp;
        private LinkLabel lnkForgotPassword;
        private Label lblTitle;

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.txtUsername = new HopeTextBox();
            this.txtPassword = new HopeTextBox();
            this.btnLogin = new HopeButton();
            this.chkShowPassword = new HopeCheckBox();
            this.lnkSignUp = new LinkLabel();
            this.lnkForgotPassword = new LinkLabel();

            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(140, 40);
            this.lblTitle.Text = "Login to System";

            // txtUsername
            this.txtUsername.Hint = "Phone number / Email";
            this.txtUsername.Size = new Size(250, 40);
            this.txtUsername.Location = new Point(115, 110);
            this.txtUsername.Font = new Font("Segoe UI", 10F);
            this.txtUsername.BorderColorA = Color.FromArgb(80, 120, 255);
            this.txtUsername.BorderColorB = Color.FromArgb(50, 80, 200);

            // txtPassword
            this.txtPassword.Hint = "Password";
            this.txtPassword.Size = new Size(250, 40);
            this.txtPassword.Location = new Point(115, 170);
            this.txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.UseSystemPasswordChar = true; // пароль приховано за замовчуванням


            // chkShowPassword
            this.chkShowPassword.Text = "Show password";
            this.chkShowPassword.Location = new Point(115, 220);
            this.chkShowPassword.ForeColor = Color.LightGray;
            this.chkShowPassword.Font = new Font("Segoe UI", 10F);


            // btnLogin
            this.btnLogin.Text = "Log in";
            this.btnLogin.Size = new Size(250, 40);
            this.btnLogin.Location = new Point(115, 260);
            this.btnLogin.PrimaryColor = Color.RoyalBlue;
            this.btnLogin.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);

            // lnkForgotPassword
            this.lnkForgotPassword.Text = "Forgot password?";
            this.lnkForgotPassword.LinkColor = Color.LightGray;
            this.lnkForgotPassword.Location = new Point(115, 310);
            this.lnkForgotPassword.AutoSize = true;
            this.lnkForgotPassword.Font = new Font("Segoe UI", 9F);

            // lnkSignUp
            this.lnkSignUp.Text = "Sign up";
            this.lnkSignUp.LinkColor = Color.LightGray;
            this.lnkSignUp.Location = new Point(290, 310);
            this.lnkSignUp.AutoSize = true;
            this.lnkSignUp.Font = new Font("Segoe UI", 9F);

            // LoginForm
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.ClientSize = new Size(480, 420);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lnkForgotPassword);
            this.Controls.Add(this.lnkSignUp);
            this.Text = "Login";
            

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
