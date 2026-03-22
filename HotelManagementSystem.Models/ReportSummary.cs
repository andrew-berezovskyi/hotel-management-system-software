namespace HotelManagementSystem.Models
{
    public class ReportSummary
    {
        public decimal TotalIncome { get; set; }
        public int TotalBookings { get; set; }
        public int ActiveBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int CancelledBookings { get; set; }
        public int TotalRooms { get; set; }
        public int OccupiedRooms { get; set; }

        // Кількість вільних номерів, що обчислюється динамічно.
        public int AvailableRooms => TotalRooms - OccupiedRooms;
        public int TotalCustomers { get; set; }
        public int TotalStaff { get; set; }
    }
}
