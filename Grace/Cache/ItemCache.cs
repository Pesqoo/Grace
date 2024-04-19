using Grace.Model.DataContext;
using System.Data;

namespace Grace.Cache;

public static class ItemCache
{
    public static readonly Dictionary<int, string> Cache = new();

    public static async Task Init()
    {
        DataTable items = await DBManager.ExecuteQueryAsync($@"
            SELECT id, string.[value] as itemName
            FROM ItemResource as item
            LEFT JOIN StringResource_EN as string ON item.[name_id] = string.[code]"
        );

        foreach (DataRow row in items.Rows)
        {
            int id = row.Field<int>("id");
            string itemName = row.Field<string>("itemName") ?? "";
            Cache.Add(id, itemName);
        }
    }
}
