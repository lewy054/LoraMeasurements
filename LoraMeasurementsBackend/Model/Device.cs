namespace LoraMeasurementsBackend.Model;

public class Device
{
    public string Id { get; set; }
    public string ApplicationId { get; set; }
    public Location Location { get; set; }
    public List<Measurement> Measurements { get; set; }
}