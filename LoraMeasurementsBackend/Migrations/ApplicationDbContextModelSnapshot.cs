// <auto-generated />
using System;
using LoraMeasurementsBackend.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoraMeasurementsBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoraMeasurementsBackend.Model.Device", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("LoraMeasurementsBackend.Model.Measurement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AnalogIn")
                        .HasColumnType("float");

                    b.Property<double>("BarometricPressure")
                        .HasColumnType("float");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<double>("ChannelRssi")
                        .HasColumnType("float");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("MeasurementTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("RelativeHumidity")
                        .HasColumnType("float");

                    b.Property<double>("Rssi")
                        .HasColumnType("float");

                    b.Property<double>("Snr")
                        .HasColumnType("float");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("LoraMeasurementsBackend.Model.Device", b =>
                {
                    b.OwnsOne("LoraMeasurementsBackend.Model.Location", "Location", b1 =>
                        {
                            b1.Property<string>("DeviceId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Latitude")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Longitude")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("DeviceId");

                            b1.ToTable("Devices");

                            b1.WithOwner()
                                .HasForeignKey("DeviceId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("LoraMeasurementsBackend.Model.Measurement", b =>
                {
                    b.HasOne("LoraMeasurementsBackend.Model.Device", "Device")
                        .WithMany("Measurements")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("LoraMeasurementsBackend.Model.Device", b =>
                {
                    b.Navigation("Measurements");
                });
#pragma warning restore 612, 618
        }
    }
}
