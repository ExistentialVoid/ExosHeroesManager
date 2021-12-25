namespace ExosHeroesManager;

public enum Elemental { Fire, Frost, Nature, Machine, Light, Darkness }
public enum EquipmentStat
{
    None,
    FlatAttack,
    ScaledAttack,
    FlatDefense,
    ScaledDefense,
    FlatHealth,
    ScaledHealth,
    ScaledCrit,
    ScaledCritDmg,
    ScaledEffectHit,
    ScaledEffectRes,
    FlatAttackSpd
}
public enum FatecoreRank { Unassigned, Orange, Silver, Black, Blue, Gold, Red }
public enum Nationality
{
    Lenombe,
    GreenLand,
    Estoris,
    WastedRed,
    VonFrosty,
    Brunn,
    Vagabond,
    SaintWest,
    Phedas,
    ShadowBane,
    Wonderland,
    Baikal
}
public enum Ranking { Unassigned, Common, Magical, Rare, Legendary, Fated, Signature, Mythic }

public static partial class Extensions
{
    /// <summary>
    /// String representation
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string FullName(this Nationality val)
    {
        return val switch
        {
            Nationality.Baikal => "Baikal Dynasty",
            Nationality.Brunn => "Brunn",
            Nationality.Estoris => "Estoris Republic",
            Nationality.GreenLand => "Green Land",
            Nationality.Lenombe => "Lenombe",
            Nationality.Phedas => "Phedas",
            Nationality.SaintWest => "Saint West",
            Nationality.ShadowBane => "Shadow Bane",
            Nationality.Vagabond => "Vagabond",
            Nationality.VonFrosty => "North von Frosty",
            Nationality.WastedRed => "Wasted Red",
            Nationality.Wonderland => "Wonderland",
            _ => string.Empty
        };
    }

    internal static string GetText(this StndVal val)
    {
        return val switch
        {
            StndVal.All => "All",
            StndVal.Any => "Any",
            StndVal.Empty => "Empty",
            StndVal.NA => "N/A",
            StndVal.NNUL => "NNULL",
            StndVal.None => "None",
            StndVal.Test => "TESTING",
            StndVal.Select => "Select",
            _ => string.Empty
        };
    }

    public static string IDPart(this EquipmentPieceBase.EquipmentType val)
    {
        return val switch
        {
            EquipmentPieceBase.EquipmentType.Accessory => "X",
            EquipmentPieceBase.EquipmentType.Armor => "A",
            EquipmentPieceBase.EquipmentType.Gloves => "G",
            EquipmentPieceBase.EquipmentType.Boots => "B",
            EquipmentPieceBase.EquipmentType.Helmet => "H",
            EquipmentPieceBase.EquipmentType.Weapon => "W",
            _ => string.Empty
        };
    }

    public static string IDPart(this EquipmentStat val)
    {
        return val switch
        {
            EquipmentStat.ScaledAttack => "A",
            EquipmentStat.FlatAttack => "a",
            EquipmentStat.ScaledDefense => "D",
            EquipmentStat.FlatDefense => "d",
            EquipmentStat.ScaledHealth => "H",
            EquipmentStat.FlatHealth => "h",
            EquipmentStat.ScaledCrit => "C",
            EquipmentStat.ScaledCritDmg => "D",
            EquipmentStat.ScaledEffectHit => "E",
            EquipmentStat.ScaledEffectRes => "R",
            EquipmentStat.FlatAttackSpd => "s",
            EquipmentStat.None => "N",
            _ => string.Empty
        };
    }

    /// <summary>
    /// String representation
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    internal static string Operator(this SQLRelation val)
    {
        return val switch
        {
            SQLRelation.EQ => "=",
            SQLRelation.NE => "<>",
            SQLRelation.GT => ">",
            SQLRelation.LT => "<",
            SQLRelation.GE => ">=",
            SQLRelation.LE => "<=",
            SQLRelation.LK => " LIKE ",
            SQLRelation.NL => " NOT LIKE ",
            SQLRelation.NU => " IS NULL ",
            SQLRelation.NN => " IS NOT NULL ",
            _ => string.Empty
        };
    }

    /// <summary>
    /// Column name of the primary key
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    internal static string PrimaryKey(this ExosHeroesTables val)
    {
        return val switch
        {
            ExosHeroesTables.Equipment => "ID",
            ExosHeroesTables.Fatecores => "NAME",
            ExosHeroesTables.Heroes => "NAME",
            ExosHeroesTables.Skills => "NAME",
            _ => string.Empty
        };
    }
    internal static string TableName(this ExosHeroesTables val) => Enum.GetName(val) ?? string.Empty;
    
    public static List<T> ToList<T>(this T[] array)
    {
        List<T> list = new();
        foreach (T item in array)
        {
            list.Add(item);
        }
        return list;
    }

    /// <summary>
    /// Return a new string with the specific trimmed string removed at the end, else returns original string
    /// </summary>
    /// <param name="text"></param>
    /// <param name="trimText"></param>
    /// <returns></returns>
    public static string TrimEnd(this string text, string trimText)
    {
        string newText = text;
        for (int t = trimText.Length - 1; t >= 0; t--)
        {
            if (newText.TrimEnd(trimText[t]).Equals(newText)) return text;
            else newText = newText.TrimEnd(trimText[t]);
        }
        return newText;
    }
}
