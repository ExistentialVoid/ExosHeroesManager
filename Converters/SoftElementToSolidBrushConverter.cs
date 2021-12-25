using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExosHeroesManager.Converters;

public class SoftElementToSolidBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return new SolidColorBrush(Colors.Transparent);
        }
        if (value?.GetType() != typeof(Elemental))
        {
            throw new ArgumentException($"{nameof(value)} must be of type {typeof(Elemental)}.");
        }

        return (Elemental)value switch
        {
            Elemental.Fire => new SolidColorBrush(Colors.PeachPuff),
            Elemental.Frost => new SolidColorBrush(Colors.LightCyan),
            Elemental.Nature => new SolidColorBrush(Colors.Honeydew),
            Elemental.Machine => new SolidColorBrush(Colors.WhiteSmoke),
            Elemental.Light => new SolidColorBrush(Colors.LightYellow),
            Elemental.Darkness => new SolidColorBrush(Colors.Lavender),
            _ => throw new NotImplementedException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
