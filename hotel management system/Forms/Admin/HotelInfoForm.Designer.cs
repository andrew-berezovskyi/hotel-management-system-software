using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class HotelInfoForm
    {
        private Label lblTitle;
        private PictureBox picLogo;
        private HopeTextBox txtName;
        private HopeTextBox txtAddress;
        private HopeTextBox txtPhone;
        private HopeTextBox txtEmail;
        private HopeTextBox txtWebsite;
        private HopeTextBox txtDescription;
        private NumericUpDown txtStars;
        private Label lblStars;
        private HopeButton btnChangeLogo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnBack;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtName = new ReaLTaiizor.Controls.HopeTextBox();
            this.txtAddress = new ReaLTaiizor.Controls.HopeTextBox();
            this.txtPhone = new ReaLTaiizor.Controls.HopeTextBox();
            this.txtEmail = new ReaLTaiizor.Controls.HopeTextBox();
            this.txtWebsite = new ReaLTaiizor.Controls.HopeTextBox();
            this.txtDescription = new ReaLTaiizor.Controls.HopeTextBox();
            this.txtStars = new System.Windows.Forms.NumericUpDown();
            this.lblStars = new System.Windows.Forms.Label();
            this.btnChangeLogo = new ReaLTaiizor.Controls.HopeButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStars)).BeginInit();
            this.SuspendLayout();
            // 
            // FORM
            // 
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.ClientSize = new Size(1200, 650);
            this.Text = "Hotel Info";
            this.StartPosition = FormStartPosition.CenterScreen;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Text = "Hotel Information";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(25, 20);
            this.lblTitle.Size = new Size(400, 50);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnBack
            // 
            this.btnBack.Text = "← Back";
            this.btnBack.BackColor = Color.FromArgb(80, 120, 255);
            this.btnBack.ForeColor = Color.White;
            this.btnBack.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.Size = new Size(110, 34);
            this.btnBack.Location = new Point(1200 - 110 - 20, 25); // правий верхній кут
            this.btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // picLogo
            // 
            this.picLogo.Size = new Size(220, 220);
            this.picLogo.Location = new Point(80, 150);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.BorderStyle = BorderStyle.FixedSingle;
            this.picLogo.BackColor = Color.FromArgb(45, 48, 60);
            // 
            // btnChangeLogo
            // 
            this.btnChangeLogo.Text = "Change Logo";
            this.btnChangeLogo.Location = new Point(80, 385);
            this.btnChangeLogo.Size = new Size(220, 40);
            this.btnChangeLogo.PrimaryColor = Color.FromArgb(80, 120, 255);
            this.btnChangeLogo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            // 
            // Основні координати для правого блоку
            //
            int rightLeft = 360 + 200; // ~ по центру
            int top = 150;
            int spacing = 55;
            int width = 550;
            // 
            // txtName
            // 
            this.txtName.Hint = "Hotel Name";
            this.txtName.Location = new Point(rightLeft, top);
            this.txtName.Size = new Size(width, 45);
            top += spacing;
            // 
            // txtAddress
            // 
            this.txtAddress.Hint = "Address";
            this.txtAddress.Location = new Point(rightLeft, top);
            this.txtAddress.Size = new Size(width, 45);
            top += spacing;
            // 
            // txtPhone
            // 
            this.txtPhone.Hint = "Phone";
            this.txtPhone.Location = new Point(rightLeft, top);
            this.txtPhone.Size = new Size(width, 45);
            top += spacing;
            // 
            // txtEmail
            // 
            this.txtEmail.Hint = "Email";
            this.txtEmail.Location = new Point(rightLeft, top);
            this.txtEmail.Size = new Size(width, 45);
            top += spacing;
            // 
            // txtWebsite
            // 
            this.txtWebsite.Hint = "Website";
            this.txtWebsite.Location = new Point(rightLeft, top);
            this.txtWebsite.Size = new Size(width, 45);
            top += spacing;
            // 
            // txtDescription
            // 
            this.txtDescription.Hint = "Description";
            this.txtDescription.Location = new Point(rightLeft, top);
            this.txtDescription.Size = new Size(width, 70);
            this.txtDescription.Multiline = true;
            top += 80;
            // 
            // lblStars
            // 
            this.lblStars.Text = "Stars:";
            this.lblStars.ForeColor = Color.White;
            this.lblStars.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblStars.Location = new Point(rightLeft, top + 4);
            this.lblStars.Size = new Size(50, 25);
            // 
            // txtStars
            // 
            this.txtStars.Location = new Point(rightLeft + 55, top);
            this.txtStars.Size = new Size(70, 25);
            this.txtStars.Minimum = 1;
            this.txtStars.Maximum = 5;
            this.txtStars.Value = 5;
            this.txtStars.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.txtStars.BackColor = Color.White;
            this.txtStars.ForeColor = Color.Black;
            top += 55;
            // 
            // btnSave
            // 
            this.btnSave.Text = "Save Changes";
            this.btnSave.BackColor = Color.FromArgb(60, 200, 120);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Size = new Size(180, 40);
            this.btnSave.Location = new Point(rightLeft, top);
            // 
            // btnReset
            // 
            this.btnReset.Text = "Reset";
            this.btnReset.BackColor = Color.FromArgb(255, 150, 80);
            this.btnReset.ForeColor = Color.White;
            this.btnReset.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnReset.FlatStyle = FlatStyle.Flat;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.Size = new Size(140, 40);
            this.btnReset.Location = new Point(rightLeft + 200, top);
            // 
            // ADD CONTROLS
            // 
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.btnChangeLogo);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtWebsite);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblStars);
            this.Controls.Add(this.txtStars);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);

            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStars)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
