using HotelManagementSystem.Models;

namespace hotel_management_system.Helpers
{
    /// <summary>
    /// Проста керованa сесія: зберігає поточного користувача, час активності і допоміжні перевірки ролей.
    /// </summary>
    public static class AuthHelper
    {
        /// <summary>Поточний користувач (null, якщо не залогінений).</summary>
        public static User CurrentUser { get; private set; }

        private static DateTime _loginUtc;
        private static DateTime _lastActivityUtc;

        /// <summary>Тривалість сесії без повторного входу.</summary>
        private static readonly TimeSpan SessionTtl = TimeSpan.FromHours(8);

        // --------- Sign in/out ---------
        public static void SetUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            CurrentUser = user;
            _loginUtc = _lastActivityUtc = DateTime.UtcNow;
        }

        public static void Logout()
        {
            CurrentUser = null;
            _loginUtc = _lastActivityUtc = DateTime.MinValue;
        }

        public static void RefreshActivity()
        {
            if (CurrentUser != null) _lastActivityUtc = DateTime.UtcNow;
        }

        // --------- Checks ---------
        /*public static bool IsSessionActive()
        {
            if (CurrentUser == null) return false;
            var now = DateTime.UtcNow;
            return (now - _loginUtc) <= SessionTtl && (now - _lastActivityUtc) <= SessionTtl;
        }
        */
        public static bool IsAdmin() =>
            CurrentUser?.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true;

        /*public static bool IsUser() =>
            CurrentUser?.Role?.Equals("User", StringComparison.OrdinalIgnoreCase) == true;

        public static bool HasRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role) || CurrentUser == null) return false;
            return CurrentUser.Role?.Equals(role, StringComparison.OrdinalIgnoreCase) == true;
        }

        public static string GetDisplayName() =>
            CurrentUser != null ? $"{CurrentUser.FullName} ({CurrentUser.Role})" : "Guest";
        */
    }
}
