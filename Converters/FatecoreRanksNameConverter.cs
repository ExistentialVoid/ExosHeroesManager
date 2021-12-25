using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

public class FatecoreRanksNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(FatecoreRank))
        {
            throw new ArgumentException($"{value.GetType()} is not supported.");
        }

        return ((FatecoreRank)value).ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(FatecoreRank))
        {
            throw new ArgumentException($"{targetType} is not supported.");
        }

        return Enum.Parse<FatecoreRank>(value?.ToString() ?? string.Empty);
    }
}
