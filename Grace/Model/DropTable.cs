using System.Data;

namespace Grace.Model;

public class DropTable : DropGroup
{
    public int SubId { get; set; }

    public static new DropTable FromDataRow(DataRow dataRow) => new()
    {
        Id = Convert.ToInt32(dataRow["id"]),
        SubId = Convert.ToInt32(dataRow["sub_id"]),
        DropItemIds = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_item_id_0{i}"])).ToArray(),
        DropMinCounts = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_min_count_0{i}"])).ToArray(),
        DropMaxCounts = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_max_count_0{i}"])).ToArray(),
        DropPercentages = Enumerable.Range(0, 10).Select(i => Convert.ToDouble(dataRow[$"drop_percentage_0{i}"])).ToArray(),
    };

    public static List<DropTable> FromDataTable(DataTable dataTable)
    {
        List<DropTable> dropTables = [];
        foreach (DataRow row in dataTable.Rows)
        {
            DropTable dropTable = FromDataRow(row);
            dropTables.Add(dropTable);
        }

        return dropTables;
    }

    /*
    public static List<T> FromDataTablee<T>(DataTable dataTable, Func<DataRow, T> FromDataRow)
    {
        List<T> list = [];
        foreach (DataRow row in dataTable.Rows)
        {
            T item = FromDataRow(row);
            list.Add(item);
        }

        return list;
    }
    */
}
