using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExosHeroesManager.Converters;

public class RankToSolidBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return new SolidColorBrush(Colors.Transparent);
        }
        if (value?.GetType() != typeof(Ranking))
        {
            throw new ArgumentException($"{nameof(value)} must be of type {typeof(Ranking)}.");
        }

        return (Ranking)value switch
        {
            Ranking.Common => new SolidColorBrush(Colors.Linen),
            Ranking.Magical => new SolidColorBrush(Colors.LightGreen),
            Ranking.Rare => new SolidColorBrush(Colors.LightSkyBlue),
            Ranking.Legendary => new SolidColorBrush(Colors.MediumSlateBlue),
            Ranking.Fated => new SolidColorBrush(Colors.Gold),
            Ranking.Signature => new LinearGradientBrush(Colors.Yellow, Colors.Gold, 0),
            Ranking.Mythic => new LinearGradientBrush(Colors.Red, Colors.Crimson, 0),
            _ => new SolidColorBrush(Colors.Transparent),
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
