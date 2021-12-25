using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExosHeroesManager.Converters;

public class ElementToSolidBrushConverter : IValueConverter
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
            Elemental.Fire => new SolidColorBrush(Colors.OrangeRed),
            Elemental.Frost => new SolidColorBrush(Colors.Aqua),
            Elemental.Nature => new SolidColorBrush(Colors.SpringGreen),
            Elemental.Machine => new SolidColorBrush(Colors.LightSlateGray),
            Elemental.Light => new SolidColorBrush(Colors.Gold),
            Elemental.Darkness => new SolidColorBrush(Colors.Indigo),
            _ => throw new NotImplementedException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
