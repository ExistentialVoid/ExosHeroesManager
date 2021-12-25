using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

public class NationToNationNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(Nationality))
        {
            throw new ArgumentException($"{value.GetType()} is not supported.");
        }
        else if (targetType != typeof(string))
        {
            throw new ArgumentException($"{targetType} is not supported");
        }

        return ((Nationality)value).FullName();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(string))
        {
            throw new ArgumentException($"{value.GetType()} is not supported.");
        }
        else if (targetType != typeof(Nationality))
        {
            throw new ArgumentException($"{targetType} is not supported");
        }

        return Enum.GetValues<Nationality>().ToList().Find(val => val.FullName().Equals(value.ToString()));
    }
}
