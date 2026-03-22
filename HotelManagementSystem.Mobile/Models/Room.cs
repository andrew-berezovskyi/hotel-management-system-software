namespace HotelMobileApp.Models;

public class Room
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
}
