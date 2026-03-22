using hotel_management_system.Helpers;
using HotelManagementSystem.Models;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.SqlQueries;

namespace hotel_management_system.Services
{
    public class ReportService
    {
        public ReportService()
        {
            // у звітах таблиці не створюються, тому нічого ініціалізувати не треба
        }

        public ReportSummary GetReport(DateTime start, DateTime end)
        {
            var result = new ReportSummary();
            string startDate = start.ToString("yyyy-MM-dd");
            string endDate = end.ToString("yyyy-MM-dd");

            try
            {
                // ---- ДОХІД ----
                object income = DbHelper.ExecuteScalar(ReportQueries.TotalIncome, cmd =>
                {
                    cmd.Parameters.AddWithValue("@start", startDate);
                    cmd.Parameters.AddWithValue("@end", endDate);
                });
                result.TotalIncome = Convert.ToDecimal(income ?? 0);

                // ---- БРОНЮВАННЯ ----
                DbHelper.ExecuteQuery(ReportQueries.BookingStats,
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@start", startDate);
                        cmd.Parameters.AddWithValue("@end", endDate);
                    },
                    reader =>
                    {
                        if (reader.Read())
                        {
                            result.TotalBookings = Convert.ToInt32(reader["Total"]);
                            result.ActiveBookings = Convert.ToInt32(reader["Active"]);
                            result.CompletedBookings = Convert.ToInt32(reader["Completed"]);
                            result.CancelledBookings = Convert.ToInt32(reader["Cancelled"]);
                        }
                    });

                // ---- КІМНАТИ ----
                object totalRooms = DbHelper.ExecuteScalar(ReportQueries.TotalRooms);
                object occupiedRooms = DbHelper.ExecuteScalar(ReportQueries.OccupiedRooms);
                result.TotalRooms = Convert.ToInt32(totalRooms ?? 0);
                result.OccupiedRooms = Convert.ToInt32(occupiedRooms ?? 0);

                // ---- КЛІЄНТИ ----
                object totalUsers = DbHelper.ExecuteScalar(ReportQueries.TotalUsers);
                result.TotalCustomers = Convert.ToInt32(totalUsers ?? 0);

                // ---- ПЕРСОНАЛ ----
                object totalStaff = DbHelper.ExecuteScalar(ReportQueries.TotalStaff);
                result.TotalStaff = Convert.ToInt32(totalStaff ?? 0);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogException(ex, "ReportService.GetReport");
            }

            return result;
        }
    }
}
