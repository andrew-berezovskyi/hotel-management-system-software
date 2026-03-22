namespace HotelManagementSystem.Data.Queries
{
    public static class StaffQueries
    {
        public const string CreateTable = @"
            CREATE TABLE IF NOT EXISTS Staff (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT NOT NULL,
                Position TEXT NOT NULL,
                Phone TEXT,
                Email TEXT,
                HireDate TEXT NOT NULL,
                Salary REAL DEFAULT 0,
                Status TEXT DEFAULT 'Active'
            );";

        public const string GetAll = @"
            SELECT * FROM Staff";

        public const string GetAllFiltered = @"
            SELECT * FROM Staff
            WHERE FullName LIKE @q OR Position LIKE @q OR Status LIKE @q;";

        public const string Insert = @"
            INSERT INTO Staff 
                (FullName, Position, Phone, Email, HireDate, Salary, Status)
            VALUES 
                (@FullName, @Position, @Phone, @Email, @HireDate, @Salary, @Status);";

        public const string Update = @"
            UPDATE Staff SET 
                FullName = @FullName,
                Position = @Position,
                Phone = @Phone,
                Email = @Email,
                HireDate = @HireDate,
                Salary = @Salary,
                Status = @Status
            WHERE Id = @Id;";

        public const string Delete = "DELETE FROM Staff WHERE Id=@Id;";
        public const string GetById = "SELECT * FROM Staff WHERE Id=@Id;";
    }
}
