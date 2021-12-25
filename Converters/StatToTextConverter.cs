using System.Globalization;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

internal class StatToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not EquipmentStat stat)
        {
            throw new ArgumentException($"{value.GetType()} is not supported.");
        }

        return stat switch
        {
            EquipmentStat.None => "N/A",
            EquipmentStat.FlatAttack => "Atk",
            EquipmentStat.ScaledAttack => "%Atk",
            EquipmentStat.FlatDefense => "Def",
            EquipmentStat.ScaledDefense => "%Def",
            EquipmentStat.FlatHealth => "HP",
            EquipmentStat.ScaledHealth => "%HP",
            EquipmentStat.ScaledCrit => "%Crit",
            EquipmentStat.ScaledCritDmg => "%C.Dmg",
            EquipmentStat.FlatAttackSpd => "A.Spd",
            EquipmentStat.ScaledEffectRes => "%E.Res",
            EquipmentStat.ScaledEffectHit => "%Eff",
            _ => string.Empty,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string stat)
        {
            throw new ArgumentException($"{value.GetType()} is not supported.");
        }
        else if (targetType != typeof(EquipmentStat))
        {
            throw new ArgumentException($"{targetType} is not supported.");
        }

        return stat switch
        {
            "N/A" => EquipmentStat.None,
            "Atk" => EquipmentStat.FlatAttack,
            "%Atk" => EquipmentStat.ScaledAttack,
            "Def" => EquipmentStat.FlatDefense,
            "%Def" => EquipmentStat.ScaledDefense,
            "HP" => EquipmentStat.FlatHealth,
            "%HP" => EquipmentStat.ScaledHealth,
            "%Crit" => EquipmentStat.ScaledCrit,
            "%C.Dmg" => EquipmentStat.ScaledCritDmg,
            "A.Spd" => EquipmentStat.FlatAttackSpd,
            "%E.Res" => EquipmentStat.ScaledEffectRes,
            "%Eff" => EquipmentStat.ScaledEffectHit,
            _ => throw new InvalidEnumArgumentException()
        };
    }
}
