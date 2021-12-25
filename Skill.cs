namespace ExosHeroesManager;

public sealed class Skill : IDataRepresenter
{
    #region Implementations
    public string PrimaryKeyValue => Name;
    TableAccessor IDataRepresenter.Accessor => Accessor;
    DataRow? IDataRepresenter.Me { get => Data; set => Data = value; }

    private SkillsAccessor Accessor { get; init; } = new();
    private DataRow? Data;
    bool IDataRepresenter.UpdateData(int clmn, string val) => throw new NotImplementedException();
    #endregion

    public double DamageMultiplier
    {
        get
        {
            return double.TryParse(Convert.ToString(Data?[(int)SkillColumn.Damage]),
                out double val) ? val : -1;
        }
    }
    public SkillEffect Effect
    {
        get
        {
            _ = Enum.TryParse(Convert.ToString(Data?[(int)SkillColumn.Effect]), out SkillType type);
            string description = Convert.ToString(Data?[(int)SkillColumn.EffectDescr]) ?? string.Empty;
            return new(type, description);
        }
    }
    public bool IsBurst
    {
        get
        {
            return bool.TryParse(Convert.ToString(Data?[(int)SkillColumn.Burst]), 
                out bool val) ? val : false;
        }
    }
    public int ManaCost
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)SkillColumn.Mana]),
                out int val) ? val : -1;
        }
    }
    public string Name => Convert.ToString(Data?[(int)SkillColumn.Name]) ?? string.Empty;
    public TargetType Target
    {
        get
        {
            return Enum.TryParse(Convert.ToString(Data?[(int)SkillColumn.Target]),
                out TargetType val) ? val : TargetType.None;
        }
    }
    public SkillType Type
    {
        get
        {
            return Enum.TryParse(Convert.ToString(Data?[(int)SkillColumn.Type]),
                out SkillType val) ? val : SkillType.Passive;
        }
    }


    internal Skill(DataRow? row) => Data = row;
    public Skill(string name) => Data = Accessor.GetMe(name);


    public override string ToString() => Name;

    public enum SkillType { Passive, Damage, Heal, Buff, Debuff, Special, Burn, Bleed, Cleanse, Revive, Shield }
    public enum TargetType { None, Single, All, BackRow, FrontRow }
    public sealed record SkillEffect(SkillType Type, string Description);
}

internal enum SkillColumn
{
    Name,
    Type,
    Effect,
    EffectDescr,
    Target,
    Damage,
    Mana,
    Burst,
    Special
}
public static partial class Extensions
{
    internal static string ColumnName(this SkillColumn val)
    {
        return val switch
        {
            SkillColumn.Name => "NAME",
            SkillColumn.Type => "TYPE",
            SkillColumn.Effect => "EFFECT",
            SkillColumn.EffectDescr => "EFFECTDESCRIPTION",
            SkillColumn.Target => "TARGET",
            SkillColumn.Damage => "DAMAGEMULT",
            SkillColumn.Mana => "MANA",
            SkillColumn.Burst => "ISBURST",
            SkillColumn.Special => "ISSPECIAL",
            _ => string.Empty,
        };
    }
}
