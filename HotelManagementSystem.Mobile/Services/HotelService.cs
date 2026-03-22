using HotelMobileApp.Models;

namespace HotelMobileApp.Services
{
    /// <summary>
    /// Сервіс для роботи з готелями, який використовує HotelRepository.
    /// НЕ static, щоб його можна було передавати в BookingViewModel через конструктор.
    /// </summary>
    public class HotelService
    {
        public Task<IReadOnlyList<Hotel>> GetAllHotelsAsync()
        {
            var hotels = HotelRepository.GetAllHotels();
            return Task.FromResult(hotels);
        }

        public Task<Hotel?> GetHotelByIdAsync(int id)
        {
            var hotel = HotelRepository.GetHotelById(id);
            return Task.FromResult(hotel);
        }
    }
}
