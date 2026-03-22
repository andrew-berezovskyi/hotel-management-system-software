using hotel_management_system.UIHelpers;
using ReaLTaiizor.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelApp
{
    partial class WelcomeForm
    {
        private System.ComponentModel.IContainer components = null;

        private GlassPanel pnlCard;
        private System.Windows.Forms.Panel pnlSeparator;
        private Label lblHotelName;
        private Label lblStars;
        private Label lblAddress;
        private Label lblContacts;
        private Label lblDescription;
        private Label lblSafety;
        private HopeButton btnLogin;
        private Label lblSubtitle;
        private Label lblFooter;
        private LinkLabel lnkWebsite;   // 🔹 новий елемент

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));

            this.pnlCard = new GlassPanel();
            this.pnlSeparator = new System.Windows.Forms.Panel();
            this.lblHotelName = new System.Windows.Forms.Label();
            this.lblStars = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblContacts = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblSafety = new System.Windows.Forms.Label();
            this.btnLogin = new ReaLTaiizor.Controls.HopeButton();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.lnkWebsite = new System.Windows.Forms.LinkLabel();

            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = Color.Transparent;
            this.pnlCard.FillColor = Color.FromArgb(190, 15, 23, 42); // трохи прозорий темний
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlCard.Controls.Add(this.lblSubtitle);
            this.pnlCard.Controls.Add(this.btnLogin);
            this.pnlCard.Controls.Add(this.lblSafety);
            this.pnlCard.Controls.Add(this.pnlSeparator);
            this.pnlCard.Controls.Add(this.lblDescription);
            this.pnlCard.Controls.Add(this.lblContacts);
            this.pnlCard.Controls.Add(this.lblAddress);
            this.pnlCard.Controls.Add(this.lblStars);
            this.pnlCard.Controls.Add(this.lblHotelName);
            this.pnlCard.Location = new System.Drawing.Point(0, 0); // реальне центрування робимо в коді
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(32, 28, 32, 32);
            this.pnlCard.Size = new System.Drawing.Size(900, 590);
            this.pnlCard.TabIndex = 0;

            // 
            // lblHotelName
            // 
            this.lblHotelName.Font = new Font("Georgia", 36F, FontStyle.Bold);
            this.lblHotelName.ForeColor = Color.White;
            this.lblHotelName.BackColor = Color.Transparent;
            this.lblHotelName.Location = new Point(0, 20);
            this.lblHotelName.Name = "lblHotelName";
            this.lblHotelName.Size = new Size(836, 70);
            this.lblHotelName.Text = "The Hotel Kyiv";
            this.lblHotelName.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblStars
            // 
            this.lblStars.Font = new Font("Segoe UI", 18F, FontStyle.Regular);
            this.lblStars.ForeColor = Color.Goldenrod;
            this.lblStars.BackColor = Color.Transparent;
            this.lblStars.Location = new Point(0, 85);
            this.lblStars.Name = "lblStars";
            this.lblStars.Size = new Size(836, 35);
            this.lblStars.Text = "★★★★★";
            this.lblStars.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblAddress
            // 
            this.lblAddress.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.lblAddress.ForeColor = Color.White;
            this.lblAddress.BackColor = Color.Transparent;
            this.lblAddress.Location = new Point(0, 125);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new Size(836, 30);
            this.lblAddress.Text = "Heroyiv Nebesnoyi Sotni Alley, 4, Kyiv";
            this.lblAddress.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblContacts
            // 
            this.lblContacts.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.lblContacts.ForeColor = Color.Gainsboro;
            this.lblContacts.BackColor = Color.Transparent;
            this.lblContacts.Location = new Point(0, 155);
            this.lblContacts.Name = "lblContacts";
            this.lblContacts.Size = new Size(836, 28);
            this.lblContacts.Text = "Phone: +380 (66) 679 72 12   |   Email: info@thehotelkyiv.com";
            this.lblContacts.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblDescription
            // 
            this.lblDescription.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.lblDescription.ForeColor = Color.Silver;
            this.lblDescription.BackColor = Color.Transparent;
            this.lblDescription.Location = new Point(70, 195);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(700, 100);
            this.lblDescription.Text = resources.GetString("lblDescription.Text");
            this.lblDescription.TextAlign = ContentAlignment.TopCenter;

            // 
            // pnlSeparator
            // 
            this.pnlSeparator.BackColor = Color.FromArgb(90, 96, 116);
            this.pnlSeparator.Location = new Point(120, 305);
            this.pnlSeparator.Name = "pnlSeparator";
            this.pnlSeparator.Size = new Size(600, 1);
            this.pnlSeparator.TabIndex = 6;

            // 
            // lblSafety
            // 
            this.lblSafety.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            this.lblSafety.ForeColor = Color.LightGray;
            this.lblSafety.BackColor = Color.Transparent;
            this.lblSafety.Location = new Point(70, 320);
            this.lblSafety.Name = "lblSafety";
            this.lblSafety.Size = new Size(700, 140);
            this.lblSafety.Text = resources.GetString("lblSafety.Text");
            this.lblSafety.TextAlign = ContentAlignment.TopCenter;

            // 
            // btnLogin
            // 
            Color loginColor = Color.FromArgb(80, 120, 255);
            this.btnLogin.PrimaryColor = loginColor;
            this.btnLogin.Text = "Log in";
            this.btnLogin.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            // Початкова позиція — приблизно, реальне центрування робимо в CenterLoginAreaInsideCard()
            this.btnLogin.Location = new Point(368, 480);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(160, 48);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new Font("Segoe UI", 8.75F, FontStyle.Regular); // трішки менший шрифт
            this.lblSubtitle.ForeColor = Color.Gainsboro;
            this.lblSubtitle.BackColor = Color.Transparent;
            this.lblSubtitle.Location = new Point(0, 535);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(836, 24);
            this.lblSubtitle.Text = "Continue to The Hotel Kyiv management system";
            this.lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblFooter
            // 
            this.lblFooter.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblFooter.ForeColor = Color.Gainsboro;
            this.lblFooter.BackColor = Color.Transparent;
            this.lblFooter.Location = new Point(0, 0); // реальне положення виставимо в коді
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new Size(800, 22);
            this.lblFooter.Text = "© 2025 The Hotel Kyiv · CEO Andrew Berezovskyi";
            this.lblFooter.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lnkWebsite
            // 
            this.lnkWebsite.AutoSize = true;
            this.lnkWebsite.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            this.lnkWebsite.LinkColor = Color.SkyBlue;
            this.lnkWebsite.ActiveLinkColor = Color.DodgerBlue;
            this.lnkWebsite.VisitedLinkColor = Color.MediumPurple;
            this.lnkWebsite.BackColor = Color.Transparent;
            this.lnkWebsite.Location = new Point(0, 0); // фактичну позицію виставимо в CenterCardPanel()
            this.lnkWebsite.Name = "lnkWebsite";
            this.lnkWebsite.Size = new Size(50, 15);
            this.lnkWebsite.TabStop = true;
            this.lnkWebsite.Text = "Website";
            this.lnkWebsite.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.FromArgb(30, 33, 45);

            this.ClientSize = new Size(1200, 800); // стартовий розмір, потім Maximize
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblFooter);
            this.Controls.Add(this.lnkWebsite);   // 🔹 додаємо лінк на форму
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome - The Hotel Kyiv";

            this.pnlCard.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
