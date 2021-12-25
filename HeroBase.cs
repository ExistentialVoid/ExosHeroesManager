using System.Runtime.CompilerServices;

namespace ExosHeroesManager;

/// <summary>
/// Unchangable properties of a Hero
/// </summary>
public abstract class HeroBase : INotifyPropertyChanged, IDataRepresenter
{
    #region Implementations
    TableAccessor IDataRepresenter.Accessor => Accessor;
    DataRow? IDataRepresenter.Me { get => Data; set => Data = value; }
    public string PrimaryKeyValue => Name;

    private HeroesAccessor Accessor { get; init; } = new();
    internal DataRow? Data;
    bool IDataRepresenter.UpdateData(int clmn, string val) => throw new NotImplementedException();

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }
    #endregion

    public Elemental Element
    {
        get
        {
            return Enum.TryParse(Convert.ToString(Data?[(int)HeroColumn.Element]),
                out Elemental val) ? val : Elemental.Light;
        }
    }
    public string Name
    {
        // Primary Key
        get => Convert.ToString(Data?[(int)HeroColumn.Name]) ?? string.Empty;
    }
    public Nationality Nation
    {
        get
        {
            return Enum.TryParse(Convert.ToString(Data?[(int)HeroColumn.Nation]),
                out Nationality val) ? val : Nationality.Lenombe;
        }
    }
    public Role Position
    {
        get
        {
            return Enum.TryParse(Convert.ToString(Data?[(int)HeroColumn.Role]),
                out Role val) ? val : Role.Attacker;
        }
    }
    public Ranking Rank
    {
        get
        {
            return Enum.TryParse(Convert.ToString(Data?[(int)HeroColumn.Rank]),
                out Ranking val) ? val : Ranking.Common;
        }
    }
    public SkillSet Skills { get; protected set; }


    internal HeroBase(DataRow? row)
    {
        Data = row;
    }
    public HeroBase(string name)
    {
        Data = Accessor.GetMe(name);
    }

    public enum Role { Attacker, Support, Defender, Chaos, Combine, Transcend }
}
