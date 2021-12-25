namespace ExosHeroesManager;

internal sealed class EquipmentAccessor : TableAccessor
{
    protected override ExosHeroesTables Table => ExosHeroesTables.Equipment;

    public DataRow? GetMe(string id)
    {
        DataTable tbl = base.Select(new() { new(Table.PrimaryKey(), SQLRelation.EQ, id) }, new());
        return tbl.Rows.Count == 1 ? tbl.Rows[0] : null;
    }
    /// <summary>
    /// Select a single equipment by ID
    /// </summary>
    /// <param name="name">pk value</param>
    /// <returns></returns>
    public EquipmentPiece Select(string id) => new(GetMe(id), null);
    /// <summary>
    /// Select specified columns of all equipment matching criteria
    /// </summary>
    /// <param name="criteria">Conditions to narrow records (none if null)</param>
    /// <param name="mods">SQL Keywords to aid search and return</param>
    /// <returns></returns>
    public List<EquipmentPiece> Select(List<EquipmentCriteria> criteria, ModifierList mods)
    {
        List<EquipmentPiece> list = new();
        DataTable table = base.Select(criteria.AsCriteria(), mods);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new EquipmentPiece(row, null));
        }
        return list;
    }
    /// <summary>
    /// Select all equipment
    /// </summary>
    /// <param name="mods">Some sorting specification</param>
    /// <returns></returns>
    public List<EquipmentPiece> SelectAll(ModifierList mods)
    {
        List<EquipmentPiece> list = new();
        DataTable table = base.Select(new(), mods);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new EquipmentPiece(row, null));
        }
        return list;
    }

    /// <summary>
    /// Update the specified equipment
    /// </summary>
    /// <param name="name">the equipment to be updated</param>
    /// <param name="values">The values to change</param>
    /// <returns></returns>
    public int Update(string id, List<EquipmentSetter> values) =>
        base.Update(values.AsSetters(), new() { new(Table.PrimaryKey(), SQLRelation.EQ, id) });

    public int Insert(List<EquipmentSetter> values) => base.Insert(values.AsSetters());

    public new int Delete(string id) => base.Delete(id);
}

internal sealed class EquipmentCriteria : Criteria
{
    public EquipmentCriteria(EquipmentColumn column, SQLRelation relation, string value) : base(column.ColumnName(), relation, value) { }
}
internal sealed class EquipmentSetter : Setter
{
    public EquipmentSetter(EquipmentColumn column, string value) : base(column.ColumnName(), value) { }
}
