using HotelMobileApp.Models;

namespace HotelMobileApp.Services;

public class BookingService
{
    private readonly List<Booking> _bookings = new();

    public Task AddBookingAsync(Booking booking)
    {
        _bookings.Add(booking);
        return Task.CompletedTask;
    }

    public Task<List<Booking>> GetBookingsAsync()
        => Task.FromResult(_bookings);
}
