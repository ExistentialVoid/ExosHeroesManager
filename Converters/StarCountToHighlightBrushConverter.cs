using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters
{
    public class StarCountToHighlightBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                throw new ArgumentException($"{nameof(values)} must contain two values");
            }

            bool condition = false;
            if (values[0] is int starPosition)
            {
                if (values[1] is int equipmentStarCount)
                {
                    condition = equipmentStarCount >= starPosition;
                }
            }
            return condition;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
