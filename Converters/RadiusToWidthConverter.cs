using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

internal class RadiusToWidthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return double.Parse(value?.ToString() ?? "0") * 2;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return double.Parse(value?.ToString() ?? "0") * 0.5;
    }
}

internal class WidthToRadiusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return double.Parse(value?.ToString() ?? "0") * 0.5;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return double.Parse(value?.ToString() ?? "0") * 2;
    }
}
