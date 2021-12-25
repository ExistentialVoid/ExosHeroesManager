using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

internal class WidthToEllipseCenterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double r = (double)value * 0.5;
        return new Point(r, r);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Point p = (Point)value;
        return p.X * 2;
    }
}
