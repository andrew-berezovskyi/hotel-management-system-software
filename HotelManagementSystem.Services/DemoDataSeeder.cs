using hotel_management_system.Helpers;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.SqlQueries;
using HotelManagementSystem.Data.Queries;

namespace hotel_management_system.Services
{
    /// <summary>
    /// Генерує демо-дані: номери, користувачів, персонал.
    /// Працює акуратно: 
    /// - додає кімнати тільки якщо їх мало;
    /// - додає персонал тільки якщо його мало;
    /// - завжди може додати трохи demo-юзерів (не чіпає твого адміна).
    /// </summary>
    public static class DemoDataSeeder
    {
        private static readonly Random _rand = new Random();

        public static Task<(bool success, string message)> SeedAsync()
        {
            // Робимо у фоні, щоб UI не підвисав
            return Task.Run<(bool success, string message)>(() =>
            {
                try
                {
                    // 1) Гарантуємо, що таблиці існують
                    DbHelper.ExecuteNonQuery(UserQueries.CreateTable);
                    DbHelper.ExecuteNonQuery(RoomQueries.CreateTable);
                    DbHelper.ExecuteNonQuery(BookingQueries.CreateTable);
                    DbHelper.ExecuteNonQuery(PaymentQueries.CreateTable);
                    DbHelper.ExecuteNonQuery(StaffQueries.CreateTable);

                    // 2) Збираємо поточну статистику
                    int roomsBefore = Convert.ToInt32(
                        DbHelper.ExecuteScalar(ReportQueries.TotalRooms) ?? 0);
                    int staffBefore = Convert.ToInt32(
                        DbHelper.ExecuteScalar(ReportQueries.TotalStaff) ?? 0);

                    // 3) Додаємо демо-дані
                    int roomsAdded = SeedRooms(targetTotal: 50, current: roomsBefore);
                    int staffAdded = SeedStaff(targetTotal: 20, current: staffBefore);
                    int usersAdded = SeedUsers(addCount: 100); // просто додаємо ~100 юзерів

                    string msg =
                        $"Demo data generated.\n" +
                        $"Rooms added: {roomsAdded}, Staff added: {staffAdded}, Users added: {usersAdded}.";

                    LoggerHelper.Log(msg);
                    return (true, msg);
                }
                catch (Exception ex)
                {
                    LoggerHelper.LogException(ex, "DemoDataSeeder.SeedAsync");
                    return (false, "Error while generating demo data: " + ex.Message);
                }
            });
        }

        // ---------- ROOMS ----------
        private static int SeedRooms(int targetTotal, int current)
        {
            int toCreate = Math.Max(0, targetTotal - current);
            if (toCreate <= 0) return 0;

            string[] types =
            {
                "Standard Room",
                "Superior Room",
                "Deluxe Room",
                "Junior Suite",
                "Presidential Suite"
            };

            int created = 0;
            for (int i = 0; i < toCreate; i++)
            {
                string number = (100 + current + i).ToString();
                string type = types[_rand.Next(types.Length)];
                decimal price = 2500 + _rand.Next(0, 15001); // 2500–17500

                DbHelper.ExecuteNonQuery(RoomQueries.Insert, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Number", number);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Status", "Available");
                    cmd.Parameters.AddWithValue("@ImagePath", string.Empty);
                });

                created++;
            }

            return created;
        }

        // ---------- STAFF ----------
        private static int SeedStaff(int targetTotal, int current)
        {
            int toCreate = Math.Max(0, targetTotal - current);
            if (toCreate <= 0) return 0;

            string[] positions =
            {
                "Receptionist",
                "Manager",
                "Housekeeper",
                "Chef",
                "Concierge"
            };

            string[] statuses = { "Active", "On Leave" };

            int created = 0;
            for (int i = 0; i < toCreate; i++)
            {
                string fullName = $"Staff Member {current + i + 1:000}";
                string position = positions[_rand.Next(positions.Length)];
                string phone = "0" + _rand.Next(500000000, 999999999).ToString();
                string email = $"staff{current + i + 1:000}@demo.local";
                string hireDate = DateTime.Today
                    .AddDays(-_rand.Next(30, 365))
                    .ToString("yyyy-MM-dd");
                decimal salary = 15000 + _rand.Next(0, 15001);
                string status = statuses[_rand.Next(statuses.Length)];

                DbHelper.ExecuteNonQuery(StaffQueries.Insert, cmd =>
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Position", position);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@HireDate", hireDate);
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@Status", status);
                });

                created++;
            }

            return created;
        }

        // ---------- USERS ----------
        private static int SeedUsers(int addCount)
        {
            // Тут спеціально НЕ перевіряємо кількість:
            // просто додаємо певну кількість демо-юзерів з унікальними логінами/мейлами.
            int created = 0;

            for (int i = 0; i < addCount; i++)
            {
                string username = $"user{i + 1:000}";
                string email = $"user{i + 1:000}@demo.local";
                string fullName = $"Demo User {i + 1:000}";
                string passport = $"AA{i + 1:000000}";
                string phone = "0" + _rand.Next(500000000, 999999999).ToString();
                string passwordHash = PasswordHelper.HashPassword("Password123!");

                DbHelper.ExecuteNonQuery(UserQueries.Insert, cmd =>
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@PassportNumber", passport);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@IsAdmin", 0);
                });

                created++;
            }

            return created;
        }
    }
}
