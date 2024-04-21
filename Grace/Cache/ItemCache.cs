using Grace.Model.DataContext;
using System.Data;

namespace Grace.Cache;

public class ItemCache
{
    private readonly DBManager _dbManager;
    public static readonly Dictionary<int, string> Cache = [];

    public ItemCache(DBManager dbManager)
    {
        _dbManager = dbManager;
    }

    public async Task Init()
    {
        DataTable items = await _dbManager.ExecuteQueryAsync($@"
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
