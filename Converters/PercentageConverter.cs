using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

public class PercentageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!double.TryParse(value?.ToString() ?? string.Empty, out double val))
        {
            throw new ArgumentException($"{nameof(value)} is not double.");
        }
        else if (targetType != typeof(string))
        {
            throw new NotSupportedException($"{targetType} should be of type {typeof(string)}");
        }

        // parameter will be decimal precision
        int defaultprecision = 3;
        int precision = int.TryParse(parameter?.ToString() ?? string.Empty, out int p) ? p : defaultprecision;

        string percentage = (val * 100).ToString();
        return (percentage.Length > precision ? percentage[0..precision] : percentage).TrimEnd('.') + '%';
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(double))
        {
            throw new ArgumentException($"{targetType} is not numerical.");
        }
        else if (value.GetType() != typeof(string))
        {
            throw new NotSupportedException($"{targetType} should be of type {typeof(string)}");
        }

        string percentage = value?.ToString() ?? string.Empty;
        double val;
        if (!double.TryParse(percentage.TrimEnd('%'), out val))
        {
            return 0;
        }
        else if (percentage.Contains('%'))
        {
            return val / 100;
        }
        else
        {
            return val;
        }
    }
}
