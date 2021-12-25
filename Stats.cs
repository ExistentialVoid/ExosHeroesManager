using System.Runtime.CompilerServices;

namespace ExosHeroesManager;

public abstract class Stats : INotifyPropertyChanged
{
    #region Implementations
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }
    #endregion

    public abstract int Attack { get; set; }
    public abstract int AttackSpeed { get; set; }
    public abstract double CombatPower { get; set; }
    public abstract int Defense { get; set; }
    public abstract int Health { get; set; }
}
