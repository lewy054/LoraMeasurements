namespace LoraMeasurementsBackend.Model;

public class Device
{
    public string Id { get; set; }
    public string ApplicationId { get; set; }
    public Location Location { get; set; }
    public List<Measurement> Measurements { get; set; }

    public void TryUpdate(string latitude, string longitude, string applicationId,
        double temperature, double barometricPressure, double relativeHumidity, double analogIn)
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
            AnalogIn = analogIn
        });
    }
}