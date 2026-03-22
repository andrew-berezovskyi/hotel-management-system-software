using hotel_management_system.Helpers;
using hotel_management_system.UIHelpers;

namespace hotel_management_system.Forms
{
    public partial class HotelInfoForm : Form
    {
        // Зберігаю відносний шлях типу "Data\\logo.png"
        private string _logoPath = "Data\\logo.png";

        public HotelInfoForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            btnChangeLogo.Click += BtnChangeLogo_Click;
            btnSave.Click += BtnSave_Click;
            btnReset.Click += BtnReset_Click;
            btnBack.Click += (_, __) => NavigationHelper.ReturnToDashboard(this);

            LoadHotelInfo();
        }

        private void LoadHotelInfo()
        {
            try
            {
                txtName.Text = ConfigHelper.GetHotelName();
                txtAddress.Text = ConfigHelper.GetHotelAddress();
                txtPhone.Text = ConfigHelper.GetHotelPhone();
                txtEmail.Text = ConfigHelper.GetHotelEmail();
                txtWebsite.Text = ConfigHelper.GetHotelWebsite();
                txtDescription.Text = ConfigHelper.GetHotelDescription();
                txtStars.Value = ConfigHelper.GetHotelStars();

                // шлях до логотипу з конфіга (може бути null/порожній)
                var configLogoPath = ConfigHelper.GetHotelLogoPath();
                if (!string.IsNullOrWhiteSpace(configLogoPath))
                    _logoPath = configLogoPath;

                // Перетворюємо у абсолютний шлях
                string absoluteLogoPath = Path.IsPathRooted(_logoPath)
                    ? _logoPath
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _logoPath);

                if (File.Exists(absoluteLogoPath))
                {
                    if (picLogo.Image != null)
                    {
                        picLogo.Image.Dispose();
                        picLogo.Image = null;
                    }

                    using (var img = Image.FromFile(absoluteLogoPath))
                    {
                        picLogo.Image = new Bitmap(img);
                    }
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(this, $"Failed to load hotel info: {ex.Message}", ToastType.Error);
            }
        }

        private void BtnChangeLogo_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Select Hotel Logo",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                if (!Directory.Exists(dataDir))
                    Directory.CreateDirectory(dataDir);

                string destPath = Path.Combine(dataDir, "logo.png");

                File.Copy(dialog.FileName, destPath, true);

                _logoPath = Path.Combine("Data", "logo.png");

                if (picLogo.Image != null)
                {
                    picLogo.Image.Dispose();
                    picLogo.Image = null;
                }

                using (var img = Image.FromFile(destPath))
                {
                    picLogo.Image = new Bitmap(img);
                }

                ToastHelper.ShowToast(this, "Logo updated! Don't forget to save changes.", ToastType.Info);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(this, $"Error updating logo: {ex.Message}", ToastType.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigHelper.SetHotelInfo(
                    txtName.Text,
                    txtAddress.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtWebsite.Text,
                    txtDescription.Text,
                    (int)txtStars.Value,
                    _logoPath   // зберігаємо наш відносний шлях "Data\\logo.png"
                );

                ToastHelper.ShowToast(this, "Hotel information saved successfully!", ToastType.Success);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(this, $"Error saving info: {ex.Message}", ToastType.Error);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            LoadHotelInfo();
            ToastHelper.ShowToast(this, "Hotel info reset to last saved values.", ToastType.Info);
        }
    }
}
