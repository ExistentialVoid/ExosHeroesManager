using System.Runtime.CompilerServices;
using System.Text;

namespace ExosHeroesManager;

public sealed class PendingEquipmentPiece : EquipmentPieceBase
{
    public bool IsValid
    {
        get => isvalid;
        private set
        {
            isvalid = value;
            NotifyPropertyChanged();
        }
    }
    public override EquipmentOption MainOption
    {
        get => mainOption;
        set
        {
            mainOption = value;
            NotifyPropertyChanged();
        }
    }
    public override EquipmentOption Option1
    {
        get => option1;
        set
        {
            option1 = value;
            NotifyPropertyChanged();
        }
    }
    public override EquipmentOption Option2
    {
        get => option2;
        set
        {
            option2 = value;
            NotifyPropertyChanged();
        }
    }
    public override EquipmentOption Option3
    {
        get => option3;
        set
        {
            option3 = value;
            NotifyPropertyChanged();
        }
    }
    public override EquipmentOption Option4
    {
        get => option4;
        set
        {
            option4 = value;
            NotifyPropertyChanged();
        }
    }
    public override Ranking Rank
    {
        get => rank;
        set
        {
            rank = value;
            NotifyPropertyChanged();
        }
    }
    public override SetEffect SetEffect
    {
        get => setEffect;
        set
        {
            setEffect = value;
            NotifyPropertyChanged();
        }
    }
    public override int Stars
    {
        get => stars;
        set
        {
            stars = value;
            NotifyPropertyChanged();
        }
    }
    public override EquipmentType Type => type;

    private EquipmentAccessor Accessor = new();
    private bool isvalid = false;
    private EquipmentOption mainOption = new(0, EquipmentStat.None);
    private EquipmentOption option1 = new(0, EquipmentStat.None);
    private EquipmentOption option2 = new(0, EquipmentStat.None);
    private EquipmentOption option3 = new(0, EquipmentStat.None);
    private EquipmentOption option4 = new(0, EquipmentStat.None);
    private Ranking rank = Ranking.Unassigned;
    private SetEffect setEffect = SetEffect.Unassigned;
    private int stars = 0;
    private readonly EquipmentType type;


    public PendingEquipmentPiece(EquipmentType piece, Hero? hero) : base(hero)
    {
        type = piece;
        Initialize();
    }


