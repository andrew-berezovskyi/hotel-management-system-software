namespace HotelMobileApp.Models;

public class Booking
{
    public Hotel Hotel { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public decimal TotalPrice { get; set; }
}
