using LoraMeasurementsBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace LoraMeasurementsBackend.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(device =>
        {
            device.HasKey(d => d.Id);
            device.Property(d => d.Id).ValueGeneratedNever().IsRequired();
            device.Property(d => d.ApplicationId);
            device.OwnsOne(d => d.Location);
            device.HasMany(d => d.Measurements)
                .WithOne(m => m.Device);
        });
        modelBuilder.Entity<Measurement>(measurement =>
        {
            measurement.HasKey(m => m.Id);
            measurement.Property(m => m.Id).ValueGeneratedNever().IsRequired();
            measurement.Property(m => m.Temperature);
            measurement.Property(m => m.AnalogIn);
            measurement.Property(m => m.BarometricPressure);
            measurement.Property(m => m.RelativeHumidity);
            measurement.Property(m => m.ChannelId);
            measurement.Property(m => m.ChannelRssi);
            measurement.Property(m => m.Rssi);
            measurement.Property(m => m.Snr);
            measurement.HasOne(p => p.Device)
                .WithMany(b => b.Measurements);
        });
    }
}