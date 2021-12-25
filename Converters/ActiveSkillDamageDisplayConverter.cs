using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

internal class ActiveSkillDamageDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || value is not Skill skill)
        {
            return string.Empty;
        }

        return $"{skill.DamageMultiplier}x to {skill.Target}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
