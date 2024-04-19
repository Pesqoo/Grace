using Grace.Model.DataContext;
using System.Data;

namespace Grace.Model.Repository;

public class DropGroupRepository
{
    public static async Task<DropGroup?> GetById(int dropGroupId)
    {
        DataTable dataTable = await DBManager.ExecuteQueryAsync(
            $"SELECT * FROM DropGroupResource WHERE id = {dropGroupId}"
        );

        return dataTable.Rows.Count == 1 ? DropGroup.FromDataRow(dataTable.Rows[0]) : null;
    }
}
