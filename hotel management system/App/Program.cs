using hotel_management_system.Forms;
using HotelApp;
using HotelManagementSystem.Persistence;
using System.Data.SQLite;


namespace hotel_management_system.App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ Ініціалізуємо базу через DatabaseInitializer,
            // який використовує шлях із appsettings.json
            DatabaseInitializer.Initialize();

            // ✅ Перевіряємо, чи існує хоч один адмін у тій самій базі,
            // яка задана в JSON
            if (!IsAdminExists())
            {
                // Якщо адміна ще нема — запускаємо форму початкового налаштування
                Application.Run(new AdminSetupForm());
            }
            else
            {
                // Інакше — стандартний вхід / welcome
                Application.Run(new WelcomeForm());
            }
        }

        private static bool IsAdminExists()
        {
            var dbPath = DatabaseInitializer.DbPath;

            // Якщо шлях не ініціалізований або файл не існує — вважаємо, що адміна нема
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath))
                return false;

            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE IsAdmin = 1", connection))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                // Якщо щось пішло не так — також повертаємо false, щоб можна було створити адміна
                return false;
            }
        }
    }
}
