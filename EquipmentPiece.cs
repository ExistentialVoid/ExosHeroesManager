namespace ExosHeroesManager;

public sealed class EquipmentPiece : EquipmentPieceBase, IDataRepresenter
{
    #region Implementations
    public string PrimaryKeyValue => ID;
    TableAccessor IDataRepresenter.Accessor => Accessor;
    DataRow? IDataRepresenter.Me { get => Data; set => Data = value; }
    bool IDataRepresenter.UpdateData(int clmn, string val) => UpdateData(clmn, val);
    private bool UpdateData(int clmn, string val)
    {
        bool hasUpdated = Accessor.Update(PrimaryKeyValue,
            new() { new((EquipmentColumn)clmn, val), }) == 1;
        if (hasUpdated)
        {
            Data = Accessor.GetMe(PrimaryKeyValue);
        }

        return hasUpdated;
    }

    private EquipmentAccessor Accessor { get; init; } = new();
    private DataRow? Data;
    #endregion

    public string ID
    {
        get
        {
            return Convert.ToString(Data?[(int)EquipmentColumn.ID]) ?? string.Empty;
        }
    }
    public override EquipmentOption MainOption
    {
        get
        {
            _ = Enum.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Stat]), out EquipmentStat stat);
            _ = double.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.StatAmnt]), out double amount);
            return new(amount, stat);
        }
        set { }
    }
    public override Ranking Rank
    {
        get
        {
            return (Enum.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Rank]), 
                out Ranking val) ? val : Ranking.Unassigned);
        }
        set { }
    }
    public override SetEffect SetEffect
    {
        get
        {
            return (Enum.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.SetEffect]),
                out SetEffect val) ? val : SetEffect.Unassigned);
        }
        set { }
    }
    public override int Stars
    {
        get
        {
            return int.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Star]), 
                out int val) ? val : 0;
        }
        set { }
    }
    public override EquipmentOption? Option1
    {
        get
        {
            _ = Enum.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Opt1]), out EquipmentStat stat);
            _ = double.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Opt1Amnt]), out double amount);
            return new(amount, stat);
        }
        set
        {
            _ = UpdateData((int)EquipmentColumn.Opt1, value?.Stat.ToString() ?? Convert.DBNull.ToString());
            _ = UpdateData((int)EquipmentColumn.Opt1Amnt, value?.Amount.ToString() ?? Convert.DBNull.ToString());
            NotifyPropertyChanged();
        }
    }
    public override EquipmentOption? Option2
    {
        get
        {
            _ = Enum.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Opt2]), out EquipmentStat stat);
            _ = double.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Opt2Amnt]), out double amount);
            return new(amount, stat);
        }
        set
        {
            _ = UpdateData((int)EquipmentColumn.Opt2, value?.Stat.ToString() ?? Convert.DBNull.ToString());
            _ = UpdateData((int)EquipmentColumn.Opt2Amnt, value?.Amount.ToString() ?? Convert.DBNull.ToString());
            NotifyPropertyChanged();
        }
    }
    public override EquipmentOption? Option3
    {
        get
        {
            _ = Enum.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Opt3]), out EquipmentStat stat);
            _ = double.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Opt3Amnt]), out double amount);
            return new(amount, stat);
        }
        set
        {
            _ = UpdateData((int)EquipmentColumn.Opt3, value?.Stat.ToString() ?? Convert.DBNull.ToString());
            _ = UpdateData((int)EquipmentColumn.Opt3Amnt, value?.Amount.ToString() ?? Convert.DBNull.ToString());
            NotifyPropertyChanged();
        }
    }
    public override EquipmentOption? Option4
    {
        get
        {
            _ = Enum.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Opt4]), out EquipmentStat stat);
            _ = double.TryParse(Convert.ToString(Data?[(int)EquipmentColumn.Opt4Amnt]), out double amount);
            return new(amount, stat);
        }
        set
        {
            _ = UpdateData((int)EquipmentColumn.Opt4, value?.Stat.ToString() ?? Convert.DBNull.ToString());
            _ = UpdateData((int)EquipmentColumn.Opt4Amnt, value?.Amount.ToString() ?? Convert.DBNull.ToString());
            NotifyPropertyChanged();
        }
    }
    public override EquipmentType Type
    {
        get
        {
            return Enum.GetValues<EquipmentType>().ToList()
                .Find(val => val.IDPart().Equals(ID[1]));
        }
    }


    internal EquipmentPiece(DataRow? row, Hero? hero) : base(hero)
    {
        Data = row;
        Initialize();
    }
    public EquipmentPiece(string id, Hero? hero) : base(hero)
    {
        Data = Accessor.GetMe(id);
        Initialize();
    }


    public bool EquipTo(string HeroName)
    {

        return false;
    }
    private void Initialize()
    {
        PossibleMainStats = new EquipmentStat[] { MainOption.Stat };
    }
}

public enum SetEffect
    {
        Unassigned,
        Breaker,
        Fortification,
        Vitality,
        Critical,
        Sniper,
        Dash,
        Destruction,
        RedBlood,
        Resistance,
        Retaliation
    }

internal enum EquipmentColumn
{
    ID,
    Rank,
    Star,
    Stat,
    Opt1,
    Opt2,
    Opt3,
    Opt4,
    StatAmnt,
    Opt1Amnt,
    Opt2Amnt,
    Opt3Amnt,
    Opt4Amnt,
    SetEffect
}
public static partial class Extensions
{
    internal static string ColumnName(this EquipmentColumn val)
    {
        return val switch
        {
            EquipmentColumn.ID => "ID",
            EquipmentColumn.Rank => "RANK",
            EquipmentColumn.Star => "STAR",
            EquipmentColumn.Stat => "STAT",
            EquipmentColumn.Opt1 => "OPTION1",
            EquipmentColumn.Opt2 => "OPTION2",
            EquipmentColumn.Opt3 => "OPTION3",
            EquipmentColumn.Opt4 => "OPTION4",
            EquipmentColumn.StatAmnt => "STATAMOUNT",
            EquipmentColumn.Opt1Amnt => "OPTION1AMOUNT",
            EquipmentColumn.Opt2Amnt => "OPTION2AMOUNT",
            EquipmentColumn.Opt3Amnt => "OPTION3AMOUNT",
            EquipmentColumn.Opt4Amnt => "OPTION4AMOUNT",
            EquipmentColumn.SetEffect => "SETEFFECT",
            _ => string.Empty,
        };
    }
}
