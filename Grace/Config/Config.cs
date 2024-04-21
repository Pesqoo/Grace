using System.Text.Json.Serialization;

namespace Grace.Config;

public class Config
{
    [JsonPropertyName("connectionString")]
    public required string ConnectionString { get; set; }
}
