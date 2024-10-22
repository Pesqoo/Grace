using Grace.Model.DataContext;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Grace.Model.Repository;

public class DropRepository(DBManager _dbManager)
{
    public async Task<List<Drop>> GetAll()
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"SELECT DISTINCT * FROM DropGroupResource"
        );

        return Drop.FromDataTable(dataTable);
    }

    public async Task<Drop?> GetGroupById(int dropGroupId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"SELECT * FROM DropGroupResource WHERE id = {dropGroupId}"
        );

        return dataTable.Rows.Count == 1 ? Drop.FromDataRow(dataTable.Rows[0]) : null;
    }

    public async Task<List<Drop>> GetGroupsByParentId(int dropGroupId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"SELECT * FROM DropGroupResource WHERE id = {dropGroupId}"
        );

        return Drop.FromDataTable(dataTable);
    }

    public async Task<List<Drop>> GetTableById(int dropTableId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
           $"SELECT * FROM MonsterDropTableResource WHERE [id] = {dropTableId}"
       );

        return Drop.FromDataTable(dataTable);
    }

    public async Task<Drop?> GetTableByIdAndSubId(int dropTableId, int subId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"SELECT * FROM MonsterDropTableResource WHERE [id] = {dropTableId} AND [sub_id] = {subId}"
        );

        return dataTable.Rows.Count == 1 ? Drop.FromDataRow(dataTable.Rows[0]) : null;
    }

    public async Task<int> UpdateGroup(Drop dropGroup)
    {
        var queryBuilder = new StringBuilder($"UPDATE DropGroupResource SET ");

        for (int i = 0; i < 10; i++)
        {
            queryBuilder.Append($"drop_item_id_{i:00} = {dropGroup.DropItemIds[i]}, ");
            queryBuilder.Append($"drop_min_count_{i:00} = {dropGroup.DropMinCounts[i]}, ");
            queryBuilder.Append($"drop_max_count_{i:00} = {dropGroup.DropMaxCounts[i]}, ");
            queryBuilder.Append($"drop_percentage_{i:00} = {dropGroup.DropPercentages[i].ToString(System.Globalization.CultureInfo.InvariantCulture)}, ");
        }

        queryBuilder.Length -= 2;
        queryBuilder.Append($" WHERE id = {dropGroup.Id}");

        Debug.WriteLine(queryBuilder.ToString());

        return await _dbManager.ExecuteNonQueryAsync(queryBuilder.ToString());
    }

    public async Task<int> UpdateTable(Drop dropTable)
    {
        var queryBuilder = new StringBuilder($"UPDATE MonsterDropTableResource SET ");

        for (int i = 0; i < 10; i++)
        {
            queryBuilder.Append($"drop_item_id_{i:00} = {dropTable.DropItemIds[i]}, ");
            queryBuilder.Append($"drop_min_count_{i:00} = {dropTable.DropMinCounts[i]}, ");
            queryBuilder.Append($"drop_max_count_{i:00} = {dropTable.DropMaxCounts[i]}, ");
            queryBuilder.Append($"drop_percentage_{i:00} = {dropTable.DropPercentages[i].ToString(System.Globalization.CultureInfo.InvariantCulture)}, ");
        }

        queryBuilder.Length -= 2;
        queryBuilder.Append($" WHERE id = {dropTable.Id} AND sub_id = {dropTable.SubId}");

        Debug.WriteLine(queryBuilder.ToString());

        return await _dbManager.ExecuteNonQueryAsync(queryBuilder.ToString());
    }

    public async Task<List<Drop>> GetByReferenceToDropGroupId(int dropGroupId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync($@"
            WITH RecursiveDropGroups AS (
	            SELECT *
	            FROM DropGroupResource
	            WHERE id = {dropGroupId}

	            UNION ALL

	            SELECT 
		            dgr.*
	            FROM DropGroupResource dgr
	            JOIN RecursiveDropGroups r ON 
		            r.id IN (
			            dgr.drop_item_id_00, 
			            dgr.drop_item_id_01, 
			            dgr.drop_item_id_02, 
			            dgr.drop_item_id_03, 
			            dgr.drop_item_id_04, 
			            dgr.drop_item_id_05, 
			            dgr.drop_item_id_06, 
			            dgr.drop_item_id_07, 
			            dgr.drop_item_id_08, 
			            dgr.drop_item_id_09
		            )
            )

            SELECT * FROM RecursiveDropGroups
        ");

        return Drop.FromDataTable(dataTable);
    }

    public async Task<List<Drop>> GetByReferenceToItemId(int itemId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync($@"
            DECLARE @DropGroupIds TABLE (drop_group_id INT);
            INSERT INTO @DropGroupIds (drop_group_id)
                SELECT id as drop_group_id
                FROM DropGroupResource dgr
                WHERE {itemId} IN (
		            dgr.drop_item_id_00, 
		            dgr.drop_item_id_01, 
		            dgr.drop_item_id_02, 
		            dgr.drop_item_id_03, 
		            dgr.drop_item_id_04, 
		            dgr.drop_item_id_05, 
		            dgr.drop_item_id_06, 
		            dgr.drop_item_id_07, 
		            dgr.drop_item_id_08, 
		            dgr.drop_item_id_09
                );  

            WITH RecursiveDropGroups AS (
	            SELECT 
		            *,
		            0 AS nesting_level
	            FROM DropGroupResource
	            WHERE id IN (SELECT drop_group_id FROM @DropGroupIds)

	            UNION ALL

	            SELECT 
		            dgr.*,
		            r.nesting_level + 1 AS nesting_level
	            FROM DropGroupResource dgr
	            JOIN RecursiveDropGroups r ON 
		            r.id IN (
			            dgr.drop_item_id_00, 
			            dgr.drop_item_id_01, 
			            dgr.drop_item_id_02, 
			            dgr.drop_item_id_03, 
			            dgr.drop_item_id_04, 
			            dgr.drop_item_id_05, 
			            dgr.drop_item_id_06, 
			            dgr.drop_item_id_07, 
			            dgr.drop_item_id_08, 
			            dgr.drop_item_id_09
		            )
            )

            SELECT * FROM RecursiveDropGroups;
        ");

        return Drop.FromDataTable(dataTable);
    }
}
