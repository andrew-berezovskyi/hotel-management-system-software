using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelMobileApp.Models;
using HotelMobileApp.Services;
using HotelMobileApp.Views;

namespace HotelMobileApp.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ObservableCollection<Hotel> AroundYouHotels { get; } = new();
    public ObservableCollection<Hotel> VirtualTourHotels { get; } = new();
    public ObservableCollection<PlaceToVisit> PlacesYouCanVisit { get; } = new();

    private string _currentLocation = "Kyiv, Ukraine";
    public string CurrentLocation
    {
        get => _currentLocation;
        set => SetProperty(ref _currentLocation, value);
    }

    public ICommand SeeMoreHotelsCommand { get; }
    public ICommand SeeMoreToursCommand { get; }
    public ICommand SeeMorePlacesCommand { get; }
    public ICommand OpenSearchCommand { get; }

    public ICommand GoToProfileCommand { get; }
    public ICommand ChangeLocationCommand { get; }
    public ICommand OpenNotificationsCommand { get; }

    // Відкриття детальної сторінки
    public ICommand OpenHotelDetailCommand { get; }

    public HomeViewModel()
    {
        Title = "Home";

        LoadHotels();
        LoadTours();
        LoadPlaces();

        SeeMoreHotelsCommand = new Command(async () =>
            await Shell.Current.DisplayAlert("Hotels", "Here will be a full hotel list screen.", "OK"));

        SeeMoreToursCommand = new Command(async () =>
            await Shell.Current.DisplayAlert("Virtual tours", "Here will be a full virtual tour list screen.", "OK"));

        SeeMorePlacesCommand = new Command(async () =>
            await Shell.Current.DisplayAlert("Places you can visit", "Here will be a full attractions list.", "OK"));

        OpenSearchCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        });

        GoToProfileCommand = new Command(async () =>
            await Shell.Current.DisplayAlert("Profile", "Далі зробимо екран профілю.", "OK"));

        ChangeLocationCommand = new Command(async () =>
            await Shell.Current.DisplayAlert("Location", "Тут буде вибір міста, де є готелі.", "OK"));

        OpenNotificationsCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(NotificationsPage));
        });

        OpenHotelDetailCommand = new Command<Hotel>(async hotel =>
        {
            if (hotel == null)
                return;

            var route = $"{nameof(HotelDetailPage)}?hotelId={hotel.Id}";
            await Shell.Current.GoToAsync(route);
        });
    }

    private void LoadHotels()
    {
        AroundYouHotels.Clear();

        foreach (var h in HotelRepository.GetAllHotels())
            AroundYouHotels.Add(h);
    }

    private void LoadTours()
    {
        VirtualTourHotels.Clear();

        // Для простоти – всі ті ж готелі у 360° блоці
        foreach (var h in HotelRepository.GetAllHotels())
            VirtualTourHotels.Add(h);
    }

    private void LoadPlaces()
    {
        PlacesYouCanVisit.Clear();

        PlacesYouCanVisit.Add(new PlaceToVisit
        {
            Name = "Maidan Nezalezhnosti",
            City = "Kyiv, Ukraine",
            NearbyHotelName = "The Hotel Kyiv",
            Image = "place_kyiv_maidan.png"
        });

        PlacesYouCanVisit.Add(new PlaceToVisit
        {
            Name = "Rynok Square",
            City = "Lviv, Ukraine",
            NearbyHotelName = "The Hotel Lviv",
            Image = "place_lviv_rynok.png"
        });

        PlacesYouCanVisit.Add(new PlaceToVisit
        {
            Name = "Dnipro Embankment",
            City = "Dnipro, Ukraine",
            NearbyHotelName = "The Hotel Dnipro",
            Image = "place_dnipro_embankment.png"
        });

        PlacesYouCanVisit.Add(new PlaceToVisit
        {
            Name = "Gorky Central Park",
            City = "Kharkiv, Ukraine",
            NearbyHotelName = "The Hotel Kharkiv",
            Image = "place_kharkiv_gorky.png"
        });

        PlacesYouCanVisit.Add(new PlaceToVisit
        {
            Name = "Old Town Market Square",
            City = "Warsaw, Poland",
            NearbyHotelName = "The Hotel Warsaw",
            Image = "place_warsaw_oldtown.png"
        });

        PlacesYouCanVisit.Add(new PlaceToVisit
        {
            Name = "Wawel Castle",
            City = "Krakow, Poland",
            NearbyHotelName = "The Hotel Krakow",
            Image = "place_krakow_wawel.png"
        });

        PlacesYouCanVisit.Add(new PlaceToVisit
        {
            Name = "Nikiszowiec district",
            City = "Katowice, Poland",
            NearbyHotelName = "The Hotel Katowice",
            Image = "place_katowice_nikiszowiec.png"
        });

        PlacesYouCanVisit.Add(new PlaceToVisit
        {
            Name = "City center & river",
            City = "Khmelnytskyi, Ukraine",
            NearbyHotelName = "The Hotel Khmelnytskyi",
            Image = "place_khmelnytskyi_center.png"
        });
    }
}
