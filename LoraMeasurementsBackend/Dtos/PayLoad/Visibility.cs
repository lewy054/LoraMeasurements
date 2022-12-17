using System.Text.Json.Serialization;

namespace LoraMeasurementsBackend.Dtos.PayLoad;

public class Visibility
{
    [JsonPropertyName("rights")]
    public List<string> Rights { get; set; }
}