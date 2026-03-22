using System.Windows.Input;
using HotelMobileApp.Models;
using HotelMobileApp.Services;

namespace HotelMobileApp.ViewModels;

[QueryProperty(nameof(HotelId), "hotelId")]
public class BookingViewModel : BaseViewModel
{
    private readonly HotelService _hotelService;
    private readonly BookingService _bookingService;

    private Hotel? hotel;
    private DateTime checkIn = DateTime.Today.AddDays(1);
    private DateTime checkOut = DateTime.Today.AddDays(2);
    private int adults = 2;
    private int children = 0;

    public string HotelId { get; set; } = string.Empty;

    public Hotel? Hotel
    {
        get => hotel;
        set => SetProperty(ref hotel, value);
    }

    public DateTime CheckIn
    {
        get => checkIn;
        set => SetProperty(ref checkIn, value);
    }

    public DateTime CheckOut
    {
        get => checkOut;
        set => SetProperty(ref checkOut, value);
    }

    public int Adults
    {
        get => adults;
        set => SetProperty(ref adults, value);
    }

    public int Children
    {
        get => children;
        set => SetProperty(ref children, value);
    }

    public decimal TotalPrice => Hotel == null ? 0 :
        (decimal)(CheckOut - CheckIn).TotalDays * Hotel.PricePerNight;

    public ICommand IncreaseAdultsCommand { get; }
    public ICommand DecreaseAdultsCommand { get; }
    public ICommand IncreaseChildrenCommand { get; }
    public ICommand DecreaseChildrenCommand { get; }
    public ICommand ContinueToPaymentCommand { get; }

    public BookingViewModel(HotelService hotelService, BookingService bookingService)
    {
        _hotelService = hotelService;
        _bookingService = bookingService;

        IncreaseAdultsCommand = new Command(() => Adults++);
        DecreaseAdultsCommand = new Command(() => { if (Adults > 1) Adults--; });
        IncreaseChildrenCommand = new Command(() => Children++);
        DecreaseChildrenCommand = new Command(() => { if (Children > 0) Children--; });

        ContinueToPaymentCommand = new Command(async () =>
        {
            if (Hotel == null) return;

            var booking = new Booking
            {
                Hotel = Hotel,
                Room = new Room { Name = "Standard Room", PricePerNight = Hotel.PricePerNight },
                CheckIn = CheckIn,
                CheckOut = CheckOut,
                Adults = Adults,
                Children = Children,
                TotalPrice = TotalPrice
            };

            await _bookingService.AddBookingAsync(booking);

            var route = $"Payment?amount={booking.TotalPrice}";
            await Shell.Current.GoToAsync(route);
        });
    }

    public async Task LoadAsync()
    {
        if (string.IsNullOrEmpty(HotelId)) return;

        var hotel = await _hotelService.GetHotelByIdAsync(int.Parse(HotelId));
        OnPropertyChanged(nameof(TotalPrice));
    }
}
