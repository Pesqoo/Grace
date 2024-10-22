using Grace.Cache;
using System.Data;

namespace Grace.Model;

/// <summary>
/// Can either be a DropTable (with SubId) or a DropGroup (SubId = -1)
/// </summary>
public class Drop
{
    public int Id { get; set; }
    public int SubId { get; set; }
    public int[] DropItemIds { get; set; } = new int[10];
    public int[] DropMinCounts { get; set; } = new int[10];
    public int[] DropMaxCounts { get; set; } = new int[10];
    public double[] DropPercentages { get; set; } = new double[10];
    public string[] ItemNames { get; set; } = new string[10];
    public string Alias { get; set; } = string.Empty;

    public static Drop FromDataRow(DataRow dataRow)
    {
        int[] dropItemIds = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_item_id_0{i}"])).ToArray();
        string[] itemNames = new string[10];

        for (int i = 0; i < 10; i++)
        {
            if (dropItemIds[i] > 0)
            {
                itemNames[i] = ItemCache.Cache[dropItemIds[i]];
            }
            else if (dropItemIds[i] < 0)
            {
                DropGroupCache.Cache.TryGetValue(dropItemIds[i], out Drop? dropGroup);
                var alias = dropGroup?.Alias;

                itemNames[i] = $"{dropItemIds[i]}" + (alias != null && alias != string.Empty ? $": {alias}" : string.Empty);
            }
            else
            {
                itemNames[i] = "Empty slot";
            }
        }

        return new Drop
        {
            Id = Convert.ToInt32(dataRow["id"]),
            SubId = dataRow.Table.Columns.Contains("sub_id") ? Convert.ToInt32(dataRow["sub_id"]) : -1,
            DropItemIds = dropItemIds,
            DropMinCounts = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_min_count_0{i}"])).ToArray(),
            DropMaxCounts = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_max_count_0{i}"])).ToArray(),
            DropPercentages = Enumerable.Range(0, 10).Select(i => Convert.ToDouble(dataRow[$"drop_percentage_0{i}"])).ToArray(),
            ItemNames = itemNames,

        };
    }

    public static List<Drop> FromDataTable(DataTable dataTable)
    {
        List<Drop> dropGroups = [];
        foreach (DataRow row in dataTable.Rows)
        {
            Drop dropGroup = FromDataRow(row);
            dropGroups.Add(dropGroup);
        }

        return dropGroups;
    }
}
