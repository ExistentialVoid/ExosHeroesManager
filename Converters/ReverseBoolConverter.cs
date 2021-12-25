using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters
{
    public class ReverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value?.GetType() ?? typeof(int)) != typeof(bool) || value.GetType() != targetType)
            {
                throw new ArgumentException("Cannot apply conversion");
            }

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value?.GetType() ?? typeof(int)) != typeof(bool) || value.GetType() != targetType)
            {
                throw new ArgumentException("Cannot apply conversion");
            }

            return !(bool)value;
        }
    }
}
