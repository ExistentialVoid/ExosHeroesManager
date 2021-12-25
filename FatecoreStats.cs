namespace ExosHeroesManager;

public sealed class FatecoreStats : Stats, IDataRepresenter
{
    #region Implementations
    TableAccessor IDataRepresenter.Accessor => Accessor;
    DataRow? IDataRepresenter.Me { get => Data; set => Data = value; }

    private FatecoreAccessor Accessor = new();
    private DataRow? Data { get; set; }
    public string PrimaryKeyValue => Convert.ToString(Data?[(int)FatecoreColumn.Name]);

    bool IDataRepresenter.UpdateData(int clmn, string val) => UpdateData(clmn, val);
    private bool UpdateData(int clmn, string val)
    {
        bool hasUpdated = Accessor.Update(PrimaryKeyValue,
            new() { new((FatecoreColumn)clmn, val), }) == 1;
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
            return int.TryParse(Convert.ToString(Data?[(int)FatecoreColumn.Atk]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(value, 0);
            _ = UpdateData((int)FatecoreColumn.Atk,val.ToString());
            NotifyPropertyChanged();
        }
    }
    public override int AttackSpeed
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)FatecoreColumn.AtkSpd]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(value, 0);
            _ = UpdateData((int)FatecoreColumn.AtkSpd, val.ToString());
            NotifyPropertyChanged();
        }
    }
    public override double CombatPower
    {
        get
        {
            return double.TryParse(Convert.ToString(Data?[(int)FatecoreColumn.CombatPower]),
                out double val) ? val : 0;
        }
        set
        {
            double val = Math.Max(value, 0);
            _ = UpdateData((int)FatecoreColumn.CombatPower, val.ToString());
            NotifyPropertyChanged();
        }
    }
    public override int Defense
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)FatecoreColumn.Def]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(value, 0);
            _ = UpdateData((int)FatecoreColumn.Def, val.ToString());
            NotifyPropertyChanged();
        }
    }
    public int EnhancmentLevel
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)FatecoreColumn.Enhance]),
                out int val) ? val : 0;
        }
        set
        {
            _ = UpdateData((int)FatecoreColumn.Enhance, value.ToString());
            NotifyPropertyChanged();
        }
    }
    public int FuseCount
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)FatecoreColumn.Fuse]),
                out int val) ? val : 0;
        }
        set
        {
            _ = UpdateData((int)FatecoreColumn.Fuse, value.ToString());
            NotifyPropertyChanged();
        }
    }
    public override int Health
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)FatecoreColumn.HP]),
                out int val) ? val : 0;
        }
        set
        {
            int val = Math.Max(value, 0);
            _ = UpdateData((int)FatecoreColumn.Atk, val.ToString());
            NotifyPropertyChanged();
        }
    }


    public FatecoreStats(DataRow heroData)
    {
        Data = heroData;
    }
}
