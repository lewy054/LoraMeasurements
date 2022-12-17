using System.Text.Json.Serialization;

public class DecodedPayload
{
    [JsonPropertyName("analog_in_4")]
    public double AnalogIn { get; set; }

    [JsonPropertyName("barometric_pressure_3")]
    public double BarometricPressure { get; set; }

    [JsonPropertyName("relative_humidity_2")]
    public double RelativeHumidity { get; set; }

    [JsonPropertyName("temperature_1")]
    public double Temperature { get; set; }
}