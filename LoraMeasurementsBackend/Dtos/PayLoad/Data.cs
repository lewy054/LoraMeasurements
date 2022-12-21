using System.Text.Json.Serialization;
using LoraMeasurementsBackend.Dtos.PayLoad;

namespace LoraMeasurementsBackend.Dtos;

public class Data
{

    [JsonPropertyName("end_device_ids")]
    public EndDeviceIds EndDeviceIds { get; set; }

    [JsonPropertyName("correlation_ids")]
    public List<string> CorrelationIds { get; set; }

    [JsonPropertyName("received_at")]
    public string ReceivedAt { get; set; }

    [JsonPropertyName("uplink_message")]
    public UplinkMessage UplinkMessage { get; set; }
}