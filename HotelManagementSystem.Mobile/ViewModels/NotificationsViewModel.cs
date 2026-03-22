using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelMobileApp.Views;   // ← ДОДАЙ ЦЕ

namespace HotelMobileApp.ViewModels;

public class NotificationsViewModel : BaseViewModel
{
    public ObservableCollection<NotificationItem> Notifications { get; } =
        new ObservableCollection<NotificationItem>();

    public bool HasNotifications => Notifications.Count > 0;
    public bool HasNoNotifications => !HasNotifications;

    public ICommand GoBackCommand { get; }

    public NotificationsViewModel()
    {
        Title = "Notifications";

        // тестові дані
        Notifications.Add(new NotificationItem
        {
            Title = "Payment succeeded",
            Message = "Your payment for hotel booking was successful.",
            TimeText = "Today, 08:54 AM",
            IsSuccess = true,
            IsHighlighted = true
        });

        Notifications.Add(new NotificationItem
        {
            Title = "Payment failed",
            Message = "Your payment for hotel booking failed.",
            TimeText = "Tomorrow, 04:58 PM",
            IsSuccess = false,
            IsHighlighted = false
        });

        // 🔙 Повернення на HomePage через абсолютний маршрут
        GoBackCommand = new Command(async () =>
        {
            if (Shell.Current != null)
            {
                // йдемо на корінь HomePage
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        });
    }
}

public class NotificationItem
{
    public string Title { get; set; } = "";
    public string Message { get; set; } = "";
    public string TimeText { get; set; } = "";
    public bool IsSuccess { get; set; }
    public bool IsHighlighted { get; set; }
}
