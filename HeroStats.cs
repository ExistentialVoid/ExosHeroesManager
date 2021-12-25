namespace ExosHeroesManager;

public sealed class HeroStats : Stats, IDataRepresenter
{
    #region Implementations
    TableAccessor IDataRepresenter.Accessor => Accessor;
    DataRow? IDataRepresenter.Me { get => Data; set => Data = value; }

    private HeroesAccessor Accessor = new();
    private DataRow? Data { get; set; }
    public string PrimaryKeyValue => Convert.ToString(Data?[(int)HeroColumn.Name]);
    bool IDataRepresenter.UpdateData(int clmn, string val) => UpdateData(clmn, val);
    private bool UpdateData(int clmn, string val)
    {
        bool hasUpdated = Accessor.Update(PrimaryKeyValue,
            new() { new((HeroColumn)clmn, val), }) == 1;
        if (hasUpdated)
        {
            Data = Accessor.GetMe(PrimaryKeyValue);
        }

        return hasUpdated;
    }
    #endregion

    public override int Attack
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)HeroColumn.Atk]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(value, 0);
            _ = UpdateData((int)HeroColumn.Atk, val.ToString());
            NotifyPropertyChanged();
        }
    }
    public override int AttackSpeed
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)HeroColumn.AtkSpd]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(value, 0);
            _ = UpdateData((int)HeroColumn.AtkSpd, val.ToString());
            NotifyPropertyChanged();
        }
    }
    public override double CombatPower
    {
        get
        {
            return double.TryParse(Convert.ToString(Data?[(int)HeroColumn.CombatPower]),
                out double val) ? val : 0;
        }
        set
        {
            double val = Math.Max(value, 0);
            _ = UpdateData((int)HeroColumn.CombatPower, val.ToString());
            NotifyPropertyChanged();
        }
    }
    public double CriticalDamage
    {
        get
        {
            return double.TryParse(Convert.ToString(Data?[(int)HeroColumn.CritDmg]),
                out double val) ? val : 0;
        }
        set
        {
            double val = Math.Max(value, 0);
            _ = UpdateData((int)HeroColumn.CritDmg, val.ToString());
            NotifyPropertyChanged();
        }
    }
    public double CriticalHitChance
    {
        get
        {
            return double.TryParse(Convert.ToString(Data?[(int)HeroColumn.CritHit]),
                out double val) ? val : 0;
        }
        set
        {
            _ = UpdateData((int)HeroColumn.Atk, value.ToString());
            NotifyPropertyChanged();
        }
    }
    public override int Defense
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)HeroColumn.Def]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(value, 0);
            _ = UpdateData((int)HeroColumn.Atk, val.ToString());
            NotifyPropertyChanged();
        }
    }
    public double DoubleTeamRate
    {
        get
        {
            return double.TryParse(Convert.ToString(Data?[(int)HeroColumn.DblTeamRate]),
                out double val) ? val : 0;
        }
        set
        {
            _ = UpdateData((int)HeroColumn.Atk, value.ToString());
            NotifyPropertyChanged();
        }
    }
    public double EffectHitChance
    {
        get
        {
            return double.TryParse(Convert.ToString(Data?[(int)HeroColumn.EffectHit]),
                out double val) ? val : 0;
        }
        set
        {
            _ = UpdateData((int)HeroColumn.Atk, value.ToString());
            NotifyPropertyChanged();
        }
    }
    public double EffectResistChance
    {
        get
        {
            return double.TryParse(Convert.ToString(Data?[(int)HeroColumn.EffectRes]),
                out double val) ? val : 0;
        }
        set
        {
            _ = UpdateData((int)HeroColumn.Atk, value.ToString());
            NotifyPropertyChanged();
        }
    }
    public override int Health
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)HeroColumn.HP]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(value, 0);
            _ = UpdateData((int)HeroColumn.Atk, val.ToString());
            NotifyPropertyChanged();
        }
    }


    public HeroStats(DataRow heroData)
    {
        Data = heroData;
    }
}
