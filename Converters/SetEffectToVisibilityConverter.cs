using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

public class SetEffectToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(SetEffect))
        {
            throw new ArgumentException($"{nameof(value)} is not supported.");
        }
        else if (targetType != typeof(Visibility))
        {
            throw new ArgumentException($"{nameof(targetType)} is not supported.");
        }

        return (SetEffect)value != SetEffect.Unassigned ? Visibility.Visible : Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
