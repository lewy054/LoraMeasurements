using System.Text.Json.Serialization;

namespace LoraMeasurementsBackend.Dtos;

public class ApplicationIds
{
    [JsonPropertyName("application_id")]
    public string ApplicationId { get; set; }
}