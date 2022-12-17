using System.Text.Json.Serialization;
using LoraMeasurementsBackend.Dtos;
using LoraMeasurementsBackend.Dtos.PayLoad;

public class RxMetadatum
{
    [JsonPropertyName("gateway_ids")]
    public GatewayIds GatewayIds { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    // [JsonPropertyName("timestamp")]
    // public string Timestamp { get; set; }

    [JsonPropertyName("rssi")]
    public int Rssi { get; set; }

    [JsonPropertyName("channel_rssi")]
    public int ChannelRssi { get; set; }

    [JsonPropertyName("snr")]
    public double Snr { get; set; }

    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("uplink_token")]
    public string UplinkToken { get; set; }

    [JsonPropertyName("channel_index")]
    public int ChannelIndex { get; set; }

    [JsonPropertyName("received_at")]
    public string ReceivedAt { get; set; }
}