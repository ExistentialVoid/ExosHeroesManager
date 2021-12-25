namespace ExosHeroesManager;

public sealed class Fatecore : FatecoreBase
{
    public FatecoreStats Stats { get; private set; }


    internal Fatecore(DataRow? row) : base(row)
    {
        Initialize();
    }
    public Fatecore(string name) : base(name)
    {
        Initialize();
    }


    private void Initialize()
    {
        Skills = new SkillSet(this);
        Stats = new(Data);
    }
}

internal enum FatecoreColumn
{
    Name,
    Hero,
    Fuse,
    Rank,
    Passives,
    Group,
    Enhance,
    Skill1,
    Skill2,
    CombatPower,
    HP,
    Atk,
    Def,
    AtkSpd
}
public static partial class Extensions
{
    internal static string ColumnName(this FatecoreColumn val)
    {
        return val switch
        {
            FatecoreColumn.Name => "NAME",
            FatecoreColumn.Hero => "HERO",
            FatecoreColumn.Fuse => "FUSED",
            FatecoreColumn.Rank => "RANK",
            FatecoreColumn.Passives => "PASSIVE",
            FatecoreColumn.Group => "GROUP",
            FatecoreColumn.Enhance => "ENHANCEMENT",
            FatecoreColumn.Skill1 => "SKILL1",
            FatecoreColumn.Skill2 => "SKILL2",
            FatecoreColumn.CombatPower => "COMBATPOWER",
            FatecoreColumn.HP => "HEALTH",
            FatecoreColumn.Atk => "ATTACK",
            FatecoreColumn.Def => "DEFENSE",
            FatecoreColumn.AtkSpd => "ATTACKSPEED",
            _ => string.Empty,
        };
    }
}
