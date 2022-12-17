using System.Text.Json.Serialization;

namespace LoraMeasurementsBackend.Dtos.PayLoad;

public class GatewayIds
{
    [JsonPropertyName("gateway_id")]
    public string GatewayId { get; set; }

    [JsonPropertyName("eui")]
    public string Eui { get; set; }
}