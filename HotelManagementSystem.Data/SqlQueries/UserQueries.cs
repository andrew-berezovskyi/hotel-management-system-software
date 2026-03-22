namespace HotelManagementSystem.Data.SqlQueries
{
    public static class UserQueries
    {
        public const string CreateTable = @"
            CREATE TABLE IF NOT EXISTS Users (
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
            );";

        public const string Exists = @"
            SELECT COUNT(*) FROM Users
            WHERE Username=@Username OR Email=@Email OR PassportNumber=@Passport OR Phone=@Phone;";

        public const string Insert = @"
            INSERT INTO Users
            (FullName, Username, Email, PasswordHash, PassportNumber, Phone, IsAdmin)
            VALUES (@FullName, @Username, @Email, @PasswordHash, @PassportNumber, @Phone, @IsAdmin);";

        // LOGIN
        public const string LoginByUsername = "SELECT * FROM Users WHERE Username=@input;";
        public const string LoginByEmail = "SELECT * FROM Users WHERE Email=@input;";
        public const string LoginByPhone = "SELECT * FROM Users WHERE Phone=@input;";

        // PASSWORD RESET
        public const string ExistsByEmail = "SELECT COUNT(*) FROM Users WHERE Email=@Email;";
        public const string UpdateResetCode = "UPDATE Users SET ResetCode=@Code WHERE Email=@Email;";
        public const string VerifyResetCode = "SELECT COUNT(*) FROM Users WHERE Email=@Email AND ResetCode=@Code;";
        public const string ResetPassword = "UPDATE Users SET PasswordHash=@Hash, ResetCode=NULL WHERE Email=@Email;";

        // MANAGEMENT
        public const string GetAll = @"
            SELECT Id, FullName, Username, Email, PasswordHash, PassportNumber, Phone, IsAdmin FROM Users";

        public const string SearchFilter = @"
            WHERE FullName LIKE @q OR Username LIKE @q OR Email LIKE @q OR Phone LIKE @q OR PassportNumber LIKE @q";

        public const string Update = @"
            UPDATE Users
            SET FullName=@FullName,
                Username=@Username,
                Email=@Email,
                PassportNumber=@PassportNumber,
                Phone=@Phone,
                IsAdmin=@IsAdmin
            WHERE Id=@Id;";

        public const string Delete = "DELETE FROM Users WHERE Id=@Id;";
    }
}
