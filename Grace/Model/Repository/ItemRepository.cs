using Grace.Model.DataContext;
using System.Data;

namespace Grace.Model.Repository;
public class ItemRepository(DBManager _dbManager)
{
    public async Task<List<int>> GetBoxesByDropGroup(int dropId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"SELECT id FROM ItemResource WHERE opt_type_0 = 94 AND opt_var_1_0 = {dropId}"
        );

        List<int> result = [];
        foreach (DataRow row in dataTable.Rows)
        {
            int id = row.Field<int>("id");
            result.Add(id);
        }

        return result;
    }
}
