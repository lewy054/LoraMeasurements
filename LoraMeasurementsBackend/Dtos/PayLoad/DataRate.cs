using System.Text.Json.Serialization;

namespace LoraMeasurementsBackend.Dtos.PayLoad;

public class DataRate
{
    [JsonPropertyName("lora")]
    public Lora Lora { get; set; }
}