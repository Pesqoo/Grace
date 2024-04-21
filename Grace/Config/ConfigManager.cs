using System.Reflection;
using System.Text.Json;

namespace Grace.Config;

public class ConfigManager
{
    public Config Config;
    private const string _configFileName = "config.json";

    public ConfigManager()
    {

        try
        {
            string configJson = File.ReadAllText(_configFileName);
            Config? parsedConfig = JsonSerializer.Deserialize<Config>(configJson) ?? throw new Exception("config is null");

            foreach (PropertyInfo property in parsedConfig.GetType().GetProperties())
            {
                if (property.GetValue(parsedConfig) == null)
                {
                    throw new Exception($"invalid {property.Name}");
                }
            }

            Config = parsedConfig;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error parsing config file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
    }
}
