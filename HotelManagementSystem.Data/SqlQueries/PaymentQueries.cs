namespace HotelManagementSystem.Data.SqlQueries
{
    public static class PaymentQueries
    {
        public const string CreateTable = @"
            CREATE TABLE IF NOT EXISTS Payments (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                BookingId INTEGER NOT NULL,
                Amount REAL NOT NULL,
                PaymentDate TEXT NOT NULL,
                PaymentMethod TEXT NOT NULL,
                Status TEXT DEFAULT 'Pending',
                Notes TEXT,
                FOREIGN KEY (BookingId) REFERENCES Bookings(Id)
            );";

        public const string GetAll = @"
            SELECT Id, BookingId, Amount, PaymentDate, PaymentMethod, Status, Notes
            FROM Payments
            ORDER BY Id DESC;";

        public const string Insert = @"
            INSERT INTO Payments (BookingId, Amount, PaymentDate, PaymentMethod, Status, Notes)
            VALUES (@BookingId, @Amount, @PaymentDate, @PaymentMethod, @Status, @Notes);";

        public const string Update = @"
            UPDATE Payments
            SET BookingId=@BookingId,
                Amount=@Amount,
                PaymentDate=@PaymentDate,
                PaymentMethod=@PaymentMethod,
                Status=@Status,
                Notes=@Notes
            WHERE Id=@Id;";

        public const string Delete = "DELETE FROM Payments WHERE Id=@Id;";
    }
}
