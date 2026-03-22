using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using HotelMobileApp.Models;
using HotelMobileApp.Services;
using Microsoft.Maui.ApplicationModel;

namespace HotelMobileApp.ViewModels;

public class HotelDetailViewModel : BaseViewModel
{
    private Hotel? _hotel;
    public Hotel? Hotel
    {
        get => _hotel;
        set
        {
            if (SetProperty(ref _hotel, value))
            {
                OnPropertyChanged(nameof(PriceText));
                OnPropertyChanged(nameof(LikeIcon));
            }
        }
    }

    // Текст ціни типу "$140/Person"
    public string PriceText =>
        Hotel == null
            ? string.Empty
            : string.Format(CultureInfo.InvariantCulture,
                "${0:0}/Person", Hotel.PricePerPerson);

    // Типи кімнат
    public ObservableCollection<string> RoomTypes { get; } = new();

    private string _selectedRoomType = string.Empty;
    public string SelectedRoomType
    {
        get => _selectedRoomType;
        set => SetProperty(ref _selectedRoomType, value);
    }

    private bool _isRoomTypeOpen;
    public bool IsRoomTypeOpen
    {
        get => _isRoomTypeOpen;
        set => SetProperty(ref _isRoomTypeOpen, value);
    }

    // Іконка лайка
    public string LikeIcon =>
        Hotel != null && Hotel.IsFavorite
            ? "icon_favorite_filled.png"
            : "icon_favorite_outline.png";

    // Команди
    public ICommand ToggleFavoriteCommand { get; }
    public ICommand OpenMapCommand { get; }
    public ICommand BookingCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand ToggleRoomTypeCommand { get; }
    public ICommand SelectRoomTypeCommand { get; }

    public HotelDetailViewModel()
    {
        Title = "Detail";

        ToggleFavoriteCommand = new Command(ToggleFavorite);
        OpenMapCommand = new Command(async () => await OpenMapAsync());
        BookingCommand = new Command(async () => await OnBookingAsync());
        BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        ToggleRoomTypeCommand = new Command(() => IsRoomTypeOpen = !IsRoomTypeOpen);
        SelectRoomTypeCommand = new Command<string>(OnSelectRoomType);
    }

    public void LoadHotel(int hotelId)
    {
        var hotel = HotelRepository.GetHotelById(hotelId);
        if (hotel == null)
            return;

        Hotel = hotel;

        RoomTypes.Clear();
        RoomTypes.Add("Standard Room");
        RoomTypes.Add("Deluxe Room");
        RoomTypes.Add("Family Room");
        RoomTypes.Add("Suite");

        SelectedRoomType = string.IsNullOrWhiteSpace(hotel.DefaultRoomType)
            ? RoomTypes[0]
            : hotel.DefaultRoomType;

        IsRoomTypeOpen = false;
    }

    private void OnSelectRoomType(string? roomType)
    {
        if (string.IsNullOrWhiteSpace(roomType)) return;
        SelectedRoomType = roomType;
        IsRoomTypeOpen = false;
    }

    private void ToggleFavorite()
    {
        if (Hotel == null) return;

        Hotel.IsFavorite = !Hotel.IsFavorite;
        OnPropertyChanged(nameof(Hotel));
        OnPropertyChanged(nameof(LikeIcon));
    }

    private async Task OpenMapAsync()
    {
        if (Hotel == null) return;

        var uri = new Uri(string.Format(
            CultureInfo.InvariantCulture,
            "https://www.google.com/maps/search/?api=1&query={0},{1}",
            Hotel.Latitude,
            Hotel.Longitude));

        await Launcher.Default.OpenAsync(uri);
    }

    private async Task OnBookingAsync()
    {
        if (Hotel == null)
        {
            await Shell.Current.DisplayAlert("Booking", "Hotel is not loaded.", "OK");
            return;
        }

        // Поки що – простий алерт. Потім можна навігувати на справжню BookingPage.
        await Shell.Current.DisplayAlert(
            "Booking",
            $"Here we will open booking flow for:\n{Hotel.Name}\nRoom: {SelectedRoomType}",
            "OK");
    }
}
