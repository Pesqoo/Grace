using System.Data;

namespace Grace.Model;

public class Monster
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int DropId { get; set; }
    public int Level { get; set; }

    private static Monster FromDataRow(DataRow dataRow) => new()
    {
        Id = dataRow.Field<int>("id"),
        Name = dataRow.Field<string>("name"),
        Location = dataRow.Field<string>("location"),
        DropId = dataRow.Field<int>("drop_table_link_id"),
        Level = dataRow.Field<int>("level"),
    };

    public static List<Monster> FromDataTable(DataTable dataTable)
    {
        List<Monster> monsters = [];
        foreach (DataRow row in dataTable.Rows)
        {
            Monster monster = FromDataRow(row);
            monsters.Add(monster);
        }

        return monsters;
    }
}
