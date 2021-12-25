namespace ExosHeroesManager;

public sealed class Hero : HeroBase
{
    public Artifact? Artifact
    {
        get
        {
            string name = Convert.ToString(Data?[(int)HeroColumn.Artifact]);
            return name?.Equals(string.Empty) ?? true ? null : new(name);
        }
        set
        {
            string val = value?.Name ?? string.Empty;
            bool hasUpdated = Accessor.Update(PrimaryKeyValue,
                new() { new(HeroColumn.Artifact, val) }) == 1;
            if (hasUpdated)
            {
                Data = Accessor.GetMe(PrimaryKeyValue);
                NotifyPropertyChanged();
            }
        }
    }
    public string[] AvailableFatecores { get; private set; }
    public Equipment Equipment { get; private set; }
    public Fatecore? Fatecore
    {
        get
        {
            string name = Convert.ToString(Data?[(int)HeroColumn.Fatecore]);
            return name?.Equals(string.Empty) ?? true ? null : new(name);
        }
        set
        {
            string val = value?.Name ?? string.Empty;
            bool hasUpdated = Accessor.Update(PrimaryKeyValue,
                new() { new(HeroColumn.Fatecore, val) }) == 1;
            if (hasUpdated)
            {
                Data = Accessor.GetMe(PrimaryKeyValue);
                NotifyPropertyChanged();
            }
        }
    }
    public bool HasExclusive
    {
        get
        {
            return bool.TryParse(Convert.ToString(Data?[(int)HeroColumn.HasExclusive]),
                out bool val) ? val : false;
        }
        set
        {
            bool hasUpdated = Accessor.Update(PrimaryKeyValue,
                new() { new(HeroColumn.HasExclusive, value.ToString()) }) == 1;
            if (hasUpdated)
            {
                Data = Accessor.GetMe(PrimaryKeyValue);
                NotifyPropertyChanged();
            }
        }
    }
    public int Level
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)HeroColumn.Level]),
                out int val) ? val : 0;
        }
        set
        {
            bool hasUpdated = Accessor.Update(PrimaryKeyValue,
                new() { new(HeroColumn.Level, value.ToString()) }) == 1;
            if (hasUpdated)
            {
                Data = Accessor.GetMe(PrimaryKeyValue);
                NotifyPropertyChanged();
            }
        }
    }
    public HeroStats Stats { get; private set; }
    public int TransendLevel
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)HeroColumn.Transcended]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(Math.Min(5, value), 0);
            bool hasUpdated = Accessor.Update(PrimaryKeyValue,
                new() { new(HeroColumn.Transcended, val.ToString()) }) == 1;
            if (hasUpdated)
            {
                Data = Accessor.GetMe(PrimaryKeyValue);
                NotifyPropertyChanged();
            }
        }
    }

    private HeroesAccessor Accessor { get; init; } = new();

    internal Hero(DataRow? row) : base(row)
    {
        Initialize();
    }
    public Hero(string name) : base(name)
    {
        Initialize();
    }


    public void Initialize()
    {
        Equipment = new(this);
        Skills = new SkillSet(this);
        Stats = new(Data);
        FatecoreAccessor fcAccess = new();
        List<FatecoreCriteria> criteria = new()
        {
            new(FatecoreColumn.Hero, SQLRelation.EQ, Name)
        };
        List<string> list = new();
        fcAccess.Select(criteria, new()).ForEach(fc => list.Add(fc.Name));
        AvailableFatecores = list.ToArray();
    }
    public override string ToString() => Name;
}

internal enum HeroColumn
{
    Name,
    Element,
    Role,
    Rank,
    Transcended,
    Nation,
    Passive,
    Active1,
    Active2,
    Weapon,
    Helm,
    Gloves,
    Armor,
    Accessory,
    Boots,
    HasExclusive,
    Artifact,
    Fatecore,
    Atk,
    Def,
    CombatPower,
    HP,
    CritHit,
    CritDmg,
    EffectHit,
    EffectRes,
    AtkSpd,
    DblTeamRate,
    Level
}
public static partial class Extensions
{
    internal static string ColumnName(this HeroColumn val)
    {
        return val switch
        {
            HeroColumn.Name => "NAME",
            HeroColumn.Element => "ELEMENT",
            HeroColumn.Role => "ROLE",
            HeroColumn.Rank => "RANK",
            HeroColumn.Transcended => "TRANSCENDED",
            HeroColumn.Nation => "NATION",
            HeroColumn.Passive => "PASSIVE",
            HeroColumn.Active1 => "ACTIVE1",
            HeroColumn.Active2 => "ACTIVE2",
            HeroColumn.Weapon => "WEAPON",
            HeroColumn.Helm => "HELM",
            HeroColumn.Gloves => "GAUNTLET",
            HeroColumn.Armor => "ARMOR",
            HeroColumn.Accessory => "ACCESSORY",
            HeroColumn.Boots => "GREAVES",
            HeroColumn.HasExclusive => "HASEXCLUSIVE",
            HeroColumn.Artifact => "ARTIFACT",
            HeroColumn.Fatecore => "FATECORE",
            HeroColumn.Atk => "ATTACK",
            HeroColumn.Def => "DEFENSE",
            HeroColumn.CombatPower => "COMBATPOWER",
            HeroColumn.HP => "HEALTH",
            HeroColumn.CritHit => "CRITCHANCE",
            HeroColumn.CritDmg => "CRITDAMAGE",
            HeroColumn.EffectHit => "EFFECTCHANCE",
            HeroColumn.EffectRes => "EFFECTRESIST",
            HeroColumn.AtkSpd => "ATTACKSPEED",
            HeroColumn.DblTeamRate => "DBLTEAMRATE",
            HeroColumn.Level => "LEVEL",
            _ => string.Empty,
        };
    }
}
