using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExosHeroesManager.Converters;

public class ElementToGradientBrushConverter : IValueConverter
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
            Elemental.Fire => new LinearGradientBrush(Colors.DarkOrange, Colors.Firebrick, 45),
            Elemental.Frost => new LinearGradientBrush(Colors.PowderBlue, Colors.DodgerBlue, 45),
            Elemental.Nature => new LinearGradientBrush(Colors.PaleGreen, Colors.Chartreuse, 45),
            Elemental.Machine => new LinearGradientBrush(Colors.LightSteelBlue, Colors.DimGray, 45),
            Elemental.Light => new LinearGradientBrush(Colors.LemonChiffon, Colors.Yellow, 45),
            Elemental.Darkness => new LinearGradientBrush(Colors.BlueViolet, Colors.DarkSlateBlue, 45),
            _ => throw new NotImplementedException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
