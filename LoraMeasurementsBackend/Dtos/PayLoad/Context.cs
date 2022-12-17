using System.Text.Json.Serialization;

public class Context
{
    [JsonPropertyName("tenant-id")]
    public string TenantId { get; set; }
}