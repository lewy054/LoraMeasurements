using System.Text.Json.Serialization;

public class Lora
{
    [JsonPropertyName("bandwidth")]
    public int Bandwidth { get; set; }

    [JsonPropertyName("spreading_factor")]
    public int SpreadingFactor { get; set; }

    [JsonPropertyName("coding_rate")]
    public string CodingRate { get; set; }
}