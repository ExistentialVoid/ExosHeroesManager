using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExosHeroesManager.Converters;

public class RoleToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(Hero.Role))
        {
            throw new ArgumentException($"{nameof(value)} is not supported.");
        }
        else if (targetType != typeof(byte[]) && targetType != typeof(ImageSource))
        {
            throw new ArgumentException($"{nameof(targetType)} is not an image format");
        }

        return (Hero.Role)value switch
        {
            Hero.Role.Attacker => Icons.AttackerIcon,
            Hero.Role.Chaos => Icons.ChaosIcon,
            Hero.Role.Defender => Icons.DefenseIcon,
            Hero.Role.Support => Icons.SupportIcon,
            _ => throw new NotImplementedException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(byte[]))
        {
            throw new ArgumentException($"{nameof(value)} is not an image format.");
        }
        else if (targetType != typeof(Hero.Role))
        {
            throw new ArgumentException($"{nameof(targetType)} of type {typeof(Hero.Role)}");
        }

        Hero.Role role;
        byte[] data = (byte[])value;
        if (data.Equals(Icons.AttackerIcon))
        {
            role = Hero.Role.Attacker;
        }
        else if (data.Equals(Icons.ChaosIcon))
        {
            role = Hero.Role.Chaos;
        }
        else if (data.Equals(Icons.DefenseIcon))
        {
            role = Hero.Role.Defender;
        }
        else if (data.Equals(Icons.SupportIcon))
        {
            role = Hero.Role.Support;
        }
        else
        {
            throw new ArgumentException($"{nameof(value)} is not supported");
        }

        return role;
    }
}
