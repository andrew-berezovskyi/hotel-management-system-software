using HotelMobileApp.Views;
using HotelMobileApp.Services;
using HotelMobileApp.ViewModels;
using HotelMobileApp.Views;

namespace HotelMobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        builder.Services.AddSingleton<HotelService>();
        builder.Services.AddSingleton<BookingService>();

        // ViewModels
        builder.Services.AddTransient<OnboardingViewModel>();
        builder.Services.AddTransient<AuthViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<HotelDetailViewModel>();
        builder.Services.AddTransient<BookingViewModel>();
        builder.Services.AddTransient<PaymentViewModel>();

        // Views
        builder.Services.AddTransient<OnboardingPage>();
        builder.Services.AddTransient<SignInPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HotelDetailPage>();
        builder.Services.AddTransient<BookingDetailPage>();
        builder.Services.AddTransient<PaymentPage>();
        builder.Services.AddTransient<PaymentSuccessPage>();

        return builder.Build();
    }
}
