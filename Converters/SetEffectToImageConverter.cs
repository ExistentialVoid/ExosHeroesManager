using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExosHeroesManager.Converters;

public class SetEffectToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(SetEffect))
        {
            throw new ArgumentException($"{nameof(value)} is not supported.");
        }
        else if (targetType != typeof(byte[]) && targetType != typeof(ImageSource))
        {
            throw new ArgumentException($"{nameof(targetType)} is not an image format");
        }

        return (SetEffect)value switch
        {
            SetEffect.Breaker => Icons.BreakerSetIcon,
            SetEffect.Fortification => Icons.FortificationSetIcon,
            SetEffect.Vitality => Icons.VitalitySetIcon,
            SetEffect.Critical => Icons.CriticalSetIcon,
            SetEffect.Sniper => Icons.SniperSetIcon,
            SetEffect.Dash => Icons.DashSetIcon,
            SetEffect.Destruction => Icons.DestructionSetIcon,
            SetEffect.RedBlood => Icons.RedBloodSetIcon,
            SetEffect.Resistance => Icons.ResistanceSetIcon,
            SetEffect.Retaliation => Icons.RetaliationSetIcon,
            SetEffect.Unassigned => null,
            _ => throw new NotImplementedException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(byte[]))
        {
            throw new ArgumentException($"{nameof(value)} is not an image format.");
        }
        else if (targetType != typeof(SetEffect))
        {
            throw new ArgumentException($"{nameof(targetType)} of type {typeof(SetEffect)}");
        }

        SetEffect effect;
        byte[] data = (byte[])value;
        if (data.Equals(Icons.BreakerSetIcon))
        {
            effect = SetEffect.Breaker;
        }
        else if (data.Equals(Icons.FortificationSetIcon))
        {
            effect = SetEffect.Fortification;
        }
        else if (data.Equals(Icons.VitalitySetIcon))
        {
            effect = SetEffect.Vitality;
        }
        else if (data.Equals(Icons.CriticalSetIcon))
        {
            effect = SetEffect.Critical;
        }
        else if (data.Equals(Icons.DashSetIcon))
        {
            effect = SetEffect.Dash;
        }
        else if (data.Equals(Icons.SniperSetIcon))
        {
            effect = SetEffect.Sniper;
        }
        else if (data.Equals(Icons.DestructionSetIcon))
        {
            effect = SetEffect.Destruction;
        }
        else if (data.Equals(Icons.RedBloodSetIcon))
        {
            effect = SetEffect.RedBlood;
        }
        else if (data.Equals(Icons.ResistanceSetIcon))
        {
            effect = SetEffect.Resistance;
        }
        else if (data.Equals(Icons.RetaliationSetIcon))
        {
            effect = SetEffect.Retaliation;
        }
        else
        {
            throw new ArgumentException($"{nameof(value)} is not supported");
        }

        return effect;
    }
}
