using System.Data.SqlClient;

namespace ExosHeroesManager;

internal enum ExosHeroesTables { Equipment, Fatecores, Heroes, Skills }
/// <summary>
/// Aid SELECT statements to provide additional functionality
/// </summary>
public enum SQLModifier { OrderAsc, OrderDesc, Distinct, Max, Min, Top }
/// <summary>
/// SQL relational operator
/// </summary>
public enum SQLRelation { EQ, NE, GT, LT, GE, LE, LK, NL, NU, NN,
    /// <summary>
    /// Signal the first value of BETWEEN statment
    /// </summary>
    B1,
    /// <summary>
    /// Signal the second value of the BETWEEN statement
    /// </summary>
    B2
}
/// <summary>
/// Values whose descriptions are meaningful
/// </summary>
public enum StndVal { All, Any, Empty, NA, NNUL, None, Test, Select }

internal class ExosHeroesDBAccess
{
    private readonly string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sabar\\source\\repos\\ExosHeroesManager\\ExosHeroes.mdf;Integrated Security=True";

    /// <summary>
    /// Try inserting, deleting, or updating records in ExosHeroes.mdf
    /// </summary>
    /// <param name="query">Completed query statement</param>
    /// <returns></returns>
    internal int ExecuteQuery(string query)
    {
        query = query.Trim();
        try
        {
            using SqlConnection myConn = new(ConnectionString);
            myConn.Open();
            using SqlCommand myCmnd = new(query, myConn);
            int affectedRecords = myCmnd.ExecuteNonQuery();
            return affectedRecords;
        }
        catch
        {
            MessageBox.Show($"{query} has thrown an exception.");
            return -1;
        }
    }

    /// <summary>
    /// Specific query to get number of records in ExosHeroes.mdf
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    internal int CountRecords(string tableName)
    {
        try
        {
            using SqlConnection myConn = new(ConnectionString);
            myConn.Open();
            using SqlCommand myCmnd = new($"SELECT COUNT(1)[Total Rows] FROM [{tableName}]", myConn);
            return (int)myCmnd.ExecuteScalar();
        }
        catch { return -1; }
    }

    /// <summary>
    /// Try selecting and storing WO_System records in ExosHeroes.mdf
    /// </summary>
    /// <param name="query">Completed query statement</param>
    /// <returns></returns>
    internal DataTable GetDataFromQuery(string query)
    {
        query = query.Trim();
        DataTable datTbl = new();
        try
        {
            using SqlConnection myConn = new(ConnectionString);
            myConn.Open();
            using SqlCommand myCmnd = new(query, myConn);
            using SqlDataReader myRdr = myCmnd.ExecuteReader();
            datTbl.Load(myRdr);
        }
        catch
        {
            MessageBox.Show($"{query} has thrown an exception.");
        }
        return datTbl;
    }
}

internal abstract class TableAccessor
{
    protected abstract ExosHeroesTables Table { get; }
    readonly ExosHeroesDBAccess dboAccess = new();

    public int Count() => dboAccess.CountRecords(Table.TableName());

    /// <summary>
    /// Remove records
    /// </summary>
    /// <param name="pkValue">Single criteria in WHERE</param>
    /// <returns></returns>
    protected int Delete(string pkValue)
    {
        return dboAccess.ExecuteQuery($"DELETE FROM {Table.TableName()} WHERE {Table.PrimaryKey()}='{pkValue}'");
    }

    /// <summary>
    /// Add a single record
    /// </summary>
    /// <param name="values">All (non-null) values</param>
    /// <returns></returns>
    protected int Insert(List<Setter> values)
    {
        return dboAccess.ExecuteQuery($"INSERT INTO {Table.TableName()} {values.BuildINSStr()}");
    }

    /// <summary>
    /// Select records
    /// </summary>
    /// <param name="table">Table to select from</param>
    /// <param name="selection">ColumnNames to retrieve (ALL if null)</param>
    /// <param name="criteria">Conditions to narrow records (none if null)</param>
    /// <param name="mods">SQL Keywords to aid search and return (Caution: no result on incompatible keywords)</param>
    /// <returns></returns>
    protected DataTable Select(List<Criteria> criteria, ModifierList mods)
    {
        if (mods == null) mods = new();
        string query = $"SELECT * {mods.FrontString()} FROM {Table.TableName()} {criteria.BuildWHRStr()} {mods.BackString()}";
        return dboAccess.GetDataFromQuery(query);
    }

    /// <summary>
    /// Update records in a table
    /// </summary>
    /// <param name="values">ColumnNames to over-write values</param>
    /// <param name="criteria">Conditions to specify which records to update</param>
    /// <returns></returns>
    protected int Update(List<Setter> values, List<Criteria> criteria)
    {
        return dboAccess.ExecuteQuery($"UPDATE {Table.TableName()} {values.BuildSETStr()} {criteria.BuildWHRStr()}");
    }
}

