using System.Text.Json.Serialization;
using LoraMeasurementsBackend.Dtos;
using LoraMeasurementsBackend.Dtos.PayLoad;

public class Settings
{
    [JsonPropertyName("data_rate")]
    public DataRate DataRate { get; set; }

    [JsonPropertyName("frequency")]
    public string Frequency { get; set; }

    // [JsonPropertyName("timestamp")]
    // public double Timestamp { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }
}