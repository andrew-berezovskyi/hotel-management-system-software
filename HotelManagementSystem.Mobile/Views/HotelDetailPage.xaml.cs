using HotelMobileApp.ViewModels;

namespace HotelMobileApp.Views;

public partial class HotelDetailPage : ContentPage, IQueryAttributable
{
    private HotelDetailViewModel ViewModel => (HotelDetailViewModel)BindingContext;

    public HotelDetailPage()
    {
        InitializeComponent();
        BindingContext = new HotelDetailViewModel();
    }

    // Отримуємо параметр hotelId з Shell-навігації
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("hotelId", out var idObj))
        {
            if (idObj is int id)
            {
                ViewModel.LoadHotel(id);
            }
            else if (idObj is string idStr && int.TryParse(idStr, out var parsed))
            {
                ViewModel.LoadHotel(parsed);
            }
        }
    }
}
