using Newtonsoft.Json;

namespace LoraMeasurementsBackend.Model;

public class Device
{
    public string Id { get; set; }
    public string ApplicationId { get; set; }
    public Location Location { get; set; }
    [JsonIgnore]
    public List<Measurement> Measurements { get; set; }

    public void TryUpdate(string latitude, string longitude, string applicationId,
        double temperature, double barometricPressure, double relativeHumidity, double analogIn,
        DateTime measurementTime, double rssi, double snr, int channelId, double channelRssi)
    {
        Location.Latitude = latitude;
        Location.Longitude = longitude;
        ApplicationId = applicationId;
        Measurements.Add(new Measurement()
        {
            DeviceId = Id,
            Device = this,
            Temperature = temperature,
            BarometricPressure = barometricPressure,
            RelativeHumidity = relativeHumidity,
            AnalogIn = analogIn,
            MeasurementTime = measurementTime,
            Rssi = rssi,
            Snr = snr,
            ChannelId = channelId,
            ChannelRssi = channelRssi,
        });
    }
}