using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoraMeasurementsBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ChannelRssi",
                table: "Measurements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rssi",
                table: "Measurements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Snr",
                table: "Measurements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "ChannelRssi",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Rssi",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Snr",
                table: "Measurements");
        }
    }
}
