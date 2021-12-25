using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

internal class EquipmentIsEnabledConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.GetType() == typeof(PendingEquipmentPiece);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
