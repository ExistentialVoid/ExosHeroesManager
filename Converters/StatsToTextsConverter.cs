using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

internal class StatsToTextsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not EquipmentStat[] stats)
        {
            throw new ArgumentException($"{value.GetType()} is not supported.");
        }

        StatToTextConverter converter = new();
        List<string> list = new();
        foreach (EquipmentStat stat in stats)
        {
            list.Add((string)converter.Convert(stat, typeof(string), null, CultureInfo.InvariantCulture));
        }
        return list;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
