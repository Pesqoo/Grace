using Grace.Model.DataContext;
using System.Data;

namespace Grace.Model.Repository;
public class ItemRepository(DBManager _dbManager)
{
    public async Task<List<Item>> GetByReferenceToItemId(int itemId)
    {
        DataTable dataTable = await _dbManager.ExecuteQueryAsync($@"
			DECLARE @DropGroupIds TABLE (drop_group_id INT);

			-- all DropGroups with a direct reference to @ItemId
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

			-- all DropGroups with a direct or nested reference to @ItemId
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
				i.id as ID, 
				s.value as Name,
				i.opt_var1_0,
				i.opt_var1_1,
				i.opt_var1_2,
				i.opt_var1_3
			FROM ItemResource i
			LEFT JOIN StringResource_EN s ON i.name_id = s.code
			JOIN RecursiveDropGroups rdg ON
				(i.opt_type_0 = 94 AND i.opt_var1_0 = rdg.id) OR
				(i.opt_type_1 = 94 AND i.opt_var1_1 = rdg.id) OR
				(i.opt_type_2 = 94 AND i.opt_var1_2 = rdg.id) OR
				(i.opt_type_3 = 94 AND i.opt_var1_3 = rdg.id)
		");

        List<Item> result = [];
        foreach (DataRow row in dataTable.Rows)
        {
            int id = row.Field<int>("ID");
            string? name = row.Field<string>("Name");
            decimal var1 = row.Field<decimal>("opt_var1_0");
            decimal var2 = row.Field<decimal>("opt_var1_1");
            decimal var3 = row.Field<decimal>("opt_var1_2");
            decimal var4 = row.Field<decimal>("opt_var1_3");
            result.Add(new Item() { Id = id, Name = name, Var1 = var1, Var2 = var2, Var3 = var3, Var4 = var4 });
        }

        return result;
    }

    public async Task<List<Item>> GetByReferenceToDropGroupId(int dropGroupId)
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
				i.id as ID, 
				s.value as Name,
				i.opt_var1_0,
				i.opt_var1_1,
				i.opt_var1_2,
				i.opt_var1_3
			FROM ItemResource i
			LEFT JOIN StringResource_EN s ON i.name_id = s.code
			JOIN RecursiveDropGroups rdg ON
				(i.opt_type_0 = 94 AND i.opt_var1_0 = rdg.id) OR
				(i.opt_type_1 = 94 AND i.opt_var1_1 = rdg.id) OR
				(i.opt_type_2 = 94 AND i.opt_var1_2 = rdg.id) OR
				(i.opt_type_3 = 94 AND i.opt_var1_3 = rdg.id)
            ");

        List<Item> result = [];
        foreach (DataRow row in dataTable.Rows)
        {
            int id = row.Field<int>("ID");
            string? name = row.Field<string>("Name");
            decimal var1 = row.Field<decimal>("opt_var1_0");
            decimal var2 = row.Field<decimal>("opt_var1_1");
            decimal var3 = row.Field<decimal>("opt_var1_2");
            decimal var4 = row.Field<decimal>("opt_var1_3");
            result.Add(new Item() { Id = id, Name = name, Var1 = var1, Var2 = var2, Var3 = var3, Var4 = var4 });
        }

        return result;
    }
}
