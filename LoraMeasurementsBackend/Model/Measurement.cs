namespace LoraMeasurementsBackend.Model;

public class Measurement
{
    public Guid Id { get;} = Guid.NewGuid();
    public DateTime MeasurementTime { get; set; }
    public double BarometricPressure { get; set; }
    public double RelativeHumidity { get; set; }
    public double Temperature { get; set; }
    public double AnalogIn { get; set; }
    public string DeviceId { get; set; }
    public double Rssi { get; set; }
    public double ChannelRssi { get; set; }
    public double Snr { get; set; }
    public int ChannelId { get; set; }
    public Device Device { get; set; }
}