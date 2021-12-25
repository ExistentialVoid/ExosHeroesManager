using System.Runtime.CompilerServices;

namespace ExosHeroesManager;

public sealed class EquipmentOption : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new(propertyName));
        if (propertyName != nameof(IsValid))
        {
            isvalid = Validate();
        }
    }

    public double Amount
    {
        get => amount;
        set
        {
            amount = value;
            NotifyPropertyChanged();
        }
    }
    public bool IsValid
    {
        get => isvalid;
        private set
        {
            isvalid = value;
            NotifyPropertyChanged();
        }
    }
    public EquipmentStat Stat
    {
        get => stat;
        set
        {
            stat = value;
            NotifyPropertyChanged();
        }
    }

    private double amount;
    private bool isvalid;
    private EquipmentStat stat;


    public EquipmentOption(double value, EquipmentStat stat)
    {
        Amount = value;
        Stat = stat;
    }


    private bool Validate()
    {
        return stat switch
        {
            EquipmentStat.FlatAttack => amount > 0,
            EquipmentStat.FlatDefense => amount > 0,
            EquipmentStat.FlatHealth => amount > 0,
            EquipmentStat.FlatAttackSpd => amount > 0,
            EquipmentStat.ScaledAttack => amount > 0,
            EquipmentStat.ScaledDefense => amount > 0,
            EquipmentStat.ScaledHealth => amount > 0,
            EquipmentStat.ScaledCrit => amount > 0,
            EquipmentStat.ScaledCritDmg => amount > 0,
            EquipmentStat.ScaledEffectHit => amount > 0,
            EquipmentStat.ScaledEffectRes => amount > 0,
            _ => false
        };
    }
}
