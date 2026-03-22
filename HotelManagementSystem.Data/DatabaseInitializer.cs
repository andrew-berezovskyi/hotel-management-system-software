using hotel_management_system.Helpers;
using Microsoft.Data.Sqlite;

namespace HotelManagementSystem.Persistence
{
    public static class DatabaseInitializer
    {
        public static string DbPath { get; private set; }

        private static string GetAbsoluteDbPath()
        {
            var relativeDbPath = ConfigHelper.GetDbPath();
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var absolutePath = Path.Combine(baseDir, relativeDbPath);
            var dataDir = Path.GetDirectoryName(absolutePath);

            if (!string.IsNullOrEmpty(dataDir) && !Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);

            return absolutePath;
        }

        public static void Initialize()
        {
            DbPath = GetAbsoluteDbPath();

            if (File.Exists(DbPath))
            {
                var fi = new FileInfo(DbPath);
                if (fi.Length == 0) fi.Delete();
            }

            CreateTablesWithRetryOnInvalidFile();
        }

        private static void CreateTablesWithRetryOnInvalidFile()
        {
            var connectionString = $"Data Source={DbPath}";
            bool retried = false;

            while (true)
            {
                try
                {
                    using var connection = new SqliteConnection(connectionString);
                    connection.Open();

                    var commands = new[]
                    {
                        // --- USERS TABLE ---
                        @"CREATE TABLE IF NOT EXISTS Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            FullName TEXT NOT NULL,
                            Username TEXT NOT NULL UNIQUE,
                            Email TEXT NOT NULL UNIQUE,
                            PasswordHash TEXT NOT NULL,
                            PassportNumber TEXT NOT NULL UNIQUE,
                            Phone TEXT NOT NULL UNIQUE,
                            Role TEXT DEFAULT 'user',
                            IsAdmin INTEGER DEFAULT 0,
                            ResetCode TEXT
                        );",

                        // --- ROOMS TABLE (оновлено під RoomService) ---
                        @"CREATE TABLE IF NOT EXISTS Rooms (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Number TEXT NOT NULL,
                            Type TEXT NOT NULL,
                            Price REAL NOT NULL,
                            Status TEXT NOT NULL,
                            ImagePath TEXT
                        );",

                        // --- BOOKINGS TABLE ---
 
                        @"CREATE TABLE IF NOT EXISTS Bookings (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            UserId INTEGER NOT NULL,
                            RoomId INTEGER NOT NULL,
                            CheckInDate TEXT NOT NULL,
                            CheckOutDate TEXT NOT NULL,
                            TotalPrice REAL DEFAULT 0,
                            Status TEXT DEFAULT 'Active',
                            PaymentMethod TEXT DEFAULT 'Unpaid',
                            Notes TEXT,
                            CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
                            FOREIGN KEY (UserId) REFERENCES Users(Id),
                            FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
                        )",

                        // ---------------- PAYMENTS TABLE ----------------
                        
                        @"CREATE TABLE IF NOT EXISTS Payments (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            BookingId INTEGER NOT NULL,
                            Amount REAL NOT NULL,
                            PaymentDate TEXT NOT NULL,
                            PaymentMethod TEXT NOT NULL,
                            Status TEXT DEFAULT 'Pending',
                            Notes TEXT,
                            FOREIGN KEY (BookingId) REFERENCES Bookings(Id)
                        );",

                        // --- STAFF TABLE ---
                        @"CREATE TABLE IF NOT EXISTS Staff (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            FullName TEXT NOT NULL,
                            Position TEXT NOT NULL,
                            Phone TEXT,
                            Email TEXT,
                            HireDate TEXT NOT NULL,
                            Salary REAL DEFAULT 0,
                            Status TEXT DEFAULT 'Active'
                        );"


                    };

                    foreach (var sql in commands)
                    {
                        using var cmd = connection.CreateCommand();
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }

                    break; // успішно
                }
                catch (SqliteException ex) when (ex.SqliteErrorCode == 26 && !retried)
                {
                    if (File.Exists(DbPath))
                    {
                        File.Delete(DbPath);
                        retried = true;
                        continue;
                    }
                }
            }
        }
    }
}


