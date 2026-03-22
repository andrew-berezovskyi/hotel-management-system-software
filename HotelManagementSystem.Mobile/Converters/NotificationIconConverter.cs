using System.Globalization;

namespace HotelMobileApp.Converters;

public class NotificationIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isSuccess && isSuccess)
            return "icon_notif_success.png";

        return "icon_notif_error.png";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
