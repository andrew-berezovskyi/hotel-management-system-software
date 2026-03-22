using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class SystemSettingsForm
    {
        private Label lblTitle;
        private HopeTextBox txtEmail;
        private HopeTextBox txtAppPassword;
        private HopeTextBox txtServer;
        private HopeTextBox txtPort;
        private HopeTextBox txtDbPath;
        private HopeTextBox txtHotelName;
        private HopeTextBox txtOTPMinutes; // ← OTP minutes
        private HopeButton btnTestEmail;
        private HopeButton btnChangePath;
        private HopeButton btnBackupDB;
        private HopeButton btnSave;
        private HopeButton btnReset;
        private HopeButton btnBack;
        private HopeButton btnSeedDemoData;   // ← НОВА КНОПКА

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtEmail = new HopeTextBox();
            txtAppPassword = new HopeTextBox();
            txtServer = new HopeTextBox();
            txtPort = new HopeTextBox();
            txtDbPath = new HopeTextBox();
            txtHotelName = new HopeTextBox();
            txtOTPMinutes = new HopeTextBox();
            btnTestEmail = new HopeButton();
            btnChangePath = new HopeButton();
            btnBackupDB = new HopeButton();
            btnSave = new HopeButton();
            btnReset = new HopeButton();
            btnBack = new HopeButton();
            btnSeedDemoData = new HopeButton();   // ← create

            SuspendLayout();

            // ---------- lblTitle ----------
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(900, 60);
            lblTitle.Text = "System Settings";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // ---------- txtEmail ----------
            txtEmail.Location = new Point(80, 100);
            txtEmail.Size = new Size(300, 43);
            txtEmail.Hint = "Email Address";

            // ---------- txtAppPassword ----------
            txtAppPassword.Location = new Point(80, 160);
            txtAppPassword.Size = new Size(300, 43);
            txtAppPassword.Hint = "App Password";
            txtAppPassword.UseSystemPasswordChar = true;

            // ---------- txtServer ----------
            txtServer.Location = new Point(80, 220);
            txtServer.Size = new Size(300, 43);
            txtServer.Hint = "SMTP Server (e.g. smtp.gmail.com)";

            // ---------- txtPort ----------
            txtPort.Location = new Point(80, 280);
            txtPort.Size = new Size(300, 43);
            txtPort.Hint = "SMTP Port (e.g. 587)";

            // ---------- btnTestEmail ----------
            btnTestEmail.Location = new Point(80, 340);
            btnTestEmail.Size = new Size(300, 40);
            btnTestEmail.Text = "Send Test Email";
            btnTestEmail.PrimaryColor = Color.FromArgb(80, 120, 255);
            // клік підписаний у .cs

            // ---------- txtDbPath ----------
            txtDbPath.Location = new Point(480, 100);
            txtDbPath.Size = new Size(300, 43);
            txtDbPath.Hint = "Database Path";

            // ---------- btnChangePath ----------
            btnChangePath.Location = new Point(480, 160);
            btnChangePath.Size = new Size(140, 35);
            btnChangePath.Text = "Change Path";
            btnChangePath.PrimaryColor = Color.FromArgb(80, 120, 255);

            // ---------- btnBackupDB ----------
            btnBackupDB.Location = new Point(640, 160);
            btnBackupDB.Size = new Size(140, 35);
            btnBackupDB.Text = "Backup DB";
            btnBackupDB.PrimaryColor = Color.FromArgb(60, 200, 120);

            // ---------- txtHotelName ----------
            txtHotelName.Location = new Point(480, 240);
            txtHotelName.Size = new Size(300, 43);
            txtHotelName.Hint = "Hotel Name";

            // ---------- txtOTPMinutes ----------
            txtOTPMinutes.Location = new Point(480, 300);
            txtOTPMinutes.Size = new Size(300, 43);
            txtOTPMinutes.Hint = "OTP Expire Minutes (e.g. 5)";

            // ---------- btnSeedDemoData (нова) ----------
            btnSeedDemoData.Location = new Point(480, 360);
            btnSeedDemoData.Size = new Size(300, 40);
            btnSeedDemoData.Text = "Generate Demo Data";
            btnSeedDemoData.PrimaryColor = Color.FromArgb(255, 190, 60);
            btnSeedDemoData.Name = "btnSeedDemoData";
            // клік підписаний у .cs

            // ---------- btnBack ----------
            btnBack.Location = new Point(200, 500);
            btnBack.Size = new Size(160, 40);
            btnBack.Text = "← Back";
            btnBack.PrimaryColor = Color.FromArgb(80, 120, 255);

            // ---------- btnReset ----------
            btnReset.Location = new Point(400, 500);
            btnReset.Size = new Size(160, 40);
            btnReset.Text = "Reset to Default";
            btnReset.PrimaryColor = Color.FromArgb(255, 120, 60);

            // ---------- btnSave ----------
            btnSave.Location = new Point(600, 500);
            btnSave.Size = new Size(160, 40);
            btnSave.Text = "Save Changes";
            btnSave.PrimaryColor = Color.FromArgb(60, 200, 120);

            // ---------- FORM ----------
            BackColor = Color.FromArgb(30, 33, 45);
            ClientSize = new Size(900, 620);
            Controls.AddRange(new Control[]
            {
                lblTitle,
                txtEmail,
                txtAppPassword,
                txtServer,
                txtPort,
                btnTestEmail,
                txtDbPath,
                btnChangePath,
                btnBackupDB,
                txtHotelName,
                txtOTPMinutes,
                btnSeedDemoData,   // ← додаємо на форму
                btnBack,
                btnReset,
                btnSave
            });
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "System Settings";
            ResumeLayout(false);
        }
    }
}
