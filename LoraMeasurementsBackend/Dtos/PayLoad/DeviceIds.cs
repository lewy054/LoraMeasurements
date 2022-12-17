using System.Text.Json.Serialization;
using LoraMeasurementsBackend.Dtos;

public class DeviceIds
{
    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; }

    [JsonPropertyName("application_ids")]
    public ApplicationIds ApplicationIds { get; set; }

    [JsonPropertyName("dev_eui")]
    public string DevEui { get; set; }

    [JsonPropertyName("dev_addr")]
    public string DevAddr { get; set; }
}