public struct Modifier
{
    public SQLModifier Type;
    public string Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="val">Enter the value without quotations</param>
    public Modifier(SQLModifier keyword, string val)
    {
        Type = keyword;
        Value = val.ToUpper();
    }

    /// <summary>
    /// Output the string as should appear in a query statement
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Type switch
        {
            SQLModifier.OrderAsc => $"{Value} ASC",
            SQLModifier.OrderDesc => $"{Value} DESC",
            SQLModifier.Distinct => $"DISTINCT({Value}) ",
            SQLModifier.Max => $"MAX({Value}) ",
            SQLModifier.Min => $"MIN({Value}) ",
            SQLModifier.Top => $"TOP {Value} ",
            _ => string.Empty
        };
    }
}
public class ModifierList : List<Modifier>
{
    /// <summary>
    /// Filter excess, or query-breaking, items from being added
    /// </summary>
    /// <param name="modifier"></param>
    public new void Add(Modifier modifier)
    {
        switch (modifier.Type)
        {
            case SQLModifier.OrderAsc:
            case SQLModifier.OrderDesc:
            case SQLModifier.Distinct:
                base.Add(modifier);
                break;
            case SQLModifier.Max:
                if (!Exists(m => m.Type == SQLModifier.Max)) base.Add(modifier);
                break;
            case SQLModifier.Min:
                if (!Exists(m => m.Type == SQLModifier.Min)) base.Add(modifier);
                break;
            case SQLModifier.Top:
                if (!Exists(m => m.Type == SQLModifier.Top)) base.Add(modifier);
                break;
        }
    }

    /// <summary>
    /// Prepare statement from all items appearing before FROM
    /// </summary>
    /// <returns>A string to append to SELECT query section</returns>
    public string FrontString()
    {
        string str = string.Empty;
        FindAll(m => m.Type == SQLModifier.Top).ForEach(m => str += m.ToString());
        FindAll(m => m.Type == SQLModifier.Distinct).ForEach(m => str += m.ToString());
        return str.TrimEnd();
    }

    /// <summary>
    /// Prepare statement from all items appearing after WHERE
    /// </summary>
    /// <returns>A string to append to WHERE query section</returns>
    public string BackString()
    {
        string str = string.Empty;
        FindAll(m => m.Type == SQLModifier.Min).ForEach(m => str += m.ToString());
        FindAll(m => m.Type == SQLModifier.Max).ForEach(m => str += m.ToString());
        List<SQLModifier> orderings = new() { SQLModifier.OrderAsc, SQLModifier.OrderDesc };
        FindAll(m => orderings.Contains(m.Type)).ForEach(m => str += (str.Contains(" BY ") ? $", " : "ORDER BY ") + m.ToString());
        return str.TrimEnd();
    }
}

/// <summary>
/// Information to build a SET query statement
/// </summary>
public class Setter
{
    public readonly string ColumnName;
    private string setValue;
    public string SetValue
    {
        get => setValue;
        set
        {
            setValue = value;
            SQLValue = $"'{value}'";
        }
    }
    internal readonly string SQLColumnName;
    internal string SQLValue;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnName">May enter column name with or without brackets</param>
    /// <param name="value">Enter exact string to be stored (\' will be applied)</param>
    public Setter(string columnName, string value)
    {
        ColumnName = columnName.Trim('[', ']');
        SQLColumnName = $"[{ColumnName}]";
        setValue = value.Equals(DBNull.Value.ToString()) || value.Equals("NULL") ? "NULL" : $"{value}";
        SQLValue = value.Equals(DBNull.Value.ToString()) || value.Equals("NULL") ? "NULL" : $"'{value}'";
    }

    public Setter AsBase() => new(ColumnName, setValue);
    /// <summary>
    /// Output a single set statment: [ColumnName]='SetValue'
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{SQLColumnName}={SQLValue}";
}

/// <summary>
/// Information to build a WHERE query statement with customized value related to a column
/// </summary>
public class Criteria
{
    public readonly string ColumnName;
    public readonly SQLRelation Relation;
    private string conditionValue;
    public string ConditionValue
    {
        get => conditionValue;
        set
        {
            conditionValue = value;
            SQLValue = $"'{value}'";
        }
    }
    internal readonly string SQLColumnName;
    internal string SQLValue;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnName">May enter column name with or without brackets</param>
    /// <param name="relation"></param>
    /// <param name="value">Enter exact string to be searched (\' will be applied)</param>
    public Criteria(string columnName, SQLRelation relation, string value)
    {
        ColumnName = columnName.Trim('[', ']');
        SQLColumnName = $"[{ColumnName}]";
        Relation = relation;
        conditionValue = value;
        SQLValue = relation switch
        {
            SQLRelation.LK => $"'%{value}%'",
            SQLRelation.NL => $"'%{value}%'",
            _ => $"'{value}'"
        };
    }

