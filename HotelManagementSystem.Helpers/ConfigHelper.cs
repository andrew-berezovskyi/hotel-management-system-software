// ConfigHelper — центральний доступ до appsettings.json: при старті шукає файл у кількох шляхах і завантажує IConfiguration.
// Дає методи читання налаштувань (SQLite шлях/ConnectionString, SMTP+AppPassword для OTP, дані готелю, час життя OTP),
// а також вміє оновлювати конфіг у файлі (UpdateSetting / SetHotelInfo / SaveAll) і перезавантажує конфіг після змін.


using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Windows.Forms;

namespace hotel_management_system.Helpers
{
    public static class ConfigHelper
    {
        private static IConfigurationRoot _configuration;
        private static readonly string[] PossiblePaths = new[]
        {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "appsettings.json")
        };

        static ConfigHelper()
        {
            LoadConfiguration();
        }

        // ------------------- LOAD CONFIG -------------------
        private static void LoadConfiguration()
        {
            try
            {
                string configPath = null;
                foreach (var path in PossiblePaths)
                {
                    if (File.Exists(path))
                    {
                        configPath = path;
                        break;
                    }
                }

                if (configPath == null)
                    throw new FileNotFoundException("Could not locate appsettings.json in known paths.");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(configPath)!)
                    .AddJsonFile(Path.GetFileName(configPath), optional: false, reloadOnChange: true);

                _configuration = builder.Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"⚠️ Configuration load failed:\n{ex.Message}",
                                "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _configuration = new ConfigurationBuilder().AddInMemoryCollection().Build(); // fallback
            }
        }

        // ------------------- DATABASE -------------------
        public static string GetDbPath() => _configuration["Database:Path"] ?? "Data\\hotel.db";

        public static string GetConnectionString()
        {
            string path = GetDbPath();
            return $"Data Source={path};Version=3;";
        }

        // ------------------- EMAIL SETTINGS -------------------
        public static string GetEmail() => _configuration["Email:Address"];
        public static string GetEmailPassword() => _configuration["Email:AppPassword"];
        public static string GetEmailServer() => _configuration["Email:Server"];
        public static int GetEmailPort() => int.TryParse(_configuration["Email:Port"], out int p) ? p : 587;

        // ------------------- APP SETTINGS -------------------
        public static string GetHotelName() => _configuration["AppSettings:HotelName"] ?? "The Hotel Kyiv";
        public static int GetOTPExpireMinutes() => int.TryParse(_configuration["AppSettings:OTPExpireMinutes"], out int t) ? t : 10;

        // ------------------- HOTEL INFO -------------------
        public static string GetHotelAddress() => _configuration["Hotel:Address"];
        public static string GetHotelPhone() => _configuration["Hotel:Phone"];
        public static string GetHotelEmail() => _configuration["Hotel:Email"];
        public static string GetHotelWebsite() => _configuration["Hotel:Website"];
        public static string GetHotelDescription() => _configuration["Hotel:Description"];
        public static int GetHotelStars()
        {
            int.TryParse(_configuration["Hotel:Stars"], out int stars);
            return stars == 0 ? 4 : stars;
        }
        public static string GetHotelLogoPath() => _configuration["Hotel:LogoPath"];

        // ------------------- UPDATE SINGLE VALUE -------------------
        public static void UpdateSetting(string section, string key, string value)
        {
            try
            {
                string path = GetExistingConfigPath();
                if (path == null)
                    throw new FileNotFoundException("Configuration file not found.");

                string json = File.ReadAllText(path);
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement.Clone();

                using var stream = new MemoryStream();
                using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });

                writer.WriteStartObject();

                foreach (var sectionElement in root.EnumerateObject())
                {
                    writer.WritePropertyName(sectionElement.Name);
                    if (sectionElement.Name.Equals(section, StringComparison.OrdinalIgnoreCase))
                    {
                        writer.WriteStartObject();
                        foreach (var kv in sectionElement.Value.EnumerateObject())
                        {
                            if (kv.Name.Equals(key, StringComparison.OrdinalIgnoreCase))
                                writer.WriteString(kv.Name, value);
                            else
                                kv.WriteTo(writer);
                        }
                        writer.WriteEndObject();
                    }
                    else
                    {
                        sectionElement.Value.WriteTo(writer);
                    }
                }

                writer.WriteEndObject();
                writer.Flush();

                File.WriteAllText(path, System.Text.Encoding.UTF8.GetString(stream.ToArray()));
                LoadConfiguration(); // reload after update
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ConfigHelper] Error updating setting: {ex.Message}");
            }
        }

        // ------------------- SAVE HOTEL INFO -------------------
        public static void SetHotelInfo(
            string name, string address, string phone,
            string email, string website, string description,
            int stars, string logoPath)
        {
            try
            {
                string path = GetExistingConfigPath();
                if (path == null)
                    throw new FileNotFoundException("Configuration file not found.");

                string json = File.ReadAllText(path);
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement.Clone();

                using var stream = new MemoryStream();
                using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });

                writer.WriteStartObject();

                foreach (var section in root.EnumerateObject())
                {
                    if (section.Name.Equals("Hotel", StringComparison.OrdinalIgnoreCase))
                    {
                        writer.WritePropertyName("Hotel");
                        writer.WriteStartObject();
                        writer.WriteString("Name", name);
                        writer.WriteString("Address", address);
                        writer.WriteString("Phone", phone);
                        writer.WriteString("Email", email);
                        writer.WriteString("Website", website);
                        writer.WriteString("Description", description);
                        writer.WriteNumber("Stars", stars);
                        writer.WriteString("LogoPath", logoPath);
                        writer.WriteEndObject();
                    }
                    else
                    {
                        section.Value.WriteTo(writer);
                    }
                }

                writer.WriteEndObject();
                writer.Flush();

                File.WriteAllText(path, System.Text.Encoding.UTF8.GetString(stream.ToArray()));
                LoadConfiguration();

                Console.WriteLine("[ConfigHelper] Hotel info saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ConfigHelper] Error saving hotel info: {ex.Message}");
            }
        }

        // ------------------- SAVE FULL CONFIG -------------------
        public static void SaveAll(object newConfig)
        {
            try
            {
                string path = GetExistingConfigPath() ?? PossiblePaths[0];
                string json = JsonSerializer.Serialize(newConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
                LoadConfiguration();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ConfigHelper] Error saving full config: {ex.Message}");
            }
        }

        // ------------------- HELPERS -------------------
        private static string GetExistingConfigPath()
        {
            foreach (var path in PossiblePaths)
                if (File.Exists(path))
                    return path;
            return null;
        }
    }
}
