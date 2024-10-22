using Grace.Model.DataContext;
using System.Data;

namespace Grace.Model.Repository;

public class MonsterRepository(DBManager _dbManager)
{
    private static string _selectQuery = $@"
            SELECT monster.[id], name.[value] as name, loc.[value] as location, monster.[drop_table_link_id], monster.[level]
            FROM [dbo].[MonsterResource] monster 
                JOIN [dbo].[StringResource_EN] name ON monster.[name_id] = name.[code]
                JOIN [dbo].[StringResource_EN] loc ON monster.[location_id] = loc.[code]";

    public async Task<List<Monster>> GetAll()
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"{_selectQuery} ORDER BY monster.[id] ASC;"
        );

        return Monster.FromDataTable(dataTable);
    }

    public async Task<List<Monster>> GetById(int id)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"{_selectQuery} WHERE monster.[id] = {id};"
        );

        return Monster.FromDataTable(dataTable);
    }

    public async Task<List<Monster>> GetByName(string name)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"{_selectQuery} WHERE name.[value] LIKE '%{name}%' ORDER BY monster.[id] ASC;"
        );

        return Monster.FromDataTable(dataTable);
    }

    public async Task<List<Monster>> GetByLocation(string location)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"{_selectQuery} WHERE loc.[value] LIKE '%{location}%' ORDER BY monster.[id] ASC;"
        );

        return Monster.FromDataTable(dataTable);
    }

    public async Task<List<Monster>> GetByDropId(int dropId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync(
            $"{_selectQuery} WHERE monster.[drop_table_link_id] = {dropId} ORDER BY monster.[id] ASC;"
        );

        return Monster.FromDataTable(dataTable);
    }

    public async Task<List<Monster>> GetByReferenceToDropGroupId(int dropGroupId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync($@"
            WITH RecursiveDropGroups AS (
	            SELECT 
		            id,
		            0 AS nesting_level,
		            drop_item_id_00,
		            drop_item_id_01,
		            drop_item_id_02,
		            drop_item_id_03,
		            drop_item_id_04,
		            drop_item_id_05,
		            drop_item_id_06,
		            drop_item_id_07,
		            drop_item_id_08,
		            drop_item_id_09
	            FROM DropGroupResource
	            WHERE id = {dropGroupId}

	            UNION ALL

	            SELECT 
		            dgr.id,
		            r.nesting_level + 1 AS nesting_level,
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

			SELECT 
				mr.id, 
				name_string.value AS name, 
				location_string.value as location,
				mr.drop_table_link_id,
				mr.level
			FROM MonsterResource mr
			JOIN StringResource_EN name_string
			ON mr.name_id = name_string.code
			JOIN StringResource_EN location_string
			ON mr.location_id = location_string.code
			WHERE 
				mr.drop_table_link_id IN (
				SELECT mdtr.id
				FROM MonsterDropTableResource mdtr
				WHERE EXISTS (
					SELECT 1
					FROM RecursiveDropGroups rdg
					WHERE 
						mdtr.drop_item_id_00 IN (rdg.id) OR
						mdtr.drop_item_id_01 IN (rdg.id) OR
						mdtr.drop_item_id_02 IN (rdg.id) OR
						mdtr.drop_item_id_03 IN (rdg.id) OR
						mdtr.drop_item_id_04 IN (rdg.id) OR
						mdtr.drop_item_id_05 IN (rdg.id) OR
						mdtr.drop_item_id_06 IN (rdg.id) OR
						mdtr.drop_item_id_07 IN (rdg.id) OR
						mdtr.drop_item_id_08 IN (rdg.id) OR
						mdtr.drop_item_id_09 IN (rdg.id)
				)
			);"
        );

        return Monster.FromDataTable(dataTable);
    }

    public async Task<List<Monster>> GetByReferenceToItemId(int itemId)
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
					id,
					0 AS nesting_level,
					drop_item_id_00,
					drop_item_id_01,
					drop_item_id_02,
					drop_item_id_03,
					drop_item_id_04,
					drop_item_id_05,
					drop_item_id_06,
					drop_item_id_07,
					drop_item_id_08,
					drop_item_id_09
				FROM DropGroupResource
				WHERE id IN (SELECT drop_group_id FROM @DropGroupIds)

				UNION ALL

				SELECT 
					dgr.id,
					r.nesting_level + 1 AS nesting_level,
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

			SELECT 
				mr.id, 
				name_string.value AS name, 
				location_string.value as location,
				mr.drop_table_link_id,
				mr.level
			FROM MonsterResource mr
			JOIN StringResource name_string
			ON mr.name_id = name_string.code
			JOIN StringResource location_string
			ON mr.location_id = location_string.code
			WHERE 
				mr.drop_table_link_id IN (
				-- all top-level drops with a direct or nested reference to @ItemId
				SELECT mdtr.id
				FROM MonsterDropTableResource mdtr
				WHERE EXISTS (
					SELECT 1
					FROM RecursiveDropGroups rdg
					WHERE 
						mdtr.drop_item_id_00 IN (rdg.id) OR
						mdtr.drop_item_id_01 IN (rdg.id) OR
						mdtr.drop_item_id_02 IN (rdg.id) OR
						mdtr.drop_item_id_03 IN (rdg.id) OR
						mdtr.drop_item_id_04 IN (rdg.id) OR
						mdtr.drop_item_id_05 IN (rdg.id) OR
						mdtr.drop_item_id_06 IN (rdg.id) OR
						mdtr.drop_item_id_07 IN (rdg.id) OR
						mdtr.drop_item_id_08 IN (rdg.id) OR
						mdtr.drop_item_id_09 IN (rdg.id)
				)
			);");

        return Monster.FromDataTable(dataTable);
    }
}