    private string GenerateID()
    {
        StringBuilder idBuilder = new();
        idBuilder.Append(type.IDPart());
        idBuilder.Append($"{(int)rank + 1}");
        idBuilder.Append(mainOption?.Stat.IDPart());
        idBuilder.Append(option1?.Stat.IDPart());
        idBuilder.Append(option2?.Stat.IDPart());
        idBuilder.Append(option3?.Stat.IDPart());
        idBuilder.Append(option4?.Stat.IDPart());

        List<EquipmentPiece> matchedEquipment = Accessor.Select(criteria: new() { new(EquipmentColumn.ID, SQLRelation.LK, idBuilder.ToString()) }, new());
        Predicate<EquipmentPiece> exactMatch = e => e.MainOption.Amount == mainOption?.Amount && e.Option1?.Amount == option1?.Amount
            && e.Option2?.Amount == option2?.Amount && e.Option3?.Amount == option3?.Amount && e.Option4?.Amount == option4?.Amount;

        if (!matchedEquipment.Exists(exactMatch))
        {
            idBuilder.Append(matchedEquipment.Count.ToString());
            return idBuilder.ToString();
        }
        
        return string.Empty;
    }
    private void Initialize()
    {
        switch (type)
        {
            case EquipmentType.Accessory:
                PossibleMainStats = new EquipmentStat[]
                {
                    EquipmentStat.FlatAttack,
                    EquipmentStat.FlatDefense,
                    EquipmentStat.FlatHealth,
                    EquipmentStat.ScaledAttack,
                    EquipmentStat.ScaledDefense,
                    EquipmentStat.ScaledHealth,
                    EquipmentStat.ScaledEffectHit,
                    EquipmentStat.ScaledEffectRes
                };
                break;
            case EquipmentType.Armor:
                mainOption.Stat = EquipmentStat.FlatDefense;
                PossibleMainStats = new EquipmentStat[] { EquipmentStat.FlatDefense };
                break;
            case EquipmentType.Boots:
                PossibleMainStats = new EquipmentStat[]
                {
                    EquipmentStat.FlatAttack,
                    EquipmentStat.FlatDefense,
                    EquipmentStat.FlatHealth,
                    EquipmentStat.ScaledAttack,
                    EquipmentStat.ScaledDefense,
                    EquipmentStat.ScaledHealth,
                    EquipmentStat.FlatAttackSpd
                };
                break;
            case EquipmentType.Gloves:
                PossibleMainStats = new EquipmentStat[]
                {
                    EquipmentStat.FlatAttack,
                    EquipmentStat.FlatDefense,
                    EquipmentStat.FlatHealth,
                    EquipmentStat.ScaledAttack,
                    EquipmentStat.ScaledDefense,
                    EquipmentStat.ScaledHealth,
                    EquipmentStat.ScaledCrit,
                    EquipmentStat.ScaledCritDmg
                };
                break;
            case EquipmentType.Helmet:
                mainOption.Stat = EquipmentStat.FlatHealth;
                PossibleMainStats = new EquipmentStat[] { EquipmentStat.FlatHealth };
                break;
            case EquipmentType.Weapon:
                mainOption.Stat = EquipmentStat.FlatAttack;
                PossibleMainStats = new EquipmentStat[] { EquipmentStat.FlatAttack };
                break;
            default: throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
    protected override void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        base.NotifyPropertyChanged(propertyName);
        if (propertyName != nameof(IsValid))
        {
            IsValid = Validate();
        }
    }
    /// <summary>
    /// Insert record into equipment table. On success replace the current hero's equipment to record
    /// </summary>
    /// <returns></returns>
    public bool Upload()
    {
        if (!isvalid)
        {
            return false;
        }

        string id = GenerateID();
        List<EquipmentSetter> values = new()
        {
            new(EquipmentColumn.ID, id),
            new(EquipmentColumn.Rank, rank.ToString()),
            new(EquipmentColumn.Star, stars.ToString()),
            new(EquipmentColumn.Stat, mainOption.Stat.ToString()),
            new(EquipmentColumn.StatAmnt, mainOption.Amount.ToString()),
            new(EquipmentColumn.Opt1, option1?.Stat.ToString() ?? string.Empty),
            new(EquipmentColumn.Opt1Amnt, option1?.Amount.ToString() ?? string.Empty),
            new(EquipmentColumn.Opt2, option2?.Stat.ToString() ?? string.Empty),
            new(EquipmentColumn.Opt2Amnt, option2?.Amount.ToString() ?? string.Empty),
            new(EquipmentColumn.Opt3, option3?.Stat.ToString() ?? string.Empty),
            new(EquipmentColumn.Opt3Amnt, option3?.Amount.ToString() ?? string.Empty),
            new(EquipmentColumn.Opt4, option4?.Stat.ToString() ?? string.Empty),
            new(EquipmentColumn.Opt4Amnt, option4?.Amount.ToString() ?? string.Empty),
            new(EquipmentColumn.SetEffect, SetEffect.ToString()),
        };

        bool hasUploaded = Accessor.Insert(values) == 1;
        if (hasUploaded)
        {
            EquipedHero.Equipment.Equip(id);
        }

        return hasUploaded;
    }
    private bool Validate()
    {
        string id = GenerateID();

        if (id.Length != 8 || mainOption.Stat == EquipmentStat.None || setEffect == SetEffect.Unassigned)
        {
            return false;
        }
        else if (mainOption.Stat == EquipmentStat.None)
        {
            return false;
        }
        else if (option1?.Stat == EquipmentStat.None && rank > Ranking.Common)
        {
            return false;
        }
        else if (option2?.Stat == EquipmentStat.None && rank > Ranking.Magical)
        {
            return false;
        }
        else if (option3?.Stat == EquipmentStat.None && rank > Ranking.Rare)
        {
            return false;
        }
        else if (option4?.Stat == EquipmentStat.None && rank > Ranking.Legendary)
        {
            return false;
        }
        else if (stars < 0 && stars > 5)
        {
            return false;
        }
        else if (!(option1?.IsValid ?? true) || !(option2?.IsValid ?? true) || !(option3?.IsValid ?? true) || !(option4?.IsValid ?? true))
        {
            return false;
        }

        return true;
    }
}
