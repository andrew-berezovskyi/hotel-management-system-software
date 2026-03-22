namespace HotelManagementSystem.Data.SqlQueries
{
    public static class ReportQueries
    {
        public const string TotalIncome = @"
            SELECT IFNULL(SUM(Amount), 0)
            FROM Payments
            WHERE DATE(PaymentDate) BETWEEN @start AND @end;";

        public const string BookingStats = @"
            SELECT 
                COUNT(*) AS Total,
                SUM(CASE WHEN Status='Active' THEN 1 ELSE 0 END) AS Active,
                SUM(CASE WHEN Status='Completed' THEN 1 ELSE 0 END) AS Completed,
                SUM(CASE WHEN Status='Cancelled' THEN 1 ELSE 0 END) AS Cancelled
            FROM Bookings
            WHERE DATE(CheckInDate) BETWEEN @start AND @end;";

        public const string TotalRooms = "SELECT COUNT(*) FROM Rooms;";
        public const string OccupiedRooms = "SELECT COUNT(*) FROM Rooms WHERE Status='Occupied';";
        public const string TotalUsers = "SELECT COUNT(*) FROM Users;";
        public const string TotalStaff = "SELECT COUNT(*) FROM Staff;";
    }
}
