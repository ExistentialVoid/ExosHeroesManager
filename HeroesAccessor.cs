namespace ExosHeroesManager;

internal sealed class HeroesAccessor : TableAccessor
{
    protected override ExosHeroesTables Table => ExosHeroesTables.Heroes;

    public List<string> GetAllNames()
    {
        List<string> list = new();
        DataTable table = base.Select(new(), new());
        foreach (DataRow row in table.Rows)
        {
            list.Add(Convert.ToString(row[(int)HeroColumn.Name]));
        }
        return list;
    }
    public DataRow? GetMe(string name)
    {
        DataTable tbl = base.Select(new() { new(Table.PrimaryKey(), SQLRelation.EQ, name) }, new());
        return tbl.Rows.Count == 1 ? tbl.Rows[0] : null;
    }
    public Hero Select(string name) => new(GetMe(name));
    /// <summary>
    /// Select specified columns of all Skills matching criteria
    /// </summary>
    /// <param name="criteria">Conditions to narrow records</param>
    /// <param name="mods">SQL Keywords to aid search and return</param>
    /// <returns></returns>
    public List<Hero> Select(List<HeroCriteria> criteria, ModifierList mods)
    {
        List<Hero> list = new();
        DataTable table = base.Select(criteria.AsCriteria(), mods);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Hero(row));
        }
        return list;
    }
    /// <summary>
    /// Select all skills
    /// </summary>
    /// <param name="mods">Some sorting specification</param>
    /// <returns></returns>
    public List<Hero> SelectAll(ModifierList mods)
    {
        List<Hero> list = new();
        DataTable table = base.Select(new(), mods);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Hero(row));
        }
        return list;
    }

    /// <summary>
    /// Update the specified skills
    /// </summary>
    /// <param name="name">the skill to be updated</param>
    /// <param name="values">The values to change</param>
    /// <returns></returns>
    public int Update(string name, List<HeroSetter> values) =>
        base.Update(values.AsSetters(), new() { new(Table.PrimaryKey(), SQLRelation.EQ, name) });

    public int Insert(List<HeroSetter> values) => base.Insert(values.AsSetters());

    public new int Delete(string name) => base.Delete(name);
}

internal sealed class HeroCriteria : Criteria
{
    public HeroCriteria(HeroColumn column, SQLRelation relation, string value) : base(column.ColumnName(), relation, value) { }
}
internal sealed class HeroSetter : Setter
{
    public HeroSetter(HeroColumn column, string value) : base(column.ColumnName(), value) { }
}
