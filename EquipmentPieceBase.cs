using System.Runtime.CompilerServices;

namespace ExosHeroesManager;

public abstract class EquipmentPieceBase : INotifyPropertyChanged
{
    #region Implementations
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }
    #endregion

    public Hero? EquipedHero { get; }
    public abstract EquipmentOption MainOption { get; set; }
    public abstract EquipmentOption Option1 { get; set; }
    public abstract EquipmentOption Option2 { get; set; }
    public abstract EquipmentOption Option3 { get; set; }
    public abstract EquipmentOption Option4 { get; set; }
    public EquipmentStat[] PossibleMainStats
    {
        get => possibleStats;
        set
        {
            possibleStats = value;
            NotifyPropertyChanged();
        }
    }
    public abstract Ranking Rank { get; set; }
    public abstract SetEffect SetEffect { get; set; }
    public abstract int Stars { get; set; }
    public abstract EquipmentType Type { get; }

    private EquipmentStat[] possibleStats = Array.Empty<EquipmentStat>();


    public EquipmentPieceBase(Hero? hero)
    {
        EquipedHero = hero;
    }


    public enum EquipmentType { Weapon, Helmet, Gloves, Armor, Accessory, Boots }
}
