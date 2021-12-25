namespace ExosHeroesManager;

internal sealed class SkillsAccessor : TableAccessor
{
    protected override ExosHeroesTables Table => ExosHeroesTables.Skills;

    public DataRow? GetMe(string name)
    {
        DataTable results = base.Select(new() { new(Table.PrimaryKey(), SQLRelation.EQ, name) }, new());
        return results.Rows.Count > 0 ? results.Rows[0] : null;
    }
    /// <summary>
    /// Select a single skill by name
    /// </summary>
    /// <param name="name">pk value</param>
    /// <returns></returns>
    public Skill Select(string name) => new(GetMe(name));
    /// <summary>
    /// Select specified columns of all Skills matching criteria
    /// </summary>
    /// <param name="criteria">Conditions to narrow records</param>
    /// <param name="mods">SQL Keywords to aid search and return</param>
    /// <returns></returns>
    public List<Skill> Select(List<SkillsCriteria> criteria, ModifierList mods)
    {
        List<Skill> list = new();
        DataTable table = base.Select(criteria.AsCriteria(), mods);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Skill(row));
        }
        return list;
    }
    /// <summary>
    /// Select all skills
    /// </summary>
    /// <param name="mods">Some sorting specification</param>
    /// <returns></returns>
    public List<Skill> SelectAll(ModifierList mods)
    {
        List<Skill> list = new();
        DataTable table = base.Select(new(), mods);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Skill(row));
        }
        return list;
    }

    /// <summary>
    /// Update the specified skills
    /// </summary>
    /// <param name="name">the skill to be updated</param>
    /// <param name="values">The values to change</param>
    /// <returns></returns>
    public int Update(string name, List<SkillsSetter> values) =>
        base.Update(values.AsSetters(), new() { new(Table.PrimaryKey(), SQLRelation.EQ, name) });

    public int Insert(List<SkillsSetter> values) => base.Insert(values.AsSetters());

    public new int Delete(string name) => base.Delete(name);
}

internal sealed class SkillsCriteria : Criteria
{
    public SkillsCriteria(SkillColumn column, SQLRelation relation, string value) : base(column.ColumnName(), relation, value) { }
}
internal sealed class SkillsSetter : Setter
{
    public SkillsSetter(SkillColumn column, string value) : base(column.ColumnName(), value) { }
}
