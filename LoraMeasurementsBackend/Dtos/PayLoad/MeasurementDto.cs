using System.Text.Json.Serialization;

namespace LoraMeasurementsBackend.Dtos.PayLoad;

public class MeasurementDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("identifiers")]
    public List<Identifier> Identifiers { get; set; }

    [JsonPropertyName("data")]
    public Data Data { get; set; }

    [JsonPropertyName("correlation_ids")]
    public List<string> CorrelationIds { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("context")]
    public Context Context { get; set; }

    [JsonPropertyName("visibility")]
    public Visibility Visibility { get; set; }

    [JsonPropertyName("unique_id")]
    public string UniqueId { get; set; }
}