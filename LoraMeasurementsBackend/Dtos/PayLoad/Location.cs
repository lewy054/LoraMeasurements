using System.Text.Json.Serialization;

public class Location
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("altitude")]
    public int Altitude { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }
}