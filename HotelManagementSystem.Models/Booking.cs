// Модель описує структуру даних предметної області та використовується
// для обміну інформацією між базою даних, бізнес-логікою та UI без реалізації логіки.
namespace HotelManagementSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public string UserName { get; set; }
        public string RoomNumber { get; set; }

        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }

        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }
        public string CreatedAt { get; set; }
    }
}
