using Grace.Model.DataContext;
using System.Data;

namespace Grace.Model.Repository;

public static class MonsterRepository
{
    private static string _selectQuery = $@"
            SELECT monster.[id], name.[value] as name, loc.[value] as location, monster.[drop_table_link_id], monster.[level]
            FROM [dbo].[MonsterResource] monster 
                JOIN [dbo].[StringResource_EN] name ON monster.[name_id] = name.[code]
                JOIN [dbo].[StringResource_EN] loc ON monster.[location_id] = loc.[code]";

    public static async Task<List<Monster>> GetAll()
    {
        DataTable dataTable = await DBManager.ExecuteQueryAsync(
            $"{_selectQuery} ORDER BY monster.[id] ASC;"
        );

        return Monster.FromDataTable(dataTable);
    }

    public static async Task<List<Monster>> GetById(int id)
    {
        DataTable dataTable = await DBManager.ExecuteQueryAsync(
            $"{_selectQuery} WHERE monster.[id] = {id};"
        );

        return Monster.FromDataTable(dataTable);
    }

    public static async Task<List<Monster>> GetByName(string name)
    {
        DataTable dataTable = await DBManager.ExecuteQueryAsync(
            $"{_selectQuery} WHERE name.[value] LIKE '%{name}%' ORDER BY monster.[id] ASC;"
        );

        return Monster.FromDataTable(dataTable);
    }

    public static async Task<List<Monster>> GetByLocation(string location)
    {
        DataTable dataTable = await DBManager.ExecuteQueryAsync(
            $"{_selectQuery} WHERE loc.[value] LIKE '%{location}%' ORDER BY monster.[id] ASC;"
        );

        return Monster.FromDataTable(dataTable);
    }

    public static async Task<List<Monster>> GetByDropId(int dropId)
    {
        DataTable dataTable = await DBManager.ExecuteQueryAsync(
            $"{_selectQuery} WHERE monster.[drop_table_link_id] = {dropId} ORDER BY monster.[id] ASC;"
        );

        return Monster.FromDataTable(dataTable);
    }
}
