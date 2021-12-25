using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExosHeroesManager.Converters;

public class FatecoreToSolidBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return new SolidColorBrush(Colors.Transparent);
        }
        if (value?.GetType() != typeof(FatecoreRank))
        {
            throw new ArgumentException($"{nameof(value)} must be of type {typeof(FatecoreRank)}.");
        }

        return (FatecoreRank)value switch
        {
            FatecoreRank.Unassigned => new SolidColorBrush(Colors.Transparent),
            FatecoreRank.Orange => new SolidColorBrush(Colors.Orange),
            FatecoreRank.Silver => new SolidColorBrush(Colors.Silver),
            FatecoreRank.Black => new SolidColorBrush(Colors.Black),
            FatecoreRank.Blue => new SolidColorBrush(Colors.DeepSkyBlue),
            FatecoreRank.Gold => new SolidColorBrush(Colors.Gold),
            FatecoreRank.Red => new SolidColorBrush(Colors.Crimson),
            _ => throw new NotImplementedException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
