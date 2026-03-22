using System.Text.RegularExpressions;

namespace hotel_management_system.Helpers
{
    public static class ValidationHelper
    {
        // ---------------- BASIC CHECKS ----------------
        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /*public static bool HasMinLength(string value, int minLength)
        {
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
        }
        */

        // ---------------- EMAIL ----------------
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            return Regex.IsMatch(email,
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                RegexOptions.IgnoreCase);
        }

        // ---------------- PHONE ----------------
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return Regex.IsMatch(phone, @"^0\d{9}$");
        }

        // ---------------- PASSPORT ----------------
        public static bool IsValidPassport(string passport)
        {
            if (string.IsNullOrWhiteSpace(passport)) return false;

            bool oldFormat = Regex.IsMatch(passport, @"^[A-ZА-ЯІЇЄ]{2}\d{6}$", RegexOptions.IgnoreCase);
            bool newFormat = Regex.IsMatch(passport, @"^\d{9}$");

            return oldFormat || newFormat;
        }

        // ---------------- PASSWORD ----------------
        public static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).{8,}$");
        }

        // ---------------- CONFIRM PASSWORD ----------------
        public static bool IsPasswordMatch(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        // ✅ Додано короткий аналог для зручності у формі
        public static bool IsValidPasswordMatch(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        // ---------------- USERNAME ----------------
        /*public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return Regex.IsMatch(username, @"^[a-zA-Z0-9._-]{3,20}$");
        }
        */
    }
}