    public Criteria AsBase() => new(ColumnName, Relation, conditionValue);
    /// <summary>
    /// Output a sinlge WHERE condition formed from these fields
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (SQLValue.Equals(StndVal.Any.GetText())) return $"({SQLColumnName} IS NOT NULL AND {SQLColumnName}<>'')";
        else if (SQLValue.Equals(StndVal.Empty)) return $"({SQLColumnName} IS NULL OR {SQLColumnName}='')";
        else if (SQLValue.Equals(StndVal.All.GetText())) return string.Empty;
        else
        {
            return Relation switch
            {
                SQLRelation.NU => $"{SQLColumnName} IS NULL",
                SQLRelation.NN => $"{SQLColumnName} IS NOT NULL",
                SQLRelation.B1 => $"{SQLColumnName} BETWEEN (CAST({SQLValue} AS DATE))",
                SQLRelation.B2 => $"(CAST({SQLValue} AS DATE))",
                _ => $"{SQLColumnName}{Relation.Operator()}{SQLValue}"
            };
        }
    }
}

public static partial class Extensions
{
    internal static List<Criteria> AsCriteria<C>(this List<C> criteria) where C : Criteria
    {
        List<Criteria> list = new();
        criteria?.ForEach(item => list.Add(item.AsBase()));
        return list;
    }

    internal static List<Setter> AsSetters<V>(this List<V> values) where V : Setter
    {
        List<Setter> list = new();
        values?.ForEach(item => list.Add(item.AsBase()));
        return list;
    }

    /// <summary>
    /// Return a string of columns and VALUES for an SQL INSERT statement
    /// </summary>
    /// <param name="values">List of [ColumnName, InsertValue]</param>
    /// <returns></returns>
    internal static string BuildINSStr(this List<Setter> values)
    {
        string columnsStr = "(";
        string valuesStr = "VALUES (";
        values.ForEach(i =>
        {
            columnsStr += $"{i.ColumnName}, ";
            valuesStr += $"{i.SQLValue}, ";
        });
        return $"{columnsStr.TrimEnd(", ")}) {valuesStr.TrimEnd(", ")})";
    }

    /// <summary>
    /// Return a string representing the SET for an SQL UPDATE statement
    /// </summary>
    /// <param name="values">List of [ColumnNames, UpdateValues] to set</param>
    /// <returns></returns>
    internal static string BuildSETStr(this List<Setter> values)
    {
        string SETStr = "SET ";
        values.ForEach(s => SETStr += $"{s}, ");
        return SETStr.TrimEnd(", ");
    }

    /// <summary>
    /// Return a string representing the WHERE for an SQL SELECT statement
    /// </summary>
    /// <param name="criteria">List of WhereStructs</param>
    /// <returns></returns>
    internal static string BuildWHRStr(this List<Criteria> criteria)
    {
        if (criteria == null || criteria.Count == 0) return string.Empty;

        string WHRStr = "WHERE ";
        int index;
        criteria.ForEach(c =>
        {
            index = criteria.IndexOf(c);

            // Same, non-date column indicates OR: "IN (val1, ..., valN)"
            if (index < criteria.Count - 1 && c.ColumnName.Equals(criteria[index + 1].ColumnName) && c.Relation != SQLRelation.B1)
            {
                // IN (val1, ..., valN) style
                //if (index == 0 || (index > 0 && !criteria[index - 1].ColumnName.Equals(c.ColumnName)))
                //{
                //    WHRStr += $"{c.SQLColumnName} IN ({c.SQLValue}, ";
                //}
                //else if (index > 0)
                //{
                //    WHRStr += $"{c.SQLValue}, ";
                //}

                // ([...] val1 OR ... OR [...] val2) style
                if (index == 0 || (index > 0 && !criteria[index - 1].ColumnName.Equals(c.ColumnName)))
                {
                    WHRStr += $"({c}";
                }
                else if (index > 0)
                {
                    WHRStr += $" OR {c}";
                }
            }
            else if (index > 0 && c.ColumnName.Equals(criteria[index - 1].ColumnName) && c.Relation != SQLRelation.B2)
            {
                //WHRStr += $"{c.SQLValue}) AND ";
                WHRStr += $" OR {c}) AND ";
            }
            else
            {
                WHRStr += $"{c} AND ";
            }
        });

        return WHRStr.TrimEnd(" AND ");
    }
}
