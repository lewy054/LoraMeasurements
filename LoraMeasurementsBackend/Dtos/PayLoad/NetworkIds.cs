using System.Text.Json.Serialization;

public class NetworkIds
{
    [JsonPropertyName("net_id")]
    public string NetId { get; set; }

    [JsonPropertyName("tenant_id")]
    public string TenantId { get; set; }

    [JsonPropertyName("cluster_id")]
    public string ClusterId { get; set; }

    [JsonPropertyName("cluster_address")]
    public string ClusterAddress { get; set; }
}