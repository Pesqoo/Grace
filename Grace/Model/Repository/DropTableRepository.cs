using Grace.Model.DataContext;
using System.Data;

namespace Grace.Model.Repository;

public static class DropTableRepository
{
    public static async Task<List<DropTable>> GetById(int dropTableId)
    {
        DataTable dataTable = await DBManager.ExecuteQueryAsync(
            $"SELECT * FROM MonsterDropTableResource WHERE [id] = {dropTableId}"
        );

        return DropTable.FromDataTable(dataTable);
    }

    public static async Task<DropTable?> GetByIdAndSubId(int dropTableId, int subId)
    {
        DataTable dataTable = await DBManager.ExecuteQueryAsync(
            $"SELECT * FROM MonsterDropTableResource WHERE [id] = {dropTableId} AND [sub_id] = {subId}"
        );

        return dataTable.Rows.Count == 1 ? DropTable.FromDataRow(dataTable.Rows[0]) : null;
    }
}
