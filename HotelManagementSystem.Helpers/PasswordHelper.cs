using System.Security.Cryptography;
using System.Text;

namespace hotel_management_system.Helpers
{
    public static class PasswordHelper
    {
        // Метод для створення хешу з пароля
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Метод для перевірки, чи співпадає пароль з хешем
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredHash = HashPassword(enteredPassword);
            return enteredHash == storedHash;
        }
    }
}
