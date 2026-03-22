namespace HotelManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PassportNumber { get; set; }
        public string Phone { get; set; }

        public bool IsAdmin { get; set; }

        // Додаємо роль для зручності
        // Обчислювана властивість, що визначає роль користувача
        // на основі прапорця IsAdmin.
        public string Role
        {
            get => IsAdmin ? "Admin" : "User";
        }
    }
}
