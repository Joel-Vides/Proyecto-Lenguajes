using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Migrations
{
    /// <inheritdoc />
    public partial class EndpointParaCoordenadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "end_latitude",
                table: "bus",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "end_longitude",
                table: "bus",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "start_latitude",
                table: "bus",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "start_longitude",
                table: "bus",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_latitude",
                table: "bus");

            migrationBuilder.DropColumn(
                name: "end_longitude",
                table: "bus");

            migrationBuilder.DropColumn(
                name: "start_latitude",
                table: "bus");

            migrationBuilder.DropColumn(
                name: "start_longitude",
                table: "bus");
        }
    }
}
