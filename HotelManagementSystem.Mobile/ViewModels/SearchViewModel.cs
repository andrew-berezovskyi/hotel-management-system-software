using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelMobileApp.Models;
using HotelMobileApp.Services;
using HotelMobileApp.Views;

namespace HotelMobileApp.ViewModels;

public class SearchViewModel : BaseViewModel
{
    public ObservableCollection<Hotel> ExploreHotels { get; } = new();
    public ObservableCollection<Hotel> InterestedHotels { get; } = new();

    private string _query = string.Empty;
    public string Query
    {
        get => _query;
        set => SetProperty(ref _query, value);
    }

    public ICommand GoBackCommand { get; }
    public ICommand SearchCommand { get; }
    public ICommand ExploreMoreCommand { get; }
    public ICommand InterestedMoreCommand { get; }

    // НОВЕ
    public ICommand OpenHotelDetailCommand { get; }

    public SearchViewModel()
    {
        Title = "Search";

        LoadHotels();

        GoBackCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        });

        SearchCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("Search",
                string.IsNullOrWhiteSpace(Query)
                    ? "Enter something to search."
                    : $"Searching for: {Query}",
                "OK");
        });

        ExploreMoreCommand = new Command(async () =>
            await Shell.Current.DisplayAlert("Explore", "Here will be full explore list.", "OK"));

        InterestedMoreCommand = new Command(async () =>
            await Shell.Current.DisplayAlert("You may be interested",
                "Here will be full recommendations list.", "OK"));

        OpenHotelDetailCommand = new Command<Hotel>(async hotel =>
        {
            if (hotel == null) return;

            var route = $"{nameof(HotelDetailPage)}?hotelId={hotel.Id}";
            await Shell.Current.GoToAsync(route);
        });
    }

    private void LoadHotels()
    {
        ExploreHotels.Clear();
        InterestedHotels.Clear();

        var hotels = HotelRepository.GetAllHotels().ToList();

        foreach (var h in hotels)
            ExploreHotels.Add(h);

        // умовно: цікаві – декілька кращих
        InterestedHotels.Add(hotels[0]); // Kyiv
        InterestedHotels.Add(hotels[3]); // Lviv
        InterestedHotels.Add(hotels[5]); // Warsaw
        InterestedHotels.Add(hotels[6]); // Krakow
    }
}
