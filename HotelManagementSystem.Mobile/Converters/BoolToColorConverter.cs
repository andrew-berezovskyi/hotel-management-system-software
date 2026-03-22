using System.Globalization;

namespace HotelMobileApp.Converters;

public class BoolToColorConverter : IValueConverter
{
    public Color HighlightColor { get; set; } = Color.FromArgb("#E6F0FF");
    public Color NormalColor { get; set; } = Colors.White;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b && b)
            return HighlightColor;

        return NormalColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
