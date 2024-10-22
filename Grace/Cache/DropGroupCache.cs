using Grace.Model;
using Grace.Model.Repository;
using System.Text.Json;

namespace Grace.Cache;
public class DropGroupCache(DropRepository dropGroupRepository)
{
    private readonly DropRepository _dropGroupRepository = dropGroupRepository;
    public static readonly Dictionary<int, Drop> Cache = [];

    public async Task Init()
    {
        Cache.Clear();

        List<Drop> dropGroups = await _dropGroupRepository.GetAll();

        foreach (var dropGroup in dropGroups)
            Cache.Add(dropGroup.Id, dropGroup);

        string json = File.ReadAllText("dropGroupNames.json");
        var dropGroupNames = JsonSerializer.Deserialize<Dictionary<int, string>>(json);
        if (dropGroupNames != null)
        {
            foreach (var pair in dropGroupNames)
                Cache[pair.Key].Alias = pair.Value;
        }
    }

    public static void AddDropGroupName(int key, string value)
    {
        try
        {
            string oldJson = File.ReadAllText("dropGroupNames.json");
            var dropGroupNames = JsonSerializer.Deserialize<Dictionary<int, string>>(oldJson);

            if (dropGroupNames == null)
                return;

            dropGroupNames[key] = value;
            Cache[key].Alias = value;

            string newJson = JsonSerializer.Serialize(dropGroupNames);
            File.WriteAllText("dropGroupNames.json", newJson);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating dropGroupNames.json: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
