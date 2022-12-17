using System.Text.Json.Serialization;

namespace LoraMeasurementsBackend.Dtos.PayLoad;

public class UplinkMessage
{
    [JsonPropertyName("f_port")]
    public int FPort { get; set; }

    [JsonPropertyName("f_cnt")]
    public int FCnt { get; set; }

    [JsonPropertyName("frm_payload")]
    public string FrmPayload { get; set; }

    [JsonPropertyName("decoded_payload")]
    public DecodedPayload DecodedPayload { get; set; }

    [JsonPropertyName("rx_metadata")]
    public List<RxMetadatum> RxMetadata { get; set; }

    [JsonPropertyName("settings")]
    public Settings Settings { get; set; }

    [JsonPropertyName("received_at")]
    public string ReceivedAt { get; set; }

    [JsonPropertyName("consumed_airtime")]
    public string ConsumedAirtime { get; set; }

    [JsonPropertyName("network_ids")]
    public NetworkIds NetworkIds { get; set; }
}