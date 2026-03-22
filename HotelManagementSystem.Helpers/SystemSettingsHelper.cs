using System.Text.Json;

namespace hotel_management_system.Helpers
{
    public static class SystemSettingsHelper
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        public static (bool success, string message) SaveSettings(string email, string appPassword, string server, string port,
                                                                  string dbPath, string hotelName, string otpMinutes)
        {
            try
            {
                if (!int.TryParse(port, out int smtpPort) || smtpPort <= 0)
                    return (false, "Invalid SMTP port!");

                if (!int.TryParse(otpMinutes, out int otp) || otp <= 0)
                    return (false, "Invalid OTP duration!");

                if (!ValidationHelper.IsValidEmail(email))
                    return (false, "Invalid sender email address!");

                if (!ValidationHelper.IsNotEmpty(server))
                    return (false, "SMTP server cannot be empty.");

                if (!ValidationHelper.IsNotEmpty(hotelName))
                    return (false, "Hotel name cannot be empty.");

                var newConfig = new
                {
                    Database = new { Path = dbPath },
                    Email = new { Address = email, AppPassword = appPassword, Server = server, Port = smtpPort },
                    AppSettings = new { HotelName = hotelName, OTPExpireMinutes = otp }
                };

                string updatedJson = JsonSerializer.Serialize(newConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, updatedJson);

                string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\appsettings.json"));
                if (File.Exists(projectPath))
                    File.WriteAllText(projectPath, updatedJson);

                return (true, "Settings saved successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Error saving settings: {ex.Message}");
            }
        }

        public static (bool success, string message) BackupDatabase(string dbPath)
        {
            try
            {
                if (!File.Exists(dbPath))
                    return (false, "Database file not found!");

                string backupName = $"hotel_backup_{DateTime.Now:yyyyMMdd_HHmm}.db";
                string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), backupName);
                File.Copy(dbPath, dest, true);
                return (true, $"Backup created on Desktop: {backupName}");
            }
            catch (Exception ex)
            {
                return (false, $"Error creating backup: {ex.Message}");
            }
        }
    }
}

