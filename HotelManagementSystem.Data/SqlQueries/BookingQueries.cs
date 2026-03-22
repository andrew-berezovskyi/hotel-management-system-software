// SQL-запити винесені в окремі файли Queries для відокремлення
// логіки доступу до БД від бізнес-логіки та UI,
// що покращує читабельність, підтримуваність і масштабованість проєкту.
namespace HotelManagementSystem.Data.SqlQueries
{
    public static class BookingQueries
    {
        public const string CreateTable = @"
            CREATE TABLE IF NOT EXISTS Bookings (
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
            );";

        public const string AutoUpdateBookings = @"
            UPDATE Bookings
            SET Status = 'Completed'
            WHERE Status = 'Active'
              AND date(CheckOutDate) < date('now');";

        public const string AutoUpdateRooms = @"
            UPDATE Rooms
            SET Status = 'Available'
            WHERE Id IN (
                SELECT RoomId FROM Bookings
                WHERE Status = 'Completed'
                  AND date(CheckOutDate) < date('now')
            );";

        // ---- СПИСОК УСІХ БРОНЮВАНЬ (для адміна) ----
        public const string GetAll = @"
            SELECT 
                b.Id,
                b.UserId,
                b.RoomId,
                u.FullName AS UserName,
                r.Number AS RoomNumber,
                b.CheckInDate,
                b.CheckOutDate,
                b.TotalPrice,
                b.Status,
                b.PaymentMethod,
                b.Notes,
                b.CreatedAt
            FROM Bookings b
            JOIN Users u ON b.UserId = u.Id
            JOIN Rooms r ON b.RoomId = r.Id";

        // ---- БРОНЮВАННЯ КОНКРЕТНОГО КОРИСТУВАЧА ----
        public const string GetByUser = @"
            SELECT 
                b.Id,
                b.UserId,
                b.RoomId,
                u.FullName AS UserName,
                r.Number AS RoomNumber,
                b.CheckInDate,
                b.CheckOutDate,
                b.TotalPrice,
                b.Status,
                b.PaymentMethod,
                b.Notes,
                b.CreatedAt
            FROM Bookings b
            JOIN Users u ON b.UserId = u.Id
            JOIN Rooms r ON b.RoomId = r.Id
            WHERE b.UserId = @UserId";

        public const string Insert = @"
            INSERT INTO Bookings 
                (UserId, RoomId, CheckInDate, CheckOutDate, TotalPrice, Status, PaymentMethod, Notes)
            VALUES 
                (@UserId, @RoomId, @CheckInDate, @CheckOutDate, @TotalPrice, @Status, @PaymentMethod, @Notes);
            SELECT last_insert_rowid();";

        public const string Update = @"
            UPDATE Bookings SET 
                UserId=@UserId,
                RoomId=@RoomId,
                CheckInDate=@CheckInDate,
                CheckOutDate=@CheckOutDate,
                TotalPrice=@TotalPrice,
                Status=@Status,
                PaymentMethod=@PaymentMethod,
                Notes=@Notes
            WHERE Id=@Id;";

        // Фізичне видалення (може ще знадобитись, залишаємо)
        public const string Delete = "DELETE FROM Bookings WHERE Id=@Id;";

        // Оновити статус кімнати
        public const string UpdateRoomStatus = "UPDATE Rooms SET Status=@Status WHERE Id=@Id;";

        // Скасування адміном (за Id)
        public const string CancelByAdmin = @"
            UPDATE Bookings
            SET Status = 'Cancelled'
            WHERE Id = @Id;";

        // Скасування користувачем (перестраховка по UserId)
        public const string CancelByUser = @"
            UPDATE Bookings
            SET Status = 'Cancelled'
            WHERE Id = @Id AND UserId = @UserId;";
    }
}
