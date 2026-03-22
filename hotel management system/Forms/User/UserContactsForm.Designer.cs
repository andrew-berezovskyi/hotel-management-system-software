using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class UserContactsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel headerPanel;
        private Label lblTitle;
        private HopeButton btnBack;

        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel cardPanel;

        // left side – hotel info
        private Label lblHotelName;
        private Label lblAddressCaption;
        private Label lblAddressValue;
        private Label lblPhoneCaption;
        private Label lblPhoneValue;
        private Label lblEmailCaption;
        private Label lblEmailValue;
        private Label lblWebsiteCaption;
        private LinkLabel linkWebsite;   // ← клікабельне посилання

        // right side – message card
        private Label lblSendingAsCaption;
        private Label lblCurrentUser;
        private Label lblSubjectCaption;
        private TextBox txtSubject;
        private Label lblMessageCaption;
        private TextBox txtMessage;
        private HopeButton btnSend;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new Label();
            this.btnBack = new HopeButton();

            this.contentPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.cardPanel = new System.Windows.Forms.Panel();

            this.lblHotelName = new Label();
            this.lblAddressCaption = new Label();
            this.lblAddressValue = new Label();
            this.lblPhoneCaption = new Label();
            this.lblPhoneValue = new Label();
            this.lblEmailCaption = new Label();
            this.lblEmailValue = new Label();
            this.lblWebsiteCaption = new Label();
            this.linkWebsite = new LinkLabel();

            this.lblSendingAsCaption = new Label();
            this.lblCurrentUser = new Label();
            this.lblSubjectCaption = new Label();
            this.txtSubject = new TextBox();
            this.lblMessageCaption = new Label();
            this.txtMessage = new TextBox();
            this.btnSend = new HopeButton();

            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.cardPanel.SuspendLayout();
            this.SuspendLayout();

            // ================= FORM =================
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.ForeColor = Color.White;
            this.Text = "Contacts";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            // ================= HEADER =================
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 70;
            this.headerPanel.BackColor = Color.FromArgb(25, 28, 38);

            this.lblTitle.Text = "Contacts";
            this.lblTitle.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Dock = DockStyle.Left;
            this.lblTitle.Width = 400;
            this.lblTitle.Padding = new Padding(30, 0, 0, 0);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            this.btnBack.Text = "← Back";
            this.btnBack.Size = new Size(120, 40);
            this.btnBack.PrimaryColor = Color.FromArgb(80, 120, 255);
            this.btnBack.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            this.btnBack.ForeColor = Color.White;
            this.btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnBack.Location = new Point(this.ClientSize.Width - this.btnBack.Width - 30, 15);

            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Controls.Add(this.btnBack);

            // тримаємо Back у правому верхньому куті
            this.Resize += (s, e) =>
            {
                btnBack.Location = new Point(this.ClientSize.Width - btnBack.Width - 30, 15);
            };

            // ================= CONTENT ROOT =================
            this.contentPanel.Dock = DockStyle.Fill;
            this.contentPanel.BackColor = Color.FromArgb(30, 33, 45);
            this.contentPanel.Padding = new Padding(30);

            // ================= LEFT PANEL – HOTEL INFO =================
            this.leftPanel.Dock = DockStyle.Left;
            this.leftPanel.Width = 320;
            this.leftPanel.BackColor = Color.FromArgb(25, 28, 38);
            this.leftPanel.Padding = new Padding(20);

            this.lblHotelName.AutoSize = true;
            this.lblHotelName.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            this.lblHotelName.ForeColor = Color.White;
            this.lblHotelName.Location = new Point(10, 10);
            this.lblHotelName.Text = "Hotel name";

            this.lblAddressCaption.AutoSize = true;
            this.lblAddressCaption.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblAddressCaption.ForeColor = Color.Gainsboro;
            this.lblAddressCaption.Location = new Point(10, 55);
            this.lblAddressCaption.Text = "Address:";

            this.lblAddressValue.AutoSize = true;
            this.lblAddressValue.Font = new Font("Segoe UI", 10F);
            this.lblAddressValue.ForeColor = Color.White;
            this.lblAddressValue.Location = new Point(10, 75);
            this.lblAddressValue.MaximumSize = new Size(260, 0);
            this.lblAddressValue.Text = "address value";

            this.lblPhoneCaption.AutoSize = true;
            this.lblPhoneCaption.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblPhoneCaption.ForeColor = Color.Gainsboro;
            this.lblPhoneCaption.Location = new Point(10, 120);
            this.lblPhoneCaption.Text = "Phone:";

            this.lblPhoneValue.AutoSize = true;
            this.lblPhoneValue.Font = new Font("Segoe UI", 10F);
            this.lblPhoneValue.ForeColor = Color.White;
            this.lblPhoneValue.Location = new Point(10, 140);
            this.lblPhoneValue.Text = "+380…";

            this.lblEmailCaption.AutoSize = true;
            this.lblEmailCaption.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblEmailCaption.ForeColor = Color.Gainsboro;
            this.lblEmailCaption.Location = new Point(10, 180);
            this.lblEmailCaption.Text = "Email:";

            this.lblEmailValue.AutoSize = true;
            this.lblEmailValue.Font = new Font("Segoe UI", 10F);
            this.lblEmailValue.ForeColor = Color.White;
            this.lblEmailValue.Location = new Point(10, 200);
            this.lblEmailValue.MaximumSize = new Size(260, 0);
            this.lblEmailValue.Text = "info@example.com";

            this.lblWebsiteCaption.AutoSize = true;
            this.lblWebsiteCaption.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblWebsiteCaption.ForeColor = Color.Gainsboro;
            this.lblWebsiteCaption.Location = new Point(10, 240);
            this.lblWebsiteCaption.Text = "Website:";

            // LinkLabel для сайту
            this.linkWebsite.AutoSize = true;
            this.linkWebsite.Font = new Font("Segoe UI", 10F);
            this.linkWebsite.Location = new Point(10, 260);
            this.linkWebsite.MaximumSize = new Size(260, 0);
            this.linkWebsite.LinkColor = Color.DeepSkyBlue;
            this.linkWebsite.ActiveLinkColor = Color.DodgerBlue;
            this.linkWebsite.VisitedLinkColor = Color.MediumPurple;
            this.linkWebsite.LinkBehavior = LinkBehavior.HoverUnderline;
            this.linkWebsite.Text = "https://example.com";

            this.leftPanel.Controls.Add(this.lblHotelName);
            this.leftPanel.Controls.Add(this.lblAddressCaption);
            this.leftPanel.Controls.Add(this.lblAddressValue);
            this.leftPanel.Controls.Add(this.lblPhoneCaption);
            this.leftPanel.Controls.Add(this.lblPhoneValue);
            this.leftPanel.Controls.Add(this.lblEmailCaption);
            this.leftPanel.Controls.Add(this.lblEmailValue);
            this.leftPanel.Controls.Add(this.lblWebsiteCaption);
            this.leftPanel.Controls.Add(this.linkWebsite);

            // ================= RIGHT PANEL – MESSAGE AREA =================
            this.rightPanel.Dock = DockStyle.Fill;
            this.rightPanel.BackColor = Color.FromArgb(30, 33, 45);
            this.rightPanel.Padding = new Padding(30, 0, 0, 0);

            this.cardPanel.Dock = DockStyle.Fill;
            this.cardPanel.BackColor = Color.FromArgb(25, 28, 38);
            this.cardPanel.Padding = new Padding(30);

            // "You are sending as"
            this.lblSendingAsCaption.AutoSize = true;
            this.lblSendingAsCaption.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblSendingAsCaption.ForeColor = Color.Gainsboro;
            this.lblSendingAsCaption.Location = new Point(20, 20);
            this.lblSendingAsCaption.Text = "You are sending as:";

            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Font = new Font("Segoe UI", 10F);
            this.lblCurrentUser.ForeColor = Color.White;
            this.lblCurrentUser.Location = new Point(20, 42);
            this.lblCurrentUser.MaximumSize = new Size(900, 0);
            this.lblCurrentUser.Text = "User name (email)";

            // Subject
            this.lblSubjectCaption.AutoSize = true;
            this.lblSubjectCaption.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            this.lblSubjectCaption.ForeColor = Color.White;
            this.lblSubjectCaption.Location = new Point(20, 80);
            this.lblSubjectCaption.Text = "Subject:";

            this.txtSubject.Location = new Point(20, 110);
            this.txtSubject.Font = new Font("Segoe UI", 10F);
            this.txtSubject.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtSubject.Size = new Size(600, 27);

            // Message
            this.lblMessageCaption.AutoSize = true;
            this.lblMessageCaption.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            this.lblMessageCaption.ForeColor = Color.White;
            this.lblMessageCaption.Location = new Point(20, 150);
            this.lblMessageCaption.Text = "Message:";

            this.txtMessage.Location = new Point(20, 180);
            this.txtMessage.Font = new Font("Segoe UI", 10F);
            this.txtMessage.Multiline = true;
            this.txtMessage.ScrollBars = ScrollBars.Vertical;
            this.txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtMessage.Size = new Size(600, 220);

            // Send button
            this.btnSend.Text = "Send message";
            this.btnSend.Size = new Size(160, 40);
            this.btnSend.PrimaryColor = Color.FromArgb(80, 120, 255);
            this.btnSend.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.btnSend.ForeColor = Color.White;
            this.btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            this.btnSend.Location = new Point(
                this.cardPanel.Width - this.btnSend.Width - 30,
                this.cardPanel.Height - this.btnSend.Height - 30
            );

            this.cardPanel.Resize += (s, e) =>
            {
                int innerWidth = cardPanel.ClientSize.Width - 40;
                if (innerWidth < 200) innerWidth = 200;

                txtSubject.Width = innerWidth;
                txtMessage.Width = innerWidth;

                int topMsg = txtMessage.Top;
                int bottomSpace = 80;
                int newMsgHeight = cardPanel.ClientSize.Height - topMsg - bottomSpace;
                if (newMsgHeight < 80) newMsgHeight = 80;
                txtMessage.Height = newMsgHeight;

                btnSend.Location = new Point(
                    cardPanel.ClientSize.Width - btnSend.Width - 30,
                    cardPanel.ClientSize.Height - btnSend.Height - 30
                );
            };

            this.cardPanel.Controls.Add(this.lblSendingAsCaption);
            this.cardPanel.Controls.Add(this.lblCurrentUser);
            this.cardPanel.Controls.Add(this.lblSubjectCaption);
            this.cardPanel.Controls.Add(this.txtSubject);
            this.cardPanel.Controls.Add(this.lblMessageCaption);
            this.cardPanel.Controls.Add(this.txtMessage);
            this.cardPanel.Controls.Add(this.btnSend);

            this.rightPanel.Controls.Add(this.cardPanel);

            this.contentPanel.Controls.Add(this.rightPanel);
            this.contentPanel.Controls.Add(this.leftPanel);

            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);

            this.headerPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.cardPanel.ResumeLayout(false);
            this.cardPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
