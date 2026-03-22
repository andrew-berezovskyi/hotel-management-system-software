namespace HotelManagementSystem.Data.SqlQueries
{
    public static class RoomQueries
    {
        public const string CreateTable = @"
            CREATE TABLE IF NOT EXISTS Rooms (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Number TEXT NOT NULL,
                Type TEXT NOT NULL,
                Price REAL NOT NULL,
                Status TEXT NOT NULL,
                ImagePath TEXT
            );";

        public const string GetAll = "SELECT * FROM Rooms ORDER BY Id DESC;";

        public const string Insert = @"
            INSERT INTO Rooms (Number, Type, Price, Status, ImagePath)
            VALUES (@Number, @Type, @Price, @Status, @ImagePath);";

        public const string Update = @"
            UPDATE Rooms
            SET Number = @Number,
                Type = @Type,
                Price = @Price,
                Status = @Status,
                ImagePath = @ImagePath
            WHERE Id = @Id;";

        public const string Delete = "DELETE FROM Rooms WHERE Id=@Id;";

        public const string GetByNumber = "SELECT * FROM Rooms WHERE Number=@Number;";
    }
}
