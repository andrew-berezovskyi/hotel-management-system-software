using System.Text;

namespace hotel_management_system.Helpers
{
    public static class LoggerHelper
    {
        private static readonly string LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string LogFile = Path.Combine(LogDir, $"log_{DateTime.Now:yyyyMMdd}.txt");
        private static readonly object LockObj = new();

        static LoggerHelper()
        {
            try
            {
                if (!Directory.Exists(LogDir))
                    Directory.CreateDirectory(LogDir);

                // автоочищення старих логів (старші за 30 днів)
                foreach (var file in Directory.GetFiles(LogDir, "log_*.txt"))
                {
                    var info = new FileInfo(file);
                    if (info.CreationTime < DateTime.Now.AddDays(-30))
                        info.Delete();
                }
            }
            catch
            {
                // ігноруємо, щоб логер не спричиняв збоїв
            }
        }

        /// <summary>
        /// Записує повідомлення у лог із часовою міткою.
        /// </summary>
        public static void Log(string message)
        {
            try
            {
                lock (LockObj)
                {
                    using var writer = new StreamWriter(LogFile, true, Encoding.UTF8);
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] INFO: {message}");
                }
            }
            catch
            {
                // ніколи не кидаємо виключення з логера
            }
        }

        /// <summary>
        /// Записує Exception у лог (з типом, повідомленням і стеком).
        /// </summary>
        public static void LogException(Exception ex, string context = "")
        {
            try
            {
                lock (LockObj)
                {
                    using var writer = new StreamWriter(LogFile, true, Encoding.UTF8);
                    writer.WriteLine("==================================================");
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR in {context}");
                    writer.WriteLine($"Type: {ex.GetType().Name}");
                    writer.WriteLine($"Message: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    if (ex.InnerException != null)
                        writer.WriteLine($"InnerException: {ex.InnerException.Message}");
                    writer.WriteLine("==================================================\n");
                }
            }
            catch
            {
                // не дозволяємо логеру кидати виключення
            }
        }
    }
}
