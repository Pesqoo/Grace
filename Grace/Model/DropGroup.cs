using System.Data;

namespace Grace.Model;

public class DropGroup
{
    public int Id { get; set; }
    public int[] DropItemIds { get; set; } = new int[10];
    public int[] DropMinCounts { get; set; } = new int[10];
    public int[] DropMaxCounts { get; set; } = new int[10];
    public double[] DropPercentages { get; set; } = new double[10];
    public double TotalDropPercentage { get; set; }

    public static DropGroup FromDataRow(DataRow dataRow) => new()
    {
        Id = Convert.ToInt32(dataRow["id"]),
        DropItemIds = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_item_id_0{i}"])).ToArray(),
        DropMinCounts = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_min_count_0{i}"])).ToArray(),
        DropMaxCounts = Enumerable.Range(0, 10).Select(i => Convert.ToInt32(dataRow[$"drop_max_count_0{i}"])).ToArray(),
        DropPercentages = Enumerable.Range(0, 10).Select(i => Convert.ToDouble(dataRow[$"drop_percentage_0{i}"])).ToArray(),
    };
}
