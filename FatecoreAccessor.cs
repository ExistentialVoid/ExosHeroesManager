namespace ExosHeroesManager;

internal sealed class FatecoreAccessor : TableAccessor
{
    protected override ExosHeroesTables Table => ExosHeroesTables.Fatecores;

    public DataRow? GetMe(string name)
    {
        DataTable tbl = base.Select(new() { new(Table.PrimaryKey(), SQLRelation.EQ, name) }, new());
        return tbl.Rows.Count == 1 ? tbl.Rows[0] : null;
    }
    /// <summary>
    /// Select a single fatecore by name
    /// </summary>
    /// <param name="name">pk value</param>
    /// <returns></returns>
    public Fatecore Select(string name) => new(GetMe(name));
    /// <summary>
    /// Select specified columns of all fatecore matching criteria
    /// </summary>
    /// <param name="criteria">Conditions to narrow records (none if null)</param>
    /// <param name="mods">SQL Keywords to aid search and return</param>
    /// <returns></returns>
    public List<Fatecore> Select(List<FatecoreCriteria> criteria, ModifierList mods)
    {
        List<Fatecore> list = new();
        DataTable table = base.Select(criteria.AsCriteria(), mods);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Fatecore(row));
        }
        return list;
    }
    /// <summary>
    /// Select all fatecore
    /// </summary>
    /// <param name="mods">Some sorting specification</param>
    /// <returns></returns>
    public List<Fatecore> SelectAll(ModifierList mods)
    {
        List<Fatecore> list = new();
        DataTable table = base.Select(new(), mods);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Fatecore(row));
        }
        return list;
    }

    /// <summary>
    /// Update the specified fatecore
    /// </summary>
    /// <param name="name">the fatecore to be updated</param>
    /// <param name="values">The values to change</param>
    /// <returns></returns>
    public int Update(string name, List<FatecoreSetter> values) =>
        base.Update(values.AsSetters(), new() { new(Table.PrimaryKey(), SQLRelation.EQ, name) });

    public int Insert(List<FatecoreSetter> values) => base.Insert(values.AsSetters());

    public new int Delete(string name) => base.Delete(name);
}

internal sealed class FatecoreCriteria : Criteria
{
    public FatecoreCriteria(FatecoreColumn column, SQLRelation relation, string value) : base(column.ColumnName(), relation, value) { }
}
internal sealed class FatecoreSetter : Setter
{
    public FatecoreSetter(FatecoreColumn column, string value) : base(column.ColumnName(), value) { }
}
