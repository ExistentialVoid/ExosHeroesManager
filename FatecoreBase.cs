using System.Runtime.CompilerServices;

namespace ExosHeroesManager;

public abstract class FatecoreBase : IDataRepresenter, INotifyPropertyChanged
{
    #region Implementations
    public string PrimaryKeyValue => Name;
    TableAccessor IDataRepresenter.Accessor => Accessor;
    DataRow? IDataRepresenter.Me { get => Data; set => Data = value; }
    private FatecoreAccessor Accessor { get; init; } = new();
    internal DataRow? Data;
    bool IDataRepresenter.UpdateData(int clmn, string val) => throw new NotImplementedException();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }
    #endregion

    public string GroupName
    {
        get
        {
            return Convert.ToString(Data?[(int)FatecoreColumn.Group]) ?? string.Empty;
        }
    }
    public string HeroName
    {
        get
        {
            return Convert.ToString(Data?[(int)FatecoreColumn.Hero]) ?? string.Empty;
        }
    }
    public string Name
    {
        get
        {
            return Convert.ToString(Data?[(int)FatecoreColumn.Name]) ?? string.Empty;
        }
    }
    public FatecoreRank Rank
    {
        get
        {
            return Enum.TryParse(Convert.ToString(Data?[(int)FatecoreColumn.Rank]),
                out FatecoreRank val) ? val : FatecoreRank.Unassigned;
        }
    }
    public SkillSet Skills { get; protected set; }


    internal FatecoreBase(DataRow? row)
    {
        Data = row;
    }
    public FatecoreBase(string name)
    {
        Data = Accessor.GetMe(name);
    }
}
