
namespace HotelManagementSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }                  // Primary Key
        public int BookingId { get; set; }           // Reference to Booking
        public decimal Amount { get; set; }          // Amount paid
        public string PaymentDate { get; set; }      // Date of payment (stored as string yyyy-MM-dd)
        public string PaymentMethod { get; set; }    // Cash, Card, Bank Transfer, Online
        public string Status { get; set; }           // Pending, Completed, Cancelled
        public string Notes { get; set; }            // Optional description or remarks

        // Optional navigation property (not mandatory for SQLite, but convenient in C#)
        //public Booking Booking { get; set; }
    }
}

