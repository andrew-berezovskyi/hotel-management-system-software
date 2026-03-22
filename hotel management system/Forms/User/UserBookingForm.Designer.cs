using System;
using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace hotel_management_system.Forms
{
    partial class UserBookingForm
    {
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel contentPanel;
        private Label lblTitle;
        private HopeButton btnBack;
        private PictureBox picRoom;
        private Label lblRoom;
        private Label lblPrice;
        private Label lblNights;
        private Label lblTotal;
        private DateTimePicker dtIn;
        private DateTimePicker dtOut;
        private Label lblIn;
        private Label lblOut;
        private Label lblPayment;
        private HopeComboBox cbPayment;
        private Label lblNotes;
        private HopeTextBox txtNotes;
        private HopeButton btnConfirm;

        // нові елементи
        private System.Windows.Forms.Panel descriptionPanel;
        private System.Windows.Forms.Panel rightPanel;
        private Label lblDescriptionTitle;
        private Label lblDescription;
        private Label lblHighlightsTitle;
        private Label lblHighlights;
        private Label lblServicesTitle;
        private Label lblServices;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // FORM
            this.BackColor = Color.FromArgb(30, 33, 45);
            this.ForeColor = Color.White;
            this.Text = "Booking";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // HEADER
            headerPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(25, 28, 38)
            };

            lblTitle = new Label
            {
                Text = "Booking",
                Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Width = 600,
                Padding = new Padding(30, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnBack = new HopeButton
            {
                Text = "Cancel",
                Size = new Size(120, 40),
                PrimaryColor = Color.FromArgb(120, 120, 120),
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.Width - 180, 15)
            };
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(btnBack);

            // CONTENT
            contentPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 33, 45),
                Padding = new Padding(32)
            };

            picRoom = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(45, 50, 70),
                Location = new Point(32, 96),
                Size = new Size(520, 300)
            };

            lblRoom = new Label
            {
                Text = "Room X • type",
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(580, 96),
                AutoSize = true
            };

            lblPrice = new Label
            {
                Text = "Price: ₴0 / night",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.Gainsboro,
                Location = new Point(580, 130),
                AutoSize = true
            };

            lblIn = new Label { Text = "Check-in:", ForeColor = Color.White, Location = new Point(580, 180), AutoSize = true };
            dtIn = new DateTimePicker { Format = DateTimePickerFormat.Short, Location = new Point(660, 176), Width = 160 };

            lblOut = new Label { Text = "Check-out:", ForeColor = Color.White, Location = new Point(840, 180), AutoSize = true };
            dtOut = new DateTimePicker { Format = DateTimePickerFormat.Short, Location = new Point(920, 176), Width = 160 };

            lblNights = new Label { Text = "Nights: 1", ForeColor = Color.Gainsboro, Location = new Point(580, 214), AutoSize = true };
            lblTotal = new Label
            {
                Text = "Total: ₴0",
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                Location = new Point(840, 212),
                AutoSize = true
            };

            lblPayment = new Label { Text = "Payment:", ForeColor = Color.White, Location = new Point(580, 256), AutoSize = true };
            cbPayment = new HopeComboBox
            {
                Location = new Point(660, 250),
                Size = new Size(180, 40),
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cbPayment.Items.AddRange(new object[] { "Online", "Card", "Cash", "Unpaid" });
            cbPayment.SelectedIndex = 0;

            lblNotes = new Label { Text = "Notes:", ForeColor = Color.White, Location = new Point(580, 306), AutoSize = true };
            txtNotes = new HopeTextBox { Location = new Point(660, 300), Size = new Size(420, 40), Hint = "Optional note" };

            btnConfirm = new HopeButton
            {
                Text = "Confirm booking",
                PrimaryColor = Color.FromArgb(80, 120, 255),
                Size = new Size(200, 44),
                Location = new Point(880, 360),
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold)
            };

            // === Панель опису знизу під картинкою ===
            descriptionPanel = new System.Windows.Forms.Panel
            {
                BackColor = Color.FromArgb(25, 28, 38),
                Location = new Point(32, 420),
                Size = new Size(520, 260),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top
            };

            lblDescriptionTitle = new Label
            {
                Text = "About this room",
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(16, 12),
                AutoSize = true
            };

            lblDescription = new Label
            {
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.Gainsboro,
                Location = new Point(16, 40),
                AutoSize = false,
                Size = new Size(descriptionPanel.Width - 32, descriptionPanel.Height - 56),
                MaximumSize = new Size(descriptionPanel.Width - 32, 0)
            };

            descriptionPanel.Controls.Add(lblDescriptionTitle);
            descriptionPanel.Controls.Add(lblDescription);

            // === Права панель: highlights + services ===
            rightPanel = new System.Windows.Forms.Panel
            {
                BackColor = Color.FromArgb(25, 28, 38),
                Location = new Point(1120, 96),
                Size = new Size(320, 584),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right
            };

            lblHighlightsTitle = new Label
            {
                Text = "Room highlights",
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(16, 16),
                AutoSize = true
            };

            lblHighlights = new Label
            {
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.Gainsboro,
                Location = new Point(16, 44),
                AutoSize = false,
                Size = new Size(rightPanel.Width - 32, 120),
                MaximumSize = new Size(rightPanel.Width - 32, 0)
            };

            lblServicesTitle = new Label
            {
                Text = "Included services",
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(16, 190),
                AutoSize = true
            };

            lblServices = new Label
            {
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.Gainsboro,
                Location = new Point(16, 218),
                AutoSize = false,
                Size = new Size(rightPanel.Width - 32, 200),
                MaximumSize = new Size(rightPanel.Width - 32, 0)
            };

            rightPanel.Controls.Add(lblHighlightsTitle);
            rightPanel.Controls.Add(lblHighlights);
            rightPanel.Controls.Add(lblServicesTitle);
            rightPanel.Controls.Add(lblServices);

            // ADD TO CONTENT
            contentPanel.Controls.Add(picRoom);
            contentPanel.Controls.Add(lblRoom);
            contentPanel.Controls.Add(lblPrice);
            contentPanel.Controls.Add(lblIn);
            contentPanel.Controls.Add(dtIn);
            contentPanel.Controls.Add(lblOut);
            contentPanel.Controls.Add(dtOut);
            contentPanel.Controls.Add(lblNights);
            contentPanel.Controls.Add(lblTotal);
            contentPanel.Controls.Add(lblPayment);
            contentPanel.Controls.Add(cbPayment);
            contentPanel.Controls.Add(lblNotes);
            contentPanel.Controls.Add(txtNotes);
            contentPanel.Controls.Add(btnConfirm);
            contentPanel.Controls.Add(descriptionPanel);
            contentPanel.Controls.Add(rightPanel);

            this.Controls.Add(contentPanel);
            this.Controls.Add(headerPanel);

            this.Resize += (_, __) =>
            {
                btnBack.Location = new Point(this.ClientSize.Width - btnBack.Width - 30, 15);

                if (descriptionPanel != null)
                {
                    descriptionPanel.Width = picRoom.Width;
                    lblDescription.Size = new Size(descriptionPanel.Width - 32, descriptionPanel.Height - 56);
                    lblDescription.MaximumSize = new Size(descriptionPanel.Width - 32, 0);
                }

                if (rightPanel != null)
                {
                    rightPanel.Location = new Point(
                        this.ClientSize.Width - rightPanel.Width - 40,
                        rightPanel.Location.Y);

                    lblHighlights.Size = new Size(rightPanel.Width - 32, 120);
                    lblHighlights.MaximumSize = new Size(rightPanel.Width - 32, 0);
                    lblServices.Size = new Size(rightPanel.Width - 32, 200);
                    lblServices.MaximumSize = new Size(rightPanel.Width - 32, 0);
                }
            };

            this.ResumeLayout(false);
        }
    }
}
