using hotel_management_system.Helpers;
using hotel_management_system.Services;  
using hotel_management_system.UIHelpers;


namespace hotel_management_system.Forms
{
    public partial class SystemSettingsForm : Form
    {
        public SystemSettingsForm()
        {
            InitializeComponent();

            FormAnimationHelper.AttachFadeInOnVisible(this);

            LoadSettings();

            btnSave.Click += BtnSave_Click;
            btnReset.Click += (_, __) =>
            {
                LoadSettings();
                ToastHelper.ShowToast(this, "Settings restored.", ToastType.Info);
            };
            btnBackupDB.Click += BtnBackupDB_Click;
            btnChangePath.Click += BtnChangePath_Click;
            btnTestEmail.Click += async (_, __) =>
                await EmailTestService.TestEmailAsync(this, txtEmail.Text);
            btnBack.Click += (_, __) => NavigationHelper.ReturnToDashboard(this);

            // Обробник кнопки генерації демо-даних
            if (btnSeedDemoData != null)
            {
                btnSeedDemoData.Click += async (_, __) =>
                {
                    btnSeedDemoData.Enabled = false;
                    var oldText = btnSeedDemoData.Text;
                    btnSeedDemoData.Text = "Generating...";

                    try
                    {
                        var result = await DemoDataSeeder.SeedAsync();
                        bool success = result.success;
                        string message = result.message;

                        ToastHelper.ShowToast(
                            this,
                            message,
                            success ? ToastType.Success : ToastType.Error);
                    }
                    catch (Exception ex)
                    {
                        ToastHelper.ShowToast(
                            this,
                            "Error while generating demo data: " + ex.Message,
                            ToastType.Error);
                    }
                    finally
                    {
                        btnSeedDemoData.Enabled = true;
                        btnSeedDemoData.Text = oldText;
                    }
                };
            }
        }

        private void LoadSettings()
        {
            try
            {
                txtEmail.Text = ConfigHelper.GetEmail();
                txtAppPassword.Text = ConfigHelper.GetEmailPassword();
                txtServer.Text = ConfigHelper.GetEmailServer();
                txtPort.Text = ConfigHelper.GetEmailPort().ToString();
                txtDbPath.Text = ConfigHelper.GetConnectionString()
                    .Replace("Data Source=", "")
                    .Replace(";Version=3;", "");
                txtHotelName.Text = ConfigHelper.GetHotelName();
                txtOTPMinutes.Text = ConfigHelper.GetOTPExpireMinutes().ToString();

                ToastHelper.ShowToast(this, "Settings loaded successfully!", ToastType.Success);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowToast(
                    this,
                    $"Failed to load settings: {ex.Message}",
                    ToastType.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var (success, msg) = SystemSettingsHelper.SaveSettings(
                txtEmail.Text,
                txtAppPassword.Text,
                txtServer.Text,
                txtPort.Text,
                txtDbPath.Text,
                txtHotelName.Text,
                txtOTPMinutes.Text
            );

            ToastHelper.ShowToast(this, msg, success ? ToastType.Success : ToastType.Error);
        }

        private void BtnBackupDB_Click(object sender, EventArgs e)
        {
            var (success, msg) = SystemSettingsHelper.BackupDatabase(txtDbPath.Text);
            ToastHelper.ShowToast(this, msg, success ? ToastType.Success : ToastType.Error);
        }

        private void BtnChangePath_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "SQLite Database (*.db)|*.db",
                Title = "Select Database Location"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDbPath.Text = dialog.FileName;
                ToastHelper.ShowToast(this, "Database path updated!", ToastType.Info);
            }
        }
    }
}
