using HotelMobileApp.Views;
using HotelMobileApp.Views;

namespace HotelMobileApp;
    
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("//Register", typeof(RegisterPage));
        Routing.RegisterRoute("//SignIn", typeof(SignInPage));
        Routing.RegisterRoute("//Home", typeof(HomePage));
        Routing.RegisterRoute("HotelDetail", typeof(HotelDetailPage));
        Routing.RegisterRoute("BookingDetail", typeof(BookingDetailPage));
        Routing.RegisterRoute("Payment", typeof(PaymentPage));
        Routing.RegisterRoute("//PaymentSuccess", typeof(PaymentSuccessPage));
        Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage)); // на майбутнє, якщо знадобиться
        Routing.RegisterRoute(nameof(Views.SearchPage), typeof(Views.SearchPage));
        Routing.RegisterRoute(nameof(HotelDetailPage), typeof(HotelDetailPage));

    }
}
