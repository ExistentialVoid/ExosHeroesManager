using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(bool))
        {
            throw new ArgumentException($"{nameof(value)} is not supported.");
        }
        else if (targetType != typeof(Visibility))
        {
            throw new ArgumentException($"{nameof(targetType)} is not supported.");
        }

        return (bool)value ? Visibility.Visible : Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(Visibility))
        {
            throw new ArgumentException($"{nameof(value)} is not supported.");
        }
        else if (targetType != typeof(bool))
        {
            throw new ArgumentException($"{nameof(targetType)} is not supported.");
        }

        return (Visibility)value switch
        {
            Visibility.Visible => true,
            Visibility.Hidden => false,
            _ => throw new NotImplementedException()
        };
    }
}
