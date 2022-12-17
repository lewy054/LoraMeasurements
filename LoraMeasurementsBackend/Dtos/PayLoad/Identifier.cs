using System.Text.Json.Serialization;

public class Identifier
{
    [JsonPropertyName("device_ids")]
    public DeviceIds DeviceIds { get; set; }
}