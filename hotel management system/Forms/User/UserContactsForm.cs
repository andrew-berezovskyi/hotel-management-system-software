using System.Diagnostics;
using hotel_management_system.Helpers;
using hotel_management_system.UIHelpers;
using HotelManagementSystem.Forms;
using HotelManagementSystem.Services;

namespace hotel_management_system.Forms
{
    public partial class UserContactsForm : Form
    {
        private readonly ContactService _contactService = new ContactService();

        public UserContactsForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            WireEvents();
            FillHotelInfo();
            PrefillUserInfo();
        }

        private void WireEvents()
        {
            btnBack.Click += BtnBack_Click;
            btnSend.Click += BtnSend_Click;
            linkWebsite.LinkClicked += LinkWebsite_LinkClicked;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            var parent = Application // var використовується для неявного визначення типу
                .OpenForms
                .OfType<UserDashboardForm>()
                .FirstOrDefault();

            if (parent != null)
            {
                if (!parent.Visible)
                    parent.Show();

                parent.WindowState = FormWindowState.Normal;
                parent.BringToFront();
            }

            Close();
        }

        private void FillHotelInfo()
        {
            lblHotelName.Text = ConfigHelper.GetHotelName();
            lblAddressValue.Text = ConfigHelper.GetHotelAddress();
            lblPhoneValue.Text = ConfigHelper.GetHotelPhone();
            lblEmailValue.Text = ConfigHelper.GetHotelEmail();

            // URL беремо як є з конфіга, нічого не додаємо
            var url = ConfigHelper.GetHotelWebsite();
            url = url?.Trim(); // Trim прибирає зайві пробіли на початку і в кінці

            if (string.IsNullOrWhiteSpace(url))
            {
                linkWebsite.Text = "Website is not configured";
                linkWebsite.Enabled = false;
                linkWebsite.Links.Clear();
                return;
            }

            linkWebsite.Enabled = true;
            linkWebsite.Text = url;
            linkWebsite.Links.Clear();

            // робимо лінком ВСЕ посилання
            linkWebsite.Links.Add(0, url.Length, url);
        }

        private void PrefillUserInfo()
        {
            var user = AuthHelper.CurrentUser;

            if (user != null)
            {
                lblCurrentUser.Text = $"{user.FullName} ({user.Email})";
            }
            else
            {
                lblCurrentUser.Text = "Guest (not logged in)";
            }
        }

        // =======================
        //      LINK CLICK
        // =======================
        private void LinkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = e.Link.LinkData as string ?? linkWebsite.Text;

                linkWebsite.LinkVisited = true;

                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Cannot open the website:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =======================
        //      SEND MESSAGE
        // =======================
        private async void BtnSend_Click(object sender, EventArgs e)
        {
            var user = AuthHelper.CurrentUser;

            if (user == null)
            {
                MessageBox.Show(
                    "Please log in to send a message.",
                    "Login required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            var subject = txtSubject.Text.Trim();
            var message = txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(message))
            {
                ToastHelper.ShowToast(this,
                    "Please fill in both subject and message.",
                    ToastType.Warning);
                return;
            }

            bool ok = await _contactService.SendUserMessageAsync(
                user.FullName,
                user.Email,
                subject,
                message
            );

            if (ok)
            {
                ToastHelper.ShowToast(this,
                    "Message sent successfully. We will contact you soon.",
                    ToastType.Success);

                txtSubject.Text = "";
                txtMessage.Text = "";
            }
            else
            {
                ToastHelper.ShowToast(this,
                    "Failed to send message. Please try again later.",
                    ToastType.Error);
            }
        }
    }
}
